namespace Ordering.API.Endpoints.Orders
{
    public record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);
    public class GetOrderByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                // Logic to retrieve orders by customerId
                // Call appropriate command/query handler using MediatR
                // Convert result to response model if necessary
                var query = new GetOrderByCustomerQuery(customerId);

                var result = await sender.Send(query);

                var response = result.Adapt<GetOrderByCustomerResponse>();

                return Results.Ok(response); // Placeholder for actual response
            })
            .WithName("GetOrderByCustomer")
            .WithDescription("Get orders by customer ID")
            .WithSummary("Retrieve all orders associated with a specific customer")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem(StatusCodes.Status404NotFound);
        }
    }
}
