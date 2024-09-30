using System.ComponentModel.DataAnnotations.Schema;
using RedMaple.Artifacts.Contracts;

namespace RedMaple.Artifacts.ApiService.Models
{
    [Table("versions")]
    public class ArtifactVersionDbo
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("product_id")]
        public long ProductId { get; set; }

        [Column("version")]
        public required string VersionString { get; set; }

        /// <summary>
        /// The version converted to a long
        /// </summary>
        [Column("serialized")]
        public required long SerializedVersion { get; set; }

        [Column("flags")]
        public VersionFlags Flags { get; set; }
    }
}
