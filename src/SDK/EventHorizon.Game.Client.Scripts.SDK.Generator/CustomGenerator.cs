﻿namespace EventHorizon.Game.Client.Scripts.SDK.Generator
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Metadata;
    using System.Text;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Text;

    [Generator]
    public class CustomGenerator
        : ISourceGenerator
    {
        public void Initialize(
            InitializationContext context
        )
        { }
        //public static Assembly[] GetSolutionAssemblies()
        //{
        //    var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
        //                        .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));
        //    return assemblies.ToArray();
        //}
        public static string[] GetSolutionAssemblies()
        {
            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                                .Select(x => AssemblyName.GetAssemblyName(x).Name);
            return assemblies.ToArray();
        }

        public void Execute(
            SourceGeneratorContext context
        )
        {
            var assemblyNames = context.Compilation.ReferencedAssemblyNames.Select(a => a.Name).ToList();
            foreach (var solutionAssembly in GetSolutionAssemblies())
            {
                assemblyNames.Add(
                    solutionAssembly
                );
            }



            var diagnostics = context.Compilation.GetDiagnostics();
            foreach (var diagnostic in diagnostics)
            {
                var AssemblyIdentities = context.Compilation.GetUnreferencedAssemblyIdentities(
                    diagnostic
                );
                foreach (var assemblyIdentity in AssemblyIdentities)
                {
                    assemblyNames.Add(assemblyIdentity.Name);
                }
            }
            var assemblyNameString = string.Join(
                "\", \"",
                assemblyNames
            );
            assemblyNameString = $"\"{assemblyNameString}\"";
            context.AddSource(
                "GameClientSDKRoot.Generated.cs",
                SourceText.From($@"
namespace GeneratedNamespace
{{
    public class GameClientSDKRootGenerated
    {{
        public static string[] Assemblies = new string[] {{ {assemblyNameString} }};
        public static void GeneratedMethod()
        {{
            // generated code
        }}
    }}
}}", Encoding.UTF8));
        }
    }
}
