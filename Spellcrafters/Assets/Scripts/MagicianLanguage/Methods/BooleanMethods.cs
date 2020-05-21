using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Determines if two variables are the same or different
/// Checks for equality if _bool is true, inequality if _bool is false
/// </summary>
public class CheckEquality : Method
{
    /// <summary>
    /// Name of the first variable to check
    /// </summary>
    private string var1Name;

    /// <summary>
    /// Name of the second variable to check
    /// </summary>
    private string var2Name;

    /// <summary>
    /// Name of the boolean value this method is looking for
    /// </summary>
    private string boolName;

    public CheckEquality(string outputName, string _var1, string _var2, string _bool) : base("Check Equality", outputName)
    {
        var1Name = _var1;
        var2Name = _var2;
        boolName = _bool;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        bool res = false;
        YeaNay yn = (YeaNay)variables[boolName];
        if (variables[var2Name].magicianType != variables[var2Name].magicianType)
            return new YeaNay(outputName, false == yn.value);
        switch(variables[var1Name].magicianType)
        {
            case Types.Number:
                Number var1Number = (Number)variables[var1Name];
                Number var2Number = (Number)variables[var2Name];
                res = (var1Number.value == var2Number.value);
                break;
            case Types.Direction:
                Direction var1Dir = (Direction)variables[var1Name];
                Direction var2Dir = (Direction)variables[var2Name];
                res = (var1Dir.value == var2Dir.value);
                break;
            case Types.YeaNay:
                YeaNay var1Y = (YeaNay)variables[var1Name];
                YeaNay var2Y = (YeaNay)variables[var2Name];
                res = (var1Y.value == var2Y.value);
                break;
            case Types.Point:
                Point var1P = (Point)variables[var1Name];
                Point var2p = (Point)variables[var2Name];
                res = (var1P.x == var2p.x && var1P.y == var2p.y);
                break;
            case Types.Area:
                Area var1A = (Area)variables[var1Name];
                Area var2A = (Area)variables[var2Name];
                res = (var1A.originX == var2A.originX && var1A.originY == var2A.originY && var1A.endX == var2A.endX && var1A.endY == var2A.endY);
                break;
            default:
                res = variables[var1Name].varName == variables[var2Name].varName;
                break;
        }
        return new YeaNay(outputName, res == yn.value);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        YeaNay yn = (YeaNay)variables[boolName];
        string operatorString = (yn.value == true) ? " is the same " : " is not the same ";
        return $"{variables[var1Name].GetVoice()}{operatorString}{variables[var2Name].GetVoice()}";
    }
}
