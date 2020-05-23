using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for addition, subtraction, division, and multiplication methods
/// </summary>
public abstract class OperationMethod : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    /// <summary>
    /// String of the operator for the method
    /// </summary>
    protected string operatorString;

    public OperationMethod(string methodName, string _operatorString)
    {
        Name = methodName;
        Output = new SpellComponent("", Types.Number);
        MethodComponents = new SpellComponent[] { new SpellComponent("numberA", Types.Number), new SpellComponent("numberB", Types.Number) };
        InitVarNames();
        operatorString = _operatorString;
    }

    public override string GetVoice(SpellStack variables)
    {
        Number varA = (Number)variables.Get(VarNames["numberA"]);
        Number varB = (Number)variables.Get(VarNames["numberB"]);
        return $"{Output.name} = {varA.GetVoice()}{operatorString}{varB.GetVoice()}";
    }
}

/// <summary>
/// Method for adding two numbers together
/// </summary>
public class AddNumbers : OperationMethod
{
    /// <summary>
    /// Returns X + Y
    /// </summary>
    public AddNumbers() : base("Add", "+") { }

    public override Variable Cast(SpellStack variables)
    {
        Number varA = (Number)variables.Get(VarNames["numberA"]);
        Number varB = (Number)variables.Get(VarNames["numberB"]);
        return new Number(Output.name, varA.value + varB.value);
    }
}

/// <summary>
/// Method for getting the difference of two numbers
/// </summary>
public class SubtractNumbers : OperationMethod
{
    /// <summary>
    /// Returns X - Y
    /// </summary>
    public SubtractNumbers(string outputName, string xName, string yName) : base("Subtract", "-") { }

    public override Variable Cast(SpellStack variables)
    {
        Number varA = (Number)variables.Get(VarNames["numberA"]);
        Number varB = (Number)variables.Get(VarNames["numberB"]);
        return new Number(Output.name, varA.value - varB.value);
    }
}

/// <summary>
/// Method for getting the product of two numbers
/// </summary>
public class MultiplyNumbers : OperationMethod
{
    /// <summary>
    /// Returns X * Y
    /// </summary>
    public MultiplyNumbers(string outputName, string xName, string yName) : base("Multiply", "*") { }

    public override Variable Cast(SpellStack variables)
    {
        Number varA = (Number)variables.Get(VarNames["numberA"]);
        Number varB = (Number)variables.Get(VarNames["numberB"]);
        return new Number(Output.name, varA.value * varB.value);
    }
}

/// <summary>
/// Method for getting the division product of two numbers
/// </summary>
public class DivideNumbers : OperationMethod
{
    /// <summary>
    /// Returns X / Y
    /// </summary>
    public DivideNumbers(string outputName, string xName, string yName) : base("Divide", "/") { }

    public override Variable Cast(SpellStack variables)
    {
        Number varA = (Number)variables.Get(VarNames["numberA"]);
        Number varB = (Number)variables.Get(VarNames["numberB"]);
        return new Number(Output.name, varA.value / varB.value);
    }
}
