namespace EventHorizon.Game.Client.Engine.Gui.Model;

using System;
using System.Collections.Generic;
using System.Linq;

using EventHorizon.Game.Client.Engine.Gui.Api;

public class GuiControlOptionsModel
    : Dictionary<string, object>,
        IGuiControlOptions
{
    public class GuiControlMetadataOptionModel
    {
        public static readonly string OPTION_NAME = "__metadata";

        public List<string> ModelOptions { get; set; } = new List<string>();
    }

    public static GuiControlOptionsModel MergeControlOptions(
        params IGuiControlOptions[] options
    )
    {
        var value = options
            .SelectMany(x => x)
            .GroupBy(d => d.Key)
            .Where(a => a.Key != GuiControlMetadataOptionModel.OPTION_NAME)
            .ToDictionary(
                x => x.Key,
                y =>
                {
                    var value = y.Last().Value;
                    // Check to see if the value is of type IGuiControlOptions
                    if (value is IGuiControlOptions)
                    {
                        // For IGuiControlOptions, merge these into a single option
                        value = MergeControlOptions(
                            y.Select(guiKeyValue => guiKeyValue.Value)
                                .Select(
                                    a =>
                                        new GuiControlOptionsModel(
                                            a as IGuiControlOptions
                                        )
                                )
                                .ToArray()
                        );
                    }
                    return value;
                }
            );
        return new GuiControlOptionsModel(value);
    }

    public GuiControlOptionsModel() { }

    public GuiControlOptionsModel(IDictionary<string, object>? dictionary)
        : base(dictionary ?? new Dictionary<string, object>()) { }

    public Option<T> GetValue<T>(string key)
    {
        if (TryGetValue(key, out var value))
        {
            return new Option<T>(value.To<T>());
        }
        return new Option<T>();
    }

    public T GetValue<T>(string key, Func<T> defaultValue)
    {
        if (TryGetValue(key, out var value))
        {
            return value.To<T>() ?? defaultValue();
        }
        return defaultValue();
    }

    public void HasValueCallback<T>(string key, Action<T> callback)
    {
        if (TryGetValue(key, out var value))
        {
            callback(value.To<T>()!);
        }
    }
}
