using MiliSoftware.Modelos;
using Newtonsoft.Json;

namespace MiliSoftware.Inventario.Modelos
{
    public class Producto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("tipo")]
        public dynamic Tipo { get; set; }

        [JsonProperty("grupo")]
        public dynamic GrupoInventario { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("codigo_barras")]
        public string CodigoDeBarras { get; set; }

        [JsonProperty("unidad_medida")]
        public dynamic UnidadDeMedida { get; set; }

        [JsonProperty("TaxClassification")]
        public dynamic ClasificacionTriburatia { get; set; }

        [JsonProperty("bodega")]
        public dynamic Bodega { get; set; }
            
        [JsonProperty("imagen")]
        public string Imagen { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("guardar_imagen")]
        public bool GuardarImagenServidor { get; set; }

        [JsonProperty("Key0")]
        public string Llave0 { get; set; }

        [JsonProperty("Value0")]
        public string Valor0 { get; set; }

        [JsonProperty("Key1")]
        public string Llave1 { get; set; }

        [JsonProperty("Value1")]
        public string Valor1 { get; set; }

        [JsonProperty("Key2")]
        public string Llave2 { get; set; }

        [JsonProperty("Value2")]
        public string Valor2 { get; set; }

        [JsonProperty("Key3")]
        public string Llave3 { get; set; }

        [JsonProperty("Value3")]
        public string Valor3 { get; set; }

        [JsonProperty("Key4")]
        public string Llave4 { get; set; }

        [JsonProperty("Value4")]
        public string Valor4 { get; set; }

        [JsonProperty("Key5")]
        public string Llave5 { get; set; }

        [JsonProperty("Value5")]
        public string Valor5 { get; set; }

        [JsonProperty("EquivalentProducts")]
        public string ProductosEquivalentes { get; set; }

        [JsonProperty("producto_componentes")]
        public ProductoComponente[] ProductoComponentes { get; set; }
    }
}
