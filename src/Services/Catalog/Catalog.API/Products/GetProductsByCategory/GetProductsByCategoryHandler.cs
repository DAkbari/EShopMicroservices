
namespace Catalog.API.Products.GetProductsByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
public class GetProductsByCategoryCommandHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await session.Query<Product>().Where(x => x.Category.Contains(request.Category)).ToListAsync();
        return new GetProductByCategoryResult(result);
    }
}
