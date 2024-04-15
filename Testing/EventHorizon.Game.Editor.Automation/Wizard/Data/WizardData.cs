namespace EventHorizon.Game.Editor.Automation.Wizard.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel;

public static class WizardData
{
    public static readonly WizardMetadata MapEditor =
        new()
        {
            Id = "1F9431BD-48E5-45D6-9802-7D3EDEE7A03A",
            Name = "[SYSTEM] Map Editor",
            Description = "Edit the details of the Map",
            Steps = new List<WizardMetadata.WizardStep>
            {
                new WizardMetadata.WizardStep
                {
                    Id = "836B8953-3E3B-469C-AA77-F3471B7691B0",
                    Type = "ServerProcess",
                    Name = "Fill Map Data",
                    Description = "This fills the current Wizard Data with the Map values",
                    //"NextStep": "55EBC04C-CBD9-4C48-932F-5D45E3777D7A"
                }
            },
        };

    public class WizardMetadata
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public List<WizardStep> Steps { get; init; } = new List<WizardStep>();

        public class WizardStep
        {
            public string Id { get; init; }
            public string Type { get; init; }
            public string Name { get; init; }
            public string Description { get; init; }
        }
    }
}
