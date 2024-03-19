using System;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using Shared.Code;

namespace NET8.Experiments.Services;

public class LocationService : ILocationService
{

    private readonly Timer _timer;
    private Action<GeolocationLocationChangedEventArgs>? _locationChanged;

    public LocationService()
    {
        _timer = new Timer(2000);
        _timer.Elapsed += TimerOnElapsed;
    }

    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    { 
        _locationChanged?.Invoke(new GeolocationLocationChangedEventArgs(new Location(e.SignalTime.Minute,e.SignalTime.Second)));
    }

    public event Action<GeolocationLocationChangedEventArgs>? LocationChanged
    {
        add => this._locationChanged += value;
        remove => this._locationChanged -= value;
    }

    public void StartListening()
    {
        Task.Run(async () =>
        {
            Console.WriteLine("StartListening");
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            Geolocation.LocationChanged += HandleLocationChanged();
            await Geolocation.StartListeningForegroundAsync(new GeolocationListeningRequest(GeolocationAccuracy.Default,
                TimeSpan.FromSeconds(1)));

            var result = await Geolocation.GetLocationAsync();
            _locationChanged?.Invoke(new GeolocationLocationChangedEventArgs(result));
        });
    }

    private EventHandler<GeolocationLocationChangedEventArgs> HandleLocationChanged()
    {
        return (sender, args) =>
        {
            Console.WriteLine("Location changed");
            _locationChanged?.Invoke(args);
        };
    }


    public void StopListening()
    {
        Geolocation.LocationChanged -= HandleLocationChanged();

    }
}