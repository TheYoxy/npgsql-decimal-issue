using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Issue;

public sealed class AppDbContext : DbContext
{
    public DbSet<MainData> MainDatas { get; set; }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(
            "Host=localhost;Username=test;Password=test;Port=5430;Database=test;Include Error Detail=true");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
