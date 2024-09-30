using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedMaple.Artifacts.Contracts.Models;

namespace RedMaple.Artifacts.Contracts.Responses
{
    public class AddProductResponse : BaseResponse
    {
        public Product? Product { get; set; }
    }
}
