using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AluracarPCL.Data;
using SQLite;
using AluracarPCL.Droid;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SQlite_android))]
namespace AluracarPCL.Droid
{
    class SQlite_android : ISQlite
    {
        const string Db = "Agendamento.db3";
        public SQLiteConnection GetConnection()
        {
            var caminho = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, Db);
            return new SQLite.SQLiteConnection(caminho);
        }
    }
}