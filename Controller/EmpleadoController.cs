using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using Examen1PMUCENM2.Models;
using Microsoft.Maui.Storage;

namespace Examen1PMUCENM2.Controllers
{
    public class EmpleadosController
    {
        private readonly SQLiteAsyncConnection _database;

        public EmpleadosController()
        {
            string dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "Empleados.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Empleados>().Wait();
        }

        // ✅ Obtener todos los empleados
        public Task<List<Empleados>> ObtenerEmpleados() =>
            _database.Table<Empleados>().ToListAsync();

        // ✅ Guardar nuevo empleado
        public Task<int> GuardarEmpleado(Empleados empleado) =>
            _database.InsertAsync(empleado);

        // ✅ Obtener empleado por ID (para editar)
        public async Task<Empleados> ObtenerEmpleadoPorId(int id)
        {
            return await _database.Table<Empleados>()
                                  .Where(e => e.Id == id)
                                  .FirstOrDefaultAsync();
        }

        // ✅ Actualizar empleado existente
        public async Task<int> ActualizarEmpleado(Empleados empleado)
        {
            return await _database.UpdateAsync(empleado);
        }

        // ✅ Eliminar empleado por ID
        public async Task<int> EliminarEmpleado(int id)
        {
            var empleado = await ObtenerEmpleadoPorId(id);
            if (empleado != null)
            {
                return await _database.DeleteAsync(empleado);
            }
            return 0;
        }

        // ✅ Convertir imagen a Base64 desde FileResult
        public async Task<string> ConvertirImagenABase64(FileResult foto)
        {
            if (foto == null) return string.Empty;

            using var stream = await foto.OpenReadAsync();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            return Convert.ToBase64String(ms.ToArray());
        }

        // ✅ Convertir imagen a Base64 desde Stream
        public async Task<string> ConvertirImagenABase64(Stream stream)
        {
            if (stream == null) return string.Empty;

            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}