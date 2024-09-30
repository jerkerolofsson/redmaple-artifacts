
namespace RedMaple.Artifacts.ApiService.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IArtifactsManager _artifactsManager;

        public ApiController(IArtifactsManager artifactsManager)
        {
            _artifactsManager = artifactsManager;
        }

        /// <summary>
        /// Adds a new product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/api/products")]
        public async Task<IActionResult> AddProductAsync(AddProductRequest request)
        {
            var response = await _artifactsManager.AddProductAsync(request);
            if (response.Error is not null)
            {
                return BadRequest(response.Error);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets products
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/products")]
        public async Task<IActionResult> GetProductsAsync()
        {
            var request = new GetProductsRequest(); 
            var response = await _artifactsManager.GetProductsAsync(request);
            if (response.Error is not null)
            {
                return BadRequest(response.Error);
            }
            return Ok(response);
        }


        /// <summary>
        /// Increments the version
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/increment")]
        public async Task<ArtifactVersion> IncrementVersionAsync([FromBody] IncrementVersionRequest request)
        {
            return await _artifactsManager.IncrementVersionAsync(request);
        }

        /// <summary>
        /// Increments the patch version
        /// </summary>
        /// <param name="productSlug"></param>
        /// <returns></returns>
        [HttpPost("/api/products/{productSlug}/increment-patch")]
        public async Task<ArtifactVersion> IncrementPatchAsync([FromRoute] string productSlug)
        {
            IncrementVersionRequest request = new IncrementVersionRequest { ProductSlug = productSlug, Patch = true, Major = false, Minor = false };
            return await _artifactsManager.IncrementVersionAsync(request);
        }


        /// <summary>
        /// Increments the major component of the semver version
        /// </summary>
        /// <param name="productSlug"></param>
        /// <returns></returns>
        [HttpPost("/api/products/{productSlug}/increment-major")]
        public async Task<ArtifactVersion> IncrementMajorAsync([FromRoute] string productSlug)
        {
            IncrementVersionRequest request = new IncrementVersionRequest { ProductSlug = productSlug, Patch = false, Major = true, Minor = false };
            return await _artifactsManager.IncrementVersionAsync(request);
        }
        /// <summary>
        /// Increments the minor version
        /// </summary>
        /// <param name="productSlug"></param>
        /// <returns></returns>
        [HttpPost("/api/products/{productSlug}/increment-minor")]
        public async Task<ArtifactVersion> IncrementMinorAsync([FromRoute] string productSlug)
        {
            IncrementVersionRequest request = new IncrementVersionRequest { ProductSlug = productSlug, Patch = false, Major = false, Minor = true };
            return await _artifactsManager.IncrementVersionAsync(request);
        }

        /// <summary>
        /// Gets versions from a product
        /// </summary>
        /// <param name="productslug"></param>
        /// <returns></returns>
        [HttpGet("/api/products/{productSlug}/versions")]
        public async Task<IActionResult> GetProductVersionsAsync([FromRoute] string productslug)
        {
            var request = new GetVersionsRequest { ProductSlug = productslug }; 
            var response = await _artifactsManager.GetVersionsAsync(request);
            if (response.Error is not null)
            {
                return BadRequest(response.Error);
            }
            return Ok(response);
        }


        /// <summary>
        /// Gets artifacts
        /// </summary>
        /// <param name="productslug"></param>
        /// <returns></returns>
        [HttpGet("/api/products/{productSlug}/versions/{version}/artifacts")]
        public async Task<IActionResult> GetArtifactsAsync([FromRoute] string productslug, [FromRoute] string version)
        {
            var request = new GetArtifactsRequest { ProductSlug = productslug, Version = version };
            var response = await _artifactsManager.GetArtifactsAsync(request);
            if (response.Error is not null)
            {
                return BadRequest(response.Error);
            }
            return Ok(response);

        }
    }
}
