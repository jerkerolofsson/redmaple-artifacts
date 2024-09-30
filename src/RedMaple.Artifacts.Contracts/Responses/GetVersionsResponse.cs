using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedMaple.Artifacts.Contracts.Models;

namespace RedMaple.Artifacts.Contracts.Responses
{
    public class GetVersionsResponse : BaseResponse
    {
        public long TotalCount { get; set; }
        public List<ArtifactVersion> Versions { get; set; } = new();
    }
}
