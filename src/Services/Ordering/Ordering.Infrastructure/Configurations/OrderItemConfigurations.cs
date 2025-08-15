namespace Ordering.Infrastructure.Configurations;

public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                dbid => OrderItemId.Of(dbid));
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)").IsRequired();
        builder
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }
}
