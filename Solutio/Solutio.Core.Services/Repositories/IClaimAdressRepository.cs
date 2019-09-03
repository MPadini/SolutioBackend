using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories
{
    public interface IClaimAdressRepository
    {
        Task DeleteClaimAdress(Claim claim);

        Task<Adress> UpdateClaimAdress(Claim claim, Adress adress);
    }
}
