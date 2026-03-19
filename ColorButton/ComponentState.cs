namespace ColorButton;

public record ComponentState
{
    public string[] Colors { get; }
    public int CurrentIndex { get; }
    public bool IsInteractive { get; }

    private static readonly string[] FallbackColors = ["gray"];

    public ComponentState(string[] colors, int currentIndex, bool isInteractive = true)
    {
        Colors = colors;
        CurrentIndex = currentIndex;
        IsInteractive = isInteractive;
    }

    public static ComponentState Create(string[]? colors = null, int? currentIndex = null)
    {
        var colorList = colors ?? FallbackColors;
        
        if (colorList.Length == 0)
        {
            colorList = FallbackColors;
        }

        var index = currentIndex ?? 0;
        
        if (index < 0 || index >= colorList.Length)
        {
            index = 0;
        }

        var isInteractive = colorList.Length > 0 && colorList != FallbackColors;
        return new ComponentState(colorList, index, isInteractive);
    }

    public ComponentState WithColors(string[] newColors)
    {
        var isInteractive = newColors.Length > 0 && newColors != FallbackColors;
        return new ComponentState(newColors, CurrentIndex, isInteractive);
    }

    public ComponentState WithIndex(int newIndex)
    {
        return new ComponentState(Colors, newIndex, IsInteractive);
    }
}
