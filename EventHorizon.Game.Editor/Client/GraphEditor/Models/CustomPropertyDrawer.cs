namespace EventHorizon.Game.Editor.Client.GraphEditor.Models;

using System;
using EventHorizon.Game.Editor.Client.GraphEditor.Components;

[AttributeUsage(AttributeTargets.Property)]
public class CustomPropertyDrawer : Attribute
{
    public Type ComponentType { get; private set; }

    public CustomPropertyDrawer(Type drawer)
    {
        if (!typeof(BasePropertyDrawer).IsAssignableFrom(drawer))
        {
            throw new ArgumentException(
                $"Custom property drawers must extend from {typeof(BasePropertyDrawer)}"
            );
        }
        this.ComponentType = drawer;
    }
}
