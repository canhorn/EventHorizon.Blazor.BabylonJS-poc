namespace EventHorizon.Game.Editor.Client.ErrorBoundary.Api;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ErrorBoundaryService
{
    IEnumerable<(
        Exception Exception,
        string FormattedException
    )> ExceptionList { get; }

    void AddException(Exception ex, string formattedException);

    delegate Task OnExceptionHandler();
    event OnExceptionHandler OnException;
}
