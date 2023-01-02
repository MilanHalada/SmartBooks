using Bogus;
using Microsoft.EntityFrameworkCore;
using SmartBooks.Domain.Entities;

namespace SmartBooks.Domain;

public class SmartBooksContext : DbContext
{
    public DbSet<Lecture> Lectures { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    public SmartBooksContext(DbContextOptions options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Subject>()
            .HasMany<Lecture>(s => s.Lectures)
            .WithOne(l => l.Subject)
            .HasForeignKey(l => l.SubjectId);


    }
}