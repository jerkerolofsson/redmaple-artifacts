
namespace RedMaple.Artifacts.ApiService.Services
{
    public class ArtifactsManager : IArtifactsManager
    {
        private readonly ArtifactsDbContext _dbContext;
        private readonly static Slugify.SlugHelper _helper = new Slugify.SlugHelper();
        public ArtifactsManager(ArtifactsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetArtifactsResponse> GetArtifactsAsync(GetArtifactsRequest request)
        {
            var response = new GetArtifactsResponse();

            var product = await GetProductBySlugAsync(request.ProductSlug);
            if (product is { })
            {
                var version = await _dbContext.Versions.Where(x => x.VersionString == request.Version && x.ProductId == product.Id).FirstOrDefaultAsync();
                if (version is { })
                {
                    var query = _dbContext.Artifacts
                    .OrderBy(x => x.VersionId)
                    .Skip(request.Offset)
                    .Take(request.Count);

                    await foreach (var artifact in query.AsAsyncEnumerable())
                    {
                        response.Artifacts.Add(artifact.ToDto());
                    }
                    response.TotalCount = await query.LongCountAsync();
                }
            }
            else
            {
                response.Error = "Product not found: " + request.ProductSlug;
            }

            return response;
        }

        public async Task<GetProductsResponse> GetProductsAsync(GetProductsRequest request)
        {
            var response = new GetProductsResponse();

            await foreach (var product in _dbContext.Products
            .OrderBy(x => x.Name)
            .Skip(request.Offset)
            .Take(request.Count)
            .AsAsyncEnumerable())
            {
                response.Products.Add(product.ToDto());
            }
            response.TotalCount = await _dbContext.Products.LongCountAsync();

            return response;
        }
        public async Task<GetVersionsResponse> GetVersionsAsync(GetVersionsRequest request)
        {
            var response = new GetVersionsResponse();

            var product = await GetProductBySlugAsync(request.ProductSlug);
            if (product is { })
            {
                var query = _dbContext.Versions.AsNoTracking()
                    .Where(x => x.ProductId == product.Id);

                response.TotalCount = await query.LongCountAsync();
                await foreach (var version in 
                    query
                    .OrderByDescending(x=>x.SerializedVersion)
                    .Skip(request.Offset).Take(request.Count)
                    .AsAsyncEnumerable())
                {
                    response.Versions.Add(version.ToDto());
                }
            }
            else
            {
                response.Error = "Product not found: " + request.ProductSlug;
            }

            return response;
        }


        public async Task<AddProductResponse> AddProductAsync(AddProductRequest request)
        {
            var slug = _helper.GenerateSlug(request.Name);

            if (await _dbContext.Products.Where(x=>x.Name == request.Name || x.Slug == slug).AnyAsync())
            {
                return new AddProductResponse
                {
                    Error = "Name already exists"
                };
            }

            var product = new ProductDbo 
            { 
                Format = VersionFormat.Semantic, 
                Name = request.Name, 
                Slug = slug,
                IconUrl = request.IconUrl 
            };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return new AddProductResponse
            {
                Product = product.ToDto()
            };
        }

        public async Task<ProductDbo?> GetProductByIdAsync(long id)
        {
            return await _dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
        }


        public async Task<ProductDbo?> GetProductBySlugAsync(string slug)
        {
            return await _dbContext.Products.Where(x => x.Slug == slug).FirstOrDefaultAsync();
        }
        public async Task<ProductDbo?> GetProductByNameAsync(string name)
        {
            return await _dbContext.Products.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
        public async Task<ProductDbo> GetOrCreateProductBySlugAsync(string slug)
        {
            var product = await GetProductBySlugAsync(slug);
            if (product is null)
            {
                product = new ProductDbo { Slug = slug, Format = VersionFormat.Semantic, Name = slug };
                await _dbContext.AddAsync(product);
                await _dbContext.SaveChangesAsync();
            }
            return product;
        }

        public async Task<ArtifactVersionDbo?> GetLatestArtifactVersionForProductAsync(long productId)
        {
            return await _dbContext.Versions
                .OrderByDescending(x=>x.SerializedVersion)
                .Where(x => x.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task<ArtifactVersionDbo?> GetLatestVersionAsync(string productSlug)
        {
            var product = await GetProductBySlugAsync(productSlug);
            if (product is not null)
            {
                return await GetLatestArtifactVersionForProductAsync(product.Id);
            }
            return null;
        }

        public async Task<ArtifactVersion> IncrementVersionAsync(IncrementVersionRequest request)
        {
            var product = await GetOrCreateProductBySlugAsync(request.ProductSlug);
            var latestVersion = await GetLatestArtifactVersionForProductAsync(product.Id);

            IVersion version = CreateVersion(product.Format, latestVersion);
            if (request.Major)
            {
                version = version.IncrementMajor();
            }
            if (request.Minor)
            {
                version = version.IncrementMinor();
            }
            if (request.Patch)
            {
                version = version.IncrementPatch();
            }

            // Save the version
            var dbo = new ArtifactVersionDbo
            {
                SerializedVersion = version.SerializedVersion,
                VersionString = version.VersionString,
                ProductId = product.Id,
            };
            await _dbContext.Versions.AddAsync(dbo);
            await _dbContext.SaveChangesAsync();
            return dbo.ToDto(); 
        }

        private IVersion CreateVersion(VersionFormat format, ArtifactVersionDbo? version)
        {
            if(version is null)
            {
                // Create a default version
                switch (format)
                {
                    case VersionFormat.Semantic:
                        return SemverVersion.Parse("0.0.0");
                    default:
                        throw new NotSupportedException("Version Format is not supported: " + format);
                }
            }
            else
            {
                switch (format)
                {
                    case VersionFormat.Semantic:
                        return SemverVersion.Parse(version.VersionString);
                    default:
                        throw new NotSupportedException("Version Format is not supported: " + format);
                }
            }
        }
    }
}
