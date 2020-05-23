using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This file contains methods that the player can use inside of a spell to create new variables.
// i.e. these methods are called if the player were to write the line 'Number x = 1.5'

/// <summary>
/// Method for constructing a new Number variable
/// </summary>
public class NewNumber : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public NewNumber()
    {
        Name = "New Number";
        Output = new SpellComponent("", Types.Number);
        MethodComponents = new SpellComponent[] { new SpellComponent("n", Types.Number)};
        InitVarNames();
    }

    /// <summary>
    /// Creates and returns a new Number Variable
    /// </summary>
    public override Variable Cast(SpellStack variables)
    {
        Number number = (Number)variables.Get(VarNames["n"]);
        return new Number(Output.name, number.value);
    }

    public override string GetVoice(SpellStack variables)
    {
        Number number = (Number)variables.Get(VarNames["n"]);
        return $"{Output.name} = {number.GetVoice()}";
    }
}

/// <summary>
/// Method for constructing a new YeaNay variable
/// </summary>
public class NewYeaNay : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public NewYeaNay()
    {
        Name = "New YeaNay";
        Output = new SpellComponent("", Types.YeaNay);
        MethodComponents = new SpellComponent[] { new SpellComponent("y", Types.YeaNay) };
        InitVarNames();
    }

    /// <summary>
    /// Creates and returns a new YeaNay Variable
    /// </summary>
    /// <param name="variables"></param>
    /// <returns></returns>
    public override Variable Cast(SpellStack variables)
    {
        YeaNay yeaNay = (YeaNay)variables.Get(VarNames["y"]);
        return new YeaNay(Output.name, yeaNay.value);
    }

    public override string GetVoice(SpellStack variables)
    {
        YeaNay yeaNay = (YeaNay)variables.Get(VarNames["y"]);
        return $"{Output.name} = {yeaNay.GetVoice()}";
    }
}

/// <summary>
/// Method for constructing a new Direction variable
/// </summary>
public class NewDirection : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public NewDirection()
    {
        Name = "New Direction";
        Output = new SpellComponent("", Types.Direction);
        MethodComponents = new SpellComponent[] { new SpellComponent("d", Types.Direction) };
        InitVarNames();
    }

    /// <summary>
    /// Creates and returns a new Direction Variable
    /// </summary>
    public override Variable Cast(SpellStack variables)
    {
        Direction dir = (Direction)variables.Get(VarNames["d"]);
        return new Direction(Output.name, dir.value);
    }

    public override string GetVoice(SpellStack variables)
    {
        Direction dir = (Direction)variables.Get(VarNames["d"]);
        return $"{Output.name} = {dir.GetVoice()}";
    }
}

/// <summary>
/// Method for constructing a new Point variable
/// </summary>
public class NewPoint : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public NewPoint()
    {
        Name = "New Point";
        Output = new SpellComponent("", Types.Direction);
        MethodComponents = new SpellComponent[] { new SpellComponent("x", Types.Number), new SpellComponent("y", Types.Number) };
        InitVarNames();
    }

    /// <summary>
    /// Creates and returns a new Point variable
    /// </summary>
    public override Variable Cast(SpellStack variables)
    {
        Number xVal = (Number)variables.Get(VarNames["x"]);
        Number yVal = (Number)variables.Get(VarNames["y"]);
        return new Point(Output.name, xVal.value, yVal.value);
    }

    public override string GetVoice(SpellStack variables)
    {
        Number xVal = (Number)variables.Get(VarNames["x"]);
        Number yVal = (Number)variables.Get(VarNames["y"]);
        return $"{Output.name} = ({xVal.GetVoice()}, {yVal.GetVoice()})";
    }
}

/// <summary>
/// Method for constructing a new Area variable
/// </summary>
public class NewArea : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public NewArea()
    {
        Name = "New Point";
        Output = new SpellComponent("", Types.Area);
        MethodComponents = new SpellComponent[] { 
            new SpellComponent("oriX", Types.Number),
            new SpellComponent("oriY", Types.Number),
            new SpellComponent("endX", Types.Number),
            new SpellComponent("endY", Types.Number)
        };
        InitVarNames();
    }

    /// <summary>
    /// Creates and returns a new Area variable
    /// </summary>
    public override Variable Cast(SpellStack variables)
    {
        Number x1Val = (Number)variables.Get(VarNames["oriX"]);
        Number x2Val = (Number)variables.Get(VarNames["oriY"]);
        Number y1Val = (Number)variables.Get(VarNames["endX"]);
        Number y2Val = (Number)variables.Get(VarNames["endY"]);
        return new Area(Output.name, x1Val.value, y1Val.value, x2Val.value, y2Val.value);
    }

    public override string GetVoice(SpellStack variables)
    {
        Number x1Val = (Number)variables.Get(VarNames["oriX"]);
        Number x2Val = (Number)variables.Get(VarNames["oriY"]);
        Number y1Val = (Number)variables.Get(VarNames["endX"]);
        Number y2Val = (Number)variables.Get(VarNames["endY"]);
        return $"{Output.name} = <({x1Val.GetVoice()}, {y1Val.GetVoice()})({x2Val.GetVoice()}, {y2Val.GetVoice()})>";
    }
}

/// <summary>
/// Method for constructing a new, empty mana variable
/// </summary>
public class NewMana : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public NewMana()
    {
        Name = "New Point";
        Output = new SpellComponent("", Types.Area);
        MethodComponents = new SpellComponent[0];
        InitVarNames();
    }

    /// <summary>
    /// Creates and returns a new, empty Mana variable
    /// </summary>
    public override Variable Cast(SpellStack variables)
    {
        return new Mana(Output.name, 0);
    }

    public override string GetVoice(SpellStack variables)
    {
        return $"{Output.name} = 0 gandalfs";
    }
}
