using HistoryApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HistoryApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<EventType> EventTypes => Set<EventType>();
    public DbSet<History> Histories => Set<History>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventType>().HasData(
            new EventType { Id = 1, Name = "Редактирование" },
            new EventType { Id = 2, Name = "Добавление записи" },
            new EventType { Id = 3, Name = "Удаление записи" }
        );
    }
}
