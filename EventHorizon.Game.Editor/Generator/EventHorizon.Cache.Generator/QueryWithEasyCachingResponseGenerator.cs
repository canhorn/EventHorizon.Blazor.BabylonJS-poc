namespace EventHorizon.Cache.Generator;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

/// <summary>
/// TODO: Not currently working, needs to be updated to the IIncrementalGenerator
/// </summary>
[Generator]
public class QueryWithEasyCachingResponseGenerator : ISourceGenerator
{
    private const string GenerateCacheAttributeName =
        "EventHorizon.Cache.GenerateCacheAttribute";

    public void Execute(GeneratorExecutionContext context)
    {
        var attributeSymbol = context.Compilation.GetTypeByMetadataName(
            GenerateCacheAttributeName
        );
        //#if DEBUG
        //            if (!Debugger.IsAttached)
        //            {
        //                Debugger.Launch();
        //            }
        //#endif

        var classWithAttributes = context.Compilation.SyntaxTrees.Where(
            st =>
                st.GetRoot()
                    .DescendantNodes()
                    .OfType<ClassDeclarationSyntax>()
                    .Any(
                        p => p.DescendantNodes().OfType<AttributeSyntax>().Any()
                    )
        );
        Generate<ClassDeclarationSyntax>(
            context,
            attributeSymbol,
            classWithAttributes
        );

        var structWithAttributes = context.Compilation.SyntaxTrees.Where(
            st =>
                st.GetRoot()
                    .DescendantNodes()
                    .OfType<StructDeclarationSyntax>()
                    .Any(
                        p => p.DescendantNodes().OfType<AttributeSyntax>().Any()
                    )
        );
        Generate<StructDeclarationSyntax>(
            context,
            attributeSymbol,
            structWithAttributes
        );
    }

    private static void Generate<T>(
        GeneratorExecutionContext context,
        INamedTypeSymbol? attributeSymbol,
        IEnumerable<SyntaxTree> elementsWithAttribute
    )
        where T : TypeDeclarationSyntax
    {
        foreach (SyntaxTree tree in elementsWithAttribute)
        {
            var semanticModel = context.Compilation.GetSemanticModel(tree);

            foreach (
                var declaredClass in tree.GetRoot()
                    .DescendantNodes()
                    .OfType<T>()
                    .Where(
                        cd =>
                            cd.DescendantNodes().OfType<AttributeSyntax>().Any()
                    )
            )
            {
                // Check for existing GenerateCacheAttributeName Attribute on <T>
                var nodes = declaredClass
                    .DescendantNodes()
                    .OfType<AttributeSyntax>()
                    .FirstOrDefault(
                        a =>
                            a.DescendantTokens()
                                .Any(
                                    dt =>
                                        dt.IsKind(SyntaxKind.IdentifierToken)
                                        && dt.Parent != null
                                        && attributeSymbol != null
                                        && semanticModel
                                            .GetTypeInfo(dt.Parent)
                                            .Type?.Name == attributeSymbol.Name
                                )
                    )
                    ?.DescendantTokens()
                    ?.Where(dt => dt.IsKind(SyntaxKind.IdentifierToken))
                    ?.ToList();

                if (nodes == null)
                {
                    continue;
                }

                if (
                    declaredClass.Parent
                    is not NamespaceDeclarationSyntax classNamespace
                )
                {
                    // Ignored, dont care about non-namespaced
                    continue;
                }

                var responseNamespace = classNamespace.Name.ToString();
                var responseUsingNamespace = classNamespace.Usings
                    .ToString()
                    .Replace("using EventHorizon.Cache;", string.Empty)
                    .Replace("using MediatR;", string.Empty);
                var requestType = declaredClass.Identifier.ToString();
                var responseType = declaredClass.BaseList?.Types
                    .Select(a => a.ToString())
                    .FirstOrDefault(a => a.StartsWith("IRequest<"));
                if (responseType == null || string.IsNullOrEmpty(responseType))
                {
                    // Ignored, dont care about non IRequest< typed queries
                    continue;
                }
                responseType = responseType.Replace("IRequest<", string.Empty);
                responseType = responseType.Substring(
#pragma warning disable IDE0057 // Use range operator
                    0,
                    responseType.Length - 1
#pragma warning restore IDE0057 // Use range operator
                );

                var generatedClass =
                    @$"
// namespace EventHorizon.Cache.Generated
namespace {responseNamespace}
{{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EasyCaching.Core;
    using MediatR;
    using Microsoft.Extensions.Logging;

#region Generated Usings
    // using {responseNamespace};

	{responseUsingNamespace}
#endregion

    public class {requestType}GeneratedCachedBehavior
        : IPipelineBehavior<{requestType}, {responseType}>
    {{
        private readonly ILogger _logger;
        private readonly IEasyCachingProvider _provider;

        public {requestType}GeneratedCachedBehavior(
            ILogger<{requestType}GeneratedCachedBehavior> logger,
            IEasyCachingProvider provider
        )
        {{
            _logger = logger;
            _provider = provider;
        }}

        public async Task<{responseType}> Handle(
            {requestType} request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<{responseType}> next
        )
        {{
            var key = request.CacheKey;
            var response = await _provider.GetAsync(
                key,
                async () =>
                {{
                    return await next();
                }}, 
                TimeSpan.FromSeconds(30)
            );

            // Remove Error Results
            if (response.HasValue
                && !response.Value.Success
            )
            {{
                await _provider.RemoveAsync(
                    key
                );
            }}

            _logger.LogDebug(
                ""Key='{{CacheKey}}', Value='{{CacheResponse}}', Time='{{CacheCheckTime}}'"",
                key,
                response,
                DateTime.Now.ToString(""yyyy-MM-dd HH:mm:ss"")
            );

            return response.Value;
        }}
    }}
}}";

                context.AddSource(
                    $"{requestType}GeneratedCachedBehavior.cs",
                    SourceText.From(generatedClass.ToString(), Encoding.UTF8)
                );
            }
        }
    }

    public void Initialize(GeneratorInitializationContext context) { }
}
