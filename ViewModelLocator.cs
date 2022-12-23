using CommunityToolkit.Mvvm.DependencyInjection;
using LeagueSound.ViewModels;

namespace LeagueSound;

public class ViewModelLocator
{
    public static MainWindowViewModel MainWindow => Ioc.Default.GetRequiredService<MainWindowViewModel>();
}