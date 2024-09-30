using RedMaple.Artifacts.Contracts.Models;
using RedMaple.Artifacts.Contracts.Requests;
using RedMaple.Artifacts.Contracts.Responses;
using System.Threading;

namespace RedMaple.Artifacts.Web;

public class ArtifactsApiClient(HttpClient httpClient)
{
    public async Task<ArtifactVersion?> IncrementMajorVersionAsync(string productSlug, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsync($"/api/products/{productSlug}/increment-major", new StringContent(""), cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ArtifactVersion>();
    }
    public async Task<ArtifactVersion?> IncrementMinorVersionAsync(string productSlug, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsync($"/api/products/{productSlug}/increment-minor", new StringContent(""), cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ArtifactVersion>();
    }
    public async Task<ArtifactVersion?> IncrementPatchVersionAsync(string productSlug, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsync($"/api/products/{productSlug}/increment-patch", new StringContent(""), cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ArtifactVersion>();
    }

    public async Task<GetProductsResponse> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<GetProductsResponse>("/api/products", cancellationToken) ?? new GetProductsResponse() { Error = "No response" };
    }
    public async Task<GetVersionsResponse> GetVersionsAsync(string productSlug, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<GetVersionsResponse>($"/api/products/{productSlug}/versions", cancellationToken) ?? new GetVersionsResponse() { Error = "No response" };
    }
    public async Task<AddProductResponse> AddProductAsync(AddProductRequest request, CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.PostAsJsonAsync("/api/products" , request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AddProductResponse>(cancellationToken) ?? new AddProductResponse { Error = "No response" };
    }
}
