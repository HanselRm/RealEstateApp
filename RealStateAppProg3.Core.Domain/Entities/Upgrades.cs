﻿using RealStateAppProg3.Core.Domain.Common;


namespace RealStateAppProg3.Core.Domain.Entities
{
    public class Upgrades : CommonNameDescription
    {
        public List<UpgradesProperty>? Properties { get; set; }

    }
}
