using Domain.ProstheticStatuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProstheticStatusConfiguration : IEntityTypeConfiguration<ProstheticStatus>
{
    public void Configure(EntityTypeBuilder<ProstheticStatus> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new ProstheticStatusId(x));
        
        builder.Property(x => x.Title).IsRequired();
    }
} 