using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMaple.Artifacts.Contracts.Requests
{
    public class IncrementVersionRequest
    {
        public required string ProductSlug { get; set; }

        /// <summary>
        /// Increment the major number
        /// </summary>
        public bool Major { get; set; } = false;

        /// <summary>
        /// Increment the minor number
        /// </summary>
        public bool Minor { get; set; } = false;

        /// <summary>
        /// Increment the patch version
        /// </summary>
        public bool Patch { get; set; } = true;
    }
}
