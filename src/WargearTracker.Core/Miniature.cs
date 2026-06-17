using System;
using System.Collections.Generic;
using System.Text;

namespace WargearTracker.Core;
public class Miniature
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UnitType { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public PaintStatus PaintStatus { get; set; } = PaintStatus.Unboxed;
    public Guid ArmyId { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
