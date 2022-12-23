using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using LeagueSound.Models;

namespace LeagueSound.Services;

public class LiveClientDataService : ILiveClientDataService
{
    public event EventHandler? OnOnline;
    public event EventHandler? OnOffline;
    public event EventHandler<GameData>? OnUpdateData;

    private bool _isOnline;

    private readonly HttpClient _httpClient = new(new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = delegate { return true; }
    })
    {
        BaseAddress = new Uri("https://127.0.0.1:2999/liveclientdata/"),
        Timeout = TimeSpan.FromSeconds(3)
    };

    public async void StartListenData(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var data = await _httpClient.GetFromJsonAsync<GameData>("allgamedata", cancellationToken);
                if (!_isOnline)
                {
                    _isOnline = true;
                    OnOnline?.Invoke(this, EventArgs.Empty);
                }
                if (data != null) OnUpdateData?.Invoke(this, data);
            }
            catch
            {
                if (_isOnline)
                {
                    _isOnline = false;
                    OnOffline?.Invoke(this, EventArgs.Empty);
                }
            }
            await Task.Delay(100, cancellationToken);
        }
    }
}