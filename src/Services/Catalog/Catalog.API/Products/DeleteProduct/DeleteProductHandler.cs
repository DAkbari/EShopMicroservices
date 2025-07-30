
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResponse>;
    public record DeleteProductResponse(bool IsSuccess);
    internal class DeleteProductHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResponse>
    {
        public async Task<DeleteProductResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            session.Delete(command.Id);
            await session.SaveChangesAsync();
            return new DeleteProductResponse(true);
        }
    }
}
