namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("products/{productId}", async (Guid productId, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(productId), cancellationToken);
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            })
            .WithDescription("GetProductById")
            .WithName("GetProductById")
            .WithSummary("GetProductById")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
