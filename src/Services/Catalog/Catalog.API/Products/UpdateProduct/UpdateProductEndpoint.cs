public record UpdateProductRequest(Guid id,
        string Name,
        string Description,
        decimal Price,
        List<string> Category,
        string ImageFile);
public record UpdateProductResponse(bool IsSuccess);

namespace Catalog.API.Products.UpdateProduct
{
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("products", async (UpdateProductRequest request, ISender sender, CancellationToken token) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            });
        }
    }
}
