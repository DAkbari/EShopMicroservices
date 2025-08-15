using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public class GetOrderByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerQueryResult>
{
    public async Task<GetOrderByCustomerQueryResult> Handle(GetOrderByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Where(o => o.CutomerId == CustomerId.Of(request.CustomerId))
            .OrderBy(o => o.OrderName.Value)
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return new GetOrderByCustomerQueryResult(orders.ToOrderDtoList());
    }
}