
public static class ColorfulDebugLog
{
    public static string SetColor(this string mes, Color color) => $"<color={color}>{mes}</color>";
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


