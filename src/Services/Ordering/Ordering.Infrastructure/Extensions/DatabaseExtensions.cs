using Microsoft.AspNetCore.Builder;
using Ordering.Infrastructure.Data.Extensions;

namespace Ordering.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
        await SeedAsync(dbContext);
        return app;
    }

    private static async Task SeedAsync(ApplicationDbContext dbContext)
    {
        await SeedCustormerAsync(dbContext);
        await SeedProductAsync(dbContext);
        await SeedOrderAsync(dbContext);
    }

    private static async Task SeedCustormerAsync(ApplicationDbContext dbContext)
    {
        if (!dbContext.Customers.Any())
        {
            await dbContext.Customers.AddRangeAsync(InitialData.Customers);
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task SeedProductAsync(ApplicationDbContext dbContext)
    {
        if (!dbContext.Products.Any())
        {
            await dbContext.Products.AddRangeAsync(InitialData.Products);
            await dbContext.SaveChangesAsync();
        }
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedOrderAsync(ApplicationDbContext dbContext)
    {
        if (!dbContext.Orders.Any())
        {
            await dbContext.Orders.AddRangeAsync(InitialData.OrdersWithItems);
            await dbContext.SaveChangesAsync();
        }
    }
}
