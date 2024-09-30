using System.ComponentModel.DataAnnotations.Schema;
using RedMaple.Artifacts.Contracts;

namespace RedMaple.Artifacts.ApiService.Models
{
    [Table("artifacts")]
    public class ArtifactDbo
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("product_id")]
        public long ProductId { get; set; }

        [Column("version_id")]
        public long VersionId { get; set; }

        [Column("platform_id")]
        public long PlatformId { get; set; }

        /// <summary>
        /// Name of the Artifact
        /// </summary>
        [Column("name")]
        public required string Name { get; set; }

        /// <summary>
        /// Platform
        /// </summary>
        [Column("platform")]
        public required string Platform { get; set; }

        /// <summary>
        /// Type of the Artifact
        /// </summary>
        [Column("type")]
        public required ArtifactType Type { get; set; }

        /// <summary>
        /// URL where the artifact can be located
        /// </summary>
        [Column("url")]
        public string? Url { get; set; }

        /// <summary>
        /// User-defined labels
        /// </summary>
        [Column("labels")]
        public string? Labels { get; set; }
    }
}
