using Microsoft.EntityFrameworkCore;
using Covid.API.Entities;
using System.Reflection;

namespace Covid.API.DataBase;
public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine,LogLevel.Information);
    }

    public DbSet<CovidBilgi> CovidBilgis {get;set;}
}