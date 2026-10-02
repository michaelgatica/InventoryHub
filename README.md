# InventoryHub

InventoryHub is a full-stack practice project that integrates a Blazor WebAssembly front end with an ASP.NET Core Minimal API back end. The application retrieves product data from the API, deserializes standardized JSON responses, handles integration errors, and displays product inventory details in the browser.

## Project Structure

```text
InventoryHub/
├── FullStackSolution.sln
├── ClientApp/
│   ├── ClientApp.csproj
│   ├── Program.cs
│   ├── App.razor
│   ├── Models/
│   ├── Services/
│   ├── Pages/
│   └── wwwroot/
├── ServerApp/
│   ├── ServerApp.csproj
│   ├── Program.cs
│   └── Properties/
├── REFLECTION.md
├── TEST_PLAN.md
├── JSON_STRUCTURE.md
├── DEBUGGING_NOTES.md
└── PERFORMANCE_NOTES.md
```

## Features

- Blazor WebAssembly front-end application.
- ASP.NET Core Minimal API back-end application.
- Product API endpoint at `/api/productlist`.
- Legacy compatibility route at `/api/products`.
- CORS configuration for front-end/back-end communication.
- JSON response containing product details and nested category objects.
- Error handling for API failures and malformed JSON.
- Client-side caching to reduce redundant API calls.
- Backend memory caching to reduce repeated product list construction.
- Reflection summary explaining how Copilot/AI assisted each step.

## How to Run

Open two terminal windows.

### Terminal 1: Run the Server API

```bash
cd ServerApp
dotnet restore
dotnet run
```

The server launch profile uses:

```text
http://localhost:5001
```

Test the API:

```text
http://localhost:5001/api/productlist
```

### Terminal 2: Run the Blazor Client

```bash
cd ClientApp
dotnet restore
dotnet run
```

Open the client URL shown in the terminal, then navigate to:

```text
/fetchproducts
```

## Expected Output

The product list page should display products such as:

- Laptop
- Headphones
- Wireless Mouse
- Mechanical Keyboard
- USB-C Dock

Each product includes an ID, name, price, stock quantity, and nested category data.
