using FoodOrdering.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("restaurants")]
        public async Task<ActionResult<int>> GetRestaurantCount()
            => Ok(await _statisticsService.GetRestaurantCountAsync());

        [HttpGet("customers")]
        public async Task<ActionResult<int>> GetCustomerCount()
            => Ok(await _statisticsService.GetCustomerCountAsync());

        [HttpGet("orders")]
        public async Task<ActionResult<int>> GetOrderCount()
            => Ok(await _statisticsService.GetOrderCountAsync());
    }
}
