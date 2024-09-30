using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMaple.Artifacts.Contracts.Requests
{
    public class AddProductRequest
    {
        public required string Name { get; set; }
        public string? IconUrl { get; set; }
    }
}
