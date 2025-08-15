namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResponse>;
public record DeleteBasketResponse(bool IsSuccess);
public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName cannot be empty");
    }
}
public class DeleteBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResponse>
{
    public async Task<DeleteBasketResponse> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await basketRepository.DeleteBasket(command.UserName, cancellationToken);
        return new DeleteBasketResponse(true);
    }
}
