namespace EventHorizon.Game.Editor.Client.AssetManagement.Clicked;

using EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent.Model;
using EventHorizon.Observer.Model;
using MediatR;

public struct AssetFileNodeClickedEvent : INotification
{
    public TreeViewNodeData Node { get; }

    public AssetFileNodeClickedEvent(TreeViewNodeData node)
    {
        Node = node;
    }
}

public interface AssetFileNodeClickedEventObserver : ArgumentObserver<AssetFileNodeClickedEvent> { }
