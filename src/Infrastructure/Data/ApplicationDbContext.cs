using System.Reflection;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entities;
using BlogApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    public DbSet<Article> Articles => Set<Article>();
    
    public DbSet<Author> Authors => Set<Author>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        builder.Entity<Author>()
            .HasMany(author => author.Articles)
            .WithOne(article => article.Author)
            .HasForeignKey(article => article.AuthorId);
    }
}
