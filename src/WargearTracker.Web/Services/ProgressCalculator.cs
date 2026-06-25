using WargearTracker.Web.Models;

namespace WargearTracker.Web.Services;

public static class ProgressCalculator
{
    public static int CalculateProgress(IEnumerable<MiniatureModel> miniatures)
    {
        var list = miniatures.ToList();
        var total = list.Count;
        var finished = list.Count(m => m.PaintStatus == "Finished");
        return total > 0 ? (finished * 100 / total) : 0;
    }
}