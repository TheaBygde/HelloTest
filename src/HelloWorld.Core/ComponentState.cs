namespace HelloWorld.Core;

/// <summary>
/// Represents the state of the ColorButton component.
/// Holds the ordered list of colors and the index of the currently displayed color.
/// </summary>
public record ComponentState
{
    /// <summary>
    /// The ordered list of color values to cycle through.
    /// Must be non-empty; defaults to ["gray"] if empty or null is provided.
    /// </summary>
    public string[] Colors { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Zero-based index of the currently displayed color.
    /// Must satisfy: 0 ≤ CurrentIndex &lt; Colors.Length.
    /// </summary>
    public int CurrentIndex { get; init; }

    /// <summary>
    /// Indicates whether the component is interactive (i.e. was initialized with a valid, non-empty color list).
    /// False when the fallback "gray" color is in use.
    /// </summary>
    public bool IsInteractive { get; init; }

    /// <summary>
    /// Returns the color at the current index.
    /// </summary>
    public string GetCurrentColor() => Colors[CurrentIndex];

    /// <summary>
    /// Returns a new ComponentState with CurrentIndex set to a uniformly random
    /// valid index in [0, Colors.Length - 1]. Colors array is preserved unchanged.
    /// </summary>
    public ComponentState Randomize(Random rng)
    {
        int newIndex = rng.Next(Colors.Length);
        return this with { CurrentIndex = newIndex };
    }

    /// <summary>
    /// Advances CurrentIndex to (CurrentIndex + 1) % Colors.Length and returns the updated state.
    /// The Colors array is preserved unchanged.
    /// </summary>
    public ComponentState HandleClick()
    {
        int nextIndex = (CurrentIndex + 1) % Colors.Length;
        return this with { CurrentIndex = nextIndex };
    }

    /// <summary>
    /// Creates a ComponentState with validation applied.
    /// - If colors is null or empty, defaults to ["gray"] and sets IsInteractive = false.
    /// - If currentIndex is out of bounds, resets to 0.
    /// </summary>
    public static ComponentState Create(string[]? colors, int currentIndex = 0)
    {
        bool isInteractive;
        string[] resolvedColors;

        if (colors == null || colors.Length == 0)
        {
            resolvedColors = new[] { "gray" };
            isInteractive = false;
            currentIndex = 0;
        }
        else
        {
            resolvedColors = colors;
            isInteractive = true;

            // Reset out-of-bounds index to 0
            if (currentIndex < 0 || currentIndex >= resolvedColors.Length)
            {
                currentIndex = 0;
            }
        }

        return new ComponentState
        {
            Colors = resolvedColors,
            CurrentIndex = currentIndex,
            IsInteractive = isInteractive
        };
    }
}
