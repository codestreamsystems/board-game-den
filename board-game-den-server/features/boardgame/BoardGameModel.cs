public class BoardGameModel
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MinPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public int MinTime { get; set; }
    public int MaxTime { get; set; }
    public decimal BGGRating { get; set; }
    public string URL { get; set; } = string.Empty;
    public string Thumbnail { get; set; } = string.Empty;
    public string Thumbnail2 { get; set; } = string.Empty;
    public string MainImage { get; set; } = string.Empty;
    public decimal SalePrice { get; set; }
    public decimal OurPrice => Math.Round(SalePrice * 1.1m, 2);
}