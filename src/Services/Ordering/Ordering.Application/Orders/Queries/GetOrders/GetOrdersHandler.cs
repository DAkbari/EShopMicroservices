
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .OrderBy(o => o.OrderName.Value)
                .Skip(query.PaginationRequest.PageIndex * query.PaginationRequest.PageSize)
                .Take(query.PaginationRequest.PageSize)
                .ToListAsync(cancellationToken);
            var result = new PaginationResult<OrderDto>(
                query.PaginationRequest.PageIndex,
                query.PaginationRequest.PageSize,
                await dbContext.Orders.LongCountAsync(cancellationToken),
                orders.ToOrderDtoList()
            );
            return new GetOrdersResult(result);
        }
    }
}