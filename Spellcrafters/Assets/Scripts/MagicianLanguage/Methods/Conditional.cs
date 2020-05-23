using System.Collections.Generic;

/// <summary>
/// Implementation of If/Else statements
/// </summary>
public class Conditional : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    /// <summary>
    /// Line to go to if conditional is true
    /// </summary>
    private int yeaLine;

    /// <summary>
    /// Line to go to if conditional is false
    /// </summary>
    private int nayLine;

    public Conditional(int _yeaLine, int _nayLine)
    {
        Name = "If";
        Output = new SpellComponent("", Types.Conditional);
        MethodComponents = new SpellComponent[] { new SpellComponent("yn", Types.YeaNay) };
        InitVarNames();
        yeaLine = _yeaLine;
        nayLine = _nayLine;
    }

    public override Variable Cast(SpellStack variables)
    {
        YeaNay yn = (YeaNay)variables.Get(VarNames["yn"]);
        int nextLine = (yn.value) ? yeaLine : nayLine;
        return new ConditionalControl(nextLine);
    }

    public override string GetVoice(SpellStack variables)
    {
        YeaNay yn = (YeaNay)variables.Get(VarNames["yn"]);
        string toReturn = (yn.value) ? "Condition is Yea" : "Condition is Nay";
        return toReturn;
    }
}
