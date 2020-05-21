using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returned by Conditional methods to direct execution of a spell
/// </summary>
public class ConditionalControl : Variable
{
    /// <summary>
    /// Line where spell execution should jump to.
    /// </summary>
    public int goToLine;

    public ConditionalControl(int _goToLine)
    {
        magicianType = Types.Conditional;
        goToLine = _goToLine;
    }

    public override Variable Clone()
    {
        return new ConditionalControl(goToLine);
    }

    public override string GetVoice()
    {
        return "";
    }
}
