using Microsoft.EntityFrameworkCore;
using SchedFrex.DataAccess.Entities;

namespace SchedFrex.DataAccess;

public class CalendarDbContext : DbContext 
{
    public CalendarDbContext(DbContextOptions<CalendarDbContext> options) :
        base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CalendarDbContext).Assembly);
    }
    
    public DbSet<ProblemEntity> Problems { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CalendarEntity> Calendars { get; set; }
}