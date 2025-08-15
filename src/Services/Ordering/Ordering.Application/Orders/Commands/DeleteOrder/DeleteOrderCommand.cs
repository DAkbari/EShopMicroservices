namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteCommandResult(bool IsSuccess);
public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteCommandResult>;
public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required");
    }
}