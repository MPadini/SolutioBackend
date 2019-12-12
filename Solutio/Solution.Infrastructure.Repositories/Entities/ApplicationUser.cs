using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Infrastructure.Repositories.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser() {
            IsEnabled = false;
        }

        public bool IsEnabled { get; set; }

        public string Matricula { get; set; }

        public long? AdressId { get; set; }
    }
}
