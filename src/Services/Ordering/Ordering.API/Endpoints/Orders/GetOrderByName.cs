namespace Ordering.API.Endpoints.Orders;

public record GetOrderByNameResponse(IEnumerable<OrderDto> Orders);
public class GetOrderByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("orders/{orderName}", async (string orderName, ISender sender) =>
        {
            // Logic to retrieve orders by orderName
            // Call appropriate command/query handler using MediatR
            // Convert result to response model if necessary
            var query = new GetOrderByNameQuery(orderName);
            var result = await sender.Send(query);
            var response = new GetOrderByNameResponse(result.Orders);
            return Results.Ok(response);
        }).WithDescription("GetOrderByName")
          .WithName("GetOrderByName")
          .WithSummary("Retrieve orders by order name")
          .Produces<GetOrderByNameResponse>(StatusCodes.Status200OK)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .ProducesValidationProblem(StatusCodes.Status404NotFound);
    }
}
