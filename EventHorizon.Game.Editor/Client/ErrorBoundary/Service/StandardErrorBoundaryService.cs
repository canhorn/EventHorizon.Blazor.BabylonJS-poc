namespace EventHorizon.Game.Editor.Client.ErrorBoundary.Service;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.ErrorBoundary.Api;

public class StandardErrorBoundaryService : ErrorBoundaryService
{
    public IEnumerable<(Exception Exception, string FormattedException)> ExceptionList
    {
        get;
        private set;
    } = new List<(Exception Exception, string FormattedException)>();

    public event ErrorBoundaryService.OnExceptionHandler OnException = () => Task.CompletedTask;

    public void AddException(Exception ex, string formattedException)
    {
        ExceptionList = ExceptionList.AddItem((ex, formattedException));
        OnException.Invoke();
    }
}
