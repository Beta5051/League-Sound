using System;
using System.Threading;
using LeagueSound.Models;

namespace LeagueSound.Services;

public interface ILiveClientDataService
{
    public event EventHandler OnOnline;
    public event EventHandler OnOffline;
    public event EventHandler<GameData> OnUpdateData;

    public void StartListenData(CancellationToken cancellationToken);
}