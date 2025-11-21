namespace Sistema_De_Inventario.Interfaces
{
    public interface IProducto
    {
        int Id { get; set; }
        string Nombre { get; set; }
        double Precio { get; set; }
        int Cantidad { get; set; }
        Categoria Categoria { get; set; }
        string Descripcion { get; set; }
        DateTime FechaCreacion { get; set; }
        DateTime? FechaActualizacion { get; set; }
        string Proveedor { get; set; }
        string CodigoBarra { get; set; }
        string Sku {  get; set; }
        bool Activo { get; set; }
    }

    public enum Categoria
    {
        Generico = 0,
        Ropa = 1,
        Bebida = 2,
        Electronico = 3,
        Otros = 4
    }
}
