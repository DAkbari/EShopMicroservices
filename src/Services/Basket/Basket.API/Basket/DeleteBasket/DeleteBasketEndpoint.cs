
namespace Basket.API.Basket.DeleteBasket;

//public record DeleteBasketRequest(string UserName);
public record DelteBasketResponse(bool IsSuccess);
public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var command = new DeleteBasketCommand(userName);

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .WithSummary("DeleteBasket")
        .WithDescription("DeleteBasket")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
