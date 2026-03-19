﻿namespace ColorButton.Tests;

public class ColorButtonTests
{
    [Fact]
    public void Constructor_WithValidColors_InitializesWithFirstColor()
    {
        // Arrange
        var colors = new[] { "red", "green", "blue" };

        // Act
        var button = new ColorButton(colors);

        // Assert
        Assert.Equal("red", button.GetCurrentColor());
        Assert.Equal(0, button.CurrentIndex);
        Assert.True(button.IsInteractive);
    }

    [Fact]
    public void Constructor_WithNullColors_UsesFallbackGray()
    {
        // Act
        var button = new ColorButton(null);

        // Assert
        Assert.Equal("gray", button.GetCurrentColor());
        Assert.Equal(0, button.CurrentIndex);
        Assert.False(button.IsInteractive);
    }

    [Fact]
    public void Constructor_WithEmptyColors_UsesFallbackGray()
    {
        // Act
        var button = new ColorButton(Array.Empty<string>());

        // Assert
        Assert.Equal("gray", button.GetCurrentColor());
        Assert.Equal(0, button.CurrentIndex);
        Assert.False(button.IsInteractive);
    }

    [Fact]
    public void HandleClick_AdvancesToNextColor()
    {
        // Arrange
        var button = new ColorButton(new[] { "red", "green", "blue" });
        Assert.Equal("red", button.GetCurrentColor());

        // Act
        button.HandleClick();

        // Assert
        Assert.Equal("green", button.GetCurrentColor());
        Assert.Equal(1, button.CurrentIndex);
    }

    [Fact]
    public void HandleClick_WrapsAroundToFirstColor()
    {
        // Arrange
        var button = new ColorButton(new[] { "red", "green", "blue" });
        button.HandleClick(); // green
        button.HandleClick(); // blue

        // Act
        button.HandleClick();

        // Assert
        Assert.Equal("red", button.GetCurrentColor());
        Assert.Equal(0, button.CurrentIndex);
    }

    [Fact]
    public void HandleClick_WhenNotInteractive_DoesNotChangeColor()
    {
        // Arrange
        var button = new ColorButton(Array.Empty<string>());
        Assert.Equal("gray", button.GetCurrentColor());
        Assert.False(button.IsInteractive);

        // Act
        button.HandleClick();

        // Assert
        Assert.Equal("gray", button.GetCurrentColor());
        Assert.Equal(0, button.CurrentIndex);
    }

    [Fact]
    public void GetCurrentColor_WithSpecificState_ReturnsCorrectColor()
    {
        // Arrange
        var button = new ColorButton();
        var state = ComponentState.Create(new[] { "red", "green", "blue" }, 2);

        // Act
        var color = button.GetCurrentColor(state);

        // Assert
        Assert.Equal("blue", color);
    }
}
