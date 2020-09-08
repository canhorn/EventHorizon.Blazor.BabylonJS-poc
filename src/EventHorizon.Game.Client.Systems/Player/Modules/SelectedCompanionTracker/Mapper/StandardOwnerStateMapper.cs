namespace EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Mapper
{
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Api;
    using EventHorizon.Game.Client.Systems.Player.Modules.SelectedCompanionTracker.Model;

    public class StandardOwnerStateMapper
        : IMapper<OwnerState>
    {
        public OwnerState Map(
            object obj
        ) => obj.Cast<StandardOwnerState>();
    }
}
