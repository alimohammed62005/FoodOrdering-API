using FoodOrdering.Application.DTOs;
using FoodOrdering.Application.Interfaces;
using FoodOrdering.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IApplicationDbContext _context;

        public CustomerService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            return await _context.Customer
                .Select(c => new CustomerDto
                {
                    CustomerId = c.Id,
                    FullName = c.FullName,
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address
                })
                .ToListAsync();
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
                return null;

            return new CustomerDto
            {
                FullName = customer.FullName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public async Task<CustomerDto> AddCustomerAsync(CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                FullName = customer.FullName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public async Task<bool> UpdateCustomerAsync(int id, UpdateCustomerDto dto)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
                return false;

            customer.FullName = dto.FullName;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
            customer.Address = dto.Address;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
                return false;

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
