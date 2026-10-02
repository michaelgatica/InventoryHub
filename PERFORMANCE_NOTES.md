# Performance Notes

## Client-Side Optimization

The Blazor client uses a `ProductApiClient` service that caches product data for a short period of time. This reduces unnecessary calls to the back-end API when the user revisits the product page.

## Server-Side Optimization

The Minimal API uses `IMemoryCache` to cache the standardized product response. This avoids rebuilding the same response repeatedly under normal conditions.

## Rendering Optimization

The product list is displayed using a clean component structure and `@key` is used when rendering products. This helps Blazor track list items more efficiently.

## Error Handling and Performance

The client avoids repeated failed operations by catching request and JSON exceptions and displaying an error message. This improves reliability and prevents the UI from repeatedly attempting to render missing or invalid data.

## Future Improvements

For a production version, the next optimization steps would include:

- Pagination for large product lists.
- Server-side filtering and sorting.
- Response compression.
- Database indexing.
- More advanced cache invalidation.
