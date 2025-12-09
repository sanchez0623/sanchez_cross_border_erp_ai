# Cross-Border ERP AI Customer Service System

A multi-agent AI customer service system for cross-border e-commerce, built with .NET 10.0, Microsoft.Extensions.AI, and GitHub Models.

## 🌟 Features

### Multi-Agent Architecture
- **RouterAgent** - Intelligently routes customer inquiries to specialized agents
- **OrderAgent** - Handles order tracking, status updates, and refund requests
- **ProductAgent** - Provides product recommendations and catalog queries
- **GeneralAgent** - Handles general inquiries with access to all tools

### AI-Powered Function Calling
- `GetOrderInfo` - Retrieve order details and status
- `TrackShipment` - Real-time shipping and logistics tracking
- `RequestRefund` - Process refund and return requests
- `SearchProducts` - Find products by keyword or category
- `GetProductDetails` - Detailed product information
- `GetRecommendations` - Personalized product suggestions

### RESTful API
- `POST /api/customerservice/inquiry` - Process customer inquiries
- `POST /api/customerservice/inquiry/stream` - Streaming responses
- `GET /health` - Health check endpoint

## 🏗️ Architecture

```
CrossBorderERP.CustomerService/
├── src/
│   ├── Core/                           # Business domain models
│   │   └── Models/
│   │       ├── OrderInfo.cs
│   │       ├── OrderItem.cs
│   │       └── ShippingInfo.cs
│   ├── Infrastructure/                 # External service integrations
│   │   └── GitHubModelsChatClient.cs   # GitHub Models client factory
│   ├── Agents/                         # AI agent logic
│   │   ├── Tools/
│   │   │   ├── OrderTools.cs           # Order management functions
│   │   │   └── ProductTools.cs         # Product catalog functions
│   │   ├── CustomerServiceAgentFactory.cs
│   │   └── CustomerServiceOrchestrator.cs
│   └── API/                            # Web API layer
│       ├── Controllers/
│       │   └── CustomerServiceController.cs
│       ├── Program.cs
│       ├── appsettings.json
│       └── TestGitHubModels.cs
├── tests/
│   └── CrossBorderERP.Agents.UnitTests/
├── Dockerfile
├── .env.example
└── README.md
```

## 🚀 Getting Started

### Prerequisites

- **.NET 10.0 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **GitHub Personal Access Token** - [Create Token](https://github.com/settings/tokens)
  - Required scope: `read:org` (for GitHub Models access)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/sanchez0623/sanchez_cross_border_erp_ai.git
cd sanchez_cross_border_erp_ai/CrossBorderERP.CustomerService
```

2. Set up environment variables:
```bash
cp .env.example .env
# Edit .env and add your GITHUB_TOKEN
export GITHUB_TOKEN=github_pat_xxxxxxxxxx
```

3. Restore dependencies:
```bash
dotnet restore
```

4. Build the solution:
```bash
dotnet build
```

### Running the Application

#### Option 1: Run the API Server
```bash
cd src/API/CrossBorderERP.API
dotnet run
```

The API will be available at `http://localhost:5000`

#### Option 2: Run Tests
```bash
# Build and run the test program
cd src/API/CrossBorderERP.API
dotnet run --project CrossBorderERP.API.csproj -- --test
```

#### Option 3: Docker
```bash
# Build the Docker image
docker build -t crossborder-erp-api .

# Run the container
docker run -p 8080:8080 -e GITHUB_TOKEN=your_token_here crossborder-erp-api
```

## 📝 API Usage Examples

### Process Customer Inquiry

```bash
curl -X POST http://localhost:5000/api/customerservice/inquiry \
  -H "Content-Type: application/json" \
  -d '{
    "message": "I want to track my order ORD-12345",
    "customerId": "CUST-001"
  }'
```

Response:
```json
{
  "message": "I can help you track your order ORD-12345. Let me retrieve the information for you...",
  "timestamp": "2025-12-09T13:25:24.595Z"
}
```

### Streaming Response

```bash
curl -X POST http://localhost:5000/api/customerservice/inquiry/stream \
  -H "Content-Type: application/json" \
  -d '{
    "message": "Can you recommend some wireless headphones?",
    "customerId": "CUST-001"
  }'
```

### Health Check

```bash
curl http://localhost:5000/health
```

Response:
```json
{
  "status": "healthy",
  "service": "CrossBorder ERP Customer Service",
  "timestamp": "2025-12-09T13:25:24.595Z"
}
```

## 🔧 Configuration

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Environment Variables

| Variable | Description | Required | Default |
|----------|-------------|----------|---------|
| `GITHUB_TOKEN` | GitHub Personal Access Token | Yes | - |
| `MODEL_ID` | AI model to use | No | `gpt-4o-mini` |
| `ASPNETCORE_ENVIRONMENT` | Environment name | No | `Production` |
| `ASPNETCORE_URLS` | Server URLs | No | `http://+:8080` |

## 🧪 Testing

Run unit tests:
```bash
dotnet test
```

Run integration tests with GitHub Models:
```bash
cd src/API/CrossBorderERP.API
GITHUB_TOKEN=your_token dotnet run
```

## 📚 Technology Stack

- **.NET 10.0 LTS** - Latest long-term support version
- **Microsoft.Extensions.AI** - AI abstraction layer (v10.0.1)
- **OpenAI SDK** - OpenAI API client (v2.7.0)
- **ASP.NET Core** - Web API framework
- **GitHub Models API** - AI model hosting (`https://models.github.ai/inference`)

## 🔑 Available Models

GitHub Models supports various AI models:

- **OpenAI**: `gpt-4o`, `gpt-4o-mini`, `gpt-4`, `gpt-3.5-turbo`
- **Meta**: `meta-llama-3.1-405b-instruct`, `meta-llama-3.1-70b-instruct`
- **Mistral**: `mistral-large-2407`, `mistral-small`
- **Cohere**: `command-r-plus`, `command-r`
- **DeepSeek**: `deepseek-coder`

## 🐛 Troubleshooting

### Issue: "GITHUB_TOKEN environment variable is not set"

**Solution**: Set your GitHub Personal Access Token:
```bash
export GITHUB_TOKEN=github_pat_xxxxxxxxxx
```

### Issue: API returns 401 Unauthorized

**Solution**: Verify your GitHub token has the correct scopes and is still valid.

### Issue: Build errors with Microsoft.Extensions.AI

**Solution**: Ensure you're using .NET 10.0 SDK and restore packages:
```bash
dotnet --version  # Should show 10.0.x
dotnet restore
dotnet build
```

## 📖 References

- [Microsoft.Extensions.AI Documentation](https://learn.microsoft.com/dotnet/ai)
- [GitHub Models API](https://docs.github.com/en/github-models)
- [GitHub Models Endpoint Migration](https://github.blog/changelog/2025-07-17-deprecation-of-azure-endpoint-for-github-models/)
- [OpenAI .NET SDK](https://github.com/openai/openai-dotnet)

## 📄 License

This project is licensed under the MIT License.

## 👥 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📧 Contact

For questions or support, please open an issue on GitHub.
