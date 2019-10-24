using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FP.ViewModels
{
    public class LoginViewModel
    {
        public Command OnFacebookLoginSuccessCommand { get; }
        public Command OnFacebookLoginErrorCommand { get; }
        public Command OnFacebookLoginCancelCommand { get; }
        public LoginViewModel()
        {
            OnFacebookLoginSuccessCommand = new Command<string>(
                (authToken) => DisplayAlert("Success", $"Authentication succeede: { authToken }"));

            OnFacebookLoginErrorCommand = new Command<string>(
                err => DisplayAlert("Error", $"Authentication failed: { err }"));

            OnFacebookLoginCancelCommand = new Command(
                () => DisplayAlert("Cancel", "Authentication cancelled by the user."));
        }

        void DisplayAlert(string title, string msg) =>
            (Application.Current as App).MainPage.DisplayAlert(title, msg, "OK");
    }
}
