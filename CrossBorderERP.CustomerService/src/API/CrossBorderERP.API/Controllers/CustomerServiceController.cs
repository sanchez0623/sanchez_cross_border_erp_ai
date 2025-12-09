using CrossBorderERP.Agents;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CrossBorderERP.API.Controllers;

/// <summary>
/// Customer service API controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomerServiceController : ControllerBase
{
    private readonly CustomerServiceOrchestrator _orchestrator;
    private readonly ILogger<CustomerServiceController> _logger;

    public CustomerServiceController(
        CustomerServiceOrchestrator orchestrator,
        ILogger<CustomerServiceController> logger)
    {
        _orchestrator = orchestrator;
        _logger = logger;
    }

    /// <summary>
    /// Process a customer inquiry
    /// </summary>
    /// <param name="request">Customer inquiry request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Customer service response</returns>
    [HttpPost("inquiry")]
    [ProducesResponseType(typeof(InquiryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<InquiryResponse>> ProcessInquiry(
        [FromBody] InquiryRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new { error = "Message cannot be empty" });
            }

            _logger.LogInformation("Processing customer inquiry from {CustomerId}", 
                request.CustomerId ?? "anonymous");

            var response = await _orchestrator.ProcessInquiryAsync(
                request.Message,
                request.CustomerId,
                cancellationToken);

            return Ok(new InquiryResponse
            {
                Message = response,
                Timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing customer inquiry");
            return StatusCode(500, new { error = "An error occurred processing your request" });
        }
    }

    /// <summary>
    /// Process a customer inquiry with streaming response
    /// </summary>
    /// <param name="request">Customer inquiry request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Streaming customer service response</returns>
    [HttpPost("inquiry/stream")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async IAsyncEnumerable<string> ProcessInquiryStream(
        [FromBody] InquiryRequest request,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            yield return "Error: Message cannot be empty";
            yield break;
        }

        _logger.LogInformation("Processing streaming customer inquiry from {CustomerId}", 
            request.CustomerId ?? "anonymous");

        await foreach (var chunk in _orchestrator.ProcessInquiryStreamAsync(
            request.Message,
            request.CustomerId,
            cancellationToken))
        {
            yield return chunk;
        }
    }

    /// <summary>
    /// Health check endpoint
    /// </summary>
    [HttpGet("/health")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult HealthCheck()
    {
        return Ok(new
        {
            status = "healthy",
            service = "CrossBorder ERP Customer Service",
            timestamp = DateTime.UtcNow
        });
    }
}

/// <summary>
/// Customer inquiry request model
/// </summary>
public class InquiryRequest
{
    /// <summary>
    /// Customer message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Optional customer ID
    /// </summary>
    public string? CustomerId { get; set; }
}

/// <summary>
/// Customer inquiry response model
/// </summary>
public class InquiryResponse
{
    /// <summary>
    /// Response message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Response timestamp
    /// </summary>
    public DateTime Timestamp { get; set; }
}
