using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
/// <summary>
/// Variable representing a direction between 0 and 360 degrees
/// </summary>
public class Direction : Variable
{
    /// <summary>
    /// The direction stored in this variable
    /// </summary>
    public float value;

    public Direction(string name, float val)
    {
        varName = name;
        magicianType = Types.Direction;
        value = val;
    }

    public override Variable Clone()
    {
        return new Direction(varName, value);
    }

    public override string GetVoice()
    {
        float modified = value * 10;
        float rounded = Mathf.RoundToInt(modified);
        float toPrint = rounded / 10;
        return $"{toPrint}'";
    }
}
