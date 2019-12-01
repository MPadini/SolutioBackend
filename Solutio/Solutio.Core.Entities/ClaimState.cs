using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities
{
    public class ClaimState : BaseEntity
    {
        public ClaimState()
        {
            AllowedStates = new List<ClaimState>();
        }

        public string Description { get; set; }

        public int MaximumTimeAllowed { get; set; }

        public List<ClaimState> AllowedStates { get; set; }

        public enum eId:long
        {
            Borrador = 11,
            En_Revision = 12,
            Rechazado_Mejores_Datos_ui = 13,
            Esperando_Denuncia = 21,
            Pendiente_de_Presentación = 22,
            Presentado = 23,
            Rechazado_Mejores_Datos_com = 24,
            Nuevo_Ofrecimiento = 31,
            Ofrecimiento_Rechazado = 32,
            Esperando_Ofrecimiento = 33,
            Ofrecimiento_Aceptado = 41,
            Firmar_Convenio = 42,
            Convenio_Firmado = 43,
            Pendiente_de_Pago = 44,
            Rechazado = 81,
            Desestimado = 82,
            Desistido = 83,
            A_Juicio = 84,
            Cerrado = 100
        }
    }
}
