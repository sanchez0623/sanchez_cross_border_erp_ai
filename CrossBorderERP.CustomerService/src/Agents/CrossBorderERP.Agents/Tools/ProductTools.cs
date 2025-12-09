using System.ComponentModel;

namespace CrossBorderERP.Agents.Tools;

/// <summary>
/// Product catalog tool functions for AI agents
/// </summary>
public class ProductTools
{
    [Description("Searches for products by keyword")]
    public static List<ProductInfo> SearchProducts(
        [Description("Search keyword")] string keyword,
        [Description("Maximum number of results (default: 10)")] int maxResults = 10)
    {
        // Mock data - in production, this would query a product database
        var allProducts = new List<ProductInfo>
        {
            new ProductInfo
            {
                ProductSku = "PROD-001",
                ProductName = "Wireless Bluetooth Headphones",
                Description = "Premium noise-canceling headphones with 30-hour battery life",
                Price = 79.99m,
                InStock = true,
                Category = "Electronics",
                ImageUrl = "https://example.com/headphones.jpg",
                Rating = 4.5
            },
            new ProductInfo
            {
                ProductSku = "PROD-002",
                ProductName = "USB-C Fast Charger",
                Description = "65W fast charging adapter with foldable plug",
                Price = 25.00m,
                InStock = true,
                Category = "Accessories",
                ImageUrl = "https://example.com/charger.jpg",
                Rating = 4.7
            },
            new ProductInfo
            {
                ProductSku = "PROD-003",
                ProductName = "Smartphone Case",
                Description = "Durable protective case with kickstand",
                Price = 15.99m,
                InStock = true,
                Category = "Accessories",
                ImageUrl = "https://example.com/case.jpg",
                Rating = 4.3
            }
        };

        return allProducts
            .Where(p => p.ProductName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                       p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .Take(maxResults)
            .ToList();
    }

    [Description("Gets detailed information about a specific product")]
    public static ProductInfo GetProductDetails(
        [Description("Product SKU")] string productSku)
    {
        // Mock data - in production, this would query a product database
        return new ProductInfo
        {
            ProductSku = productSku,
            ProductName = "Wireless Bluetooth Headphones",
            Description = "Premium noise-canceling headphones with 30-hour battery life. " +
                         "Features include: Active Noise Cancellation (ANC), Bluetooth 5.0, " +
                         "Premium sound quality, Comfortable over-ear design, Foldable for travel",
            Price = 79.99m,
            InStock = true,
            Category = "Electronics",
            ImageUrl = "https://example.com/headphones.jpg",
            Rating = 4.5,
            ReviewCount = 1234,
            Specifications = new Dictionary<string, string>
            {
                { "Battery Life", "30 hours" },
                { "Connectivity", "Bluetooth 5.0" },
                { "Weight", "250g" },
                { "Color Options", "Black, Silver, Blue" }
            }
        };
    }

    [Description("Gets personalized product recommendations for a customer")]
    public static List<ProductInfo> GetRecommendations(
        [Description("Customer ID")] string customerId,
        [Description("Number of recommendations (default: 5)")] int count = 5)
    {
        // Mock data - in production, this would use ML/recommendation engine
        return new List<ProductInfo>
        {
            new ProductInfo
            {
                ProductSku = "PROD-004",
                ProductName = "Wireless Mouse",
                Description = "Ergonomic wireless mouse with precision tracking",
                Price = 29.99m,
                InStock = true,
                Category = "Accessories",
                ImageUrl = "https://example.com/mouse.jpg",
                Rating = 4.6
            },
            new ProductInfo
            {
                ProductSku = "PROD-005",
                ProductName = "Laptop Stand",
                Description = "Adjustable aluminum laptop stand for better ergonomics",
                Price = 39.99m,
                InStock = true,
                Category = "Accessories",
                ImageUrl = "https://example.com/stand.jpg",
                Rating = 4.8
            }
        }.Take(count).ToList();
    }
}

/// <summary>
/// Product information model
/// </summary>
public class ProductInfo
{
    public string ProductSku { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool InStock { get; set; }
    public string Category { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public Dictionary<string, string>? Specifications { get; set; }
}
