using FoodOrdering.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Application.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IApplicationDbContext _context;

        public StatisticsService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetRestaurantCountAsync()
            => await _context.Restaurant.CountAsync();

        public async Task<int> GetCustomerCountAsync()
            => await _context.Customer.CountAsync();

        public async Task<int> GetOrderCountAsync()
            => await _context.Order.CountAsync();
    }
}
