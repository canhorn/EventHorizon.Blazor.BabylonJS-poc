namespace EventHorizon.Game.Server.ServerModule.Game.Model
{
    using System;
    using System.Collections.Generic;

    public class GamePlayerCaptureState
        : IGamePlayerCaptureState
    {
        public int Captures { get; set; }
        public IList<string> CompanionsCaught { get; set; } = new List<string>();
        public DateTime EscapeCaptureTime { get; set; }
        public bool ShownTenSecondMessage { get; set; }
        public bool ShownFiveSecondMessage { get; set; }
    }
}
