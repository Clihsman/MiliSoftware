using Newtonsoft.Json;

namespace MiliSoftware.Acceso.Modelos
{
    public class Credenciales
    {
        [JsonProperty("email")]
        public string Correo { get; set; }
        [JsonProperty("password")]
        public string Contraseña { get; set; }

        public Credenciales() { }

        public Credenciales(string correo, string contraseña) {
            Correo = correo;
            Contraseña = contraseña;
        }
    }
}
