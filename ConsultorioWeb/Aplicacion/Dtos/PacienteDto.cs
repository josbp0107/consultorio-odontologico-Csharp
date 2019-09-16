using System;
using System.Runtime.Serialization;

namespace Aplicacion.Dtos
{
    [DataContract (Name = "pacientes"), Serializable]
    public class PacienteDto
    {
        [DataMember (Name = "id_paciente")]
        public int Id_paciente { get; set; }
        
        [DataMember (Name = "identificacion")]
        public string Identificacion { get; set; }

        [DataMember (Name = "nombre_completo")]
        public string Nombre_completo { get; set; }

        [DataMember(Name = "direccion")]
        public string Direccion { get; set; }

        [DataMember(Name = "telefono")]
        public int Telefono { get; set; }

        [DataMember(Name = "horario_contacto")]
        public int Horario_contacto { get; set; }
    }
}
