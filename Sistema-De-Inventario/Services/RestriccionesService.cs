using Sistema_De_Inventario.Interfaces;

namespace Sistema_De_Inventario.Services
{
    public static class RestriccionesService
    {
        private static Dictionary<int, Categoria> categoriasList;

        static RestriccionesService()
        {
            categoriasList = new Dictionary<int, Categoria>();
            categoriasList[1] = Categoria.Ropa;
            categoriasList[2] = Categoria.Bebida;
            categoriasList[3] = Categoria.Electronico;
            categoriasList[4] = Categoria.Otros;
        }

        public static string ValidarInputString(string indicacion)
        {
            string input =string.Empty;

            while(true)
            {
                Console.WriteLine(indicacion);
                input = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(input)) break;
                else Console.WriteLine("\nIngrese una opción válida!\n");
            }

            return input;
        }

        public static int ValidarInputEntero(string indicacion)
        {
            while (true)
            {
                Console.WriteLine(indicacion);
                var input = Console.ReadLine()!;
                if (int.TryParse(input, out int inputParseado)) return inputParseado;
                else Console.WriteLine("\nIngrese una opción válida!\n");
            }
        }

        public static double ValidarInputDouble(string indicacion)
        {
            while (true)
            {
                Console.WriteLine(indicacion);
                var input = Console.ReadLine()!;
                if (double.TryParse(input, out double inputParseado)) return inputParseado;
                else Console.WriteLine("\nIngrese una opción válida!\n");
            }
        }

        public static Categoria ValidarCategoria(string indicacion)
        {
            Categoria categoria = Categoria.Generico;
            while (true)
            {
                Console.WriteLine(indicacion);
                var categoriaIngresada = Console.ReadLine()!;
                if (int.TryParse(categoriaIngresada, out int numero))
                {
                    if (categoriasList.TryGetValue(numero, out categoria)) break;
                }
            }
            return categoria;
        }
    }
}
