using Domain.Reviews;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new ReviewId(x));

        builder.Property(x => x.Cons).IsRequired();
        builder.Property(x => x.Pros).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        
        builder.Property(x => x.Date)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_Review_User")
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Prosthetic)
            .WithMany()
            .HasForeignKey(x => x.ProstheticId)
            .HasConstraintName("FK_Review_Prosthetic")
            .OnDelete(DeleteBehavior.Restrict);
    }
}