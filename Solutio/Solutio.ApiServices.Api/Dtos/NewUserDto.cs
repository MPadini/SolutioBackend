using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class NewUserDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string RoleName { get; set; }

        public string Matricula { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public List<long> Offices { get; set; }

        public long? Country { get; set; }

        public long? Province   { get; set; }

        public long? City { get; set; }
    }
}
