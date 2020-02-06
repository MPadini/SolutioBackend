using Solutio.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solutio.Core.Entities {
    public class FileType : BaseEntity {

        public string Description { get; set; }

        public enum eId : long {
            Archivo_varios = 1,
            Dni = 2,
            Denuncia = 3,
            reclamo = 4,
            ConvenioFirmado = 6
        }
    }
}
