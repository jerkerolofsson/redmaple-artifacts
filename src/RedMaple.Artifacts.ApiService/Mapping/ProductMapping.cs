using RedMaple.Artifacts.Contracts.Models;

namespace RedMaple.Artifacts.ApiService.Mapping
{
    public static class ProductMapping
    {
        public static Artifact ToDto(this ArtifactDbo dbo)
        {
            return new Artifact
            {
                Labels = dbo.Labels,
                Platform = dbo.Platform,
                Name = dbo.Name,
                Type = dbo.Type,
                Url = dbo.Url,
            };
        }
        public static ArtifactVersion ToDto(this ArtifactVersionDbo dbo)
        {
            return new ArtifactVersion
            {
                SerializedVersion = dbo.SerializedVersion,
                VersionString = dbo.VersionString,
                Flags = dbo.Flags,
            };
        }
       

        public static Product ToDto(this ProductDbo dbo)
        {
            return new Product
            {
                Format = dbo.Format,
                Name = dbo.Name,
                Slug = dbo.Slug,
                IconUrl = dbo.IconUrl,
            };
        }
    }
}
