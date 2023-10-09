namespace EventHorizon.Game.Client.Core.Command.Api;

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using MediatR;

public interface ICommandService
{
    Task<R> Send<T, R>(T command)
        where T : ICommand<R>;
}
