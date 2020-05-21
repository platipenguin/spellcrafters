using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loops through a set of code if a YeaNay variable is Yea, and exits the loop when the variable is Nay
/// </summary>
public class Loop : Method
{
    /// <summary>
    /// Index of spell line that forms the start of the loop
    /// </summary>
    private int entryIndex;

    /// <summary>
    /// Index of the spell line where the loop ends, and execution should return to this method to determine
    /// if the loop should continue, or exit
    /// </summary>
    private int loopCheckIndex;

    /// <summary>
    /// Name of the variable that determines if the loop continues or exits
    /// </summary>
    private string ynName;

    public Loop(int _loopIndex, int _checkIndex, string _ynName) : base("Loop While", "")
    {
        entryIndex = _loopIndex;
        loopCheckIndex = _checkIndex;
        ynName = _ynName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        YeaNay yn = (YeaNay)variables[ynName];
        int targetIndex = (yn.value == true) ? entryIndex : loopCheckIndex;
        int shouldCheck = (yn.value == true) ? loopCheckIndex : -1;
        return new LoopControl(targetIndex, shouldCheck);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        YeaNay yn = (YeaNay)variables[ynName];
        return (yn.value == true) ? "Enter Loop" : "Exit Loop";
    }
}
