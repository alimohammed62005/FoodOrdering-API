namespace FoodOrdering.Application.Interfaces
{
    public interface IStatisticsService
    {
        Task<int> GetRestaurantCountAsync();
        Task<int> GetCustomerCountAsync();
        Task<int> GetOrderCountAsync();
    }
}
