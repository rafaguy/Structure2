using CoreLocation;
using System;
using System.Collections.Generic;
using System.Text;
using Structure.Model;
using Xamarin.Forms;
using Structure.iOS.Implementation;
using Structure.Interface;
using System.Globalization;

[assembly: Dependency(typeof(LocationServiceEventArgs))]

namespace Structure.iOS.Implementation
{
    /// <summary>
    /// Get the location in IOS
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    /// <seealso cref="Structure.Interface.ILocationService" />
    public class LocationServiceEventArgs : EventArgs, ILocationService
    {
        /// <summary>
        /// The loc model
        /// </summary>
        LocationModel locModel = new LocationModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationServiceEventArgs"/> class.
        /// </summary>
        public LocationServiceEventArgs()
        {
            CLLocationManager locationManager = new CLLocationManager();
            
            locationManager.RequestWhenInUseAuthorization();
            locationManager.RequestAlwaysAuthorization();
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                if (CLLocationManager.LocationServicesEnabled)
                {
                    locationManager.DesiredAccuracy = 1;
                    locationManager.LocationsUpdated += LocationUpdatedEventArgs;
                    locationManager.StartUpdatingLocation();
                    if (locationManager.Location != null)
                    {
                        locModel.Latitude = locationManager.Location.Coordinate.Latitude.ToString();
                        locModel.Longitude = locationManager.Location.Coordinate.Longitude.ToString();
                    }
                
                }
                return false;
            });
        }

        /// <summary>
        /// Locations the updated event arguments.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CLLocationsUpdatedEventArgs"/> instance containing the event data.</param>
        private void LocationUpdatedEventArgs(object sender, CLLocationsUpdatedEventArgs e)
        {
            if (e.Locations.Length > 0)
            {
                
                var loc = e.Locations[0];
                    locModel = (new LocationModel
                    { Longitude =  loc.Coordinate.Longitude.ToString(),
                        Latitude = loc.Coordinate.Latitude.ToString()});
                
            }
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <returns>
        /// location model that contains parameters for the location
        /// </returns>
        public LocationModel GetLocation()
        {
            return locModel;
        }

    }
}
