﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMaple.Artifacts.Contracts.Requests
{
    public class GetProductsRequest
    {
        public int Offset { get; set; } = 0;
        public int Count { get; set; } = 100;
    }
}
