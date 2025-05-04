using Domain.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RequestConfiguration: IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new RequestId(x));
        
        builder.Property(x => x.Description).IsRequired().HasColumnType("varchar(1000)");
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("FK_Request_User")
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Prosthetic)
            .WithMany()
            .HasForeignKey(x => x.ProstheticId)
            .HasConstraintName("FK_Request_Prosthetic")
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Status)
            .WithMany()
            .HasForeignKey(x => x.StatusId)
            .HasConstraintName("FK_Request_Status")
            .OnDelete(DeleteBehavior.Restrict);
    } 
}