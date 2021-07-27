namespace EventHorizon.Game.Editor.Client.AssetManagement.Load
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public struct LoadAssetServerFilterPathCommand
        : IRequest<StandardCommandResult>
    {
        public string FilterPath { get; }

        public LoadAssetServerFilterPathCommand(
            string filterPath
        )
        {
            FilterPath = filterPath;
        }
    }
}
