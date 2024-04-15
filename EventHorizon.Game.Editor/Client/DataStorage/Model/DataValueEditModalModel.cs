namespace EventHorizon.Game.Editor.Client.DataStorage.Components.Modal;

using System.Collections.Generic;
using System.Linq;
using EventHorizon.Game.Editor.Client.DataStorage.Model;
using EventHorizon.Game.Editor.Client.Shared.Components.Select;
using EventHorizon.Game.Editor.Client.Shared.Properties;

public struct DataValueEditModalModel
{
    public bool IsNewValue { get; private set; }

    public string InitialDataName { get; private set; }
    public string DataName { get; private set; }
    public object DataValue { get; private set; }
    public string DataType { get; private set; }
    public StandardSelectOption DataTypeOption { get; private set; }
    public PropertyDisplayType PropertyDisplayType { get; private set; }

    public List<StandardSelectOption> PropertyTypeOptions { get; private set; }

    public string MessageCode { get; private set; }

    public DataValueEditModalModel(string name, object value, string type)
    {
        IsNewValue = true;

        InitialDataName = name;
        DataName = name;
        DataValue = value;
        DataType = type;

        DataTypeOption = new();
        PropertyDisplayType = new();

        PropertyTypeOptions = new();

        MessageCode = string.Empty;

        Validate();
    }

    private void Validate()
    {
        MessageCode = string.Empty;

        if (string.IsNullOrWhiteSpace(DataName))
        {
            MessageCode = DataValueErrorCodes.InvalidDataName;
        }
    }

    internal DataValueEditModalModel UpdateName(string name)
    {
        DataName = name;

        UpdateDisplayType();

        Validate();

        return this;
    }

    internal DataValueEditModalModel Reset(bool isNew, string name, object value, string type)
    {
        IsNewValue = isNew;
        DataName = name;
        InitialDataName = name;
        DataValue = value;
        DataType = type;
        DataTypeOption = PropertyTypeOptions.First(a => a.Value == type);

        UpdateDisplayType();

        Validate();

        return this;
    }

    internal DataValueEditModalModel UpdateValue(string name, object value)
    {
        DataName = name;
        DataValue = value;

        UpdateDisplayType();

        Validate();

        return this;
    }

    internal DataValueEditModalModel ChangeValueType(string type, object value)
    {
        DataType = type;
        DataValue = value;
        DataTypeOption = PropertyTypeOptions.First(a => a.Value == type);

        UpdateDisplayType();

        Validate();

        return this;
    }

    internal DataValueEditModalModel UpdateOptions(
        List<StandardSelectOption> propertyTypeOptions,
        StandardSelectOption dataTypeOption
    )
    {
        PropertyTypeOptions = propertyTypeOptions;
        DataTypeOption = dataTypeOption;

        Validate();

        return this;
    }

    private void UpdateDisplayType()
    {
        PropertyDisplayType = new()
        {
            Label = DataName,
            Name = DataName,
            Type = DataType,
            Value = DataValue
        };
    }

    internal DataValueEditModalModel UpdateErrorCode(string errorCode)
    {
        MessageCode = errorCode;

        return this;
    }
}
