namespace EventHorizon.Game.Server.Game.Model
{
    using System;
    using System.Collections.Generic;

    public interface GamePlayerCaptureState
    {
        public static string NAME = "gamePlayerCaptureState";

        int Captures { get; }
        IList<string> CompanionsCaught { get; }
        DateTime EscapeCaptureTime { get; }
        bool ShownTenSecondMessage { get; }
        bool ShownFiveSecondMessage { get; }
    }

}
