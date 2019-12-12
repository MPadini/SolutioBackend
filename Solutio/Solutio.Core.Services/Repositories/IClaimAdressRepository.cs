using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.Repositories
{
    public interface IClaimAdressRepository
    {
        Task Delete(Claim claim);

        Task<Adress> Update(Claim claim, Adress adress);

        Task<Adress> Save(Claim claim, Adress adress);

        Task<long> Save(Adress adress);
    }
}
