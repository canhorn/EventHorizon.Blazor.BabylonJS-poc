namespace EventHorizon.Game.Server.ServerModule.Game.Model
{
    using System;
    using System.Collections.Generic;

    public interface IGamePlayerCaptureState
    {
        public static string NAME = "modelState";

        int Captures { get; }
        IList<string> CompanionsCaught { get; }
        DateTime EscapeCaptureTime { get; }
        bool ShownTenSecondMessage { get; }
        bool ShownFiveSecondMessage { get; }
    }

}
