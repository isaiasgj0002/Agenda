using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Agenda.Tablas;
using System.Collections.ObjectModel;
using System.IO;
using Agenda.Datos;
namespace Agenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
            btnvolver.Clicked += Btnvolver_Clicked;
        }

        private void Btnvolver_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Principal());
        }
    }
}