namespace WargearTracker.Web.Models;

public class ArmyModel
{
    public Guid Id
    {
        get; set;
    }
    public string Name { get; set; } = string.Empty;
    public string Faction { get; set; } = string.Empty;
    public string Game { get; set; } = string.Empty;
    public List<MiniatureModel> Miniatures { get; set; } = new();
}

