namespace EventHorizon.Cache
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class GenerateCacheAttribute
        : Attribute
    { 
    }
}
