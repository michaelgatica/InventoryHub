# JSON Structure

The back-end API returns a standardized JSON response from `/api/productlist`.

## Example Response

```json
{
  "success": true,
  "count": 2,
  "generatedAt": "2026-10-02T12:00:00Z",
  "data": [
    {
      "id": 1,
      "name": "Laptop",
      "price": 1200.50,
      "stock": 25,
      "category": {
        "id": 101,
        "name": "Electronics"
      }
    },
    {
      "id": 2,
      "name": "Headphones",
      "price": 50.00,
      "stock": 100,
      "category": {
        "id": 102,
        "name": "Accessories"
      }
    }
  ]
}
```

## Required Fields

Each product contains:

- `id`
- `name`
- `price`
- `stock`
- `category`

The `category` field is a nested object containing:

- `id`
- `name`

## Why This Structure Was Used

This structure keeps the API response predictable and easy for the Blazor front end to consume. Wrapping the product list in a response object allows the API to include helpful metadata such as success status, record count, and generated timestamp. The nested category object supports richer product details without requiring a separate API call for category information.
