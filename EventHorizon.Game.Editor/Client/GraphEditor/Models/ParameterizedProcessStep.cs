namespace EventHorizon.Game.Editor.Client.GraphEditor.Models;

public abstract class ParameterizedProcessStep<ParamType>(ParamType parameters)
    : ParameterizedProcessStep
    where ParamType : class
{
    public ParamType Parameters { get; set; } = parameters;

    public override object GetParameters() => Parameters;
}

public abstract class ParameterizedProcessStep : ProcessStep
{
    public abstract object GetParameters();
}
