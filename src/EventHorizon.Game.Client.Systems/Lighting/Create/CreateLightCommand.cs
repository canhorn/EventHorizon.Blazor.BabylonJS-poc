namespace EventHorizon.Game.Client.Systems.Lighting.Create;

using EventHorizon.Game.Client.Systems.Lighting.Model;
using MediatR;

public class CreateLightCommand : IRequest
{
    public LightDetailsModel LightDetailsModel { get; }

    public CreateLightCommand(LightDetailsModel lightDetailsModel)
    {
        LightDetailsModel = lightDetailsModel;
    }
}
