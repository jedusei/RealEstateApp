using Android.App;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(RealEstate.Droid.Platform))]
namespace RealEstate.Droid
{
    class Platform : IPlatform
    {
        static Activity _activity;

        public static void Init(Activity activity)
        {
            _activity = activity;
        }

        public void SetStatusBarColor(Color color)
        {
            _activity.Window.SetStatusBarColor(color.ToAndroid());
        }
    }
}