using Newtonsoft.Json;

namespace MiliSoftware.Inventario.Modelos
{
    public class ProductoComponente
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("cantidad")]
        public int Cantidad { get; set; }

        public ProductoComponente() { }

        public ProductoComponente(int id,string nombre, int cantidad) {
            Id = id;
            Nombre = nombre;
            Cantidad = cantidad;
        }
    }
}
