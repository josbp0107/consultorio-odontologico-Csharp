using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Dtos
{
    [DataContract (Name = "estado_citas"), Serializable]
    public class EstadoCitaDto
    {
        [DataMember (Name = "id")]
        public int Id { get; set; }

        [DataMember (Name = "estado_cita")]
        public string Estado_cita { get; set; }

    }
}
