namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base("Couldn't find any product")
        {

        }
    }
}
