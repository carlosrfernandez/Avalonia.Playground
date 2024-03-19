using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NET8.Experiments.ViewModels;

namespace NET8.Experiments.Views;

public partial class MainView : UserControl
{
    private MainViewModel _viewModel => DataContext as MainViewModel ??
                                        throw new InvalidOperationException("DataContext is not MainViewModel");
    public MainView()
    {
        InitializeComponent();
    }
    
    private void StartListeningForLocations(object? sender, RoutedEventArgs e)
    {
        _viewModel.StartListeningForLocations();
    }

    private void StopListeningForLocations(object? sender, RoutedEventArgs e)
    {
          _viewModel.StopListeningForLocations();
    }
}