using System;
using System.Runtime.Serialization;

namespace Aplicacion.Dtos
{
    [DataContract (Name = "tablilla_precios"), Serializable ]
    public class TablillaDto
    {
        [DataMember(Name = "id_tablilla")]
        public int Id_tablilla { get; set; }

        [DataMember(Name = "nombre")]
        public string Nombre { get; set; }

        [DataMember(Name = "precio")]
        public int Precio { get; set; }

    }
}
