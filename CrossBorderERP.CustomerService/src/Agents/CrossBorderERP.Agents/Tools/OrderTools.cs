using CrossBorderERP.Core.Models;
using System.ComponentModel;

namespace CrossBorderERP.Agents.Tools;

/// <summary>
/// Order management tool functions for AI agents
/// </summary>
public class OrderTools
{
    [Description("Retrieves order information by order ID")]
    public static OrderInfo GetOrderInfo(
        [Description("The order ID to retrieve")] string orderId)
    {
        // Mock data - in production, this would query a database
        return new OrderInfo
        {
            OrderId = orderId,
            CustomerId = "CUST-12345",
            Status = "Shipped",
            OrderDate = DateTime.UtcNow.AddDays(-5),
            TotalAmount = 299.99m,
            PaymentMethod = "Credit Card",
            CustomerEmail = "customer@example.com",
            Items = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductSku = "PROD-001",
                    ProductName = "Wireless Bluetooth Headphones",
                    Quantity = 1,
                    UnitPrice = 79.99m,
                    ImageUrl = "https://example.com/headphones.jpg"
                },
                new OrderItem
                {
                    ProductSku = "PROD-002",
                    ProductName = "USB-C Fast Charger",
                    Quantity = 2,
                    UnitPrice = 25.00m,
                    ImageUrl = "https://example.com/charger.jpg"
                }
            },
            ShippingInfo = new ShippingInfo
            {
                TrackingNumber = "TRK123456789",
                Carrier = "DHL Express",
                Status = "In Transit",
                EstimatedDeliveryDate = DateTime.UtcNow.AddDays(2),
                ShippingAddress = "123 Main St, New York, NY 10001, USA",
                TrackingEvents = new List<TrackingEvent>
                {
                    new TrackingEvent
                    {
                        Timestamp = DateTime.UtcNow.AddDays(-2),
                        Location = "Shanghai, China",
                        Description = "Package departed from origin facility"
                    },
                    new TrackingEvent
                    {
                        Timestamp = DateTime.UtcNow.AddDays(-1),
                        Location = "Los Angeles, CA, USA",
                        Description = "Arrived at customs"
                    },
                    new TrackingEvent
                    {
                        Timestamp = DateTime.UtcNow,
                        Location = "New York, NY, USA",
                        Description = "Out for delivery"
                    }
                }
            }
        };
    }

    [Description("Tracks shipment by tracking number")]
    public static ShippingInfo TrackShipment(
        [Description("The tracking number")] string trackingNumber)
    {
        // Mock data - in production, this would query a logistics API
        return new ShippingInfo
        {
            TrackingNumber = trackingNumber,
            Carrier = "DHL Express",
            Status = "In Transit",
            EstimatedDeliveryDate = DateTime.UtcNow.AddDays(2),
            ShippingAddress = "123 Main St, New York, NY 10001, USA",
            TrackingEvents = new List<TrackingEvent>
            {
                new TrackingEvent
                {
                    Timestamp = DateTime.UtcNow.AddDays(-2),
                    Location = "Shanghai, China",
                    Description = "Package departed from origin facility"
                },
                new TrackingEvent
                {
                    Timestamp = DateTime.UtcNow.AddDays(-1),
                    Location = "Los Angeles, CA, USA",
                    Description = "Arrived at customs"
                },
                new TrackingEvent
                {
                    Timestamp = DateTime.UtcNow,
                    Location = "New York, NY, USA",
                    Description = "Out for delivery"
                }
            }
        };
    }

    [Description("Initiates a refund request for an order")]
    public static string RequestRefund(
        [Description("The order ID")] string orderId,
        [Description("Reason for refund")] string reason)
    {
        // Mock response - in production, this would create a refund request
        return $"Refund request submitted successfully for order {orderId}. " +
               $"Reference number: REF-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}. " +
               $"Expected processing time: 5-7 business days. " +
               $"You will receive a confirmation email shortly.";
    }
}
