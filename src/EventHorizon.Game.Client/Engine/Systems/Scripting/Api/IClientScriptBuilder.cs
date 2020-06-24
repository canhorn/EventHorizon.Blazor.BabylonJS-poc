using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Game.Client.Engine.Systems.Scripting.Api
{
    public interface IClientScriptBuilder
    {
        IClientScript CreateScript(
            string scriptId,
            string scriptName
        );
    }
}
