﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class ProvinceDto : BaseEntityDto
    {
        public string Name { get; set; }

        public long CountryId { get; set; }
    }
}
