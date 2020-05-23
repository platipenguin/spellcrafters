using System.Collections.Generic;

/// <summary>
/// Determines if two variables are the same or different
/// Checks for equality if _bool is true, inequality if _bool is false
/// </summary>
public class CheckEquality : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public CheckEquality()
    {
        Name = "CheckEquality";
        Output = new SpellComponent("", Types.YeaNay);
        MethodComponents = new SpellComponent[] { 
            new SpellComponent("var1", Types.Object),
            new SpellComponent("var2", Types.Mana),
            new SpellComponent("equal", Types.YeaNay)
        };
        InitVarNames();
    }

    public override Variable Cast(SpellStack variables)
    {
        bool res = false;
        YeaNay yn = (YeaNay)variables.Get(VarNames["equal"]);
        if (variables.Get(VarNames["var1"]).magicianType != variables.Get(VarNames["var2"]).magicianType)
            return new YeaNay(Output.name, false == yn.value);
        switch(variables.Get(VarNames["var1"]).magicianType)
        {
            case Types.Number:
                Number var1Number = (Number)variables.Get(VarNames["var1"]);
                Number var2Number = (Number)variables.Get(VarNames["var2"]);
                res = (var1Number.value == var2Number.value);
                break;
            case Types.Direction:
                Direction var1Dir = (Direction)variables.Get(VarNames["var1"]);
                Direction var2Dir = (Direction)variables.Get(VarNames["var2"]);
                res = (var1Dir.value == var2Dir.value);
                break;
            case Types.YeaNay:
                YeaNay var1Y = (YeaNay)variables.Get(VarNames["var1"]);
                YeaNay var2Y = (YeaNay)variables.Get(VarNames["var2"]);
                res = (var1Y.value == var2Y.value);
                break;
            case Types.Point:
                Point var1P = (Point)variables.Get(VarNames["var1"]);
                Point var2p = (Point)variables.Get(VarNames["var2"]);
                res = (var1P.x == var2p.x && var1P.y == var2p.y);
                break;
            case Types.Area:
                Area var1A = (Area)variables.Get(VarNames["var1"]);
                Area var2A = (Area)variables.Get(VarNames["var2"]);
                res = (var1A.originX == var2A.originX && var1A.originY == var2A.originY && var1A.endX == var2A.endX && var1A.endY == var2A.endY);
                break;
            default:
                res = variables.Get(VarNames["var1"]).varName == variables.Get(VarNames["var2"]).varName;
                break;
        }
        return new YeaNay(Output.name, res == yn.value);
    }

    public override string GetVoice(SpellStack variables)
    {
        YeaNay yn = (YeaNay)variables.Get(VarNames["equal"]);
        string operatorString = (yn.value == true) ? " is the same " : " is not the same ";
        return $"{variables.Get(VarNames["var1"]).GetVoice()}{operatorString}{variables.Get(VarNames["var2"]).GetVoice()}";
    }
}
