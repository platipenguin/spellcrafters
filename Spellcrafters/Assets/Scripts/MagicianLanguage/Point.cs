/// <summary>
/// Variable representing a point in the game world
/// </summary>
public class Point : Variable
{
    /// <summary>
    /// The x coordinate of this point
    /// </summary>
    public float x;

    /// <summary>
    /// The y coordinate of this point
    /// </summary>
    public float y;

    public Point(string _name, float _x, float _y)
    {
        varName = _name;
        magicianType = Types.Point;
        x = _x;
        y = _y;
    }

    public override Variable Clone()
    {
        return new Point(varName, x, y);
    }

    public override string GetVoice()
    {
        return $"({x}, {y})";
    }
}
