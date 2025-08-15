using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);
public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotEmpty().WithMessage("the cart cannot be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName cannot be empty");

    }
}
public class StoreBasketCommandHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;
        await ApplyDiscount(cart, cancellationToken);
        await basketRepository.StoreBasket(command.Cart, cancellationToken);
        //update cache
        return new StoreBasketResult(cart.UserName);
    }

    private async Task ApplyDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var discount = await discountClient.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= discount.Amount;
        }
    }
}
