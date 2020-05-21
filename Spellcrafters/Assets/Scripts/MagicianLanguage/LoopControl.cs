/// <summary>
/// This type of variable tells a spell to move to a different line.
/// They are created by Loop and Conditional methods.
/// The player never interacts with ExecutionControl variables, they are purely backend constructs.
/// </summary>
public class LoopControl : Variable
{
    /// <summary>
    /// The 0-indexed line the spell should execute next
    /// </summary>
    public int goToLine;

    /// <summary>
    /// The 0-indexed line where the spell should go back to the start of the loop to see if looping should continue
    /// </summary>
    public int loopCheckLine;

    public LoopControl(int _goToLine, int _checkLine)
    {
        magicianType = Types.Loop;
        goToLine = _goToLine;
        loopCheckLine = _checkLine;
    }

    public override Variable Clone()
    {
        return new LoopControl(goToLine, loopCheckLine);
    }

    public override string GetVoice()
    {
        return "";
    }
}
