using Microsoft.EntityFrameworkCore;
using FoodOrdering.Domain.Entities;

namespace FoodOrdering.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<City> City { get; } 
        DbSet<Customer> Customer { get; }
        DbSet<MenuItem> MenuItem { get; }
        DbSet<Order> Order { get; }
        DbSet<OrderItem> OrderItem { get; }
        DbSet<Restaurant> Restaurant { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
