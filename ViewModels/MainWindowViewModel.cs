using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LeagueSound.Models;
using LeagueSound.Services;
using MvvmDialogs;

namespace LeagueSound.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    
    public MainWindowViewModel(ILiveClientDataService liveClientDataService, IDialogService dialogService)
    {
        liveClientDataService.OnOnline += delegate { IsGameOnline = true; };
        liveClientDataService.OnOffline += delegate { IsGameOnline = false; };
        liveClientDataService.OnUpdateData += OnUpdateGameData;
        liveClientDataService.StartListenData(CancellationToken.None);
        _dialogService = dialogService;
    }

    public ObservableCollection<SoundEvent> SoundEvents { get; } = new();

    [ObservableProperty] private bool _isGameOnline;

    private int _saveGameEventsCount = -1;

    private void OnUpdateGameData(object? sender, GameData data)
    {
        if (_saveGameEventsCount >= 0)
            for (var i = _saveGameEventsCount; i < data.Events.Events.Length; i++)
                foreach (var @event in SoundEvents.Where(@event =>
                             @event.Enable && @event.GameEvent.GetEventName() == data.Events.Events[i].EventName))
                {
                    if (data.Events.Events[i].KillerName != null &&
                        data.Events.Events[i].KillerName != data.ActivePlayer.SummonerName) continue;
                    var player = new MediaPlayer();
                    player.Open(new Uri(@event.SoundFile));
                    player.Play();
                }
        _saveGameEventsCount = data.Events.Events.Length;
    }

    [RelayCommand]
    private void ShowCreateSoundEventDialog()
    {
        var viewModel = new CreateSoundEventViewModel(_dialogService);
        if (_dialogService.ShowDialog(this, viewModel) == true)
            SoundEvents.Add(new SoundEvent
            {
                Name = viewModel.Name,
                GameEvent = viewModel.GameEvent,
                SoundFile = viewModel.SoundFile
            });
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveSelectedSoundEventCommand))]
    private int _selectedSoundEventIndex = -1;

    [RelayCommand(CanExecute = nameof(CanRemoveSelectedSoundEvent))]
    private void RemoveSelectedSoundEvent() => SoundEvents.RemoveAt(SelectedSoundEventIndex);

    private bool CanRemoveSelectedSoundEvent() => SelectedSoundEventIndex >= 0;
}