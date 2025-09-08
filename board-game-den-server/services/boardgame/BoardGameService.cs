public interface IBoardGameService
{

}

public class BoardGameService(HttpClient httpClient, ILogger<BoardGameService> logger) : IBoardGameService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<BoardGameService> _logger = logger;
    private const string ExternalApiUrl = "http://myboardgamelibrary.com/boardgames.json";
    
    
}