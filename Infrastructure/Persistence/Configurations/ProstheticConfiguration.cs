using Domain.Prosthetics;
using Domain.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProstheticConfiguration : IEntityTypeConfiguration<Prosthetic>
{
    public void Configure(EntityTypeBuilder<Prosthetic> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new ProstheticId(x));
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnType("varchar(255)");
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(x => x.Weight)
            .IsRequired();
        
        builder.HasOne(x => x.Material)
            .WithMany()
            .HasForeignKey(x => x.MaterialId)
            .HasConstraintName("FK_Prosthetics_Materials")
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Type)
            .WithMany()
            .HasForeignKey(x => x.TypeId)
            .HasConstraintName("FK_Prosthetics_Types")
            .OnDelete(DeleteBehavior.Restrict);
        
        
        builder.HasOne(x => x.Functionality)
            .WithMany()
            .HasForeignKey(x => x.FunctionalityId)
            .HasConstraintName("FK_Prosthetics_Functionalities")
            .OnDelete(DeleteBehavior.Restrict);
    }
}