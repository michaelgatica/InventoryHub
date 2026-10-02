namespace ClientApp.Models;

public class ProductListResponse
{
    public bool Success { get; set; }

    public int Count { get; set; }

    public DateTime GeneratedAt { get; set; }

    public List<Product> Data { get; set; } = new();
}
