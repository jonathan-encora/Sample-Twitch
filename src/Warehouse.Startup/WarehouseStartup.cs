namespace Warehouse.Startup
{
    using Components.Consumers;
    using Components.StateMachines;
    using MassTransit;
    using Microsoft.Extensions.DependencyInjection;


    public class WarehouseStartup :
        IPlatformStartup
    {
        public void ConfigureMassTransit(IBusRegistrationConfigurator configurator, IServiceCollection services)
        {
            configurator.AddConsumersFromNamespaceContaining<AllocateInventoryConsumer>();
            configurator.AddSagaStateMachine<AllocationStateMachine, AllocationState>(typeof(AllocateStateMachineDefinition))
                .MongoDbRepository(r =>
                {
                    r.Connection = "mongodb://mongo";
                    r.DatabaseName = "allocations";
                });
        }

        public void ConfigureBus<TEndpointConfigurator>(IBusFactoryConfigurator<TEndpointConfigurator> configurator, IBusRegistrationContext context)
            where TEndpointConfigurator : IReceiveEndpointConfigurator
        {
        }
    }
}