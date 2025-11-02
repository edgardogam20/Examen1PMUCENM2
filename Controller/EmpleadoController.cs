using SQLite;
using System.IO;
using Examen1PMUCENM2.Models;

namespace Examen1PMUCENM2.Controllers
{
    public class EmpleadosController
    {
        private readonly SQLiteAsyncConnection _database;

        public EmpleadosController()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Empleados.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            // Crear la tabla si no existe
            _database.CreateTableAsync<Empleados>().Wait();
        }

        // ✅ Método para convertir imagen a Base64
        public string ConvertirImagenABase64(Stream imageStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                imageStream.CopyTo(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        // ✅ Operaciones CRUD
        public Task<int> AgregarEmpleadoAsync(Empleados empleado)
        {
            return _database.InsertAsync(empleado);
        }

        public Task<List<Empleados>> ObtenerEmpleadosAsync()
        {
            return _database.Table<Empleados>().ToListAsync();
        }

        public Task<int> EliminarEmpleadoAsync(Empleados empleado)
        {
            return _database.DeleteAsync(empleado);
        }

        public Task<int> ActualizarEmpleadoAsync(Empleados empleado)
        {
            return _database.UpdateAsync(empleado);
        }
    }
}