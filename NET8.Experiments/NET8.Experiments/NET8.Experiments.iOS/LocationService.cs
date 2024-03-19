// using System;
// using CoreLocation;
// using Microsoft.Maui.ApplicationModel;
// using NET8.Experiments.Views;
//
// namespace NET8.Experiments.iOS;
//
// public class LocationService
// {
//     public event EventHandler<LocationModel> LocationChanged;
//     public event EventHandler<string> StatusChanged;
//
//     private CLLocationManager _iosLocationManager;
//     public void Initialize()
//     {
//         OnStatusChanged($"LocationService->Initialize");
//         _iosLocationManager ??= new CLLocationManager()
//         {
//             DesiredAccuracy = CLLocation.AccuracyBest,
//             DistanceFilter = CLLocationDistance.FilterNone,
//             PausesLocationUpdatesAutomatically = false
//         };
//
//         MainThread.BeginInvokeOnMainThread(async () =>
//         {
//             var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
//             if (status != PermissionStatus.Granted)
//             {
//                 OnStatusChanged("Permission for location is not granted, we can't get location updates");
//                 return;
//             }
//             _iosLocationManager.RequestAlwaysAuthorization();
//             _iosLocationManager.LocationsUpdated += LocationsUpdated;
//             _iosLocationManager.StartUpdatingLocation();
//         });
//     }
//     
//     protected virtual void OnLocationChanged(LocationModel e)
//     {
//         LocationChanged?.Invoke(this, e);
//     }
//
//     protected virtual void OnStatusChanged(string e)
//     {
//         StatusChanged?.Invoke(this, e);
//     }
//     
//     private void LocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
//     {
//         var locations = e.Locations;
//         LocationChanged?.Invoke(this, new LocationModel(
//             locations[^1].Coordinate.Latitude,
//             locations[^1].Coordinate.Longitude,
//             (float)locations[^1].Course));
//     }
// }