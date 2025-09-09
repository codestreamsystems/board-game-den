public class BoardGameProductsResponse 
{
    public List<BoardGameModel> Products { get; set; } = new();
    public int TotalCount { get; set; }
    public DateTime FetchedAt { get; set; }
}