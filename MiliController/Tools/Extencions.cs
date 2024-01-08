using MiliSoftware.Inventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware
{
    internal static class Extencions
    {
        public static Product ToProduct(this Producto producto)
        {
            return new Product();
        }

        public static Producto ToProducto(this Product product)
        {
            Producto producto = new Producto()
            {
                Nombre = product.Name,
                Descripcion = product.Description,
                Codigo = product.Code,
                ProductoComponentes = product.ProductComponents?.Select(componente => new ProductoComponente(componente.id, componente.Name, componente.Amount)).ToArray()
            };

            return producto;
        }
    }
}
