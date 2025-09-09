using Microsoft.AspNetCore.Mvc;

namespace board_game_den_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController(IBoardGameService boardGameService, ILogger<ProductsController> logger) : ControllerBase
    {
        private readonly IBoardGameService _boardGameService = boardGameService;
        private readonly ILogger<ProductsController> _logger = logger;

        /// <summary>
        /// Get all board game products
        /// </summary>
        /// <returns>List of board game products with calculated "Our Price" (Sale Price + 10%)</returns>
        [HttpGet]
        public async Task<ActionResult<BoardGameProductsResponse>> GetProducts()
        {
            try
            {
                _logger.LogInformation("Getting all products");

                var result = await _boardGameService.GetProductsAsync();

                if (result.Products.Count == 0)
                {
                    return NotFound(new
                    {
                        message = "No products found"
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products");
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving products",
                    error = ex.Message
                });
            }
        }

    }
}