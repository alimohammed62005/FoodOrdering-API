using Microsoft.EntityFrameworkCore;
using FoodOrdering.Domain.Entities;
using FoodOrdering.Application.Interfaces;

namespace FoodOrdering.Persistence.Contexts
{
    public partial class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> City { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
    }
}