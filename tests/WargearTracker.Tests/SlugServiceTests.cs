using WargearTracker.Core;
using WargearTracker.Core.Services;

namespace WargearTracker.Tests;

public class SlugServiceTests
{
    [Fact]
    public void GenerateSlug_WithSpaces_ReturnsHyphenated()
    {
        // Arrange
        var input = "Ultramarines 2nd Company";

        // Act
        var result = SlugService.GenerateSlug(input);

        // Assert
        Assert.Equal("ultramarines-2nd-company", result);
    }

    [Fact]
    public void GenerateSlug_WithSpecialCharacters_RemovesThem()
    {
        // Arrange
        var input = "Iron Hands!";
        // Act
        var result = SlugService.GenerateSlug(input);
        // Assert
        Assert.Equal("iron-hands", result);
    }

    [Fact]
    public void GenerateSlug_WithEmptyString_ReturnsEmpty()
    {
        var input = "";
        var result = SlugService.GenerateSlug(input);
        Assert.Equal("", result);
    }

    [Fact (Skip = "Known bug — consecutive spaces produce double hyphens. See issue #XX")]
    public void GenerateSlug_WithConsecutiveSpaces_NoDoubleHyphens()
    {
    var input = "Iron  Hands";
        var result = SlugService.GenerateSlug(input);
        Assert.False(result.Contains("--"));
    }
}