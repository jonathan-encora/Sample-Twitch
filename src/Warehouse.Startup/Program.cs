namespace Warehouse.Startup
{
    using MassTransit;
    using Microsoft.Extensions.DependencyInjection;
    using Sample.Components.StateMachines;
    using Warehouse.Components.Consumers;

    public class Program
    {
        public static void Main()
        {
            var services = new ServiceCollection();

            services.AddMassTransit(x =>
            {
                x.AddConsumersFromNamespaceContaining<AllocateInventoryConsumer>();
                x.AddSagaStateMachine<OrderStateMachine, OrderState>()
                    .MongoDbRepository(r =>
                    {
                        r.Connection = "mongodb://mongo";
                        r.DatabaseName = "allocations";
                    });
            });
        }
    }
}