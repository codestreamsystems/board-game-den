namespace board_game_den_server.Models
{
    public class BoardGameProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal SalePrice { get; set; }
        public decimal OurPrice => Math.Round(SalePrice * 1.1m, 2);
        public string SupplierCode { get; set; } = string.Empty;
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int PlayTimeMinutes { get; set; }
        public decimal Rating { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int YearPublished { get; set; }
        public bool InStock { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}