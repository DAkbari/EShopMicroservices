﻿
namespace Catalog.API.Products.GetProducts
{
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
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
