namespace EventHorizon.Game.Client.Engine.Systems.ClientAction.Execptions
{
    using System;


    public class InvalidClientActionArgument
        : Exception
    {
        public string Param { get; }

        public InvalidClientActionArgument(
            string param, 
            string message
        ) : base(message)
        {
            Param = param;
        }
    }
}
