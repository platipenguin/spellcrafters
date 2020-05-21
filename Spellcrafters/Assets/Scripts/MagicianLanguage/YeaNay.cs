/// <summary>
/// Variable representing a boolean value
/// </summary>
public class YeaNay : Variable
{
    /// <summary>
    /// Boolean value stored in this variable
    /// </summary>
    public bool value;

    public YeaNay(string name, bool val)
    {
        varName = name;
        magicianType = Types.YeaNay;
        value = val;
    }

    public override Variable Clone()
    {
        return new YeaNay(varName, value);
    }

    public override string GetVoice()
    {
        return (value == true) ? "Yea" : "Nay";
    }
}
