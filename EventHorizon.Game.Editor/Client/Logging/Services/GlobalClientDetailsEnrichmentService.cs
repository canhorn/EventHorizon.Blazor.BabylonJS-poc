namespace EventHorizon.Game.Editor.Client
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class GlobalClientDetailsEnrichmentService
        : ClientDetailsEnrichmentService
    {
        private readonly IDictionary<string, object> _values = new Dictionary<string, object>();

        public void EnrichReference(
            IDictionary<string, object> data
        )
        {
            foreach (var value in _values)
            {
                data[value.Key] = value.Value;
            }

            EnrichWithActivity(
                data
            );
        }

        public void EnrichWith(
            string key,
            string value
        )
        {
            _values[key] = value;
        }

        public void EnrichWith(
            string key,
            bool value
        )
        {
            _values[key] = value;
        }

        private static void EnrichWithActivity(
            IDictionary<string, object> data
        )
        {
            void SetIfNotEmpty(
                string key,
                string value
            )
            {
                if (!string.IsNullOrWhiteSpace(
                    value
                ))
                {
                    data[key] = value;
                }
            }
            var activity = Activity.Current;

            SetIfNotEmpty("SpanId", activity.GetSpanId());
            SetIfNotEmpty("TraceId", activity.GetTraceId());
            SetIfNotEmpty("ParentId", activity.GetParentId());
        }
    }

    internal static class ActivityExtensions
    {
        public static string GetSpanId(
            this Activity? activity
        )
        {
            return activity?.IdFormat switch
            {
                ActivityIdFormat.Hierarchical => activity.Id,
                ActivityIdFormat.W3C => activity.SpanId.ToHexString(),
                _ => null,
            } ?? string.Empty;
        }

        public static string GetTraceId(
            this Activity? activity
        )
        {
            return activity?.IdFormat switch
            {
                ActivityIdFormat.Hierarchical => activity.RootId,
                ActivityIdFormat.W3C => activity.TraceId.ToHexString(),
                _ => null,
            } ?? string.Empty;
        }

        public static string GetParentId(
            this Activity? activity
        )
        {
            return activity?.IdFormat switch
            {
                ActivityIdFormat.Hierarchical => activity.ParentId,
                ActivityIdFormat.W3C => activity.ParentSpanId.ToHexString(),
                _ => null,
            } ?? string.Empty;
        }
    }
}
