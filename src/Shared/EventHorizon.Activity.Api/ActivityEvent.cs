namespace EventHorizon.Activity
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using EventHorizon.Observer.Model;

    using MediatR;

    public struct ActivityEvent
        : INotification
    {
        public string Category { get; }
        public string Action { get; }
        public string Tag { get; }
        public IReadOnlyDictionary<string, object> Data { get; }

        public ActivityEvent(
            string category,
            string action,
            string tag,
            IReadOnlyDictionary<string, object> data
        )
        {
            Category = category;
            Action = action;
            Tag = tag;
            Data = data;
        }

        public ActivityEvent(
            string category,
            string action,
            string tag
        )
        {
            Category = category;
            Action = action;
            Tag = tag;
            Data = new ReadOnlyDictionary<string, object>(
                new Dictionary<string, object>()
            );
        }
    }

    public interface ActivityEventObserver
        : ArgumentObserver<ActivityEvent>
    {
    }
}
