# InventoryHub Test Plan

## 1. Repository Check

Confirm the project has been uploaded to a public GitHub repository and includes:

- `FullStackSolution.sln`
- `ClientApp/`
- `ServerApp/`
- `README.md`
- `REFLECTION.md`
- `TEST_PLAN.md`
- `JSON_STRUCTURE.md`
- `DEBUGGING_NOTES.md`
- `PERFORMANCE_NOTES.md`

## 2. Server API Test

Run the API:

```bash
cd ServerApp
dotnet run
```

Open:

```text
http://localhost:5001/api/productlist
```

Expected result:

- Status code `200 OK`
- JSON response includes `success`, `count`, `generatedAt`, and `data`.
- Each product includes `id`, `name`, `price`, `stock`, and `category`.

## 3. Legacy Route Test

Open:

```text
http://localhost:5001/api/products
```

Expected result:

- The legacy route returns the same product data.
- This confirms backward compatibility with the original activity route.

## 4. Client Integration Test

Run the Blazor client:

```bash
cd ClientApp
dotnet run
```

Navigate to:

```text
/fetchproducts
```

Expected result:

- The product list displays in the browser.
- Product name, price, stock, and category appear correctly.

## 5. CORS Test

Run the client and server on separate localhost ports.

Expected result:

- The Blazor client successfully fetches data from the server.
- No CORS error appears in the browser console.

## 6. JSON Error Handling Test

Temporarily change the client endpoint to an invalid route.

Expected result:

- The page displays a user-friendly error message.
- The app does not crash.

## 7. Performance Test

Click **Refresh Products** on the product page.

Expected result:

- Products reload successfully.
- The client cache reduces unnecessary API calls unless a forced refresh is requested.
- The server uses memory caching for the product response.
