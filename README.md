# Sistema De Inventario

> A C# console CRUD app for managing product inventory, with JSON-based persistence and report export.

---

## 🧩 Problem / Context

A learning project built to practice OOP fundamentals in C#: interfaces, dependency-driven services, async I/O, and JSON serialization — without a database or web framework in the way.

---

## 🛠️ Stack

| Layer      | Technology            |
|------------|------------------------|
| Language   | C# (.NET 9)             |
| Runtime    | Console App (.NET SDK)  |
| Persistence| JSON file (`System.Text.Json`) |

---

## 🏗️ Architecture

- Services implement interfaces (`IInventarioService`, `IArchivoService`, `IProducto`) so the menu layer depends on abstractions, not concrete classes.
- In-memory product list (`List<IProducto>`) acts as the runtime store; soft-delete via an `Activo` flag instead of physically removing records.
- `ArchivoService` handles two independent concerns: exporting a human-readable `.txt` report and loading/saving inventory as JSON, keeping serialization out of the domain service.
- Input validation is centralized in `RestriccionesService`, so the menu never trusts raw console input directly.

---

## 🧠 Technical challenges and decisions

- **Problem:** Loading inventory data from disk shouldn't block the console UI. → **Solution:** `CargarDatos` is async and takes a `CancellationToken`. → **Why:** keeps file I/O cancellable and non-blocking, following async best practices even in a console app.
- **Problem:** Deleting a product outright loses history and can break references by Id. → **Solution:** soft delete via the `Activo` flag; `Listar()` filters inactive items. → **Why:** preserves data integrity without needing a full audit log.
- **Problem:** Console input is unreliable (empty strings, non-numeric values, invalid enum options). → **Solution:** `RestriccionesService` wraps every input type (string, int, double, `Categoria`) in a validation loop that re-prompts until valid. → **Why:** keeps validation logic out of the menu and reusable across every CRUD operation.

---

## 🚀 How to run it

```bash
git clone https://github.com/Carlou134/Sistema-De-Inventario.git
cd Sistema-De-Inventario
dotnet run --project Sistema-De-Inventario
```
