using System.Collections.Generic;

/// <summary>
/// Implementation of If/Else statements
/// </summary>
public class Conditional : Method
{
    /// <summary>
    /// Line to go to if conditional is true
    /// </summary>
    private int yeaLine;

    /// <summary>
    /// Line to go to if conditional is false
    /// </summary>
    private int nayLine;

    /// <summary>
    /// Name of the YeaNay variable that determines this conditional
    /// </summary>
    private string ynName;

    public Conditional(int _yeaLine, int _nayLine, string _ynName) : base("Conditional", "")
    {
        yeaLine = _yeaLine;
        nayLine = _nayLine;
        ynName = _ynName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        YeaNay yn = (YeaNay)variables[ynName];
        int nextLine = (yn.value) ? yeaLine : nayLine;
        return new ConditionalControl(nextLine);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        YeaNay yn = (YeaNay)variables[ynName];
        string toReturn = (yn.value) ? "Condition is Yea" : "Condition is Nay";
        return toReturn;
    }
}
