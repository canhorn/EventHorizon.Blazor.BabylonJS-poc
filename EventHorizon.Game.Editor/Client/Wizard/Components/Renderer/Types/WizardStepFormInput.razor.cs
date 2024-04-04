namespace EventHorizon.Game.Editor.Client.Wizard.Components.Renderer.Types;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Engine.Input.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
using EventHorizon.Game.Client.Systems.Player.Modules.Input.Model;
using EventHorizon.Game.Editor.Client.Shared.Properties;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
using EventHorizon.Game.Editor.Properties.Api;
using EventHorizon.Game.Editor.Properties.Model;
using EventHorizon.Zone.Systems.Wizard.Model;
using Microsoft.Extensions.Logging;
using static EventHorizon.Game.Editor.Client.Shared.Properties.InputKeyMapControlModel;

public class WizardStepFormInputBase : WizardStepCommonBase
{
    public Dictionary<string, string> PropertyLabelMap { get; private set; } =
        new();
    public Dictionary<string, object> PropertiesData { get; private set; } =
        new();
    public PropertiesMetadataModel PropertiesMetadata { get; private set; } =
        new();

    protected override void OnInitializing()
    {
        var properties = Step.Details.Where(
            a =>
                a.Key.StartsWith("property:")
                && a.Key.EndsWith(":label").IsNotTrue()
        );
        foreach (var property in properties)
        {
            var propertyKey = property.Key.Replace("property:", string.Empty);
            PropertiesMetadata[propertyKey] = property.Value;
            PropertiesData[propertyKey] = ParseDataValue(
                propertyKey,
                property.Value
            );
            PropertyLabelMap[propertyKey] = GetLabel(propertyKey);
            Data[property.Key] = property.Value;
        }
    }

    public string GetLabel(string key)
    {
        var label = Step.Details[$"property:{key}:label"];
        if (string.IsNullOrWhiteSpace(label).IsNotTrue())
        {
            return Localizer[label];
        }

        return label;
    }

    public object ParseDataValue(string key, string keyType)
    {
        switch (keyType)
        {
            case PropertyType.Boolean:
                return bool.TryParse(Data[key], out var boolValue)
                    ? boolValue
                    : GetDefaultProperty(keyType);
            case PropertyType.Decimal:
                return decimal.TryParse(Data[key], out var decimalValue)
                    ? decimalValue
                    : GetDefaultProperty(keyType);
            case PropertyType.Long:
                return long.TryParse(Data[key], out var longValue)
                    ? longValue
                    : GetDefaultProperty(keyType);
            case PropertyType.InputKeyMap:
                return GetInputKeyMap(key, Data);
            case PropertyType.String:
            case PropertyType.Asset:
            case PropertyType.AssetServerPath:
            case PropertyType.AssetServerFile:
            case PropertyType.AssetServerFileName:
                return Data[key];
            default:
                Mediator.Publish(
                    new ShowMessageEvent(
                        Localizer["Wizard Input Form Issue"],
                        Localizer[
                            "'{0}' is an invalid Property Type used for {1}.",
                            keyType,
                            key
                        ],
                        MessageLevel.Warning
                    )
                );
                return string.Empty;
        }
    }

    public static string GetInputKeyMap(string parentKey, WizardData data)
    {
        var inputKeyMap = new Dictionary<string, ControlKeyInput>();

        var keyInputList = data.Where(a => a.Key.StartsWith($"{parentKey}:"))
            .Select(
                a =>
                    a.Key.Replace($"{parentKey}:", string.Empty)
                        .Split(":", StringSplitOptions.RemoveEmptyEntries)
                        .First()
            )
            .Distinct()
            .ToList();

        foreach (var key in keyInputList)
        {
            var keyInput = new ControlKeyInput
            {
                Key = data[$"{parentKey}:{key}:key"],
                Type = data[$"{parentKey}:{key}:type"],
                Camera = data.TryGetValue(
                    $"{parentKey}:{key}:camera",
                    out var camera
                )
                    ? camera
                    : null,
                Pressed = data.TryGetValue(
                    $"{parentKey}:{key}:pressed",
                    out var pressed
                )
                    ? Enum.TryParse<MoveDirection>(pressed, out var pressedEnum)
                        ? pressedEnum
                        : null
                    : null,
                Released = data.TryGetValue(
                    $"{parentKey}:{key}:released",
                    out var released
                )
                    ? Enum.TryParse<MoveDirection>(
                        released,
                        out var releasedEnum
                    )
                        ? releasedEnum
                        : null
                    : null,
            };
            inputKeyMap[key] = keyInput;
        }

        return JsonSerializer.Serialize(
            inputKeyMap,
            JsonExtensions.DEFAULT_OPTIONS
        );
    }

    public static object GetDefaultProperty(string propertyType)
    {
        return propertyType switch
        {
            PropertyType.Boolean => false,
            PropertyType.Decimal => 0.0m,
            PropertyType.Long => 0,
            PropertyType.Vector3 => ServerVector3.Zero(),
            PropertyType.InputKeyMap => "{}",
            _ => string.Empty,
        };
    }

    public Task HandleDataChanged(PropertiesDisplayChangedArgs args)
    {
        PropertiesData = new Dictionary<string, object>(args.Data);

        var propertyKey = args.PropertyName;
        var propertyType = PropertiesMetadata[propertyKey];

        if (propertyType == PropertyType.InputKeyMap)
        {
            var inputKeyMap =
                JsonSerializer.Deserialize<Dictionary<string, ControlKeyInput>>(
                    PropertiesData[propertyKey]?.ToString() ?? "{}",
                    JsonExtensions.DEFAULT_OPTIONS
                ) ?? [];
            FlattenInputKeyMapIntoData(Data, propertyKey, inputKeyMap);
        }
        else
        {
            Data[propertyKey] =
                PropertiesData[propertyKey]?.ToString() ?? string.Empty;
        }

        return Task.CompletedTask;
    }

    public static WizardData FlattenInputKeyMapIntoData(
        WizardData data,
        string propertyKey,
        Dictionary<string, ControlKeyInput> inputKeyMap
    )
    {
        foreach (var (key, control) in inputKeyMap)
        {
            data[$"{propertyKey}:{key}:key"] = control.Key;
            data[$"{propertyKey}:{key}:type"] = control.Type;
            if (control.Camera.IsNotNullOrEmpty())
            {
                data[$"{propertyKey}:{key}:camera"] = control.Camera;
            }
            if (control.Pressed.HasValue)
            {
                data[$"{propertyKey}:{key}:pressed"] =
                    ((int?)control.Pressed).ToString() ?? "0";
            }
            if (control.Released.HasValue)
            {
                data[$"{propertyKey}:{key}:released"] =
                    ((int?)control.Released).ToString() ?? "0";
            }
        }

        return data;
    }

    public class PropertiesMetadataModel
        : Dictionary<string, string>,
            PropertiesMetadata
    {
        public string GetPropertyType(string key, object _)
        {
            return TryGetValue(key, out var value)
                ? value
                : PropertyType.String;
        }

        public object GetDefaultValueForPropertyType(string propertyType)
        {
            return string.Empty;
        }
    }
}
