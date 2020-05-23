using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loops through a set of code if a YeaNay variable is Yea, and exits the loop when the variable is Nay
/// </summary>
public class Loop : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    /// <summary>
    /// Index of spell line that forms the start of the loop
    /// </summary>
    private int entryIndex;

    /// <summary>
    /// Index of the spell line where the loop ends, and execution should return to this method to determine
    /// if the loop should continue, or exit
    /// </summary>
    private int loopCheckIndex;

    public Loop(int _entry, int _check)
    {
        Name = "Loop While";
        Output = new SpellComponent("", Types.Loop);
        MethodComponents = new SpellComponent[] { new SpellComponent("yn", Types.YeaNay) };
        InitVarNames();
        entryIndex = _entry;
        loopCheckIndex = _check;
    }

    public override Variable Cast(SpellStack variables)
    {
        YeaNay yn = (YeaNay)variables.Get(VarNames["yn"]);
        int targetIndex = (yn.value == true) ? entryIndex : loopCheckIndex;
        int shouldCheck = (yn.value == true) ? loopCheckIndex : -1;
        return new LoopControl(targetIndex, shouldCheck);
    }

    public override string GetVoice(SpellStack variables)
    {
        YeaNay yn = (YeaNay)variables.Get(VarNames["yn"]);
        return (yn.value == true) ? "Enter Loop" : "Exit Loop";
    }
}
