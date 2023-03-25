﻿using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using SQLite;
using Xamarin.Forms;
using System.IO;
using Agenda.Datos;
using Agenda.iOS;
[assembly: Dependency(typeof(SQLiteDB))]
namespace Agenda.iOS
{
    public class SQLiteDB : ISQLiteDB
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(ruta, "db.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}