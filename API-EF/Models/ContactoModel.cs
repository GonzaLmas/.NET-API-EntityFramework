using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BalearesChallenge.Models
{
    public class ContactoModel
    {
        [JsonIgnore]
        public int IdContacto { get; set; }
        public string Nombre { get; set; }
        public string Empresa { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }
        public int? IdTransporte { get; set; }
        public TransporteModel? Transporte { get; set; }
    }
}
