using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Configurations;

public class ProblemConfiguration : IEntityTypeConfiguration<ProblemEntity>
{
    public void Configure(EntityTypeBuilder<ProblemEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .OwnsMany(p => p.TimeIntervals, b =>
            {
                b.ToTable("ProblemIntervals");

                b.Property(p => p.Start).HasColumnName("Start");
                b.Property(p => p.End).HasColumnName("End");

                b.WithOwner().HasForeignKey("ProblemId");
            });

        builder
            .HasOne(p => p.User)
            .WithMany(u => u.Problems)
            .HasForeignKey(p => p.UserId);
    }
}