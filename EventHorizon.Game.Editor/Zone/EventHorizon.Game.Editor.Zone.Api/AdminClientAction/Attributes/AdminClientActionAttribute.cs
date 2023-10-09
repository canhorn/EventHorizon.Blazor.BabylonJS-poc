namespace EventHorizon.Game.Editor.Zone.AdminClientAction.Attributes;

using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class AdminClientActionAttribute : Attribute
{
    public string Name { get; }

    public AdminClientActionAttribute(string name)
    {
        Name = name;
    }
}
