using Sistema_De_Inventario.Models;
using Sistema_De_Inventario.Services;

InventarioService inventarioService = new InventarioService();
ArchivoService archivoService = new ArchivoService();
await Menu.DesplegarMenu(inventarioService, archivoService);