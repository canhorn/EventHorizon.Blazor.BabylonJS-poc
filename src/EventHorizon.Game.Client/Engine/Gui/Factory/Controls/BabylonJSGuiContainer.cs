namespace EventHorizon.Game.Client.Engine.Gui.Factory.Controls;

using System;
using System.Collections.Generic;
using BabylonJS.GUI;
using EventHorizon.Blazor.Interop;
using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Model;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;

public class BabylonJSGuiContainer : IBabylonJSGuiControl
{
    private readonly Rectangle _control;

    public string Id { get; }
    public GuiControlType Type => GuiControlType.CONTAINER;
    private bool _isVisiable = true;
    public bool IsVisible
    {
        get { return _isVisiable; }
        set
        {
            _isVisiable = value;
            Control.isVisible = _isVisiable;
        }
    }
    public int Layer { get; set; }
    public IGuiControlOptions Options { get; private set; }
    public string? ParentId { get; }
    public IGuiGridLocation? GridLocation { get; }

    public Control Control => _control;

    public BabylonJSGuiContainer(
        string id,
        IGuiControlOptions options,
        IGuiGridLocation? gridLocation
    )
    {
        Id = id;
        Options = options;
        GridLocation = gridLocation;

        _control = CreateControl(id, options);
    }

    public void AddControl(IGuiControl guiControl)
    {
        if (guiControl is IBabylonJSGuiControl bjsGuiControl)
        {
            _control.addControl(bjsGuiControl.Control);
        }
    }

    public void Dispose()
    {
        Control.dispose();
    }

    public void LinkWith(object obj)
    {
        if (obj is BabylonJSEngineMesh mesh)
        {
            Control.linkWithMesh(mesh.Mesh);
        }
    }

    public void Update(IGuiControlOptions options)
    {
        Update(options, _control);

        Options = GuiControlOptionsModel.MergeControlOptions(Options, options);
    }

    private Rectangle CreateControl(string id, IGuiControlOptions options)
    {
        var containerControl = new Rectangle($"{id}_container");
        containerControl.thickness = 0;
        containerControl.isHitTestVisible = false;

        Update(options, containerControl);

        return containerControl;
    }

    private static IList<string> IGNORE_PROPERTY_LIST = new List<string> { "animation", };

    private void Update(IGuiControlOptions options, Rectangle buttonControl)
    {
        foreach (var option in options)
        {
            if (!IGNORE_PROPERTY_LIST.Contains(option.Key))
            {
                SetPropertyOnControl(buttonControl, option.Key, option.Value);
            }
        }
    }

    private void SetPropertyOnControl(Control control, string property, object value)
    {
        EventHorizonBlazorInterop.Set(control.___guid, property, value);
    }
}
