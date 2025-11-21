using Sistema_De_Inventario.Interfaces;
using Sistema_De_Inventario.Services;

namespace Sistema_De_Inventario.Models
{
    public static class Menu
    {
        private static int id;

        static Menu()
        {
            id = 0;
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
                    case OpcionesMenu.Actualizar: Actualizar(inventarioService); break;
                    case OpcionesMenu.Eliminar: Eliminar(inventarioService); break;
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

            nombreProducto = RestriccionesService.ValidarInputString("Ingrese el nombre del producto: ");
            cantidad = RestriccionesService.ValidarInputEntero("Ingrese la cantidad de productos disponibles: ");
            precio = RestriccionesService.ValidarInputDouble("Ingrese el precio del producto: ");
            categoria = RestriccionesService.ValidarCategoria("Ingrese la categoria del producto: (Ropa: 1, Bebida: 2, Electronico: 3, Otros: 4)");
            descripcion = RestriccionesService.ValidarInputString("Ingrese la descricpion del producto: ");
            proveedor = RestriccionesService.ValidarInputString("Ingrese el nombre del proveedor: ");
            codigoBarra = RestriccionesService.ValidarInputString("Ingrese el codigo de barras: ");
            sku = RestriccionesService.ValidarInputString("Ingrese el sku: ");

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
            int idElegido = RestriccionesService.ValidarInputEntero("Ingrese el id del producto: ");
            var producto = inventarioService.Buscar(idElegido);
            if (producto != null) Console.WriteLine(producto);
            else Console.WriteLine("\nProducto no encontrado!\n");
        }

        public static void Actualizar(InventarioService inventarioService)
        {
            int idElegido = RestriccionesService.ValidarInputEntero("Ingrese el id del producto que desea actualizar: ");
            var producto = inventarioService.Buscar(idElegido);

            if (producto != null)
            {
                while(true)
                {
                    bool updateSucess = true;

                    Console.WriteLine("\nIngrese el dato que desea actualizar: ");
                    Console.WriteLine("1. Nombre");
                    Console.WriteLine("2. Precio");
                    Console.WriteLine("3. Cantidad");
                    Console.WriteLine("4. Categoria");
                    Console.WriteLine("5. Descripcion");
                    Console.WriteLine("6. Proveedor");
                    Console.WriteLine("7. CodigoBarra");
                    Console.WriteLine("8. Sku");

                    var opcion = RestriccionesService.ValidarInputEntero("Elija una opción: ");

                    switch(opcion)
                    {
                        case 1: producto.Nombre = RestriccionesService.ValidarInputString("Ingrese el nuevo nombre del producto: "); break;
                        case 2: producto.Precio = RestriccionesService.ValidarInputDouble("Ingrese el nuevo precio del producto: "); break;
                        case 3: producto.Cantidad = RestriccionesService.ValidarInputEntero("Ingrese la nueva cantidad disponible de productos: "); break;
                        case 4: producto.Categoria = RestriccionesService.ValidarCategoria("Ingrese la nueva categoria del producto: (Ropa: 1, Bebida: 2, Electronico: 3, Otros: 4)"); break;
                        case 5: producto.Descripcion = RestriccionesService.ValidarInputString("Ingrese la nueva categoria del producto: "); break;
                        case 6: producto.Proveedor = RestriccionesService.ValidarInputString("Ingrese el nuevo proveedor: "); break;
                        case 7: producto.CodigoBarra = RestriccionesService.ValidarInputString("Ingrese el nuevo codigo de barra: "); break;
                        case 8: producto.Sku = RestriccionesService.ValidarInputString("Ingrese el nuevo sku: "); break;
                        default: 
                            Console.WriteLine("Esa opción no es válida");
                            updateSucess = false;
                            break;
                    }

                    if (updateSucess) inventarioService.Actualizar(idElegido, producto);
                }
            }
            else
            {
                Console.WriteLine("\nProducto no encontrado!\n");
            }
        }

        public static void Eliminar(InventarioService inventarioService)
        {
            int idElegido = RestriccionesService.ValidarInputEntero("\nIngrese el id del producto que desea eliminar: ");
            inventarioService.Eliminar(idElegido);
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