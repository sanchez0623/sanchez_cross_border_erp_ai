using CrossBorderERP.Agents;

namespace CrossBorderERP.API;

/// <summary>
/// Test program to verify GitHub Models integration
/// </summary>
public class TestGitHubModels
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Testing GitHub Models Integration");
        Console.WriteLine("=====================================\n");

        // Check for GitHub token
        var githubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
        if (string.IsNullOrWhiteSpace(githubToken))
        {
            Console.WriteLine("ERROR: GITHUB_TOKEN environment variable is not set!");
            Console.WriteLine("Please set your GitHub Personal Access Token:");
            Console.WriteLine("  export GITHUB_TOKEN=github_pat_xxxxxxxxxx");
            return;
        }

        Console.WriteLine("✓ GitHub token found\n");

        try
        {
            // Create the orchestrator
            var factory = new CustomerServiceAgentFactory(githubToken, "gpt-4o-mini");
            var orchestrator = new CustomerServiceOrchestrator(factory);

            // Test 1: Order inquiry
            Console.WriteLine("Test 1: Order Inquiry");
            Console.WriteLine("----------------------");
            var orderResponse = await orchestrator.ProcessInquiryAsync(
                "I want to track my order ORD-12345. Can you help me?",
                "CUST-001");
            Console.WriteLine($"Response: {orderResponse}\n");

            // Test 2: Product inquiry
            Console.WriteLine("Test 2: Product Inquiry");
            Console.WriteLine("------------------------");
            var productResponse = await orchestrator.ProcessInquiryAsync(
                "Can you recommend some good wireless headphones?",
                "CUST-001");
            Console.WriteLine($"Response: {productResponse}\n");

            // Test 3: Streaming response
            Console.WriteLine("Test 3: Streaming Response");
            Console.WriteLine("---------------------------");
            Console.Write("Response: ");
            await foreach (var chunk in orchestrator.ProcessInquiryStreamAsync(
                "What products do you have in stock?",
                "CUST-001"))
            {
                Console.Write(chunk);
            }
            Console.WriteLine("\n");

            Console.WriteLine("=====================================");
            Console.WriteLine("All tests completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }
}
