using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientAppPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();

var app = builder.Build();

app.UseCors("ClientAppPolicy");
app.UseResponseCaching();

app.MapGet("/", () => Results.Ok(new
{
    application = "InventoryHub Server API",
    productEndpoint = "/api/productlist"
}));

app.MapGet("/api/productlist", GetProductList)
    .WithName("GetProductList");

app.MapGet("/api/products", GetProductList)
    .WithName("GetProductsLegacyRoute");

app.MapGet("/api/productlist/{id:int}", (int id) =>
{
    var product = InventoryData.GetProducts().FirstOrDefault(item => item.Id == id);

    return product is null
        ? Results.NotFound(new { error = "Product not found.", id })
        : Results.Ok(product);
});

app.Run();

static IResult GetProductList(IMemoryCache cache, ILogger<Program> logger)
{
    const string cacheKey = "inventoryhub-product-list";

    var response = cache.GetOrCreate(cacheKey, entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

        logger.LogInformation("Building product list response and storing it in cache.");

        var products = InventoryData.GetProducts();

        return new ProductListResponse(
            Success: true,
            Count: products.Count,
            GeneratedAt: DateTime.UtcNow,
            Data: products);
    });

    return Results.Ok(response);
}

public record Category(int Id, string Name);

public record Product(int Id, string Name, decimal Price, int Stock, Category Category);

public record ProductListResponse(bool Success, int Count, DateTime GeneratedAt, IReadOnlyList<Product> Data);

public static class InventoryData
{
    public static IReadOnlyList<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product(1, "Laptop", 1200.50m, 25, new Category(101, "Electronics")),
            new Product(2, "Headphones", 50.00m, 100, new Category(102, "Accessories")),
            new Product(3, "Wireless Mouse", 24.99m, 80, new Category(102, "Accessories")),
            new Product(4, "Mechanical Keyboard", 89.99m, 40, new Category(102, "Accessories")),
            new Product(5, "USB-C Dock", 129.99m, 15, new Category(103, "Computer Peripherals")),
            new Product(6, "Monitor", 249.99m, 18, new Category(101, "Electronics"))
        };
    }
}
