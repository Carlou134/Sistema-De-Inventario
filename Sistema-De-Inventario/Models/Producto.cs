using Sistema_De_Inventario.Interfaces;

namespace Sistema_De_Inventario.Models
{
    public class Producto : IProducto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public Categoria Categoria { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string Proveedor { get; set; }
        public string CodigoBarra { get; set; }
        public string Sku { get; set; }
        public bool Activo { get; set; }

        public Producto()
        {
            this.Id = 0;
            this.Nombre = string.Empty;
            this.Precio = 0;
            this.Cantidad = 0;
            this.Categoria = Categoria.Generico;
            this.Descripcion = string.Empty;
            this.FechaCreacion = DateTime.Now;
            this.Proveedor = string.Empty;
            this.CodigoBarra = string.Empty;
            this.Sku = string.Empty;
            this.Activo = true;
        }

        public Producto(int id, string nombre, double precio, int cantidad, Categoria categoria, 
            string descripcion, DateTime fechaCreacion, string proveedor, string codigoBarra, 
            string sku, bool activo)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Precio = precio;
            this.Cantidad = cantidad;
            this.Categoria = categoria;
            this.Descripcion = descripcion;
            this.FechaCreacion = fechaCreacion;
            this.Proveedor = proveedor;
            this.CodigoBarra = codigoBarra;
            this.Sku = sku;
            this.Activo = activo;
        }

        public override string ToString()
        {
            return $"Producto: {this.Nombre}\nId: {this.Id}\nPrecio: {this.Precio}\nCantidad: {this.Cantidad}\nCategoria: {this.Categoria}";
        }
    }
}
