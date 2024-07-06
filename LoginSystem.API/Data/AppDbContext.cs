using Microsoft.EntityFrameworkCore;
using Models.Login;

namespace LoginSystem.API.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Users> Users { get; set; } = default!;
}