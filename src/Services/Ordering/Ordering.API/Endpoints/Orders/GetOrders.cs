using Mapster;

namespace Ordering.API.Endpoints.Orders
{
    public record GetOrdersResponse(PaginationResult<OrderDto> Orders);
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                // Logic to retrieve all orders
                // Call appropriate command/query handler using MediatR
                // Convert result to response model if necessary
                var query = new GetOrdersQuery(request);

                var result = await sender.Send(query);

                var response = result.Adapt<GetOrdersResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrders")
            .WithDescription("Get all orders")
            .WithSummary("Retrieve all orders in the system")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem(StatusCodes.Status404NotFound);
        }
    }
}
