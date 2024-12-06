using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BalearesChallenge.Models
{
    public class UsuarioModel
    {
        [JsonIgnore]
        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [JsonIgnore]
        public string? Salt { get; set; }
    }
}
