using ColorButton;
using FsCheck;
using FsCheck.Xunit;

namespace ColorButton.Tests;

public class ComponentStateTests
{
    [Fact]
    public void Create_WithValidColors_SetsColorsAndIndex()
    {
        // Arrange
        var colors = new[] { "red", "green", "blue" };

        // Act
        var state = ComponentState.Create(colors, 1);

        // Assert
        Assert.Equal(colors, state.Colors);
        Assert.Equal(1, state.CurrentIndex);
        Assert.True(state.IsInteractive);
    }

    [Fact]
    public void Create_WithNullColors_UsesFallback()
    {
        // Act
        var state = ComponentState.Create(null, null);

        // Assert
        Assert.Equal(new[] { "gray" }, state.Colors);
        Assert.Equal(0, state.CurrentIndex);
        Assert.False(state.IsInteractive);
    }

    [Fact]
    public void Create_WithEmptyColors_UsesFallback()
    {
        // Act
        var state = ComponentState.Create(Array.Empty<string>(), null);

        // Assert
        Assert.Equal(new[] { "gray" }, state.Colors);
        Assert.Equal(0, state.CurrentIndex);
        Assert.False(state.IsInteractive);
    }

    [Fact]
    public void Create_WithNegativeIndex_ResetsToZero()
    {
        // Act
        var state = ComponentState.Create(new[] { "red", "green" }, -1);

        // Assert
        Assert.Equal(0, state.CurrentIndex);
    }

    [Fact]
    public void Create_WithOutOfBoundsIndex_ResetsToZero()
    {
        // Act
        var state = ComponentState.Create(new[] { "red", "green" }, 5);

        // Assert
        Assert.Equal(0, state.CurrentIndex);
    }

    [Property]
    public bool WithIndex_PreservesColors(string[] colors, int newIndex)
    {
        // Arrange
        var state = ComponentState.Create(colors, 0);

        // Act
        var newState = state.WithIndex(newIndex);

        // Assert
        return state.Colors.SequenceEqual(newState.Colors);
    }

    [Property]
    public bool WithColors_PreservesIndex(string[] colors, int index)
    {
        // Arrange
        var state = ComponentState.Create(colors, index);

        // Act
        var newState = state.WithColors(colors);

        // Assert
        return state.CurrentIndex == newState.CurrentIndex;
    }
}
