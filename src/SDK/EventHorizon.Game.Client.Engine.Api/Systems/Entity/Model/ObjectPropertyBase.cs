namespace EventHorizon.Game.Client.Engine.Systems.Entity.Model
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

    public abstract class ObjectPropertyBase
        : Dictionary<string, object>,
#pragma warning disable CS8644 // Type does not implement interface member. Nullability of reference types in interface implemented by the base type doesn't match.
        // disable CS8644 - This needs to be done because of the 2.0 -> 5.0 usage
        ObjectProperty
#pragma warning restore CS8644 // Type does not implement interface member. Nullability of reference types in interface implemented by the base type doesn't match.
    {
        public ObjectPropertyBase(
            IDictionary<string, object> dictionary
        ) : base(dictionary)
        {
        }
    }
}
