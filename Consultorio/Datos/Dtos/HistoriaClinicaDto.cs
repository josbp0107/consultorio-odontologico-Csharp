using System;
using System.Runtime.Serialization;

namespace Datos.Dtos
{
   [DataContract (Name = "historias_clinicas"), Serializable]
   public class HistoriaClinicaDto
   {
        [DataMember(Name = "id_historia")]
        public int Id_historia { get; set; }

        [DataMember(Name = "id_paciente")]
        public int Id_paciente { get; set; }

        [DataMember(Name = "procedimiento_hecho")]
        public string Procedimiento_hecho { get; set; }

        [DataMember(Name = "fecha_realizacion")]
        public DateTime Fecha_realizacion { get; set; }

        [DataMember(Name = "descripcion")]
        public string Descripcion { get; set; }

        [DataMember(Name = "id_tablilla")]
        public int Id_tablilla { get; set; }

    }
}
