using CrossBorderERP.Agents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Configure Customer Service Orchestrator
var githubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
if (string.IsNullOrWhiteSpace(githubToken))
{
    Console.WriteLine("WARNING: GITHUB_TOKEN environment variable is not set!");
    Console.WriteLine("The API will not work without a valid GitHub token.");
}

// Register services
builder.Services.AddSingleton(sp => 
    new CustomerServiceAgentFactory(githubToken ?? "", "gpt-4o-mini"));
builder.Services.AddSingleton<CustomerServiceOrchestrator>();

// Add CORS for development
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Display startup information
Console.WriteLine("===================================");
Console.WriteLine("Cross-Border ERP Customer Service API");
Console.WriteLine("===================================");
Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"GitHub Token: {(string.IsNullOrWhiteSpace(githubToken) ? "NOT SET" : "SET")}");
Console.WriteLine("API Endpoints:");
Console.WriteLine("  POST /api/customerservice/inquiry");
Console.WriteLine("  POST /api/customerservice/inquiry/stream");
Console.WriteLine("  GET /health");
Console.WriteLine("===================================");

app.Run();
