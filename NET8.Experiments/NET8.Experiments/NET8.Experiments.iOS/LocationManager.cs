// using System;
// using System.Threading.Tasks;
// using CoreLocation;
// using Microsoft.Maui.Devices.Sensors;
// using Shared.Code;
//
// namespace NET8.Experiments.iOS;
//
// public class LocationManager : ILocationService
// {
//     private readonly CLLocationManager _locationManager;
//
//     public LocationManager()
//     {
//         _locationManager = new CLLocationManager();
//         _locationManager.AuthorizationChanged += (sender, e) =>
//         {
//             if (e.Status == CLAuthorizationStatus.AuthorizedWhenInUse ||
//                 e.Status == CLAuthorizationStatus.AuthorizedAlways)
//             {
//                 _locationManager.StartUpdatingLocation();
//             }
//             else
//             {
//                 _locationManager.StopUpdatingLocation();
//             }
//         };
//         
//         _locationManager.DidChangeAuthorization += (sender, e) =>
//         {
//             if (_locationManager.AuthorizationStatus == CLAuthorizationStatus.AuthorizedWhenInUse ||
//                 _locationManager.AuthorizationStatus == CLAuthorizationStatus.AuthorizedAlways)
//             {
//                 _locationManager.StartUpdatingLocation();
//             }
//             else
//             {
//                 _locationManager.StopUpdatingLocation();
//             }
//         };
//     }
//
//     public async Task<Location> GetLocationAsync()
//     {
//         // Check for Location Services
//         if (!CLLocationManager.LocationServicesEnabled)
//         {
//             // Location services are not enabled; inform the user
//             return null;
//         }
//
//         // Check authorization status
//         var status = _locationManager.AuthorizationStatus;
//
//         if (status == CLAuthorizationStatus.NotDetermined)
//         {
//             // Permission has not been asked yet, request it
//             _locationManager.RequestWhenInUseAuthorization();
//             // Wait for status change
//             await WaitForStatusChangeAsync();
//         }
//         else if (status == CLAuthorizationStatus.Denied || status == CLAuthorizationStatus.Restricted)
//         {
//             // Permission denied or restricted, handle accordingly
//             return null;
//         }
//
//         // At this point, we either have permission, or it's still not determined.
//         // We can try getting the current location with the understanding
//         // that if permission is still undetermined, the request will prompt the user.
//
//         // var locationTask = GetLocationAsyncInternal();
//         // if (await Task.WhenAny(locationTask, Task.Delay(10000)) == locationTask)
//         // {
//         //     // Task completed within timeout
//         //     return await locationTask;
//         // }
//         // else
//         // {
//         //     // Timeout logic here, could not get location in time
//         //     return null;
//         // }
//         Location location = null;
//         await Task.Run(async () =>
//         {
//             location = await GetLocationAsyncInternal();
//         });
//
//         return location;
//     }
//
//     private Task<Location> GetLocationAsyncInternal()
//     {
//         var tcs = new TaskCompletionSource<Location>();
//
//         _locationManager.LocationsUpdated += (sender, e) =>
//         {
//             // Last known location
//             var location = e.Locations[^1];
//             Console.WriteLine($"Location: {location.Coordinate.Latitude}, {location.Coordinate.Longitude}");
//             tcs.TrySetResult(new Location(location.Coordinate.Latitude, location.Coordinate.Longitude));
//             _locationManager.StopUpdatingLocation();
//         };
//
//         _locationManager.StartUpdatingLocation();
//
//         return tcs.Task;
//     }
//
//     private Task<CLAuthorizationStatus> WaitForStatusChangeAsync()
//     {
//         var tcs = new TaskCompletionSource<CLAuthorizationStatus>();
//
//         EventHandler<CLAuthorizationChangedEventArgs> handler = null;
//         handler = (sender, e) =>
//         {
//             _locationManager.AuthorizationChanged -= handler;
//             tcs.SetResult(e.Status);
//         };
//
//         _locationManager.AuthorizationChanged += handler;
//
//         return tcs.Task;
//     }
// }