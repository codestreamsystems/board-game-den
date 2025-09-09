namespace board_game_den_server.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiKeyMiddleware> _logger;
        private const string API_KEY_HEADER = "X-API-Key";

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<ApiKeyMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip API key check for root endpoint and Swagger
            var path = context.Request.Path.Value?.ToLower();
            if (path == "/" || path?.StartsWith("/swagger") == true)
            {
                await _next(context);
                return;
            }

            // Check for API key in header
            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var providedApiKey))
            {
                _logger.LogWarning("API Key missing from request to {Path}", context.Request.Path);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key is missing");
                return;
            }

            var validApiKey = _configuration["ApiKey"];
            if (string.IsNullOrEmpty(validApiKey))
            {
                _logger.LogError("API Key not configured in appsettings");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("API Key not configured");
                return;
            }

            if (!validApiKey.Equals(providedApiKey))
            {
                _logger.LogWarning("Invalid API Key provided: {ProvidedKey}", providedApiKey);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            _logger.LogDebug("Valid API Key provided for {Path}", context.Request.Path);
            await _next(context);
        }
    }
}