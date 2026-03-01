using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess.Configurations;

public class CalendarConfiguration : IEntityTypeConfiguration<CalendarEntity>
{
    public void Configure(EntityTypeBuilder<CalendarEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder
            .HasOne(c => c.User)
            .WithMany(u => u.Calendars);

        builder
            .HasMany(c => c.Entries)
            .WithOne(e => e.Calendar)
            .HasForeignKey(e => e.CalendarId);
    }
}