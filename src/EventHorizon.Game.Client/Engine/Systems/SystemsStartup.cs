using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace EventHorizon.Game.Client.Engine.Systems
{
    public static class SystemsStartup
    {
        public static IServiceCollection AddEngineSystemServices(
            this IServiceCollection services
        ) => services
        ;
    }
}
