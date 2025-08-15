namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteCommandResult>
{
    public async Task<DeleteCommandResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.OrderId);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);
        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }
        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteCommandResult(true);
    }
}