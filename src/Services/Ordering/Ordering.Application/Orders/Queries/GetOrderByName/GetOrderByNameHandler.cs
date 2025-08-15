using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrderByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrderByNameQuery, GetOrderByNameQueryResult>
{
    public async Task<GetOrderByNameQueryResult> Handle(GetOrderByNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Where(o => o.OrderName.Value.Contains(request.Name))
            .OrderBy(o => o.OrderName.Value)
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        var ordersDto = orders.ToOrderDtoList();
        return new GetOrderByNameQueryResult(ordersDto);
    }
}