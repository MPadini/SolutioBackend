using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.FileService;
using Solutio.Core.Services.ServicesProviders.ClaimsStatesServices;
using Solutio.Core.Services.ServicesProviders.ClaimsStatesServices.ClaimStatesValidators;

namespace Solutio.Core.Services.Factories
{
    public class ClaimStateFactory : IClaimStateFactory
    {
        private readonly IGetFileService getFileService;
        private readonly IDeleteFileService deleteFileService;

        public ClaimStateFactory(IGetFileService getFileService, IDeleteFileService deleteFileService) {
            this.getFileService = getFileService;
            this.deleteFileService = deleteFileService;
        }

        public async Task<IClaimStateValidator> GetStateValidator(long claimStateId)
        {
            if (claimStateId == (long)ClaimState.eId.Borrador)
            {
                return new Borrador();
            }

            if (claimStateId == (long)ClaimState.eId.En_Revision) {
                return new EnRevision(getFileService,deleteFileService);
            }

            if (claimStateId == (long)ClaimState.eId.Rechazado_Mejores_Datos_ui) {
                return new RechazadoMejoresDatosUi();
            }

            if (claimStateId == (long)ClaimState.eId.Esperando_Denuncia) {
                return new EsperandoDenuncia();
            }

            if (claimStateId == (long)ClaimState.eId.Pendiente_de_Presentación) {
                return new PendienteDePresentación();
            }

            if (claimStateId == (long)ClaimState.eId.Presentado) {
                return new Presentado();
            }

            if (claimStateId == (long)ClaimState.eId.Rechazado_Mejores_Datos_com) {
                return new RechazadoMejoresDatosCom();
            }

            if (claimStateId == (long)ClaimState.eId.Nuevo_Ofrecimiento) {
                return new NuevoOfrecimiento();
            }


            if (claimStateId == (long)ClaimState.eId.Ofrecimiento_Rechazado) {
                return new OfrecimientoRechazado();
            }

            if (claimStateId == (long)ClaimState.eId.Esperando_Ofrecimiento) {
                return new EsperandoOfrecimiento();
            }

            if (claimStateId == (long)ClaimState.eId.Ofrecimiento_Aceptado) {
                return new OfrecimientoAceptado();
            }

            if (claimStateId == (long)ClaimState.eId.Firmar_Convenio) {
                return new FirmarConvenio();
            }

            if (claimStateId == (long)ClaimState.eId.Convenio_Firmado) {
                return new ConvenioFirmado();
            }

            if (claimStateId == (long)ClaimState.eId.Pendiente_de_Pago) {
                return new PendienteDePago();
            }

            if (claimStateId == (long)ClaimState.eId.Rechazado) {
                return new Rechazado();
            }

            if (claimStateId == (long)ClaimState.eId.Desestimado) {
                return new Desestimado();
            }

            if (claimStateId == (long)ClaimState.eId.Desistido) {
                return new Desistido();
            }

            if (claimStateId == (long)ClaimState.eId.A_Juicio) {
                return new AJuicio();
            }

            if (claimStateId == (long)ClaimState.eId.Cerrado) {
                return new Cerrado();
            }

            throw new ApplicationException("State manager not configured");
        }
    }
}
