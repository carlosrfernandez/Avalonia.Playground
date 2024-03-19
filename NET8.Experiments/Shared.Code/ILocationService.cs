using Microsoft.Maui.Devices.Sensors;

namespace Shared.Code;

public interface ILocationService
{
    event Action<GeolocationLocationChangedEventArgs> LocationChanged;
    void StartListening();
    void StopListening();
}