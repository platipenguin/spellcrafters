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
    /// <summary>
    /// The number that will be placed in the Number variable
    /// </summary>
    private string numberName;

    public NewNumber(string outputName, string _number) : base("New Number", outputName)
    {
        numberName = _number;
    }

    /// <summary>
    /// Creates and returns a new Number Variable
    /// </summary>
    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Number number = (Number)variables[numberName];
        return new Number(outputName, number.value);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Number number = (Number)variables[numberName];
        return $"{outputName} = {number.GetVoice()}";
    }
}

/// <summary>
/// Method for constructing a new YeaNay variable
/// </summary>
public class NewYeaNay : Method
{
    /// <summary>
    /// Name of the input variable
    /// </summary>
    private string inputName;

    public NewYeaNay(string outputName, string yeaNay) : base("New YeaNay", outputName)
    {
        inputName = yeaNay;
    }

    /// <summary>
    /// Creates and returns a new YeaNay Variable
    /// </summary>
    /// <param name="variables"></param>
    /// <returns></returns>
    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        YeaNay yeaNay = (YeaNay)variables[this.inputName];
        return new YeaNay(this.outputName, yeaNay.value);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        YeaNay yeaNay = (YeaNay)variables[this.inputName];
        return $"{outputName} = {yeaNay.GetVoice()}";
    }
}

/// <summary>
/// Method for constructing a new Direction variable
/// </summary>
public class NewDirection : Method
{
    /// <summary>
    /// Name of the input variable
    /// </summary>
    private string inputName;

    public NewDirection(string outputName, string direction) : base("New Direction", outputName)
    {
        this.inputName = direction;
    }

    /// <summary>
    /// Creates and returns a new Direction Variable
    /// </summary>
    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Direction dir = (Direction)variables[this.inputName];
        return new Direction(this.outputName, dir.value);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Direction dir = (Direction)variables[this.inputName];
        return $"{outputName} = {dir.GetVoice()}";
    }
}

/// <summary>
/// Method for constructing a new Point variable
/// </summary>
public class NewPoint : Method
{
    /// <summary>
    /// Name of the x coordinate variable
    /// </summary>
    private string xName;

    /// <summary>
    /// Name of the y coordinate variable
    /// </summary>
    private string yName;

    public NewPoint(string outputName, string x, string y) : base("New Point", outputName)
    {
        xName = x;
        yName = y;
    }

    /// <summary>
    /// Creates and returns a new Point variable
    /// </summary>
    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Number xVal = (Number)variables[this.xName];
        Number yVal = (Number)variables[this.yName];
        return new Point(this.outputName, xVal.value, yVal.value);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Number xVal = (Number)variables[this.xName];
        Number yVal = (Number)variables[this.yName];
        return $"{outputName} = ({xVal.GetVoice()}, {yVal.GetVoice()})";
    }
}

/// <summary>
/// Method for constructing a new Area variable
/// </summary>
public class NewArea : Method
{
    /// <summary>
    /// Name of the origin x coordinate variable
    /// </summary>
    private string x1Name;

    /// <summary>
    /// Name of the end x coordinate variable
    /// </summary>
    private string x2Name;

    /// <summary>
    /// Name of the origin y coordinate variable
    /// </summary>
    private string y1Name;

    /// <summary>
    /// Name of the end y coordinate variable
    /// </summary>
    private string y2Name;

    public NewArea(string outputName, string x1, string x2, string y1, string y2) : base("New Area", outputName)
    {
        this.x1Name = x1;
        this.x2Name = x2;
        this.y1Name = y1;
        this.y2Name = y2;
    }

    /// <summary>
    /// Creates and returns a new Area variable
    /// </summary>
    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Number x1Val = (Number)variables[this.x1Name];
        Number x2Val = (Number)variables[this.x2Name];
        Number y1Val = (Number)variables[this.y1Name];
        Number y2Val = (Number)variables[this.y2Name];
        return new Area(this.outputName, x1Val.value, y1Val.value, x2Val.value, y2Val.value);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Number x1Val = (Number)variables[this.x1Name];
        Number x2Val = (Number)variables[this.x2Name];
        Number y1Val = (Number)variables[this.y1Name];
        Number y2Val = (Number)variables[this.y2Name];
        return $"{outputName} = <({x1Val.GetVoice()}, {y1Val.GetVoice()})({x2Val.GetVoice()}, {y2Val.GetVoice()})>";
    }
}

/// <summary>
/// Method for constructing a new, empty mana variable
/// </summary>
public class NewMana : Method
{
    public NewMana(string outputName) : base("New Mana", outputName) { }

    /// <summary>
    /// Creates and returns a new, empty Mana variable
    /// </summary>
    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        return new Mana(outputName, 0);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        return $"{outputName} = 0 gandalfs";
    }
}
