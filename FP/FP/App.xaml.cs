using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FP.Views;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FP
{
    public partial class App : Application
    {
        public App()
        {
            //MTUyMTAyQDMxMzcyZTMzMmUzME1mUUtaaU1MUHlNOEdOK2hBemtNN0ZkbDZ5RGNwaittNXVJNDVyWkZnSmM9
            //Old Key - Mzc3MjBAMzEzNjJlMzMyZTMwTmJEeERTalBNaUlWaGQvVzFXamJKb3k2ckc1OXBhcnV2V0toWlZld25WRT0=
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTUyMTAyQDMxMzcyZTMzMmUzME1mUUtaaU1MUHlNOEdOK2hBemtNN0ZkbDZ5RGNwaittNXVJNDVyWkZnSmM9");

            InitializeComponent();

            //MainPage = new NavigationPage(new FPMainPage());
            MainPage = new NavigationPage(new SplashPage());

            //Prism.Navigation.INavigationService.NavigationService.NavigateAsync("NavigationPage/MainPage?title=Facebook Native Login");
            //MainPage = new NavigationPage(new FacebookLoginPage());
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
    }
}
