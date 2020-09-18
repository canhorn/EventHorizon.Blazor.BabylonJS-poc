namespace EventHorizon.Game.Client.Engine.Gui.Setup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using MediatR;

    public class SetupGuiLayoutCommandHandler
        : IRequestHandler<SetupGuiLayoutCommand, StandardCommandResult>
    {
        private readonly IRenderingGui _renderingGui;
        private readonly IGuiControlState _controlState;

        public SetupGuiLayoutCommandHandler(
            IRenderingGui renderingGui,
            IGuiControlState controlState
        )
        {
            _renderingGui = renderingGui;
            _controlState = controlState;
        }

        public Task<StandardCommandResult> Handle(
            SetupGuiLayoutCommand request,
            CancellationToken cancellationToken
        )
        {
            var parent = _controlState.Get(
                request.ParentControlId ?? string.Empty
            );

            foreach (var layoutControl in request.Layout.ControlList.OrderBy(a => a.Sort))
            {
                AddControlToLayout(
                    request.GuiId,
                    layoutControl,
                    parent
                );
            }

            return new StandardCommandResult()
                .FromResult();
        }

        private void AddControlToLayout(
            string guiId,
            IGuiLayoutControlData layoutControl,
            Option<IGuiControl> parent
        )
        {
            var control = _controlState.Get(
                guiId,
                layoutControl.Id
            );
            if (!control.HasValue)
            {
                return;
            }
            if (layoutControl.Layer.HasValue)
            {
                control.Value.Layer = layoutControl.Layer.Value;
            }

            if (parent.HasValue)
            {
                parent.Value.AddControl(
                    control.Value
                );
            }
            else
            {
                _renderingGui.GetGuiCanvas().AddControl(
                    control.Value
                );
            }
            if (layoutControl.LinkWith != null)
            {
                control.Value.LinkWith(
                    layoutControl.LinkWith
                );
            }

            foreach (var layoutChildControl in (layoutControl.ControlList ?? new List<IGuiLayoutControlData>()).OrderBy(a => a.Sort))
            {
                AddControlToLayout(
                    guiId,
                    layoutChildControl,
                    control
                );
            }
        }
    }
}
