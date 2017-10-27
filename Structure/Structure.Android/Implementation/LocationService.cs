using System;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Structure.Interface;
using Structure.Model;
using Android.Locations;
using Xamarin.Forms;
using Object = Java.Lang.Object;
using Plugin.Permissions;

[assembly: Dependency(typeof(Structure.Droid.Implementation.LocationService))]

namespace Structure.Droid.Implementation
{
  
    public class LocationService : Object, ILocationService, ILocationListener
    {
        /// <summary>
        /// The location manager
        /// </summary>
        readonly LocationManager _locationManager;

        /// <summary>
        /// The loc model
        /// </summary>
        private LocationModel _locModel = new LocationModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationService"/> class.
        /// </summary>
        public LocationService()
        {
            CrossPermissions.Current.RequestPermissionsAsync(new Plugin.Permissions.Abstractions.Permission[] { Plugin.Permissions.Abstractions.Permission.Location });

            Location GpsLocation; 
            Location NetworkLocation;
            _locationManager = (LocationManager) Forms.Context.GetSystemService(Context.LocationService);
            InitLocation();
            InitLocation();

            GpsLocation = _locationManager.GetLastKnownLocation(LocationManager.GpsProvider);            

            NetworkLocation = _locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);

            if (GpsLocation != null)
            {
                _locModel.Latitude = GpsLocation.Latitude.ToString();
                _locModel.Longitude = GpsLocation.Longitude.ToString();
            }
            else
            {
                if (NetworkLocation != null)
                {
                    _locModel.Latitude = NetworkLocation.Latitude.ToString();
                    _locModel.Longitude = NetworkLocation.Longitude.ToString();
                }
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
            return _locModel;        
        }

        /// <summary>
        /// Initializes the location.
        /// </summary>
        public void InitLocation()
        {
            if (_locationManager.AllProviders.Contains(LocationManager.NetworkProvider)
               && _locationManager.IsProviderEnabled(LocationManager.NetworkProvider))
            {
                _locationManager.RequestLocationUpdates(LocationManager.NetworkProvider, 2000, 5, this);
            }
            else
            {
                _locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 1, 5, this);

                if (_locationManager.AllProviders.Contains(LocationManager.GpsProvider)
                    &&
                    _locationManager.IsProviderEnabled(LocationManager.GpsProvider))
                {
                    _locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 20000, 5, this);

                }
            }
        }


        /// <summary>
        /// Called when the location has changed.
        /// </summary>
        /// <param name="location">The new location, as a Location object.</param>
        /// <remarks>
        /// <para tool="javadoc-to-mdoc">Called when the location has changed.
        /// </para>
        /// <para tool="javadoc-to-mdoc"> There are no restrictions on the use of the supplied Location object.</para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/location/LocationListener.html#onLocationChanged(android.location.Location)" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para>
        /// </remarks>
        /// <since version="Added in API level 1" />
        public void OnLocationChanged(Location location)
        {
            var latitude = location.Latitude;
            var longitude =location.Longitude;
            _locModel = (new LocationModel { Longitude = longitude.ToString(), Latitude = latitude.ToString() });            
            
        }


        /// <summary>
        /// Called when the provider is disabled by the user.
        /// </summary>
        /// <param name="provider">the name of the location provider associated with this
        /// update.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <remarks>
        /// <para tool="javadoc-to-mdoc">Called when the provider is disabled by the user. If requestLocationUpdates
        /// is called on an already disabled provider, this method is called
        /// immediately.</para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/location/LocationListener.html#onProviderDisabled(java.lang.String)" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para>
        /// </remarks>
        /// <since version="Added in API level 1" />
        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called when the provider is enabled by the user.
        /// </summary>
        /// <param name="provider">the name of the location provider associated with this
        /// update.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <remarks>
        /// <para tool="javadoc-to-mdoc">Called when the provider is enabled by the user.</para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/location/LocationListener.html#onProviderEnabled(java.lang.String)" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para>
        /// </remarks>
        /// <since version="Added in API level 1" />
        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Called when the provider status changes.
        /// </summary>
        /// <param name="provider">the name of the location provider associated with this
        /// update.</param>
        /// <param name="status"><c>
        ///   <see cref="F:Android.Locations.Availability.OutOfService" />
        /// </c> if the
        /// provider is out of service, and this is not expected to change in the
        /// near future; <c><see cref="F:Android.Locations.Availability.TemporarilyUnavailable" /></c> if
        /// the provider is temporarily unavailable but is expected to be available
        /// shortly; and <c><see cref="F:Android.Locations.Availability.Available" /></c> if the
        /// provider is currently available.</param>
        /// <param name="extras">an optional Bundle which will contain provider specific
        /// status variables.
        /// <para tool="javadoc-to-mdoc" /> A number of common key/value pairs for the extras Bundle are listed
        /// below. Providers that use any of the keys on this list must
        /// provide the corresponding value as described below.
        /// <list type="bullet"><item><term> satellites - the number of satellites used to derive the fix
        /// </term></item></list></param>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <remarks>
        /// <para tool="javadoc-to-mdoc">Called when the provider status changes. This method is called when
        /// a provider is unable to fetch a location or if the provider has recently
        /// become available after a period of unavailability.</para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/location/LocationListener.html#onStatusChanged(java.lang.String, int, android.os.Bundle)" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para>
        /// </remarks>
        /// <since version="Added in API level 1" />
        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }


    }
}