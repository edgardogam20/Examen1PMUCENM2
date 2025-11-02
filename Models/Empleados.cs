using System;
using SQLite;

namespace Examen1PMUCENM2.Models
{
    [SQLite.Table("Empleados")]
    public class Empleados
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        [SQLite.MaxLength(250)]
        public string Nombre { get; set; } = string.Empty;

        public DateTime FechaIngreso { get; set; }

        [SQLite.MaxLength(100)]
        public string Puesto { get; set; } = string.Empty;

        [SQLite.MaxLength(100), SQLite.Unique]
        public string Correo { get; set; } = string.Empty;

        [SQLite.MaxLength(500000)]
        public string Foto { get; set; } = string.Empty;
    }
}