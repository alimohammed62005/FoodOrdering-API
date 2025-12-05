using FoodOrdering.Application.DTOs;
using FoodOrdering.Application.Interfaces;
using FoodOrdering.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IApplicationDbContext _context;

        public OrderService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            return await _context.Order
                .Select(o => new OrderDto
                {
                    OrderId = o.Id,
                    CustomerId = o.CustomerId,
                    RestaurantId = o.RestaurantId,
                    OrderDate = o.OrderDate,
                    TotalPrice = o.TotalPrice
                })
                .ToListAsync();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Order.FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return null;

            return new OrderDto
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                RestaurantId = order.RestaurantId,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            };
        }

        public async Task<OrderDto> AddOrderAsync(CreateOrderDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                throw new Exception("Items is null");

            foreach (var i in dto.Items)
            {
                Console.WriteLine($"MenuItemId = {i.MenuItemId}, Quantity = {i.Quantity}");
            }

            Customer customer = null;

            if (dto.CustomerId > 0)
            {
                customer = await _context.Customer.FirstOrDefaultAsync(c => c.Id == dto.CustomerId);
            }

            if (customer == null && dto.Customer != null)
            {
                customer = await _context.Customer
                    .FirstOrDefaultAsync(c => c.Phone == dto.Customer.Phone);

                if (customer == null)
                {
                    customer = new Customer
                    {
                        FullName = dto.Customer.FullName,
                        Phone = dto.Customer.Phone,
                        Email = dto.Customer.Email,
                        Address = dto.Customer.Address
                    };

                    _context.Customer.Add(customer);
                    await _context.SaveChangesAsync();
                }
            }

            if (customer == null)
                throw new Exception("Customer not found");

            var order = new Order
            {
                CustomerId = customer.Id,
                RestaurantId = dto.RestaurantId,
                OrderDate = DateTime.Now,
                TotalPrice = 0
            };

            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in dto.Items)
            {
                var menuItem = await _context.MenuItem.FirstOrDefaultAsync(m => m.Id == item.MenuItemId);

                if (menuItem == null)
                    throw new Exception($"MenuItem {item.MenuItemId} not found");

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    MenuItemId = menuItem.Id,
                    Quantity = item.Quantity,
                    Price = menuItem.Price
                };

                _context.OrderItem.Add(orderItem);
            }

            await _context.SaveChangesAsync();

            var totalPrice = await _context.OrderItem
                .Where(oi => oi.OrderId == order.Id)
                .SumAsync(oi => oi.Price * oi.Quantity);

            order.TotalPrice = totalPrice;

            await _context.SaveChangesAsync();

            return new OrderDto
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                RestaurantId = order.RestaurantId,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            };
        }


        public async Task<bool> UpdateOrderAsync(UpdateOrderDto dto)
        {
            var order = await _context.Order
                .Include(o => o.OrderItem)
                .FirstOrDefaultAsync(o => o.Id == dto.OrderId);

            if (order == null)
                return false;

            order.OrderDate = dto.OrderDate;

            _context.OrderItem.RemoveRange(order.OrderItem);

            foreach (var item in dto.Items)
            {
                var menuItem = await _context.MenuItem.FirstOrDefaultAsync(m => m.Id == item.MenuItemId);

                if (menuItem == null)
                    throw new Exception($"MenuItem {item.MenuItemId} not found");

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    MenuItemId = menuItem.Id,
                    Quantity = item.Quantity,
                    Price = menuItem.Price
                };

                _context.OrderItem.Add(orderItem);
            }

            await _context.SaveChangesAsync();

            var totalPrice = await _context.OrderItem
                .Where(oi => oi.OrderId == order.Id)
                .SumAsync(oi => oi.Price * oi.Quantity);

            order.TotalPrice = totalPrice;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Order
                .Include(o => o.OrderItem)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return false;

            _context.OrderItem.RemoveRange(order.OrderItem);
            _context.Order.Remove(order);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
