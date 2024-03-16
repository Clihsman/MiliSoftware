using Newtonsoft.Json;

namespace MiliSoftware.Inventario.Modelos
{
    public class InventarioGrupo
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        public string _id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("codigo")]
        public string Codigo { get; set;}
    }
}
