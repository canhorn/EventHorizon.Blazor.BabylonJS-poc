namespace EventHorizon.Zone.Systems.Wizard.Query;

using System.Collections.Generic;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Zone.Systems.Wizard.Model;

using MediatR;

public struct QueryForAllZoneWizards
    : IRequest<CommandResult<IEnumerable<WizardMetadata>>> { }
