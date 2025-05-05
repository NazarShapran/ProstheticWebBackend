using Domain.Functionalities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class FunctionalityConfiguration: IEntityTypeConfiguration<Functionality>
{
    public void Configure(EntityTypeBuilder<Functionality> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new FunctionalityId(x));

        builder.Property(x => x.Title).IsRequired().HasColumnType("varchar(255)");
    }
}