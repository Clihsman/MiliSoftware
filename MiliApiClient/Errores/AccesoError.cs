using Newtonsoft.Json;

namespace MiliSoftware.Errores
{
    public class AccesoError
    {
        [JsonProperty("status")]
        public string Estado { get; set; }
    }
}
