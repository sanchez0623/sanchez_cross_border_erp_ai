namespace CrossBorderERP.Core.Models;

/// <summary>
/// Shipping and logistics information
/// </summary>
public class ShippingInfo
{
    /// <summary>
    /// Tracking number
    /// </summary>
    public string TrackingNumber { get; set; } = string.Empty;

    /// <summary>
    /// Carrier name (e.g., DHL, FedEx, UPS)
    /// </summary>
    public string Carrier { get; set; } = string.Empty;

    /// <summary>
    /// Shipping status
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Estimated delivery date
    /// </summary>
    public DateTime? EstimatedDeliveryDate { get; set; }

    /// <summary>
    /// Actual delivery date
    /// </summary>
    public DateTime? ActualDeliveryDate { get; set; }

    /// <summary>
    /// Shipping address
    /// </summary>
    public string ShippingAddress { get; set; } = string.Empty;

    /// <summary>
    /// Recent tracking events
    /// </summary>
    public List<TrackingEvent> TrackingEvents { get; set; } = new();
}

/// <summary>
/// A single tracking event
/// </summary>
public class TrackingEvent
{
    /// <summary>
    /// Event timestamp
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Event location
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Event description
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
