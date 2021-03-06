﻿namespace EventHorizon.Game.Editor.Client
{
    using System.Collections.Generic;

    public interface ClientDetailsEnrichmentService
    {
        void EnrichReference(
            IDictionary<string, object> data
        );

        void EnrichWith(
            string key,
            string value
        );

        void EnrichWith(
            string key,
            bool value
        );
    }
}
