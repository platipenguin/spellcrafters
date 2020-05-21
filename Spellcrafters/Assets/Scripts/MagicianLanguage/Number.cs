/// <summary>
/// Variable representing a number. Can be a float or an integer.
/// </summary>
public class Number : Variable
{
    /// <summary>
    /// Number stored in this variable
    /// </summary>
    public float value;

    public Number(string name, float n)
    {
        varName = name;
        magicianType = Types.Number;
        value = n;
    }

    public override Variable Clone()
    {
        return new Number(varName, value);
    }

    public override string GetVoice()
    {
        return value.ToString();
    }
}
