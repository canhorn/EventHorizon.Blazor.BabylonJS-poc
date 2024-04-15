namespace EventHorizon.ApplicationDetails.Component.State;

using System;
using EventHorizon.ApplicationDetails.Component.Api;

public class StandardApplicationDetailsState : ApplicationDetailsState
{
    public string ApplicationVersion { get; init; } = "0.0.0-dev";
}
