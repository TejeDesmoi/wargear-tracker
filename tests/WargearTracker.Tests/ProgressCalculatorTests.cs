using System;
using System.Collections.Generic;
using System.Text;
using WargearTracker.Web.Services;

namespace WargearTracker.Tests;
public class ProgressCalculatorTests
{

    [Fact]
    public void CalculateProgress_ArmyWithOutMinis_ReturnZero()
    {
        //Arrange 
        var miniatures = new List<MiniatureModel>();

        //Act
        var progress = ProgressCalculator.CalculateProgress(miniatures);

        //Assert
        Assert.Equal(0, progress);
    }

    [Fact]
    public void CalculateProgress_ArmyWithAllFinished_ReturnHundred()
    {
        //Arrange 
        var miniatures = new List<MiniatureModel>
        {
            new MiniatureModel { PaintStatus = "Finished" },
            new MiniatureModel { PaintStatus = "Finished" },
            new MiniatureModel { PaintStatus = "Finished" }
        };
        //Act
        var progress = ProgressCalculator.CalculateProgress(miniatures);
        //Assert
        Assert.Equal(100, progress);
    }

    [Fact]
    public void CalculateProgress_ArmyWithMinis_ReturnCorrectProgress()
    {
        //Arrange 
        var miniatures = new List<MiniatureModel>
        {
            new MiniatureModel { PaintStatus = "Finished" },
            new MiniatureModel { PaintStatus = "In Progress" },
            new MiniatureModel { PaintStatus = "Finished" },
            new MiniatureModel { PaintStatus = "Not Started" }
        };
        //Act
        var progress = ProgressCalculator.CalculateProgress(miniatures);
        //Assert
        Assert.Equal(50, progress);
    }
}
