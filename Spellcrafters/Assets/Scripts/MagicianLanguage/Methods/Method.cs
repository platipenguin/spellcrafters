using System.Collections.Generic;
/// <summary>
/// Base class for all methods - those not made by the player.
/// </summary>
public abstract class Method
{
    /// <summary>
    /// The name of this method
    /// </summary>
    public string name;

    /// <summary>
    /// The name of the variable returned by the instance of this method
    /// If this is a void method, outputName is null
    /// </summary>
    public string outputName;

    public Method(string n, string output)
    {
        this.name = n;
        this.outputName = output;
    }

    /// <summary>
    /// Executes whatever function is performed by this method, using the arguments given to it.
    /// </summary>
    public abstract Variable Cast(Dictionary<string, Variable> variables);

    /// <summary>
    /// Returns a string that should be printed above the spellcaster as they cast this method.
    /// </summary>
    public abstract string GetVoice(Dictionary<string, Variable> variables);
}
