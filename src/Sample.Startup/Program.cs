namespace Sample.Startup
{
    using MassTransit;
    using Microsoft.Extensions.DependencyInjection;
    using Sample.Components.BatchConsumers;
    using Sample.Components.Consumers;
    using Sample.Components.CourierActivities;
    using Sample.Components.StateMachines;
    using Sample.Components.StateMachines.OrderStateMachineActivities;
    using Warehouse.Contracts;

    public class Program
    {
        public static void Main()
        {
            var services = new ServiceCollection();

            services.AddScoped<AcceptOrderActivity>();

            services.AddMassTransit(x =>
            {
                x.AddConsumersFromNamespaceContaining<SubmitOrderConsumer>();
                x.AddActivitiesFromNamespaceContaining<AllocateInventoryActivity>();
                x.AddConsumersFromNamespaceContaining<RoutingSlipBatchEventConsumer>();
                x.AddSagaStateMachine<OrderStateMachine, OrderState>()
                    .MongoDbRepository(r =>
                    {
                        r.Connection = "mongodb://mongo";
                        r.DatabaseName = "allocations";
                    });
                x.AddRequestClient<AllocateInventory>();
            });
        }
    }
}