namespace EventHorizon.Game.Client.Engine.Services.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Services.Api;

    public class StandardInitializeServices
        : IInitializeServices
    {
        private readonly IEnumerable<IServiceEntity> _serviceEntities;

        public StandardInitializeServices(
            IEnumerable<IServiceEntity> serviceEntities
        )
        {
            _serviceEntities = serviceEntities;
        }

        public async Task InitializeServices()
        {
            var orderedServiceEntities = _serviceEntities.OrderBy(
                a => a.Priority
            );

            foreach (var serviceEntity in orderedServiceEntities)
            {
                await serviceEntity.Initialize();
            }
        }
    }
}
