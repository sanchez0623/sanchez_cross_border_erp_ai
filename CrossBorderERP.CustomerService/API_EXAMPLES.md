# API Usage Examples

This document provides practical examples of using the Cross-Border ERP Customer Service API.

## Base URL

Development: `http://localhost:5000`
Production: `http://your-domain.com`

## Authentication

Currently, authentication is handled via the `GITHUB_TOKEN` environment variable on the server side. No authentication headers are required for API requests.

## Endpoints

### 1. Health Check

Check the API status.

**Request:**
```bash
curl http://localhost:5000/health
```

**Response:**
```json
{
  "status": "healthy",
  "service": "CrossBorder ERP Customer Service",
  "timestamp": "2025-12-09T13:25:24.595Z"
}
```

---

### 2. Process Customer Inquiry

Process a customer service inquiry and get a complete response.

**Request:**
```bash
curl -X POST http://localhost:5000/api/customerservice/inquiry \
  -H "Content-Type: application/json" \
  -d '{
    "message": "I want to track my order ORD-12345. Can you tell me where it is?",
    "customerId": "CUST-001"
  }'
```

**Response:**
```json
{
  "message": "I'll help you track your order ORD-12345. Let me retrieve the information for you.\n\nYour order (ID: ORD-12345) is currently **Shipped** and on its way to you. Here are the details:\n\n**Order Summary:**\n- Order Date: 2025-12-04\n- Status: Shipped\n- Total Amount: $299.99\n- Payment: Credit Card\n\n**Items in your order:**\n1. Wireless Bluetooth Headphones (1x $79.99)\n2. USB-C Fast Charger (2x $25.00)\n\n**Shipping Information:**\n- Carrier: DHL Express\n- Tracking Number: TRK123456789\n- Status: In Transit\n- Estimated Delivery: 2025-12-11\n\n**Recent Tracking Updates:**\n- 2025-12-09 - New York, NY, USA: Out for delivery\n- 2025-12-08 - Los Angeles, CA, USA: Arrived at customs\n- 2025-12-07 - Shanghai, China: Package departed from origin facility\n\nIs there anything else you'd like to know about your order?",
  "timestamp": "2025-12-09T13:25:24.595Z"
}
```

---

### 3. Product Search

Search for products.

**Request:**
```bash
curl -X POST http://localhost:5000/api/customerservice/inquiry \
  -H "Content-Type: application/json" \
  -d '{
    "message": "Can you recommend some good wireless headphones under $100?",
    "customerId": "CUST-001"
  }'
```

**Response:**
```json
{
  "message": "I'd be happy to help you find wireless headphones under $100! Let me search our catalog for you.\n\nBased on your budget, I found some great options:\n\n**1. Wireless Bluetooth Headphones - $79.99** ⭐ 4.5/5\n- Premium noise-canceling technology\n- 30-hour battery life\n- Bluetooth 5.0\n- Comfortable over-ear design\n- Available in Black, Silver, and Blue\n\nThis is our top seller and offers excellent value for the price. The active noise cancellation is particularly impressive at this price point.\n\nWould you like more details about this product, or would you like me to show you other options?",
  "timestamp": "2025-12-09T13:25:24.595Z"
}
```

---

### 4. Refund Request

Request a refund for an order.

**Request:**
```bash
curl -X POST http://localhost:5000/api/customerservice/inquiry \
  -H "Content-Type: application/json" \
  -d '{
    "message": "I need to return order ORD-12345. The product arrived damaged.",
    "customerId": "CUST-001"
  }'
```

**Response:**
```json
{
  "message": "I'm sorry to hear that your product arrived damaged. Let me help you process the return for order ORD-12345.\n\nI've initiated a refund request with the following details:\n- Reference Number: REF-A3B5C7D9\n- Expected Processing Time: 5-7 business days\n- You will receive a confirmation email shortly\n\nThe refund will be credited to your original payment method (Credit Card) once processed.\n\n**What happens next:**\n1. You'll receive an email with return shipping instructions\n2. Package the damaged item securely\n3. Use the prepaid return label we'll send you\n4. Track your return shipment\n5. Refund will be processed once we receive the item\n\nIs there anything else I can help you with?",
  "timestamp": "2025-12-09T13:25:24.595Z"
}
```

---

### 5. Streaming Response

Get a streaming response for real-time updates.

**Request:**
```bash
curl -X POST http://localhost:5000/api/customerservice/inquiry/stream \
  -H "Content-Type: application/json" \
  -d '{
    "message": "What electronics do you have in stock?",
    "customerId": "CUST-001"
  }'
```

**Response (streamed):**
```
I'll search our electronics inventory for you...

We currently have these electronics in stock:

1. **Wireless Bluetooth Headphones** - $79.99
   - Premium noise-canceling
   - 30-hour battery life
   - Rating: 4.5/5

2. **USB-C Fast Charger** - $25.00
   - 65W fast charging
   - Foldable plug design
   - Rating: 4.7/5

3. **Smartphone Case** - $15.99
   - Durable protective case
   - Built-in kickstand
   - Rating: 4.3/5

All items are in stock and ready to ship. Would you like more details about any of these products?
```

---

## Request/Response Schema

### InquiryRequest

```json
{
  "message": "string (required) - The customer's message or question",
  "customerId": "string (optional) - Customer identifier for personalization"
}
```

### InquiryResponse

```json
{
  "message": "string - The AI agent's response",
  "timestamp": "string (ISO 8601) - Response timestamp"
}
```

---

## Error Responses

### 400 Bad Request

When the request is invalid (e.g., empty message).

```json
{
  "error": "Message cannot be empty"
}
```

### 500 Internal Server Error

When an unexpected error occurs.

```json
{
  "error": "An error occurred processing your request"
}
```

---

## Testing with Different Scenarios

### Scenario 1: Order Tracking
```bash
# Track a specific order
curl -X POST http://localhost:5000/api/customerservice/inquiry \
  -H "Content-Type: application/json" \
  -d '{"message": "Where is my order ORD-12345?", "customerId": "CUST-001"}'
```

### Scenario 2: Product Recommendation
```bash
# Get personalized recommendations
curl -X POST http://localhost:5000/api/customerservice/inquiry \
  -H "Content-Type: application/json" \
  -d '{"message": "I need a gift for a tech-savvy friend", "customerId": "CUST-001"}'
```

### Scenario 3: General Inquiry
```bash
# Ask about shipping policies
curl -X POST http://localhost:5000/api/customerservice/inquiry \
  -H "Content-Type: application/json" \
  -d '{"message": "What is your shipping policy?", "customerId": "CUST-001"}'
```

### Scenario 4: Multi-turn Conversation
```bash
# First question
curl -X POST http://localhost:5000/api/customerservice/inquiry \
  -H "Content-Type: application/json" \
  -d '{"message": "Show me wireless headphones", "customerId": "CUST-001"}'

# Follow-up question
curl -X POST http://localhost:5000/api/customerservice/inquiry \
  -H "Content-Type: application/json" \
  -d '{"message": "What about the battery life of PROD-001?", "customerId": "CUST-001"}'
```

---

## Notes

- The API uses AI-powered routing to automatically direct inquiries to the appropriate specialized agent (Order, Product, or General).
- Function calling is used automatically when specific actions are needed (e.g., tracking orders, searching products).
- Responses are generated in real-time using GitHub Models API.
- All mock data in development will be replaced with actual database queries in production.
