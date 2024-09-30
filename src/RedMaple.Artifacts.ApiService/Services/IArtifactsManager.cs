namespace RedMaple.Artifacts.ApiService.Services
{
    public interface IArtifactsManager
    {
        Task<GetProductsResponse> GetProductsAsync(GetProductsRequest request);
        Task<GetVersionsResponse> GetVersionsAsync(GetVersionsRequest request);
        Task<GetArtifactsResponse> GetArtifactsAsync(GetArtifactsRequest request);
        Task<AddProductResponse> AddProductAsync(AddProductRequest request);

        Task<ArtifactVersion> IncrementVersionAsync(IncrementVersionRequest request);
    }
}
