namespace CrossBorderERP.Core.Models;

/// <summary>
/// Represents an item in an order
/// </summary>
public class OrderItem
{
    /// <summary>
    /// Product SKU
    /// </summary>
    public string ProductSku { get; set; } = string.Empty;

    /// <summary>
    /// Product name
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Quantity ordered
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Unit price in USD
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Total price for this item (Quantity * UnitPrice)
    /// </summary>
    public decimal TotalPrice => Quantity * UnitPrice;

    /// <summary>
    /// Product image URL
    /// </summary>
    public string? ImageUrl { get; set; }
}
