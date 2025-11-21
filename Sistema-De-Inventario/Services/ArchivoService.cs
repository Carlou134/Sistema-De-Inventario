using Sistema_De_Inventario.Interfaces;
using Sistema_De_Inventario.Models;
using System.Text;
using System.Text.Json;

namespace Sistema_De_Inventario.Services
{
    public class ArchivoService : IArchivoService
    {
        public void ExportarReporteInventario(IInventarioService inventarioService)
        {
            try
            {
                var inventarioList = inventarioService.Listar();
                string nombreArchivo = $"Reporte-inventario-{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.txt";

                using (FileStream fileStream = new FileStream(nombreArchivo, FileMode.Create, FileAccess.Write))
                {
                    using(StreamWriter writer = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        writer.WriteLine("Reporte de productos:");
                        writer.WriteLine("=============================");
                        if(inventarioList.Any())
                        {
                            foreach (var producto in inventarioService.Listar())
                            {
                                writer.WriteLine($"Id: {producto.Id}");
                                writer.WriteLine($"Nombre del producto: {producto.Nombre}");
                                writer.WriteLine($"Precio: {producto.Precio}");
                                writer.WriteLine($"Cantidad: {producto.Cantidad}");
                                writer.WriteLine($"Categoria: {producto.Categoria}");
                                writer.WriteLine($"Descripcion: {producto.Descripcion}");
                                writer.WriteLine($"Proveedor: {producto.Proveedor}");
                                writer.WriteLine($"Codigo de barras: {producto.CodigoBarra}");
                                writer.WriteLine($"Sku: {producto.Sku}");
                                writer.WriteLine("=============================");
                            }
                        }
                        else
                        {
                            writer.WriteLine("No hay datos en el inventario!");
                            writer.WriteLine("=============================");
                        }
                    }
                    Console.WriteLine("\nSe generó el reporte correctamente\n");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task CargarDatos(IInventarioService inventarioService, CancellationToken cancellationToken)
        {
            try
            {
                string rutaArchivoJson = "data.json";

                if (!File.Exists(rutaArchivoJson))
                {
                    Console.WriteLine("\nNo hay archivo para cargar.\n");
                    return;
                }

                cancellationToken.ThrowIfCancellationRequested();

                using (FileStream fileStream = new FileStream(rutaArchivoJson, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string json = await reader.ReadToEndAsync();
                        var productos = JsonSerializer.Deserialize<List<Producto>>(json);

                        if(productos != null)
                        {
                            inventarioService.Limpiar();

                            foreach (var producto in productos)
                            {
                                inventarioService.AgregarDesdeArchivo(producto);
                            }

                            inventarioService.SetId(productos.Max(x => x.Id));
                        }
                    }

                    Console.WriteLine("\nSe cargaron los datos de manera correcta!\n");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
