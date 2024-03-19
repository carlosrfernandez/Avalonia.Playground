using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Devices.Sensors;
using NET8.Experiments.Services;
using Shared.Code;

namespace NET8.Experiments.ViewModels;

public partial class MainViewModel(ILocationService locationService) : ObservableObject
{
    [ObservableProperty]
    private string _userCoordinates = "No location yet";

    public MainViewModel() : this(new LocationService())
    {
    }
    
    public void StartListeningForLocations()
    {
        locationService.LocationChanged += LocationServiceOnLocationChanged;
        locationService.StartListening();
    }

    private void LocationServiceOnLocationChanged(GeolocationLocationChangedEventArgs e)
    {
        UserCoordinates = $"Latitude: {e.Location.Latitude}, Longitude: {e.Location.Longitude}"; 
    }

    public void StopListeningForLocations()
    {
        locationService.StopListening();
    }
}