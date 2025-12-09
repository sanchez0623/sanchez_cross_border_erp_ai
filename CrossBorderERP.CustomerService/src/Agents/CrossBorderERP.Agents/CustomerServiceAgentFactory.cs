using CrossBorderERP.Agents.Tools;
using CrossBorderERP.Infrastructure;
using Microsoft.Extensions.AI;

namespace CrossBorderERP.Agents;

/// <summary>
/// Factory for creating specialized customer service agents
/// </summary>
public class CustomerServiceAgentFactory
{
    private readonly string _githubToken;
    private readonly string _modelId;

    public CustomerServiceAgentFactory(string githubToken, string modelId = "gpt-4o-mini")
    {
        _githubToken = githubToken ?? throw new ArgumentNullException(nameof(githubToken));
        _modelId = modelId;
    }

    /// <summary>
    /// Creates a router agent that analyzes customer inquiries
    /// </summary>
    public IChatClient CreateRouterAgent()
    {
        return GitHubModelsChatClient.Create(_githubToken, _modelId);
    }

    /// <summary>
    /// Creates an order management agent with order tools
    /// </summary>
    public IChatClient CreateOrderAgent()
    {
        return GitHubModelsChatClient.Create(_githubToken, _modelId);
    }

    /// <summary>
    /// Creates a product agent with product tools
    /// </summary>
    public IChatClient CreateProductAgent()
    {
        return GitHubModelsChatClient.Create(_githubToken, _modelId);
    }

    /// <summary>
    /// Creates a general customer service agent with all tools
    /// </summary>
    public IChatClient CreateGeneralAgent()
    {
        return GitHubModelsChatClient.Create(_githubToken, _modelId);
    }

    /// <summary>
    /// Gets order management tools
    /// </summary>
    public static IList<AITool> GetOrderTools()
    {
        var tools = new List<AITool>
        {
            AIFunctionFactory.Create(OrderTools.GetOrderInfo),
            AIFunctionFactory.Create(OrderTools.TrackShipment),
            AIFunctionFactory.Create(OrderTools.RequestRefund)
        };
        return tools;
    }

    /// <summary>
    /// Gets product catalog tools
    /// </summary>
    public static IList<AITool> GetProductTools()
    {
        var tools = new List<AITool>
        {
            AIFunctionFactory.Create(ProductTools.SearchProducts),
            AIFunctionFactory.Create(ProductTools.GetProductDetails),
            AIFunctionFactory.Create(ProductTools.GetRecommendations)
        };
        return tools;
    }

    /// <summary>
    /// Gets all available tools
    /// </summary>
    public static IList<AITool> GetAllTools()
    {
        var tools = new List<AITool>();
        tools.AddRange(GetOrderTools());
        tools.AddRange(GetProductTools());
        return tools;
    }
}
