using RedMaple.Artifacts.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedMaple.Artifacts.ApiService.Models
{
    /// <summary>
    /// Represents an application/service etc
    /// </summary>
    [Table("products")]
    public class ProductDbo
    {
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Image/icon for the product
        /// </summary>
        [Column("icon_url")]
        public string? IconUrl { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        [Column("name")]
        public required string Name { get; set; }

        /// <summary>
        /// Slug of the product
        /// </summary>
        [Column("slug")]
        public required string Slug { get; set; }

        /// <summary>
        /// Format of the version. This is needed to increment the version properly
        /// </summary>
        [Column("version_format")]
        public required VersionFormat Format { get; set; }
    }
}
