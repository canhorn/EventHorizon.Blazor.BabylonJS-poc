using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Game.Client.Engine.Systems.ServerModule.Model
{
    public class ServerModuleScripts
    {
        public string Name { get; set; }
        public string InitializeScript { get; set; }
        public string DisposeScript { get; set; }
        public string UpdateScript { get; set; }
    }
}
