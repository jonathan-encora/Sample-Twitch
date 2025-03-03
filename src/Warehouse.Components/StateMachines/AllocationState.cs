namespace Warehouse.Components.StateMachines
{
    using System;
    using MassTransit;
    using MongoDB.Bson.Serialization.Attributes;


    public class AllocationState :
        SagaStateMachineInstance,
        ISagaVersion
    {
        public string CurrentState { get; set; }

        public Guid? HoldDurationToken { get; set; }

        public int Version { get; set; }

        [BsonId]
        public Guid CorrelationId { get; set; }
    }
}