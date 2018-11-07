using MyManga.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyManga
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        //Singleton services
        private static InMangaService inMangaService;
        public static InMangaService InMangaService {
            get
            {
                if (inMangaService == null)
                {
                    inMangaService = new InMangaService();
                }
                return inMangaService;
            }
        }
    }
}
