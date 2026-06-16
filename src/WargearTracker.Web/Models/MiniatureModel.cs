public class MiniatureModel
{
    public Guid Id
    {
        get; set;
    }
    public string Name { get; set; } = string.Empty;
    public string UnitType { get; set; } = string.Empty;
    public int Quantity
    {
        get; set;
    }
    public string PaintStatus { get; set; } = string.Empty;
}