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
using Xamarin.Essentials;

namespace Agenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
            btnvolver.Clicked += Btnvolver_Clicked;
            MessagingCenter.Subscribe<Inicio, (string, string)>(this, "ShareMessage", (sender, args) =>
            {
                string title = args.Item1;
                string message = args.Item2;

                // Invocar la funcionalidad de compartir del sistema operativo
                ShareText(title, message);
            });
        }

        private void Btnvolver_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Principal());
        }
        private void OnShareButtonClicked(object sender, EventArgs e)
        {
            string message = "¡Hola! Te recomiento esta app: AgendaX. Puedes descargarla usando este enlace: https://play.google.com/store/apps/details?id=com.twoploapps.agendax";
            string title = "Compartir";

            MessagingCenter.Send(this, "ShareMessage", (title, message));
        }
        private async void ShareText(string title, string message)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = title,
                Text = message
            });
        }
    }
}