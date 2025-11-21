using Sistema_De_Inventario.Interfaces;

namespace Sistema_De_Inventario.Services
{
    public class InventarioService : IInventarioService
    {
        private readonly List<IProducto> _productos;
        private int Id;

        public InventarioService()
        {
            _productos = new List<IProducto>();
            Id = 0;
        }

        public IEnumerable<IProducto> Listar()
        {
            foreach(var producto in _productos)
            {
                if(producto.Activo) yield return producto;
            }
        }

        public void Agregar(IProducto producto)
        {
            this.Id++;
            producto.Id = this.Id;
            this._productos.Add(producto);
        }

        public void Actualizar(int id, IProducto producto)
        {
            var productoElegido = this._productos.FirstOrDefault(x => x.Id == id);

            if(productoElegido != null)
            {
                productoElegido.Nombre = producto.Nombre;
                productoElegido.Descripcion = producto.Descripcion;
                productoElegido.Precio = producto.Precio;
                productoElegido.Cantidad = producto.Cantidad;
                productoElegido.Categoria = producto.Categoria;
                productoElegido.FechaActualizacion = DateTime.Now;
                productoElegido.Proveedor = producto.Proveedor;
                productoElegido.CodigoBarra = producto.CodigoBarra;
                productoElegido.Sku = producto.Sku;
                Console.WriteLine("\nProducto actualizado exitosamente!");
            }
            else
            {
                Console.WriteLine("No se encontró el producto");
            }
        }

        public void Eliminar(int id)
        {
            var productoElegido = this._productos.FirstOrDefault(x => x.Id == id);

            if (productoElegido != null) productoElegido.Activo = false;
            else Console.WriteLine("No se encontro el producto");
        }

        public IProducto? Buscar(int id)
        {
            var productoEncontrado = this._productos.FirstOrDefault(x => x.Id == id);
            if (productoEncontrado != null)
            {
                return productoEncontrado;
            }
            else
            {
                return null;
            }
        }

    }
}
