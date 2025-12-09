# Cross-Border ERP AI Customer Service

An intelligent multi-agent customer service system for cross-border e-commerce, powered by .NET 10.0, Microsoft.Extensions.AI, and GitHub Models.

## 🚀 Quick Start

```bash
cd CrossBorderERP.CustomerService
export GITHUB_TOKEN=your_github_token_here
dotnet run --project src/API/CrossBorderERP.API
```

Visit: `http://localhost:5000/health`

## 📖 Documentation

See the [complete documentation](./CrossBorderERP.CustomerService/README.md) in the project directory.

## 🏗️ Project Structure

- **CrossBorderERP.CustomerService/** - Main application
  - **src/Core/** - Domain models
  - **src/Infrastructure/** - GitHub Models client
  - **src/Agents/** - AI agents and tools
  - **src/API/** - REST API
  - **tests/** - Unit tests

## 🌟 Features

- **Multi-Agent System**: Router, Order, Product, and General agents
- **Function Calling**: Order tracking, product search, recommendations
- **GitHub Models**: Uses latest `models.github.ai` endpoint
- **Streaming Support**: Real-time response streaming
- **RESTful API**: Modern ASP.NET Core Web API

## 📦 Technology Stack

- .NET 10.0 LTS
- Microsoft.Extensions.AI (v10.0.1)
- OpenAI SDK (v2.7.0)
- ASP.NET Core
- GitHub Models API

## 📄 License

MIT License
