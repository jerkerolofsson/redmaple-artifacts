using RedMaple.Artifacts.Contracts;

namespace RedMaple.Artifacts.Contracts.Models
{
    public class ArtifactVersion
    {
        public required string VersionString { get; set; }

        /// <summary>
        /// The version converted to a long
        /// </summary>
        public required long SerializedVersion { get; set; }

        public VersionFlags Flags { get; set; }
    }
}
