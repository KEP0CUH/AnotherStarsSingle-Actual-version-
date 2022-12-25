///////////////////////////////////////////
///     Created:    -
///     Author:     KEPOLLlblLLlKA
///     Updated:    25.12.2022
///     Tested:     Not
///////////////////////////////////////////

public static class ColorfulDebugLog
{
    public static           string          SetColor(this string message, Color color) => $"<color={color}>{message}</color>";
}
public enum Color
{
    Red = 1,
    Cyan,
    Blue,
    DarkBlue,
    LightBlue,
    Purple,
    Yellow,
    Green,
    Magenta
}


