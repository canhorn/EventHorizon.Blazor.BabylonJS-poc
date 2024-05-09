namespace EventHorizon.Game.Editor.Client.GraphEditor.Components;

using System;
using System.Reflection;
using Microsoft.AspNetCore.Components;

public partial class FieldDrawer
{
    [Parameter]
    public required object Owner { get; set; }

    [Parameter]
    public required PropertyInfo Property { get; set; }

    private bool Boolean
    {
        get => (bool)Property.GetValue(Owner)!;
        set => Property.SetValue(Owner, value);
    }
    private string Text
    {
        get => (string)Property.GetValue(Owner)!;
        set => Property.SetValue(Owner, value);
    }
    private int Integer
    {
        get => (int)Property.GetValue(Owner)!;
        set => Property.SetValue(Owner, value);
    }
    private long Long
    {
        get => (long)Property.GetValue(Owner)!;
        set => Property.SetValue(Owner, value);
    }
    private float Float
    {
        get => (float)Property.GetValue(Owner)!;
        set => Property.SetValue(Owner, value);
    }
    private double Double
    {
        get => (double)Property.GetValue(Owner)!;
        set => Property.SetValue(Owner, value);
    }
    private int Enumeration
    {
        get => (int)Property.GetValue(Owner)!;
        set => Property.SetValue(Owner, value);
    }

    private RenderFragment RenderWidget(Type t)
    {
        return builder =>
        {
            builder.OpenComponent(0, t);
            builder.AddAttribute(1, nameof(BasePropertyDrawer.Instance), this.Owner);
            builder.AddAttribute(2, nameof(BasePropertyDrawer.Property), this.Property);
            builder.CloseComponent();
        };
    }
}
