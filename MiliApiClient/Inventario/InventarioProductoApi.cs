using MiliSoftware.Errores;
using MiliSoftware.Inventario.Modelos;
using OpenRestClient;
using OpenRestClient.Attributes;
using OpenRestClient.Enums;
using System.Threading.Tasks;

namespace MiliSoftware.Inventario
{
    [RestController("inventario/productos")]
    public class InventarioProductoApi : Api
    {
        public InventarioProductoApi() : base(typeof(InventarioProductoApi)) { }

        [RestMethod(MethodType.GET)]
        public Task<RestResponse<Producto[], AccesoError>> ObtenerTodosLosProductos() =>
            CallArray<Producto, AccesoError>(nameof(ObtenerTodosLosProductos));

        [RestMethod(MethodType.POST)]
        public Task<RestResponse<Producto, AccesoError>> GuardarProducto(Producto producto) =>
           Call<Producto, AccesoError>(nameof(GuardarProducto), producto);

    }
}
