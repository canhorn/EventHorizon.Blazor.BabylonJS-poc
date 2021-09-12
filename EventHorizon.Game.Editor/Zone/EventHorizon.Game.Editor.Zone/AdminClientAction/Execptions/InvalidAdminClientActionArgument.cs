namespace EventHorizon.Game.Editor.Zone.AdminClientAction.Execptions
{
    using System;

    public class InvalidAdminClientActionArgument
        : Exception
    {
        public string Param { get; }

        public InvalidAdminClientActionArgument(
            string param, 
            string message
        ) : base(message)
        {
            Param = param;
        }
    }
}
