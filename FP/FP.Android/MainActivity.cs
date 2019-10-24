
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Xamarin.Forms;
using FacebookNativeLogin.Services.Contracts;
using FacebookNativeLogin.Android;
using Xamarin.Facebook;
using Android.Runtime;

namespace FP.Droid
{
    [Activity(Label = "Franchise Players", Icon = "@mipmap/fp_120", Theme = "@style/MainTheme", MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        public static ICallbackManager CallbackManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // Create callback manager using CallbackManagerFactory
            CallbackManager = CallbackManagerFactory.Create();

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState); //10/17/2019
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            var manager = DependencyService.Get<IFacebookManager>();
            if (manager != null)
            {
                (manager as FacebookManager)._callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}