using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NET8.Experiments.Services;
using NET8.Experiments.ViewModels;
using NET8.Experiments.Views;
using Shared.Code;
using Splat;

namespace NET8.Experiments;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // var locationService = Locator.Current.GetService<ILocationService>();
        // if(locationService is null)
        //     throw new InvalidOperationException("Location service not found");
        var locationService = new LocationService();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(locationService)
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel(locationService)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}