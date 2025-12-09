using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace CrossBorderERP.Infrastructure;

/// <summary>
/// Factory for creating GitHub Models chat clients
/// </summary>
public static class GitHubModelsChatClient
{
    /// <summary>
    /// Creates an IChatClient configured for GitHub Models API
    /// </summary>
    /// <param name="githubToken">GitHub Personal Access Token</param>
    /// <param name="modelId">Model ID (default: gpt-4o-mini)</param>
    /// <returns>Configured IChatClient</returns>
    public static IChatClient Create(string githubToken, string modelId = "gpt-4o-mini")
    {
        if (string.IsNullOrWhiteSpace(githubToken))
        {
            throw new ArgumentException("GitHub token cannot be empty", nameof(githubToken));
        }

        // GitHub Models uses the new endpoint https://models.github.ai/inference
        var openAIClient = new OpenAIClient(
            new ApiKeyCredential(githubToken),
            new OpenAIClientOptions
            {
                Endpoint = new Uri("https://models.github.ai/inference")
            });

        var chatClient = openAIClient.GetChatClient(modelId);
        return chatClient.AsIChatClient();
    }

    /// <summary>
    /// Creates an IChatClient from environment variable GITHUB_TOKEN
    /// </summary>
    /// <param name="modelId">Model ID (default: gpt-4o-mini)</param>
    /// <returns>Configured IChatClient</returns>
    public static IChatClient CreateFromEnvironment(string modelId = "gpt-4o-mini")
    {
        var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidOperationException(
                "GITHUB_TOKEN environment variable is not set. " +
                "Please set it with your GitHub Personal Access Token.");
        }

        return Create(token, modelId);
    }
}
