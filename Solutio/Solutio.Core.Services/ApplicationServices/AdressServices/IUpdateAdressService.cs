using Solutio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solutio.Core.Services.ApplicationServices.AdressServices
{
    public interface IUpdateAdressService
    {
        Task<Adress> UpdateClaimAdress(Claim claim, Adress adress);
    }
}
