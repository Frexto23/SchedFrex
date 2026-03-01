using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Configurations;

public class EntryConfiguration : IEntityTypeConfiguration<EntryEntity>
{
    public void Configure(EntityTypeBuilder<EntryEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .HasOne(e => e.Calendar)
            .WithMany(c => c.Entries)
            .HasForeignKey(e => e.CalendarId);

        builder
            .OwnsOne(e => e.Slot, t =>
            {
                t.Property(p => p.Start).HasColumnName("Start");
                t.Property(p => p.End).HasColumnName("End");
            });

        builder.Property(e => e.Title).IsRequired().HasMaxLength(250);
    }
}