using System.Text.Json;

public interface IBoardGameService
{
    Task<BoardGameProductsResponse> GetProductsAsync();
}

public class BoardGameService(HttpClient httpClient, ILogger<BoardGameService> logger) : IBoardGameService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<BoardGameService> _logger = logger;
    private const string ExternalApiUrl = "https://myboardgamelibrary.com/boardgames.json";

    public async Task<BoardGameProductsResponse> GetProductsAsync()
    {
        try
        {
            _logger.LogInformation("Fetching board games from external API: {Url}", ExternalApiUrl);

            var response = await _httpClient.GetAsync(ExternalApiUrl);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("External API returned {StatusCode}", response.StatusCode);
                return new BoardGameProductsResponse();
            }

            var jsonContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ExternalApiResponse>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (apiResponse?.Sheet1 == null)
            {
                _logger.LogWarning("Failed to deserialize external API response or Sheet1 is null");
                return new BoardGameProductsResponse();
            }

            if (apiResponse == null)
            {
                _logger.LogWarning("Failed to deserialize external API response");
                return new BoardGameProductsResponse();
            }
            
            var products = apiResponse.Sheet1.ToList();

            return new BoardGameProductsResponse
            {
                Products = products,
                TotalCount = products.Count,
                FetchedAt = DateTime.UtcNow
            };
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Network error while fetching from external API");
            return new BoardGameProductsResponse();
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "JSON parsing error from external API");
            return new BoardGameProductsResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while fetching board games");
            return new BoardGameProductsResponse();
        }
    }
}
