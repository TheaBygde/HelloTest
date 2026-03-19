﻿﻿namespace ColorButton;

public class ColorButton
{
    private ComponentState _state;

    public ColorButton(string[]? colors = null)
    {
        _state = ComponentState.Create(colors);
    }

    public string GetCurrentColor()
    {
        return GetCurrentColor(_state);
    }

    public string GetCurrentColor(ComponentState state)
    {
        return state.Colors[state.CurrentIndex];
    }

    public void HandleClick()
    {
        if (_state.IsInteractive)
        {
            var newIndex = (_state.CurrentIndex + 1) % _state.Colors.Length;
            _state = _state.WithIndex(newIndex);
        }
    }

    public bool IsInteractive => _state.IsInteractive;

    public string[] Colors => _state.Colors;

    public int CurrentIndex => _state.CurrentIndex;
}


