using HelloWorld.Core;

namespace HelloWorld.Tests;

public class ComponentStateTests
{
    [Fact]
    public void Create_WithValidColors_SetsCurrentIndexToZero()
    {
        var state = ComponentState.Create(new[] { "red", "green", "blue" });

        Assert.Equal(0, state.CurrentIndex);
    }

    [Fact]
    public void Create_WithValidColors_IsInteractive()
    {
        var state = ComponentState.Create(new[] { "red", "green" });

        Assert.True(state.IsInteractive);
    }

    [Fact]
    public void Create_WithNullColors_DefaultsToGrayAndNotInteractive()
    {
        var state = ComponentState.Create(null);

        Assert.Equal(new[] { "gray" }, state.Colors);
        Assert.Equal(0, state.CurrentIndex);
        Assert.False(state.IsInteractive);
    }

    [Fact]
    public void Create_WithEmptyColors_DefaultsToGrayAndNotInteractive()
    {
        var state = ComponentState.Create(Array.Empty<string>());

        Assert.Equal(new[] { "gray" }, state.Colors);
        Assert.Equal(0, state.CurrentIndex);
        Assert.False(state.IsInteractive);
    }

    [Fact]
    public void Create_WithOutOfBoundsIndex_ResetsToZero()
    {
        var state = ComponentState.Create(new[] { "red", "green" }, currentIndex: 99);

        Assert.Equal(0, state.CurrentIndex);
    }

    [Fact]
    public void Create_WithNegativeIndex_ResetsToZero()
    {
        var state = ComponentState.Create(new[] { "red", "green" }, currentIndex: -1);

        Assert.Equal(0, state.CurrentIndex);
    }

    [Fact]
    public void Create_WithValidIndex_PreservesIndex()
    {
        var state = ComponentState.Create(new[] { "red", "green", "blue" }, currentIndex: 2);

        Assert.Equal(2, state.CurrentIndex);
    }

    [Fact]
    public void GetCurrentColor_ReturnsColorAtCurrentIndex()
    {
        var state = ComponentState.Create(new[] { "red", "green", "blue" }, currentIndex: 1);

        Assert.Equal("green", state.GetCurrentColor());
    }

    [Fact]
    public void GetCurrentColor_AfterHandleClick_ReturnsNextColor()
    {
        var state = ComponentState.Create(new[] { "red", "green", "blue" });
        var next = state.HandleClick();

        Assert.Equal("green", next.GetCurrentColor());
    }

    [Fact]
    public void Randomize_SingleColorList_AlwaysReturnsIndexZero()
    {
        var state = ComponentState.Create(new[] { "red" });
        var rng = new Random(42);

        for (int i = 0; i < 20; i++)
        {
            var result = state.Randomize(rng);
            Assert.Equal(0, result.CurrentIndex);
        }
    }

    [Fact]
    public void Randomize_PreservesColorsArray()
    {
        var colors = new[] { "red", "green", "blue" };
        var state = ComponentState.Create(colors);
        var rng = new Random(42);

        var result = state.Randomize(rng);

        Assert.Same(state.Colors, result.Colors);
    }

    [Fact]
    public void Randomize_PreservesIsInteractive()
    {
        var state = ComponentState.Create(new[] { "red", "green", "blue" });
        var rng = new Random(42);

        var result = state.Randomize(rng);

        Assert.Equal(state.IsInteractive, result.IsInteractive);
    }

    [Fact]
    public void Randomize_IndexAlwaysInBounds()
    {
        var state = ComponentState.Create(new[] { "red", "green", "blue", "yellow" });
        var rng = new Random(0);

        for (int i = 0; i < 100; i++)
        {
            var result = state.Randomize(rng);
            Assert.InRange(result.CurrentIndex, 0, state.Colors.Length - 1);
        }
    }
}
