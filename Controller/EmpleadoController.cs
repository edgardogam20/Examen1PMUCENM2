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

        public Task<List<Empleados>> ObtenerEmpleados() =>
            _database.Table<Empleados>().ToListAsync();

        public Task<int> GuardarEmpleado(Empleados empleado) =>
            _database.InsertAsync(empleado);

        public async Task<string> ConvertirImagenABase64(FileResult foto)
        {
            if (foto == null) return string.Empty;

            using var stream = await foto.OpenReadAsync();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            return Convert.ToBase64String(ms.ToArray());
        }

        public async Task<string> ConvertirImagenABase64(Stream stream)
        {
            if (stream == null) return string.Empty;

            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}