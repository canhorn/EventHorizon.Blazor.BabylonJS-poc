using EventHorizon.Observer.Admin.Model;
using EventHorizon.Observer.Admin.State;
using EventHorizon.Observer.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventHorizon.Observer.State
{
    public class GenericObserverState : ObserverState, AdminObserverState
    {
        public IEnumerable<ObserverBase> List { get; private set; } = new List<ObserverBase>().AsReadOnly();

        private readonly ILogger _logger;

        private IEnumerable<AdminObserverBase> _adminList = new List<AdminObserverBase>().AsReadOnly();

        public GenericObserverState(
            ILogger<GenericObserverState> logger
        )
        {
            _logger = logger;
        }

        public void Register(
            ObserverBase observer
        )
        {
            if (observer == null || List.Contains(observer))
            {
                return;
            }

            List = List.AddItem(observer);
        }

        public void Remove(
            ObserverBase observer
        )
        {
            if (observer == null || !List.Contains(observer))
            {
                return;
            }
            List = List.RemoveItem(observer);
        }

        [SuppressMessage(
            "Design",
            "CA1031:Do not catch general exception types",
            Justification = "Ignore any and all Exception to not break all triggered observers."
        )]
        public async Task Trigger<TInstance, TArgs>(
            TArgs args,
            CancellationToken cancellationToken = default
        ) where TInstance : ArgumentObserver<TArgs>
        {
            var typeOf = typeof(TInstance);
            var list = List.Where(a => typeOf.IsAssignableFrom(a.GetType())).ToList();
            var shouldRunOnce = ShouldRunOnce(typeOf);
            var exceptionListToRemove = new List<ObserverBase>();
            foreach (var observer in list)
            {
                try
                {
                    await ((TInstance)observer).Handle(
                        args
                    );
                    if (shouldRunOnce)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(
                        ex,
                        "Exception thrown while triggering observer."
                    );
                    exceptionListToRemove.Add(
                        observer
                    );
                }
            }
            foreach (var toRemoveObserver in exceptionListToRemove)
            {
                List = List.RemoveItem(
                    toRemoveObserver
                );
            }

            var adminExceptionListToRemove = new List<AdminObserverBase>();
            foreach (var observer in _adminList)
            {
                try
                {
                    await observer.HandleAdminTrigger(
                        typeOf.Name,
                        args
                    );
                    if (shouldRunOnce)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(
                        ex,
                        "Exception thrown while triggering observer."
                    );
                    adminExceptionListToRemove.Add(
                        observer
                    );
                }
            }
            foreach (var toRemoveObserver in adminExceptionListToRemove)
            {
                _adminList = _adminList.RemoveItem(
                    toRemoveObserver
                );
            }
        }

        private bool ShouldRunOnce(
            Type observerType
        )
        {
            return observerType
                .GetInterfaces()
                .Any(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(SingleArgumentBasedObserver<>)));
        }

        public void RegisterAdminObserver(
            AdminObserverBase observer
        )
        {
            if (observer == null || _adminList.Contains(observer))
            {
                return;
            }

            _adminList = _adminList.AddItem(observer);
        }

        public void RemoveAdminObserver(
            AdminObserverBase observer
        )
        {
            if (observer == null || !_adminList.Contains(observer))
            {
                return;
            }

            _adminList = _adminList.RemoveItem(observer);
        }
    }
}
