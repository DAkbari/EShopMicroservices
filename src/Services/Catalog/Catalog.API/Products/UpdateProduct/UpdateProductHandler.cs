namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid id,
        string Name,
        string Description,
        decimal Price,
        List<string> Category,
        string ImageFile) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>().Where(x => x.Id == command.id).FirstOrDefaultAsync(cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.Category = command.Category;
            product.ImageFile = command.ImageFile;
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
