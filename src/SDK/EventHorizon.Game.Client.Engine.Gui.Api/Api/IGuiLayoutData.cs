namespace EventHorizon.Game.Client.Engine.Gui.Api
{
    using System;
    using System.Collections.Generic;

    public interface IGuiLayoutData
    {
        string Id { get; }
        int Sort { get; }
        IEnumerable<IGuiLayoutControlData> ControlList { get; }
        string? InitializeScript { get; }
        string? ActivateScript { get; }
        string? DisposeScript { get; }
        string? UpdateScript { get; }
        string? DrawScript { get; }
    }
}
