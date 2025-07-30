public record GetProductsByCategoryResponse(IEnumerable<Product> Products);
namespace Catalog.API.Products.GetProductsByCategory
{
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender, CancellationToken cancellationToken) =>
            {
                var request = await sender.Send(new GetProductByCategoryQuery(category));
                var response = request.Adapt<GetProductsByCategoryResponse>();
                return Results.Ok(response);
            })
                .WithDescription("GetProductByCategory")
                .WithName("GetProductByCategory")
                .WithSummary("GetProductByCategory")
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
