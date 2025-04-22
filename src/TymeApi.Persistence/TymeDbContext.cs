using Microsoft.EntityFrameworkCore;
using TymeApi.Domain.Entities;

namespace TymeApi.Persistence
{
    public class TymeDbContext(DbContextOptions<TymeDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
    }
}