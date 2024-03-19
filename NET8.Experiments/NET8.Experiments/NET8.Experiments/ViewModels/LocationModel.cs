namespace NET8.Experiments.ViewModels;

public class LocationModel
{
    public LocationModel(double latitude, double longitude, float course)
    {
        Latitude = latitude;
        Longitude = longitude;
        Course = course;
    }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public float Course { get; }
}