using Catalog.API.Products.CreateProduct;


public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("name property cannot be empty");
        RuleFor(x => x.Description).NotEmpty().WithMessage("description cannot be empty");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("price should be higher than zero");
        RuleFor(x => x.Category).NotEmpty().WithMessage("category is required");
    }
}

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        string Description,
        decimal Price,
        List<string> Category,
        string ImageFile) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IDocumentSession session = session;

        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                Category = command.Category,
                ImageFile = command.ImageFile
            };

            session.Store(product);
            await session.SaveChangesAsync();

            return new CreateProductResult(product.Id);
        }
    }
}
