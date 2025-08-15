using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints.Orders
{
    public record CreateOrderRequest(OrderDto Order);
    public record CreateOrderResponse(Guid OrderId);

    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                //Map CreateOrderRequest to CreateOrderCommand
                //Call CreateOrderCommandHandler using MediatR
                //Convert CreateOrderResult to CreateOrderResponse
                //Return CreateOrderResponse
                var command = request.Adapt<CreateOrderCommand>();

                var result = await sender.Send(command);

                var response = new CreateOrderResponse(result.Id);

                return Results.Created($"/orders/{result.Id}", response);
            })
                .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("CreateOrder")
                .WithName("CreateOrder")
                .WithDescription("CreateOrder");
        }
    }
}
