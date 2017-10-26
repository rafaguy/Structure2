using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;


namespace Structure.Droid
{
    /// <summary>
    /// 
    /// </summary>
    [Activity(Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {

        /// <summary>
        /// Called when [resume].
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();

            StartActivity(typeof(MainActivity));
        }

    }
}