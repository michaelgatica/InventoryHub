using System.Net.Http.Json;
using System.Text.Json;
using ClientApp.Models;

namespace ClientApp.Services;

public class ProductApiClient
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(2);

    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductApiClient> _logger;

    private IReadOnlyList<Product>? _cachedProducts;
    private DateTimeOffset _cacheExpiresAt = DateTimeOffset.MinValue;

    public ProductApiClient(HttpClient httpClient, ILogger<ProductApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ApiResult<IReadOnlyList<Product>>> GetProductsAsync(
        bool forceRefresh = false,
        CancellationToken cancellationToken = default)
    {
        if (!forceRefresh &&
            _cachedProducts is not null &&
            DateTimeOffset.UtcNow < _cacheExpiresAt)
        {
            return ApiResult<IReadOnlyList<Product>>.Success(
                _cachedProducts,
                "Products loaded from client cache.");
        }

        try
        {
            using var response = await _httpClient.GetAsync("api/productlist", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return ApiResult<IReadOnlyList<Product>>.Failure(
                    $"The API returned {(int)response.StatusCode} {response.ReasonPhrase}.");
            }

            var payload = await response.Content.ReadFromJsonAsync<ProductListResponse>(
                cancellationToken: cancellationToken);

            if (payload?.Data is null)
            {
                return ApiResult<IReadOnlyList<Product>>.Failure(
                    "The API response was empty or malformed.");
            }

            _cachedProducts = payload.Data;
            _cacheExpiresAt = DateTimeOffset.UtcNow.Add(CacheDuration);

            return ApiResult<IReadOnlyList<Product>>.Success(payload.Data);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "The product JSON response could not be deserialized.");
            return ApiResult<IReadOnlyList<Product>>.Failure(
                "The product data could not be read because the JSON response was malformed.");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "The product API request failed.");
            return ApiResult<IReadOnlyList<Product>>.Failure(
                "The product API could not be reached. Confirm the server is running.");
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "The product API request timed out or was canceled.");
            return ApiResult<IReadOnlyList<Product>>.Failure(
                "The product API request timed out.");
        }
    }
}
