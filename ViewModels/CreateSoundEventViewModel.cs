using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LeagueSound.Models;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.OpenFile;

namespace LeagueSound.ViewModels;

public partial class CreateSoundEventViewModel : ObservableObject, IModalDialogViewModel
{
    private readonly IDialogService _dialogService;

    public CreateSoundEventViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    [ObservableProperty]
    private bool? _dialogResult;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CreateSoundEventCommand))]
    private string _name = string.Empty;
    
    public static GameEvent[] GameEvents => Enum.GetValues<GameEvent>();

    [ObservableProperty]
    private GameEvent _gameEvent;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CreateSoundEventCommand))]
    private string _soundFile = string.Empty;
    
    [RelayCommand]
    private void ShowOpenSoundFileDialog()
    {
        var settings = new OpenFileDialogSettings();
        if (_dialogService.ShowOpenFileDialog(this, settings) == true) SoundFile = settings.FileName;
    }

    [RelayCommand(CanExecute = nameof(CanCreateSoundEvent))]
    private void CreateSoundEvent() => DialogResult = true;
    
    private bool CanCreateSoundEvent() => Name != string.Empty && SoundFile != string.Empty;
}