using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for addition, subtraction, division, and multiplication methods
/// </summary>
public abstract class OperationMethod : Method
{
    /// <summary>
    /// First number to operate on
    /// </summary>
    public string numberA;

    /// <summary>
    /// Second number to operate on
    /// </summary>
    public string numberB;

    /// <summary>
    /// String of the operator for the method
    /// </summary>
    public string operatorString;

    public OperationMethod(string methodName, string outputName, string _xName, string _yName, string _operatorString) : base(methodName, outputName)
    {
        numberA = _xName;
        numberB = _yName;
        operatorString = _operatorString;
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Number varA = (Number)variables[numberA];
        Number varB = (Number)variables[numberB];
        return $"{outputName} = {varA.GetVoice()}{operatorString}{varB.GetVoice()}";
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
    public AddNumbers(string outputName, string xName, string yName) : base("Add", outputName, xName, yName, "+") { }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Number varA = (Number)variables[this.numberA];
        Number varB = (Number)variables[this.numberB];
        return new Number(this.outputName, varA.value + varB.value);
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
    public SubtractNumbers(string outputName, string xName, string yName) : base("Subtract", outputName, xName, yName, "-") { }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Number varA = (Number)variables[this.numberA];
        Number varB = (Number)variables[this.numberB];
        return new Number(this.outputName, varA.value - varB.value);
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
    public MultiplyNumbers(string outputName, string xName, string yName) : base("Multiply", outputName, xName, yName, "*") { }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Number varA = (Number)variables[this.numberA];
        Number varB = (Number)variables[this.numberB];
        return new Number(this.outputName, varA.value * varB.value);
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
    public DivideNumbers(string outputName, string xName, string yName) : base("Divide", outputName, xName, yName, "/") { }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Number varA = (Number)variables[this.numberA];
        Number varB = (Number)variables[this.numberB];
        return new Number(this.outputName, varA.value / varB.value);
    }
}
