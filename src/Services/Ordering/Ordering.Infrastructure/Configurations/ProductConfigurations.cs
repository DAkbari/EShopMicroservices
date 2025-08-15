namespace Ordering.Infrastructure.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, dbid => ProductId.Of(dbid));
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
    }
}
