using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Agenda.Tablas;
using System.IO;
using Agenda.Datos;
using System.Collections.ObjectModel;

namespace Agenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : ContentPage
    {
        private SQLiteAsyncConnection cn;
        private ObservableCollection<Actividades> actividades;
        public Principal()
        {
            InitializeComponent();
            cn = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnbuscar.Clicked += Btnbuscar_Clicked;
            btnagregar.Clicked += Btnagregar_Clicked;
            acercade.Clicked += Acercade_Clicked;
            listaActividadesMain.ItemSelected += ListaActividadesMain_ItemSelected;
        }

        private void Acercade_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Inicio());
        }

        private void ListaActividadesMain_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Actividades)e.SelectedItem;
            var id = obj.Id.ToString();
            var titulo = obj.Title.ToString();
            var contenido = obj.Contenido.ToString();
            var etiqueta = obj.Etiqueta.ToString();
            int idAct = Convert.ToInt32(id);
            try
            {
                Navigation.PushAsync(new Contenido(idAct, titulo, contenido, etiqueta));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Btnagregar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Edicion());
        }

        private void Btnbuscar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txttitulo.Text))
            {
                try
                {
                    var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "db.db3");
                    var db = new SQLiteConnection(path);
                    db.CreateTable<Actividades>();
                    IEnumerable<Actividades> resultado = SELECT_WHERE(db, txttitulo.Text.Trim());
                    if (resultado.Count() > 0)
                    {
                        DisplayAlert("Aviso", "Actividades encontradas", "Ok");
                        //Navigation.PushAsync(new Inicio());
                        listaActividadesMain.ItemsSource = resultado.ToList();
                    }
                    else
                    {
                        DisplayAlert("Aviso", "No hay resultados", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message.ToString(), "OK");
                }
            }
            else
            {
                DisplayAlert("Error", "Introduzca un titulo", "OK");
            }
        }
        protected async override void OnAppearing()
        {
            try
            {
                var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "db.db3");
                var db = new SQLiteConnection(path);
                db.CreateTable<Actividades>();
                var resultados = await cn.Table<Actividades>().ToListAsync();
                actividades = new ObservableCollection<Actividades>(resultados);
                listaActividadesMain.ItemsSource = actividades;
                base.OnAppearing();
            }
            catch(Exception ex)
            {
                DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }

        private IEnumerable<Actividades> SELECT_WHERE(SQLiteConnection db, string titulo)
        {
            return db.Query<Actividades>("SELECT * FROM Actividades where Title = ?", titulo);
        }
    }
}