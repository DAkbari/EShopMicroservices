namespace Catalog.API.Products.GetProducts
{
    public record GetProductRequest(int? PageNumber = 1, int? PageSize = 5);
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender, [AsParameters] GetProductRequest request) =>
            {
                var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            })
            .WithDescription("GetProducts")
            .WithName("GetProducts")
            .WithSummary("GetProducts")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
