namespace LeagueSound.Models;

public enum GameEvent
{
    GameStart,
    ChampionKill,
    MultiKill,
    DragonKill
}

public static class GameEventExtender
{
    public static string GetEventName(this GameEvent @event) => @event switch
    {
        GameEvent.MultiKill => "Multikill",
        _ => @event.ToString()
    };
}