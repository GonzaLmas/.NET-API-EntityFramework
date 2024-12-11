using System.Text.Json.Serialization;

namespace BalearesChallenge.Models
{
    public class TransporteModel
    {
        [JsonIgnore]
        public int IdTransporte { get; set; }
        public string TipoTransporte { get; set; }
    }
}
