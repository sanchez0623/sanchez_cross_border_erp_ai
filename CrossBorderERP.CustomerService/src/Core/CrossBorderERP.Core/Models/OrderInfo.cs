namespace CrossBorderERP.Core.Models;

/// <summary>
/// Order information model
/// </summary>
public class OrderInfo
{
    /// <summary>
    /// Order ID
    /// </summary>
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Customer ID
    /// </summary>
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// Order status
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Order creation date
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Total order amount in USD
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Order items
    /// </summary>
    public List<OrderItem> Items { get; set; } = new();

    /// <summary>
    /// Shipping information
    /// </summary>
    public ShippingInfo? ShippingInfo { get; set; }

    /// <summary>
    /// Payment method
    /// </summary>
    public string PaymentMethod { get; set; } = string.Empty;

    /// <summary>
    /// Customer email
    /// </summary>
    public string CustomerEmail { get; set; } = string.Empty;

    /// <summary>
    /// Order notes
    /// </summary>
    public string? Notes { get; set; }
}
