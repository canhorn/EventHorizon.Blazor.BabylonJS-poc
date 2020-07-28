namespace EventHorizon.Game.Client.Systems.Local.Scenes.Start
{
    using MediatR;

    public struct StartSceneCommand
        : IRequest<bool>
    {
        public string SceneId { get; }

        public StartSceneCommand(
            string sceneId
        )
        {
            SceneId = sceneId;
        }
    }
}
