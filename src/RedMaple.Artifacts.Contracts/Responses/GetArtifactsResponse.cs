using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedMaple.Artifacts.Contracts.Models;

namespace RedMaple.Artifacts.Contracts.Responses
{
    public class GetArtifactsResponse : BaseResponse
    {
        public long TotalCount { get; set; }
        public List<Artifact> Artifacts { get; set; } = new();
    }
}
