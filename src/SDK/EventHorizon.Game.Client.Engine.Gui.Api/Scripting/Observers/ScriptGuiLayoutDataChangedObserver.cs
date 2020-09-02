namespace EventHorizon.Game.Client.Engine.Gui.Scripting.Observers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Gui.Activate;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Gui.Changed;
    using EventHorizon.Game.Client.Engine.Gui.Create;
    using EventHorizon.Game.Client.Engine.Gui.Dispose;
    using EventHorizon.Game.Client.Engine.Scripting.Data;
    using EventHorizon.Game.Client.Engine.Scripting.Services;

    /// <summary>
    /// This Observer can be used with Scripts to make it easer to handle reloading a GUI from a Layout.
    /// </summary>
    public class ScriptGuiLayoutDataChangedObserver
        : GuiLayoutDataChangedEventObserver
    {
        /// <summary>
        /// Generate Key for usage with the ScriptData unique to this type of Observer.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DataKey(
            string layoutId,
            string guiId,
            string key
        ) => $"{layoutId}-{guiId}-{key}";

        private readonly ScriptServices _services;
        private readonly ScriptData _data;
        private readonly string _guiId;
        private readonly string _layoutId;
        private readonly Func<List<IGuiControlData>> _getGuiControlData;

        public ScriptGuiLayoutDataChangedObserver(
            ScriptServices services,
            ScriptData data,
            string layoutId,
            string guiId,
            Func<List<IGuiControlData>> getGuiControlData
        )
        {
            _services = services;
             _data = data;
            _layoutId = layoutId;
            _guiId = guiId;
            _getGuiControlData = getGuiControlData;

            _data.Set(
                DataKey(_layoutId, _guiId, "active"),
                false
            );
        }

        public Task Handle(
            GuiLayoutDataChangedEvent args
        )
        {
            if (args.Id == _layoutId)
            {
                return OnChange();
            }
            return Task.CompletedTask;
        }

        public async Task OnChange()
        {
            if (_data.Get<bool>(DataKey(_layoutId, _guiId, "active")))
            {
                await _services.Mediator.Send(
                    new DisposeOfGuiCommand(
                        _guiId
                    )
                );
                _data.Set(
                    DataKey(_layoutId, _guiId, "active"),
                    false
                );
            }
            var result = await _services.Mediator.Send(
                new CreateGuiCommand(
                    _guiId,
                    _layoutId,
                    _getGuiControlData()
                )
            );

            if (result.Success)
            {
                await _services.Mediator.Send(
                    new ActivateGuiCommand(
                        _guiId
                    )
                );
                _data.Set(
                    DataKey(_layoutId, _guiId, "active"),
                    true
                );
            }
        }
    }
}
