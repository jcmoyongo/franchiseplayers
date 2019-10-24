﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(0.9, 500, Easing.Linear);
            await splashImage.ScaleTo(0.6, 200, Easing.Linear);
            await splashImage.ScaleTo(0.3, 200, Easing.Linear);
            await splashImage.ScaleTo(0.0, 200, Easing.Linear);

            App.Current.MainPage = new NavigationPage(new FPMainPage());
        }
    }
}