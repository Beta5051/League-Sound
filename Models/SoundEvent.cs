namespace LeagueSound.Models;

public class SoundEvent
{
    public string Name { get; set; } = string.Empty;
    public GameEvent GameEvent { get; set; }
    public string SoundFile { get; set; } = string.Empty;
    public bool Enable { get; set; } = true;

    public override string ToString() => $"{Name} ({GameEvent})";
}