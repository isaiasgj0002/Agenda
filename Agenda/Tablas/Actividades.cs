using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Agenda.Tablas
{
    public class Actividades
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(int.MaxValue)]
        public string Contenido { get; set; }
        [MaxLength(20)]
        public string Etiqueta { get; set; }

    }
}
