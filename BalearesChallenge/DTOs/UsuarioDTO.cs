using System.Text.Json.Serialization;

namespace BalearesChallenge.DTOs
{
    public class UsuarioDTO
    {
        public string Correo { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public string? Nombre { get; set; }

        [JsonIgnore]
        public string? Apellido { get; set; }

        [JsonIgnore]
        public string? Salt { get; set; }
    }
}
