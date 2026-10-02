# Debugging Notes

## Issue 1: Incorrect API Route

The original integration expected the front end to call `/api/products`, but the updated back-end route is `/api/productlist`.

## Fix

The front-end service now calls:

```csharp
api/productlist
```

The server also includes a compatibility endpoint at `/api/products`.

## Issue 2: CORS Restrictions

Because the Blazor WebAssembly client and Minimal API server run on different localhost ports, browser security rules can block requests unless CORS is configured.

## Fix

The server registers and applies a CORS policy:

```csharp
builder.Services.AddCors(...);
app.UseCors("ClientAppPolicy");
```

## Issue 3: Malformed JSON or API Failure

If the API returns unexpected JSON or becomes unavailable, the front end should not crash.

## Fix

The `ProductApiClient` uses a `try/catch` block and returns an `ApiResult` object that contains either product data or a clear error message.

## Issue 4: User Feedback

The first version of the product page only displayed loading text.

## Fix

The final version shows loading, success, empty, and error states so users understand what is happening.
