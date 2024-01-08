using Newtonsoft.Json;


namespace MiliSoftware.Acceso.Modelos
{
    public class AccesoToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
