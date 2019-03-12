using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;

namespace TimesheetMobileApp.Droid
{
    [Activity(Label = "TimesheetMobileApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,
        ILocationListener

    {
        public static Android.Locations.LocationManager LocationManager;

        public void OnLocationChanged(Location location)
        {
            TimesheetMobileApp.Models.GpsLocationModel.Latitude =
                 location.Latitude;
            TimesheetMobileApp.Models.GpsLocationModel.Longitude =
                   location.Longitude;
            TimesheetMobileApp.Models.GpsLocationModel.Altitude =
                location.Altitude;
        }
        

        public void OnProviderDisabled(string provider)
        {
           // throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
          // throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
           // throw new NotImplementedException();
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            //käynnistetään Gps -paikannus
            try
            {
                LocationManager = GetSystemService(
                    "location") as LocationManager;
                string Provider = LocationManager.GpsProvider;

                if (LocationManager.IsProviderEnabled(Provider))
                {
                    LocationManager.RequestLocationUpdates(
                        Provider, 2000, 1, this);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }



   
    }
}