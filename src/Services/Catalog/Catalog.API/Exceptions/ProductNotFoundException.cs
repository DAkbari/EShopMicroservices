namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id) : base($"Couldn't find product {id}")
        {
        }
    }
}
