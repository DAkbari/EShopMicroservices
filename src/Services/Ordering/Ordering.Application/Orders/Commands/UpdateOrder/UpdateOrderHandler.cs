namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Order.Id);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }

        UpdateOrderWithNewValues(order, command.Order);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }

    private void UpdateOrderWithNewValues(Order order, OrderDto newOrder)
    {
        var shippingAddress = Address.Of(newOrder.ShippingAddress.FirstName, newOrder.ShippingAddress.LastName,
            newOrder.ShippingAddress.EmailAddress, newOrder.ShippingAddress.AddressLine,
            newOrder.ShippingAddress.Country, newOrder.ShippingAddress.State, newOrder.ShippingAddress.ZipCode);
        var billingAddress = Address.Of(newOrder.BillingAddress.FirstName, newOrder.BillingAddress.LastName, newOrder.BillingAddress.EmailAddress,
            newOrder.BillingAddress.AddressLine, newOrder.BillingAddress.Country,
            newOrder.BillingAddress.State, newOrder.BillingAddress.ZipCode);
        order.Update(
            CustomerId.Of(newOrder.CustomerId),
            OrderName.Of(newOrder.OrderName),
            shippingAddress,
            billingAddress,
            Payment.Of(newOrder.Payment.CardName, newOrder.Payment.CardNumber, newOrder.Payment.Expiration, newOrder.Payment.Cvv, newOrder.Payment.PaymentMethod),
            OrderStatus.Pending);
    }
}