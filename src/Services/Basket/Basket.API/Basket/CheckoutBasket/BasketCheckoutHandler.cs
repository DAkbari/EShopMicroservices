
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record BasketCheckoutCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<BasketCheckoutResult>;
public record BasketCheckoutResult(bool IsSuccess);

public class BasketCheckoutCommandHandler(IBasketRepository Repository, IPublishEndpoint PublishEndpoint)
    : ICommandHandler<BasketCheckoutCommand, BasketCheckoutResult>
{
    public async Task<BasketCheckoutResult> Handle(
        BasketCheckoutCommand command,
        CancellationToken cancellationToken)
    {
        var basket = await Repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        if (basket == null)
        {
            return new BasketCheckoutResult(false);
        }

        var checkoutEvent = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        checkoutEvent.TotalPrice = basket.TotalPrice;

        await PublishEndpoint.Publish(checkoutEvent);

        await Repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

        return new BasketCheckoutResult(true);
    }
}
