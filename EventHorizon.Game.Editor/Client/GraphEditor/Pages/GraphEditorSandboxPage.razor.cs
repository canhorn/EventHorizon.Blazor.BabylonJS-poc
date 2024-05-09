namespace EventHorizon.Game.Editor.Client.GraphEditor.Pages;

using System;
using EventHorizon.Game.Editor.Client.GraphEditor.Models;

public partial class GraphEditorSandboxPage
{
    protected ProcessGraph Graph = new ProcessGraph();
    protected INodeGenerator<ProcessStep>[] NodeTypes = [];
    protected OutputNode? ResultNode;

    protected override void OnInitialized()
    {
        Graph = new ProcessGraph();
        NodeTypes =
        [
            new SimpleNodeGenerator(
                "Input Number",
                () => new InputNode() { Id = Guid.NewGuid().ToString() }
            ),
            new SimpleNodeGenerator(
                "Addition",
                () => new AddNode() { Id = Guid.NewGuid().ToString() }
            ),
        ];

        var input1 = new InputNode
        {
            Id = "input1",
            Name = "Input 1",
            Description = "Input Number 1",
            Position = new Point { X = 0, Y = 0 }
        };
        var input2 = new InputNode
        {
            Id = "input2",
            Name = "Input 2",
            Description = "Input Number 2",
            Position = new Point { X = 0, Y = 256 }
        };
        var op = new AddNode
        {
            Id = "op",
            Name = "Addition",
            Description = "Addition Operation",
            Position = new Point { X = 320, Y = 180, }
        };
        var output = new OutputNode
        {
            Id = "output",
            Name = "Output",
            Description = "Output Result",
            Position = new Point { X = 600, Y = 180 }
        };
        ResultNode = output;

        Graph.AddNode(input1);
        Graph.AddNode(input2);
        Graph.AddNode(op);
        Graph.AddNode(output);

        Graph.Connect(
            input1,
            op,
            new NodePortReference { FromPortName = "value", ToPortName = "first" }
        );
        Graph.Connect(
            input2,
            op,
            new NodePortReference { FromPortName = "value", ToPortName = "second" }
        );
        Graph.Connect(
            op,
            output,
            new NodePortReference { FromPortName = "value", ToPortName = "value" }
        );
    }

    private void GetResult()
    {
        if (ResultNode == null)
        {
            return;
        }

        Graph.Recalculate(ResultNode);
        StateHasChanged();
    }
}
