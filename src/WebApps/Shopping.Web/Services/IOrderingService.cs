namespace Shopping.Web.Services
{
    public interface IOrderingService
    {
        [Get("/ordering-service/orders?pageIndex={pageIndex}&pageSize={pageSize}")]
        Task<GetOrdersResponse> GetOrdersAsync(int pageIndex, int pageSize);

        [Get("/ordering-service/orders/customer/{orderName}")]
        Task<GetOrdersByNameResponse> GetOrdersByNameAsync(string orderName);

        [Get("/ordering-service/orders/customer/{customerId}")]
        Task<GetOrdersByCustomerResponse> GetOrdersByCustomerAsync(Guid customerId);
    }
}
