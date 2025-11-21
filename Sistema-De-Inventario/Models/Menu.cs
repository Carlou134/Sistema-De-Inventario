namespace Sistema_De_Inventario.Models
{
    public static class Menu
    {
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

        public static void ElegirOpcion(byte opcion)
        {
            if(Enum.IsDefined(typeof(OpcionesMenu), (int)opcion))
            {
                switch ((OpcionesMenu)opcion)
                {
                    case OpcionesMenu.Salir: Environment.Exit(0); break;
                    case OpcionesMenu.Agregar: Console.WriteLine("Opcion 1 elegida"); break;
                    case OpcionesMenu.Listar: Console.WriteLine("Opcion 2 elegida"); break;
                    case OpcionesMenu.Buscar: Console.WriteLine("Opcion 3 elegida"); break;
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

        public static void DesplegarMenu()
        {
            while (true)
            {
                MostrarMenu();
                var opcionElegida = Console.ReadLine()!;

                if (!string.IsNullOrWhiteSpace(opcionElegida) 
                    && byte.TryParse(opcionElegida, out byte opcion)) ElegirOpcion(opcion);
                else Console.WriteLine("Ingrese una opción valida!");
            }
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