using System;
using CleanArchitectureInventory.Application.Contracts;
using MassTransit;
using MassTransit.Mediator;

namespace CleanArchitectureInventory.Receiving.API.Worker
{
    public class ProductConsumer : IConsumer<ProductCreated>
    {
        private readonly ILogger<ProductConsumer> _logger;

        public ProductConsumer(ILogger<ProductConsumer> logger)
        {
            _logger = logger;
        }

        public  Task  Consume(ConsumeContext<ProductCreated> context)
        {
            _logger.LogInformation("Name:{Name}", context.Message.Name);
           return Task.CompletedTask;
        }
    }
}

