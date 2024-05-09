namespace EventHorizon.Game.Editor.Client.GraphEditor.Pages;

using System;
using EventHorizon.Game.Editor.Client.GraphEditor.Components;
using EventHorizon.Game.Editor.Client.GraphEditor.Models;

public class SimpleNodeGenerator(string name, Func<ProcessStep> constructor)
    : INodeGenerator<ProcessStep>
{
    private readonly string _name = name;
    private readonly Func<ProcessStep> _constructor = constructor;

    public string GeneratorName() => _name;

    public ProcessStep Generate() => _constructor();
}

public class InputNode : ParameterizedProcessStep<InputNode.ParameterSet>
{
    public class ParameterSet
    {
        public float Value { get; set; }
    }

    public InputNode()
        : base(new ParameterSet())
    {
        Name = "Input";

        Inputs = null;
        Outputs = new NodePortCollection(
            [new NodePort<float> { Id = Guid.NewGuid().ToString(), Name = "value" }]
        );
    }

    public override void Recalculate()
    {
        Outputs?["value"]?.Store(Parameters.Value);
        Console.WriteLine("Pushing " + Parameters.Value);
    }
}

public class OutputNode : ParameterizedProcessStep<OutputNode.ParameterSet>
{
    public class ParameterSet
    {
        [CustomPropertyDrawer(typeof(OutputDrawer))]
        public float Result { get; set; }
    }

    public OutputNode()
        : base(new ParameterSet())
    {
        Name = "Output";
        Inputs = new NodePortCollection(
            [new NodePort { Id = Guid.NewGuid().ToString(), Name = "value" }]
        );
    }

    public override void Recalculate()
    {
        var result = Inputs?["value"]?.Fetch<float>();
        if (!result.HasValue)
        {
            return;
        }

        Parameters.Result = result.Value;
        Console.WriteLine("Computed Result " + Parameters.Result);
    }
}

public class AddNode : ProcessStep
{
    public AddNode()
    {
        Name = "Addition";
        Inputs = new NodePortCollection(
            [
                new NodePort<float> { Id = Guid.NewGuid().ToString(), Name = "first" },
                new NodePort<float> { Id = Guid.NewGuid().ToString(), Name = "second" }
            ]
        );
        Outputs = new NodePortCollection(
            [new NodePort<float> { Id = Guid.NewGuid().ToString(), Name = "value" }]
        );
    }

    public override void Recalculate()
    {
        var firstInput = Inputs?["first"]?.Fetch<float>();
        var secondInput = Inputs?["second"]?.Fetch<float>();
        if (!firstInput.HasValue || !secondInput.HasValue)
        {
            return;
        }

        Outputs?["value"]?.Store(firstInput.Value + secondInput.Value);
        Console.WriteLine("Adding " + firstInput + " + " + secondInput);
    }
}
