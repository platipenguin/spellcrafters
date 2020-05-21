using System.Numerics;
/// <summary>
/// Variable representing a rectangular area in the game.
/// </summary>
public class Area : Variable
{
    /// <summary>
    /// X coordinate of the upper left vertex of this area
    /// </summary>
    public float originX;

    /// <summary>
    /// Y coordinate of the upper left vertex of this area
    /// </summary>
    public float originY;

    /// <summary>
    /// X coordinate of the lower left vertex of this area
    /// </summary>
    public float endX;

    /// <summary>
    /// Y coordinate of the lower left verex of this area
    /// </summary>
    public float endY;

    /// <summary>
    /// Square area of this Area
    /// </summary>
    public float size;

    /// <summary>
    /// The center point of this area
    /// </summary>
    public Vector2 center;

    public Area(string name, float startX, float startY, float finishX, float finishY)
    {
        varName = name;
        magicianType = Types.Area;
        originX = startX;
        originY = startY;
        endX = finishX;
        endY = finishY;
        size = (finishX - startX) * (startY - finishY);
        float width = endX - originX;
        float height = originY - endY;
        center = new Vector2(endX - (width / 2), finishY - (height / 2));
    }

    /// <summary>
    /// Returns a Group containing all the WorldObjects currently inside this area
    /// </summary>
    public Group GetObjectsInside(string returnName)
    {
        // TODO: implement
        return new Group(returnName, Types.Object, 0);
    }

    public override Variable Clone()
    {
        return new Area(varName, originX, originY, endX, endY);
    }

    public override string GetVoice()
    {
        return $"<({originX}, {originY})({endX}, {endY})>";
    }
}
