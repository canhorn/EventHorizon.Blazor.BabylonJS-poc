namespace EventHorizon.Platform.LogProvider.Connection.Service;

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Connection.Shared;
using EventHorizon.Connection.Shared.Unauthorized;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Platform.LogProvider.Connection.Api;
using EventHorizon.Platform.LogProvider.Connection.Model;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class SignalrClientLoggerConnection : PlatformLoggerConnection
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    private readonly string _loggingServerUrl;

    private bool _initializing = false;
    private bool _initialized = false;
    private string _connectionAccessToken = string.Empty;
    private HubConnection? _connection;

    public bool IsConnected => _initialized;

    public SignalrClientLoggerConnection(
        ILogger<SignalrClientLoggerConnection> logger,
        IMediator mediator,
        IConfiguration configuration
    )
    {
        _logger = logger;
        _mediator = mediator;
        _loggingServerUrl =
            configuration["Platform:Logging:Server"] ?? string.Empty;
    }

    public async Task<StandardCommandResult> Connect(
        string accessToken,
        CancellationToken cancellationToken
    )
    {
        if (
            !string.IsNullOrWhiteSpace(_connectionAccessToken)
            && accessToken != _connectionAccessToken
        )
        {
            await Dispose();
        }

        if (_initializing)
        {
            return new(ConnectionErrorTypes.NotInitialized);
        }
        else if (_initialized)
        {
            return new();
        }
        try
        {
            _initializing = true;
            _connection = new HubConnectionBuilder()
                .WithUrl(
                    _loggingServerUrl,
                    options =>
                    {
                        // options..LogLevel = SignalRLogLevel.Error;
                        options.AccessTokenProvider = () =>
                            Task.FromResult<string?>(accessToken);
                    }
                )
                .Build();

            await _connection.StartAsync(cancellationToken);

            _initializing = false;
            _initialized = true;
            _connectionAccessToken = accessToken;
            return new();
        }
        catch (HttpRequestException ex)
        {
            await LogAndDispose(
                ex,
                "Failed to start connection to Logging Service."
            );
            if (ex.Message.Contains("401 (Unauthorized)"))
            {
                await _mediator.Publish(
                    new ConnectionUnauthorizedEvent("client-logger-connection"),
                    cancellationToken
                );
                return new(ConnectionErrorTypes.Unauthorized);
            }
            return new(ConnectionErrorTypes.Unknown);
        }
        catch (InvalidOperationException ex)
        {
            await LogAndDispose(
                ex,
                "Operation Exception starting connection to Logging Service."
            );
            return new(ConnectionErrorTypes.Operational);
        }
        catch (Exception ex)
        {
            await LogAndDispose(
                ex,
                "Generic Exception starting connection to Logging Service."
            );
            return new(ConnectionErrorTypes.Unknown);
        }
    }

    private async Task LogAndDispose(Exception ex, string message)
    {
        _logger.LogWarning(ex, message);
        if (_connection != null)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }
        _initializing = false;
        _initialized = false;
    }

    public async Task Dispose()
    {
        if (_connection != null)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }
        _initializing = false;
        _initialized = false;
    }

    public async Task<StandardCommandResult> LogMessage(
        ClientLogMessage message,
        CancellationToken cancellationToken
    )
    {
        if (!_initialized || _initializing || _connection.IsNotConnected())
        {
            return new(ConnectionErrorTypes.NotInitialized);
        }
        var success = await _connection.InvokeAsync<bool>(
            "LogMessage",
            message,
            cancellationToken: cancellationToken
        );
        if (!success)
        {
            return new("FailedLogMessageInvoke");
        }

        return new();
    }

    public class ApiCommandResult
    {
        public bool Success { get; }
        public string? ErrorCode { get; }
    }
}
