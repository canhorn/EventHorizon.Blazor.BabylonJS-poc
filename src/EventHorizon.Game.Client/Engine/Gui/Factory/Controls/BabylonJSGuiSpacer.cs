namespace EventHorizon.Game.Client.Engine.Gui.Factory.Controls;

using System;
using System.Collections.Generic;
using BabylonJS.GUI;
using EventHorizon.Blazor.Interop;
using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Model;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;

public class BabylonJSGuiSpacer : IBabylonJSGuiControl
{
    private readonly Rectangle _control;

    public string Id { get; }
    public GuiControlType Type => GuiControlType.SPACER;
    private bool _isVisible = true;
    public bool IsVisible
    {
        get { return _isVisible; }
        set
        {
            // TODO: GUI Animation
            _isVisible = value;
            Control.isVisible = _isVisible;
        }
    }
    public int Layer { get; set; }
    public IGuiControlOptions Options { get; private set; }
    public string? ParentId { get; }
    public IGuiGridLocation? GridLocation { get; }

    public Control Control => _control;

    public BabylonJSGuiSpacer(string id, IGuiControlOptions options, IGuiGridLocation? gridLocation)
    {
        Id = id;
        Options = options;
        GridLocation = gridLocation;

        _control = CreateControl(id, options);
    }

    public void AddControl(IGuiControl guiControl)
    {
        throw new GameException(
            "gui_add_control_not_supported",
            "GuiSpacer does not support adding Controls"
        );
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
        var rectangleControl = new Rectangle($"{id}_spacer");
        rectangleControl.thickness = 0;
        rectangleControl.isHitTestVisible = false;

        options.HasValueCallback<int>(
            "padding",
            value =>
            {
                rectangleControl.height = $"{value}px";
                rectangleControl.width = $"{value}px";
            }
        );

        Update(options, rectangleControl);

        return rectangleControl;
    }

    private static readonly IList<string> IGNORE_PROPERTY_LIST = new List<string>
    {
        "animation",
        "onClick",
    };

    private void Update(IGuiControlOptions options, Rectangle rectangleControl)
    {
        foreach (var option in options)
        {
            if (!IGNORE_PROPERTY_LIST.Contains(option.Key))
            {
                SetPropertyOnControl(rectangleControl, option.Key, option.Value);
            }
        }
    }

    private void SetPropertyOnControl(Control control, string property, object value)
    {
        EventHorizonBlazorInterop.Set(control.___guid, property, value);
    }
}
