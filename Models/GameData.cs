using System;

namespace LeagueSound.Models;

public class GameData
{
    public ActivePlayer ActivePlayer { get; set; } = new();
    public GEvents Events { get; set; } = new();
}

public class ActivePlayer
{
    public string SummonerName { get; set; } = string.Empty;
}

public class GEvents
{
    public Event[] Events { get; set; } = Array.Empty<Event>();
}

public class Event
{
    public string EventName { get; set; } = string.Empty;
    public string? KillerName { get; set; }
} 