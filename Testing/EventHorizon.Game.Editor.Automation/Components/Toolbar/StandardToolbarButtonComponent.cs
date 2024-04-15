namespace EventHorizon.Game.Editor.Automation.Components.Toolbar;

using System;
using Atata;

public class StandardToolbarButtonComponent<TOwner> : Button<TOwner>
    where TOwner : PageObject<TOwner> { }
