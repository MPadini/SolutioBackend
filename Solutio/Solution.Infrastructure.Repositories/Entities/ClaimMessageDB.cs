﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ClaimMessageDB : BaseEntityDB
    {
        public long ClaimId { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }

        public bool Viewed { get; set; }

    }
}
