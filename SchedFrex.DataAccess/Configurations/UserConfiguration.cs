using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .HasMany(u => u.Problems)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        builder
            .HasMany(u => u.Calendars)
            .WithOne(c => c.User);

        builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(250);

        builder
            .HasIndex(u => u.UserName)
            .IsUnique();
    }
}