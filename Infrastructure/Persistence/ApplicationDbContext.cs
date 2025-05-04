using System.Reflection;
using Domain.Functionalitys;
using Domain.Materials;
using Microsoft.EntityFrameworkCore;
using Domain.Prosthetics;
using Domain.Request;
using Domain.Reviews;
using Domain.Roles;
using Domain.Statuses;
using Domain.Types;
using Domain.Users;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<Request> Requests { get; set; }
    public DbSet<Prosthetic> Prosthetics { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<ProstheticType> ProstheticTypes { get; set; }
    public DbSet<Functionality> Functionalities { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}