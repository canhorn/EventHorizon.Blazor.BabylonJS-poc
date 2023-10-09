namespace EventHorizon.Game.Editor.Automation.ZoneCommands.Components.ClientEntity;

using System;

using Atata;

public class ClientEntityListComponent<TOwner>
    : ControlList<ClientEntityListItem<TOwner>, TOwner>
    where TOwner : PageObject<TOwner> { }

public class ClientEntityListItem<TOwner> : Button<TOwner>
    where TOwner : PageObject<TOwner> { }
