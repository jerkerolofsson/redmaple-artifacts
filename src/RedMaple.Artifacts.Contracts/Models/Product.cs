using RedMaple.Artifacts.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedMaple.Artifacts.Contracts.Models
{
    /// <summary>
    /// Represents an application/service etc
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Image/icon for the product
        /// </summary>
        public string? IconUrl { get; set; }

        /// <summary>
        /// Product slug
        /// </summary>
        public required string Slug { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Format of the version. This is needed to increment the version properly
        /// </summary>
        public required VersionFormat Format { get; set; }
    }
}
