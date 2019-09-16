using System;
using System.Runtime.Serialization;

namespace Aplicacion.Dtos
{
    [DataContract (Name = "citas"), Serializable]
    public class CitaDto
    {
        [DataMember (Name = "id_citas")]
        public int Id_citas { get; set; }

        [DataMember(Name = "id_paciente")]
        public int Id_paciente { get; set; }

        [DataMember(Name = "fecha_cita")]
        public DateTime Fecha_cita { get; set; }

        [DataMember(Name = "hora_inicio")]
        public string Hora_inicio { get; set; }

        [DataMember(Name = "hora_fin")]
        public string Hora_fin { get; set; }

        [DataMember(Name = "estado_cita")]
        public int Estado_cita { get; set; }

    }
}
