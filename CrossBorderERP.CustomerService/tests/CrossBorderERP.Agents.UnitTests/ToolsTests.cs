using CrossBorderERP.Agents.Tools;
using Xunit;

namespace CrossBorderERP.Agents.UnitTests;

public class ToolsTests
{
    [Fact]
    public void OrderTools_GetOrderInfo_ReturnsValidOrder()
    {
        // Arrange
        var orderId = "ORD-12345";

        // Act
        var order = OrderTools.GetOrderInfo(orderId);

        // Assert
        Assert.NotNull(order);
        Assert.Equal(orderId, order.OrderId);
        Assert.NotEmpty(order.Items);
        Assert.NotNull(order.ShippingInfo);
    }

    [Fact]
    public void OrderTools_TrackShipment_ReturnsValidShippingInfo()
    {
        // Arrange
        var trackingNumber = "TRK123456789";

        // Act
        var shippingInfo = OrderTools.TrackShipment(trackingNumber);

        // Assert
        Assert.NotNull(shippingInfo);
        Assert.Equal(trackingNumber, shippingInfo.TrackingNumber);
        Assert.NotEmpty(shippingInfo.TrackingEvents);
    }

    [Fact]
    public void OrderTools_RequestRefund_ReturnsConfirmation()
    {
        // Arrange
        var orderId = "ORD-12345";
        var reason = "Product damaged";

        // Act
        var result = OrderTools.RequestRefund(orderId, reason);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("Refund request submitted", result);
        Assert.Contains(orderId, result);
    }

    [Fact]
    public void ProductTools_SearchProducts_ReturnsResults()
    {
        // Arrange
        var keyword = "headphones";

        // Act
        var products = ProductTools.SearchProducts(keyword);

        // Assert
        Assert.NotNull(products);
        Assert.NotEmpty(products);
        Assert.Contains(products, p => 
            p.ProductName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void ProductTools_GetProductDetails_ReturnsProduct()
    {
        // Arrange
        var productSku = "PROD-001";

        // Act
        var product = ProductTools.GetProductDetails(productSku);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(productSku, product.ProductSku);
        Assert.NotNull(product.Specifications);
    }

    [Fact]
    public void ProductTools_GetRecommendations_ReturnsRecommendations()
    {
        // Arrange
        var customerId = "CUST-001";
        var count = 3;

        // Act
        var recommendations = ProductTools.GetRecommendations(customerId, count);

        // Assert
        Assert.NotNull(recommendations);
        Assert.True(recommendations.Count <= count);
    }

    [Fact]
    public void CustomerServiceAgentFactory_GetOrderTools_ReturnsTools()
    {
        // Act
        var tools = CustomerServiceAgentFactory.GetOrderTools();

        // Assert
        Assert.NotNull(tools);
        Assert.Equal(3, tools.Count); // GetOrderInfo, TrackShipment, RequestRefund
    }

    [Fact]
    public void CustomerServiceAgentFactory_GetProductTools_ReturnsTools()
    {
        // Act
        var tools = CustomerServiceAgentFactory.GetProductTools();

        // Assert
        Assert.NotNull(tools);
        Assert.Equal(3, tools.Count); // SearchProducts, GetProductDetails, GetRecommendations
    }

    [Fact]
    public void CustomerServiceAgentFactory_GetAllTools_ReturnsAllTools()
    {
        // Act
        var tools = CustomerServiceAgentFactory.GetAllTools();

        // Assert
        Assert.NotNull(tools);
        Assert.Equal(6, tools.Count); // All order and product tools
    }
}
