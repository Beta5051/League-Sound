using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using LeagueSound.Services;
using LeagueSound.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using MvvmDialogs;

namespace LeagueSound;

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Ioc.Default.ConfigureServices(new ServiceCollection()
            .AddSingleton<ILiveClientDataService, LiveClientDataService>()
            .AddSingleton<IDialogService, DialogService>()
            .AddTransient<MainWindowViewModel>()
            .BuildServiceProvider());
    }
}

