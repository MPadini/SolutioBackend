using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solutio.ApiServices.Api.Dtos
{
    public class ClaimDto : BaseEntityDto
    {
        public string Story { get; set; }

        public DateTime Date { get; set; }

        public string Hour { get; set; }

        public long StateId { get; set; }

        public bool StateAlarmActive { get; set; }

        public string SinisterNumber { get; set; }

        public string Outro { get; set; }

        public ClaimStateDto State { get; set; }

        public List<PersonDto> ClaimInsuredPersons { get; set; }

        public List<VehicleDto> ClaimInsuredVehicles { get; set; }

        public List<PersonDto> ClaimThirdInsuredPersons { get; set; }

        public List<VehicleDto> ClaimThirdInsuredVehicles { get; set; }

        public List<ClaimMessageDto> ClaimMessages { get; set; }

        public decimal? TotalBudgetAmount { get; set; }

        public AdressDto ClaimAdress { get; set; }

        public List<ClaimSimpleFileDto> Files { get; set; }
    }

    public class ClaimSimpleFileDto
    {
        public long Id { get; set; }

        public string FileExtension { get; set; }

        public string FileName { get; set; }

        public FileTypeDto FileType { get; set; }
    }
}
