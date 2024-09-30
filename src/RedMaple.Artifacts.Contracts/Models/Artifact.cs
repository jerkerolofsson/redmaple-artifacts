using System.ComponentModel.DataAnnotations.Schema;
using RedMaple.Artifacts.Contracts;

namespace RedMaple.Artifacts.Contracts.Models
{
    public class Artifact
    {
        /// <summary>
        /// Name of the Artifact
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Platform (e.g. linux-arm64, windows-x64)
        /// </summary>
        public required string Platform { get; set; }

        /// <summary>
        /// User defined labels. ; separated
        /// </summary>
        public string? Labels { get; set; } = "";

        /// <summary>
        /// Type of the Artifact
        /// </summary>
        public required ArtifactType Type { get; set; }

        /// <summary>
        /// URL where the artifact can be located
        /// </summary>
        public string? Url { get; set; }

    }
}
