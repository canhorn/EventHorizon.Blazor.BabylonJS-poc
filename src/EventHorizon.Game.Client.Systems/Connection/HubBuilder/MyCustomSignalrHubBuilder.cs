namespace EventHorizon.Game.Client.Systems.Connection.HubBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Connections;
    using Microsoft.AspNetCore.Http.Connections.Client;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    internal static class MyCustomSignalrHubBuilder
    {
        internal static HubConnection BuildHubConnection(
            Uri url,
            Func<Task<string>> accessTokenProvider,
            Action<ILoggingBuilder> configureLogging
        )
        {
            var builder = new HubConnectionBuilder();
            var httpConnectionOptions = MyCustomSignalrHubBuilderConnectionFactoryInternal.CreateHttpConnectionOptions(); // work around constructor call
            httpConnectionOptions.Url = url;
            httpConnectionOptions.AccessTokenProvider = accessTokenProvider;
            builder.Services.AddSingleton<EndPoint>(
                new UriEndPoint(
                    httpConnectionOptions.Url
                )
            );
            var opt = Options.Create(
                httpConnectionOptions
            );
            builder.Services.AddSingleton(
                opt
            );
            builder.Services.AddSingleton<IConnectionFactory, MyCustomSignalrHubBuilderConnectionFactoryInternal>();

            return builder.ConfigureLogging(
                configureLogging
            ).Build();
        }

        /// <summary>
        /// TODO: Remove this in the future when fixed (.NET 5 RC/Full Release)
        /// </summary>
        internal class MyCustomSignalrHubBuilderConnectionFactoryInternal : IConnectionFactory
        {
            private readonly HttpConnectionOptions _httpConnectionOptions;
            private readonly ILoggerFactory _loggerFactory;

            /// <summary>
            /// Initializes a new instance of the <see cref="HttpConnectionFactory"/> class.
            /// </summary>
            /// <param name="options">The connection options.</param>
            /// <param name="loggerFactory">The logger factory.</param>
            public MyCustomSignalrHubBuilderConnectionFactoryInternal(ILoggerFactory loggerFactory, IOptions<HttpConnectionOptions> options)
            {
                _httpConnectionOptions = options.Value;
                _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            }

            internal static HttpConnectionOptions CreateHttpConnectionOptions()
            {
                var o = System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(HttpConnectionOptions))
                    as HttpConnectionOptions;
                o.NullCheck();

                o.Headers = new Dictionary<string, string>();
                o.Cookies = new System.Net.CookieContainer();
                o.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransports.All;
                o.DefaultTransferFormat = TransferFormat.Binary;
                o.CloseTimeout = TimeSpan.FromSeconds(5);
                return o;
            }

            /// <summary>
            /// Creates a new connection to an <see cref="UriEndPoint"/>.
            /// </summary>
            /// <param name="endPoint">The <see cref="UriEndPoint"/> to connect to.</param>
            /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None" />.</param>
            /// <returns>
            /// A <see cref="ValueTask{TResult}" /> that represents the asynchronous connect, yielding the <see cref="ConnectionContext" /> for the new connection when completed.
            /// </returns>
            public async ValueTask<ConnectionContext> ConnectAsync(EndPoint endPoint, CancellationToken cancellationToken = default)
            {
                if (endPoint == null)
                {
                    throw new ArgumentNullException(nameof(endPoint));
                }

                if (!(endPoint is UriEndPoint uriEndPoint))
                {
                    throw new NotSupportedException($"The provided {nameof(EndPoint)} must be of type {nameof(UriEndPoint)}.");
                }

                if (_httpConnectionOptions.Url != null && _httpConnectionOptions.Url != uriEndPoint.Uri)
                {
                    throw new InvalidOperationException($"If {nameof(HttpConnectionOptions)}.{nameof(HttpConnectionOptions.Url)} was set, it must match the {nameof(UriEndPoint)}.{nameof(UriEndPoint.Uri)} passed to {nameof(ConnectAsync)}.");
                }

                // Shallow copy before setting the Url property so we don't mutate the user-defined options object.
                var shallowCopiedOptions = ShallowCopyHttpConnectionOptions(_httpConnectionOptions);
                shallowCopiedOptions.Url = uriEndPoint.Uri;

                var connection = new HttpConnection(shallowCopiedOptions, _loggerFactory);

                try
                {
                    await connection.StartAsync(cancellationToken);
                    return connection;
                }
                catch
                {
                    // Make sure the connection is disposed, in case it allocated any resources before failing.
                    await connection.DisposeAsync();
                    throw;
                }
            }

            // Internal for testing
            internal static HttpConnectionOptions ShallowCopyHttpConnectionOptions(
                HttpConnectionOptions options
            )
            {
                return options;
            }
        }
    }
}