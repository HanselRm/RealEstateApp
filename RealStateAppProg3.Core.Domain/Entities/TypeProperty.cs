﻿
using RealStateAppProg3.Core.Domain.Common;

namespace RealStateAppProg3.Core.Domain.Entities
{
    public class TypeProperty : CommonNameDescription
    {
        public List<Property>? Properties { get; set; }

    }
}
