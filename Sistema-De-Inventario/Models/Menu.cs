using Sistema_De_Inventario.Interfaces;
using Sistema_De_Inventario.Services;
using System.Runtime.CompilerServices;

namespace Sistema_De_Inventario.Models
{
    public static class Menu
    {
        private static int id;
        private static Dictionary<int, Categoria> categoriasList;

        static Menu()
        {
            id = 0;
            categoriasList = new Dictionary<int, Categoria>();
            categoriasList[1] = Categoria.Ropa;
            categoriasList[2] = Categoria.Bebida;
            categoriasList[3] = Categoria.Electronico;
            categoriasList[4] = Categoria.Otros;
        }

        public static void MostrarMenu()
        {
            Console.WriteLine("1. Agregar producto");
            Console.WriteLine("2. Listar productos");
            Console.WriteLine("3. Buscar producto");
            Console.WriteLine("4. Actualizar producto");
            Console.WriteLine("5. Eliminar producto");
            Console.WriteLine("6. Guardar inventario");
            Console.WriteLine("7. Cargar inventario");
            Console.WriteLine("0. Salir");
            Console.Write("Elija una opción: ");
        }

        public static void ElegirOpcion(byte opcion, InventarioService inventarioService)
        {
            if(Enum.IsDefined(typeof(OpcionesMenu), (int)opcion))
            {
                switch ((OpcionesMenu)opcion)
                {
                    case OpcionesMenu.Salir: Environment.Exit(0); break;
                    case OpcionesMenu.Agregar: Guardar(inventarioService); break;
                    case OpcionesMenu.Listar: Listar(inventarioService); break;
                    case OpcionesMenu.Buscar: Buscar(inventarioService); break;
                    case OpcionesMenu.Actualizar: Console.WriteLine("Opcion 4 elegida"); break;
                    case OpcionesMenu.Eliminar: Console.WriteLine("Opcion 5 elegida"); break;
                    case OpcionesMenu.Guardar: Console.WriteLine("Opcion 6 elegida"); break;
                    case OpcionesMenu.Cargar: Console.WriteLine("Opcion 7 elegida"); break;
                }
            }
            else
            {
                Console.WriteLine("Esa opción no existe!");
            }
        }

        public static void DesplegarMenu(InventarioService inventarioService)
        {
            while (true)
            {
                MostrarMenu();
                var opcionElegida = Console.ReadLine()!;

                if (!string.IsNullOrWhiteSpace(opcionElegida) 
                    && byte.TryParse(opcionElegida, out byte opcion)) ElegirOpcion(opcion, inventarioService);
                else Console.WriteLine("Ingrese una opción valida!");
            }
        }

        public static void Guardar(InventarioService inventarioService)
        {
            id++;
            Console.WriteLine("Ingrese información del producto a agregar: ");

            string nombreProducto = string.Empty;
            int cantidad = 0;
            double precio = 0;
            Categoria categoria = Categoria.Generico;
            string proveedor = string.Empty;
            string codigoBarra = string.Empty;
            string sku = string.Empty;
            string descripcion = string.Empty;

            do
            {
                Console.WriteLine("Ingrese el nombre del producto: ");
                nombreProducto = Console.ReadLine()!;
            } while (string.IsNullOrWhiteSpace(nombreProducto));

            while (true)
            {
                Console.WriteLine("Ingrese la cantidad de productos disponibles: ");
                var cantidadIngresada = Console.ReadLine()!;
                if (int.TryParse(cantidadIngresada, out cantidad)) break;
            }

            while (true)
            {
                Console.WriteLine("Ingrese el precio del producto: ");
                var precioIngresado = Console.ReadLine()!;
                if (double.TryParse(precioIngresado, out precio)) break;
            }

            while (true)
            {
                Console.WriteLine("Ingrese la categoria del producto: (Ropa: 1, Bebida: 2, Electronico: 3, Otros: 4)");
                var categoriaIngresada = Console.ReadLine()!;
                if (int.TryParse(categoriaIngresada, out int numero))
                {
                    if (categoriasList.TryGetValue(numero, out categoria)) break;
                }
            }

            do
            {
                Console.WriteLine("Ingrese la descricpion: ");
                descripcion = Console.ReadLine()!;
            } while (string.IsNullOrWhiteSpace(descripcion));

            do
            {
                Console.WriteLine("Ingrese el nombre del proveedor: ");
                proveedor = Console.ReadLine()!;
            } while (string.IsNullOrWhiteSpace(proveedor));

            do
            {
                Console.WriteLine("Ingrese el codigo de barras: ");
                codigoBarra = Console.ReadLine()!;
            } while (string.IsNullOrWhiteSpace(codigoBarra));

            do
            {
                Console.WriteLine("Ingrese el sku: ");
                sku = Console.ReadLine()!;
            } while (string.IsNullOrWhiteSpace(sku));

            Producto producto = new Producto(id, nombreProducto, precio, cantidad, 
                categoria, descripcion, DateTime.Now, proveedor, codigoBarra, sku, true);
            
            inventarioService.Agregar(producto);

            Console.WriteLine("\nProducto registrado con éxito\n");
        }

        public static void Listar(InventarioService inventarioService)
        {
            Console.WriteLine("\nLista de productos:\n");
            var inventario = inventarioService.Listar();
            if(inventario.Any())
            {
                foreach (var producto in inventario) Console.WriteLine(producto);
            }
            else
            {
                Console.WriteLine("No hay productos para mostrar");
            }
            Console.WriteLine("\n");
        }

        public static void Buscar(InventarioService inventarioService)
        {
            int id = 0;

            while(true)
            {
                Console.WriteLine("Ingrese el id del producto: ");
                string input = Console.ReadLine()!;
                if (int.TryParse(input, out id)) break;
            }

            var producto = inventarioService.Buscar(id);
            if (producto != null) Console.WriteLine(producto);
            else Console.Write("\nProducto no encontrado!\n");
        }

        public static void Actualizar(InventarioService inventarioService)
        {

        }

        public static void Eliminar(InventarioService inventarioService)
        {

        }
    }

    public enum OpcionesMenu
    {
        Salir = 0,
        Agregar = 1,
        Listar = 2,
        Buscar = 3,
        Actualizar = 4,
        Eliminar = 5,
        Guardar = 6,
        Cargar = 7,
    }
}