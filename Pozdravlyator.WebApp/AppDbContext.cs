using Microsoft.EntityFrameworkCore;
using Pozdravlyator.Domain;

namespace Pozdravlyator.WebApp;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
    {
    }

    public DbSet<User> Users { get; set; }
}