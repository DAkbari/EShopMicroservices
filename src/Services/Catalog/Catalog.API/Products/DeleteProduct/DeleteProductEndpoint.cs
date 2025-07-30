public record DeleteProductResponse(bool IsSuccess);

namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("products/{productId}", async (Guid productId, CancellationToken cancellation, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(productId), cancellation);
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            })
            .WithDescription("DeleteProduct")
            .WithName("DeleteProduct")
            .WithSummary("DeleteProduct")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest); ;
        }
    }
}
