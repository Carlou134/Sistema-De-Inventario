using Sistema_De_Inventario.Models;

namespace Sistema_De_Inventario.Interfaces
{
    public interface IInventarioService
    {
        IEnumerable<IProducto> Listar();
        void Agregar(IProducto producto);
        void Actualizar(int id, IProducto producto);
        void Eliminar(int id);
        IProducto? Buscar(int id);
        void Limpiar();
        int getId();
        void SetId(int id);
        void AgregarDesdeArchivo(IProducto producto);
    }
}
