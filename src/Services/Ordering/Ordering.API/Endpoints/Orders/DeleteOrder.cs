using Mapster;
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints.Orders
{
    public record DeleteOrderResponse(bool IsSuccess);
    //public record DeleteOrderRequest(int OrderId);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("orders/{orderId}", async (Guid orderId, ISender sender) =>
            {
                //Creat DeleteOrderCommand by orderId
                //Call DeleteOrderCommandHandler using MediatR
                //Convert DeleteOrderResult to DeleteOrderResponse
                var result = await sender.Send(new DeleteOrderCommand(orderId));

                var response = result.Adapt<DeleteOrderResponse>();

                return Results.Ok(response);
            })
                .WithName("DeleteOrder")
                .WithDescription("DeleteOrder")
                .WithSummary("Delete an order by its ID")
                .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesValidationProblem(StatusCodes.Status404NotFound);
        }
    }
}
