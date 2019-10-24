﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FP.Droid;
using FP.Views;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginButtonRenderer))]
namespace FP.Droid
{
    public class FacebookLoginButtonRenderer : ViewRenderer
    {
        Context ctx;
        bool disposed;
        public FacebookLoginButtonRenderer(Context ctx) : base(ctx)
        {
            this.ctx = ctx;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            if (Control == null)
            {
                var fbLoginBtnView = e.NewElement as FacebookLoginButton;
                var fbLoginbBtnCtrl = new Xamarin.Facebook.Login.Widget.LoginButton(ctx)
                {
                    LoginBehavior = LoginBehavior.NativeWithFallback
                };

                fbLoginbBtnCtrl.SetReadPermissions(fbLoginBtnView.Permissions);
                fbLoginbBtnCtrl.RegisterCallback(MainActivity.CallbackManager, new MyFacebookCallback(Element as FacebookLoginButton));

                SetNativeControl(fbLoginbBtnCtrl);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                if (this.Control != null)
                {
                    (this.Control as Xamarin.Facebook.Login.Widget.LoginButton).UnregisterCallback(MainActivity.CallbackManager);
                    this.Control.Dispose();
                }
                this.disposed = true;
            }
            base.Dispose(disposing);
        }

        class MyFacebookCallback : Java.Lang.Object, IFacebookCallback
        {
            FacebookLoginButton view;

            public MyFacebookCallback(FacebookLoginButton view)
            {
                this.view = view;
            }

            public void OnCancel() =>
                view.OnCancel?.Execute(null);

            public void OnError(FacebookException fbException) =>
                view.OnError?.Execute(fbException.Message);

            public void OnSuccess(Java.Lang.Object result) =>
                view.OnSuccess?.Execute(((LoginResult)result).AccessToken.Token);

        }
    }
}