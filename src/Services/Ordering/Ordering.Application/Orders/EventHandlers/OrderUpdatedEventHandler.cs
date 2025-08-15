using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
        // Here you can add additional logic to handle the order update event, such as sending notifications or updating other systems.
        return Task.CompletedTask;
    }
}