namespace Ordering.Infrastructure.Configurations;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
            id => id.Value,
            dbid => CustomerId.Of(dbid))
            .IsRequired();
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(255);
        builder.HasIndex(x => x.Email).IsUnique();
    }
}
