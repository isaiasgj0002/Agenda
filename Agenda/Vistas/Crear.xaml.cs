using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Agenda.Datos;
using Agenda.Tablas;
namespace Agenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edicion : ContentPage
    {
        private SQLiteAsyncConnection cn;
        public Edicion()
        {
            InitializeComponent();
            cn = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnagregar.Clicked += Btnagregar_Clicked;
        }

        private void Btnagregar_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txttitulo.Text) || string.IsNullOrWhiteSpace(txtcontenido.Text) || string.IsNullOrWhiteSpace(txtediqueta.Text))
            {
                DisplayAlert("Error", "Todos los campos son obligatorios", "Ok");
                return;
            }
            try
            {
                var datosAct = new Actividades
                {
                    Title = txttitulo.Text.Trim(),
                    Contenido = txtcontenido.Text.Trim(),
                    Etiqueta = txtediqueta.Text.Trim()
                };
                cn.InsertAsync(datosAct);
                DisplayAlert("Mensaje", "Se agrego la actividad", "OK");
                limpiarForm();
            }catch(Exception ex)
            {
                DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }

        private void limpiarForm()
        {
            txttitulo.Text = "";
            txtcontenido.Text = "";
            txtediqueta.Text = "";
        }
    }
}