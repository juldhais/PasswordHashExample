using Microsoft.EntityFrameworkCore;
using PasswordHashExample.WebAPI.Entities;

namespace PasswordHashExample.WebAPI.Data;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}