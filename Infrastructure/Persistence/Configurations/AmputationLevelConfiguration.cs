using Domain.AmputationLevels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AmputationLevelConfiguration: IEntityTypeConfiguration<AmputationLevel>
{
    public void Configure(EntityTypeBuilder<AmputationLevel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new AmputationLevelId(x));

        builder.Property(x => x.Title).IsRequired().HasColumnType("varchar(255)");
    }
}