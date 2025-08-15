namespace Ordering.Application.Orders.Queries.GetOrderByName;

public record GetOrderByNameQueryResult(IEnumerable<OrderDto> Orders);
public record GetOrderByNameQuery(string Name) : IQuery<GetOrderByNameQueryResult>;