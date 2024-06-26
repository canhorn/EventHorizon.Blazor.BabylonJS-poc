﻿namespace EventHorizon.Game.Editor.Client.Wizard.Previous;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public record GoToPreviousWizardStepCommand(string Context) : IRequest<StandardCommandResult>;
