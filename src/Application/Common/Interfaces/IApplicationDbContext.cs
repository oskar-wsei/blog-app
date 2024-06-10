using BlogApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlogApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Article> Articles { get; }
    
    DbSet<Author> Authors { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    EntityEntry Entry(object entity);
}
