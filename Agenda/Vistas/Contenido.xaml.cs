using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agenda.Tablas;
using SQLite;
using Agenda.Datos;
using System.IO;
namespace Agenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contenido : ContentPage
    {
        public int idSeleccionado;
        public string tituloseleccionado, contenidoseleccionado, etiquetaseleccionada;
        private SQLiteAsyncConnection cn;
        IEnumerable<Actividades> delete;
        IEnumerable<Actividades> edit;
        public Contenido(int id, string Titulo, string contenido, string etiqueta)
        {
            InitializeComponent();
            cn = DependencyService.Get<ISQLiteDB>().GetConnection();
            idSeleccionado = id;
            tituloseleccionado = Titulo;
            contenidoseleccionado = contenido;
            etiquetaseleccionada = etiqueta;
            btneditar.Clicked += Btneditar_Clicked;
            btneliminar.Clicked += Btneliminar_Clicked;
        }
        protected override void OnAppearing()
        {
            txttituloed.Text = tituloseleccionado;
            txtcontenidoed.Text = contenidoseleccionado;
            txtediquetaed.Text = etiquetaseleccionada;
            base.OnAppearing();
        }

        private async void Btneliminar_Clicked(object sender, EventArgs e)
        {
            var respuesta = await DisplayAlert("Aviso", "¿Esta seguro que desea eliminar esta actividad?", "Si", "No");
            if (respuesta)
            {
                var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "db.db3");
                var db = new SQLiteConnection(path);
                delete = Delete(db, idSeleccionado);
                DisplayAlert("Mensaje", "Se borro la actividad", "OK");
                Limpiar();
                Navigation.PushAsync(new Principal());
            }
        }

        private void Limpiar()
        {
            txttituloed.Text = "";
            txtcontenidoed.Text = "";
            txtediquetaed.Text = "";
        }

        private IEnumerable<Actividades> Delete(SQLiteConnection db, int idSeleccionado)
        {
            return db.Query<Actividades>("DELETE FROM Actividades where Id = ?", idSeleccionado);
        }

        private void Btneditar_Clicked(object sender, EventArgs e)
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "db.db3");
            var db = new SQLiteConnection(path);
            if(string.IsNullOrWhiteSpace(txttituloed.Text) || string.IsNullOrWhiteSpace(txtcontenidoed.Text) || string.IsNullOrWhiteSpace(txtediquetaed.Text))
            {
                DisplayAlert("Error", "Todos los campos son obligatorios", "Ok");
                return;
            }
            edit = Edit(db, idSeleccionado, txttituloed.Text, txtcontenidoed.Text, txtediquetaed.Text);
            DisplayAlert("Mensaje", "Se modifico la actividad", "OK");
        }

        private IEnumerable<Actividades> Edit(SQLiteConnection db, int idSeleccionado, string text1, string text2, string text3)
        {
            return db.Query<Actividades>("UPDATE Actividades set Title = ?, Contenido = ?, Etiqueta = ? where Id = ?",text1,text2,text3, idSeleccionado);
        }
    }
}