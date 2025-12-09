using Microsoft.Extensions.AI;
using System.Text;

namespace CrossBorderERP.Agents;

/// <summary>
/// Orchestrates multiple AI agents to handle customer service inquiries
/// </summary>
public class CustomerServiceOrchestrator
{
    private readonly CustomerServiceAgentFactory _agentFactory;

    public CustomerServiceOrchestrator(CustomerServiceAgentFactory agentFactory)
    {
        _agentFactory = agentFactory ?? throw new ArgumentNullException(nameof(agentFactory));
    }

    /// <summary>
    /// Processes a customer inquiry using the appropriate agent
    /// </summary>
    public async Task<string> ProcessInquiryAsync(
        string customerMessage,
        string? customerId = null,
        CancellationToken cancellationToken = default)
    {
        // Step 1: Use router agent to classify the inquiry
        var category = await ClassifyInquiryAsync(customerMessage, cancellationToken);

        // Step 2: Route to appropriate specialized agent
        var agent = category switch
        {
            "order" => _agentFactory.CreateOrderAgent(),
            "product" => _agentFactory.CreateProductAgent(),
            _ => _agentFactory.CreateGeneralAgent()
        };

        // Step 3: Get appropriate tools
        var tools = category switch
        {
            "order" => CustomerServiceAgentFactory.GetOrderTools(),
            "product" => CustomerServiceAgentFactory.GetProductTools(),
            _ => CustomerServiceAgentFactory.GetAllTools()
        };

        // Step 4: Process the inquiry with the selected agent
        var systemPrompt = GetSystemPrompt(category);
        var messages = new List<ChatMessage>
        {
            new ChatMessage(ChatRole.System, systemPrompt),
            new ChatMessage(ChatRole.User, customerMessage)
        };

        var options = new ChatOptions
        {
            Temperature = 0.7f,
            Tools = tools
        };

        var response = await agent.GetResponseAsync(messages, options, cancellationToken);
        return response.Text ?? "I apologize, but I couldn't process your request. Please try again.";
    }

    /// <summary>
    /// Processes a customer inquiry with streaming response
    /// </summary>
    public async IAsyncEnumerable<string> ProcessInquiryStreamAsync(
        string customerMessage,
        string? customerId = null,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        // Classify the inquiry
        var category = await ClassifyInquiryAsync(customerMessage, cancellationToken);

        // Route to appropriate agent
        var agent = category switch
        {
            "order" => _agentFactory.CreateOrderAgent(),
            "product" => _agentFactory.CreateProductAgent(),
            _ => _agentFactory.CreateGeneralAgent()
        };

        // Get appropriate tools
        var tools = category switch
        {
            "order" => CustomerServiceAgentFactory.GetOrderTools(),
            "product" => CustomerServiceAgentFactory.GetProductTools(),
            _ => CustomerServiceAgentFactory.GetAllTools()
        };

        // Process with streaming
        var systemPrompt = GetSystemPrompt(category);
        var messages = new List<ChatMessage>
        {
            new ChatMessage(ChatRole.System, systemPrompt),
            new ChatMessage(ChatRole.User, customerMessage)
        };

        var options = new ChatOptions
        {
            Temperature = 0.7f,
            Tools = tools
        };

        await foreach (var update in agent.GetStreamingResponseAsync(messages, options, cancellationToken))
        {
            if (!string.IsNullOrEmpty(update.Text))
            {
                yield return update.Text;
            }
        }
    }

    private async Task<string> ClassifyInquiryAsync(string message, CancellationToken cancellationToken)
    {
        var routerAgent = _agentFactory.CreateRouterAgent();
        
        var classificationPrompt = @"You are a customer service router. Analyze the customer message and classify it into ONE category:
- 'order' for order tracking, status, refunds, or returns
- 'product' for product information, recommendations, or availability
- 'general' for anything else

Respond with ONLY the category word (order, product, or general).

Customer message: " + message;

        var messages = new List<ChatMessage>
        {
            new ChatMessage(ChatRole.User, classificationPrompt)
        };

        var options = new ChatOptions { Temperature = 0.3f };
        var response = await routerAgent.GetResponseAsync(messages, options, cancellationToken);

        var category = response.Text?.Trim().ToLowerInvariant() ?? "general";
        
        // Validate category
        if (!new[] { "order", "product", "general" }.Contains(category))
        {
            category = "general";
        }

        return category;
    }

    private static string GetSystemPrompt(string category)
    {
        return category switch
        {
            "order" => @"You are a helpful order management specialist for a cross-border e-commerce platform.
You assist customers with:
- Tracking orders and shipments
- Checking order status
- Processing refund requests
- Answering questions about delivery

Be professional, empathetic, and provide accurate information using the available tools.
Always confirm order details before making changes.",

            "product" => @"You are a knowledgeable product consultant for a cross-border e-commerce platform.
You assist customers with:
- Finding products based on their needs
- Providing detailed product information
- Making personalized recommendations
- Answering questions about availability and specifications

Be enthusiastic, helpful, and focus on understanding customer needs.
Provide relevant product suggestions based on their interests.",

            _ => @"You are a friendly and professional customer service representative for a cross-border e-commerce platform.
You assist customers with all types of inquiries including orders, products, and general questions.
Use the available tools when needed to provide accurate information.
Be helpful, polite, and efficient in resolving customer issues."
        };
    }
}
