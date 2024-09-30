using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMaple.Artifacts.Contracts
{
    public interface IVersion
    {
        public long SerializedVersion { get; }
        public string VersionString { get; }

        IVersion IncrementPatch();
        IVersion IncrementMinor();
        IVersion IncrementMajor();
    }
}
