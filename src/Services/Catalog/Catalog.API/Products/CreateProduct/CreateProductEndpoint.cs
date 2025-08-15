namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Category, string Description, decimal Price, string ImageFile);
public record CreateProductResponse(Guid Id);
public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await mediator.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", new CreateProductResponse(response.Id));
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("CreateProduct")
        .WithSummary("CreateProduct");
    }
}
