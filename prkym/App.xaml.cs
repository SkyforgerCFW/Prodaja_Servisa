using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using prkym.Services;
using prkym.Views;

namespace prkym
{
    public partial class App : Application
    {
        public static Theme AppTheme { get; set; }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

    public enum Theme
    {
        Light,
        Dark
    }
}
