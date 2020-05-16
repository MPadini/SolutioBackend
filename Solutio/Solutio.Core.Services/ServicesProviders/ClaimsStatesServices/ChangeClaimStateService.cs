using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;
using Solutio.Core.Services.Factories;
using Solutio.Core.Services.Repositories;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Solutio.Core.Services.ApplicationServices.ClaimWorkflowServices;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.ApplicationServices;

namespace Solutio.Core.Services.ServicesProviders.ClaimsStatesServices {
    public class ChangeClaimStateService : IChangeClaimStateService {
        private readonly IClaimStateFactory claimStateFactory;
        private readonly IClaimRepository claimRepository;
        private readonly IClaimStateRepository stateRepository;
        private readonly IClaimWorkflowService claimWorkflowService;
        private readonly IGetClaimService getClaimService;
        private readonly IEmailSender emailSender;

        public ChangeClaimStateService(
            IClaimStateFactory claimStateFactory,
            IClaimRepository claimRepository,
            IClaimStateRepository stateRepository,
            IClaimWorkflowService claimWorkflowService,
            IGetClaimService getClaimService,
            IEmailSender emailSender) {
            this.claimStateFactory = claimStateFactory;
            this.claimRepository = claimRepository;
            this.stateRepository = stateRepository;
            this.claimWorkflowService = claimWorkflowService;
            this.getClaimService = getClaimService;
            this.emailSender = emailSender;
        }

        public async Task<bool> ChangeState(Claim claim, long newStateId, string userName) {
            if (claim == null) throw new ArgumentException(nameof(Claim), "null");

            //var actualState = await claimStateFactory.GetStateValidator(claim.StateId);
            //newStateId = await actualState.ChangeNextState(claim, newStateId);

            // if (claim.StateId != newStateId) {
            if (newStateId > 0) {
                await Change(claim, newStateId);
                await claimWorkflowService.RegisterWorkflow(newStateId, claim.Id, userName);
            }
           
            // }

            return true;
        }

        public async Task SendClaimsToAjuicio(string userName) {
            var claims = await getClaimService.GetClaimByState((long)ClaimState.eId.Esperando_Denuncia);
            foreach (var claim in claims) {
                var datediff = (DateTime.Now - claim.StateModifiedDate).TotalDays;
                if (datediff > 20) {

                    await Change(claim, (long)ClaimState.eId.A_Juicio);
                    await claimWorkflowService.RegisterWorkflow((long)ClaimState.eId.A_Juicio, claim.Id, userName);

                    var message = $"El reclamo {claim.Id} fue enviado a juicio porque superó el tiempo especificado";
                    await emailSender.SendEmailAsync(claim.UserName, message, message);
                }
            }
        }

        private async Task Change(Claim claim, long newStateId) {
            var state = await stateRepository.GetById(newStateId);
            if (state == null) throw new ApplicationException("Selected state does not exists.");

            claim.StateId = newStateId;
            claim.StateModifiedDate = DateTime.Now;

            await claimRepository.UpdateState(claim, claim.Id);

            await SendEmail(claim, state);
        }

        private async Task<bool> ValidateAllowedState(Claim claim, long newStateId) {
            var allowedState = claim.State.AllowedStates.Where(x => x.Id == newStateId).ToList();
            if (allowedState == null || !allowedState.Any()) throw new ApplicationException("Invalid status change.");

            return true;
        }


        private async Task SendEmail(Claim claim, ClaimState claimState) {
            try {
                if (claim == null) return;
                if (claimState == null) return;

                string message = string.Empty;
                if (claimState.Id == (long)ClaimState.eId.Rechazado_Mejores_Datos_com) {
                    message = $"El trámite { claim.Id } ha sido rechazado para mejores datos. Verfique por favor";
                }

                if (claimState.Id == (long)ClaimState.eId.Rechazado_Mejores_Datos_ui) {
                    message = $"El trámite { claim.Id } ha sido rechazado para mejores datos. Verfique por favor";
                }

                if (claimState.Id == (long)ClaimState.eId.Nuevo_Ofrecimiento) {
                    message = $"El trámite { claim.Id } ha recibido un nuevo ofrecimiento.";
                }

                if (claimState.Id == (long)ClaimState.eId.Firmar_Convenio) {
                    message = $"El trámite { claim.Id } posee un convenio para firmar.";
                }


                if (!string.IsNullOrEmpty(message)) {
                    await emailSender.SendEmailAsync(claim.UserName, $"Sr. productor, el trámite { claim.Id } requiere de su atención", message);
                }
            }
            catch (Exception) {
                //Log
            }
        }
    }
}
