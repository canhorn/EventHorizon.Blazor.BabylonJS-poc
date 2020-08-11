namespace EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ClientActionAttribute
        : Attribute
    {
        public string Name { get; }

        public ClientActionAttribute(
            string name
        )
        {
            Name = name;
        }
    }
}
