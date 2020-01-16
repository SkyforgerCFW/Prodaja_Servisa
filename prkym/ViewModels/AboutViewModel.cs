using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace prkym.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "O nama";
            ClickCommand = new Command<string>(async (url) => await Browser.OpenAsync(url));
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/SkyforgerCFW/Prodaja_Servisa"));
        }

        public ICommand ClickCommand { get; }
        public ICommand OpenWebCommand { get; }
    }
}