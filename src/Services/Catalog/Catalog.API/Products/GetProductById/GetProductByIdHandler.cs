namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await session.Query<Product>().Where(x => x.Id == query.id).FirstOrDefaultAsync(cancellationToken);
            if (result is null)
            {
                throw new ProductNotFoundException(query.id);
            }
            return new GetProductByIdResult(result);
        }
    }
}
