using Microsoft.EntityFrameworkCore;
using SeleniumBot.Data.Mapping;
using SeleniumBot.Models;

namespace SeleniumBot.Data;
public class AppDataContext : DbContext
{
    public DbSet<Lesson> Lessons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=DEVELOPMENT;Database=FatecDB;Integrated Security=True;TrustServerCertificate=True");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LessonMap());
    }

    
}