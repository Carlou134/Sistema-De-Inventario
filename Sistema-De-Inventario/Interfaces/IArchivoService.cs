namespace Sistema_De_Inventario.Interfaces
{
    public interface IArchivoService
    {
        void ExportarReporteInventario(IInventarioService inventarioService);
        Task CargarDatos(IInventarioService inventarioService, CancellationToken cancellationToken);
    }
}
