using Domain.ProstheticTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProstheticTypeConfiguration: IEntityTypeConfiguration<ProstheticType>
{
    public void Configure(EntityTypeBuilder<ProstheticType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new TypeId(x));

        builder.Property(x => x.Title).IsRequired().HasColumnType("varchar(255)");
    }
}