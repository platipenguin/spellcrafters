using System.Collections.Generic;
/// <summary>
/// Base class for all methods - those not made by the player.
/// </summary>
public abstract class Method
{
    /// <summary>
    /// The name of this method
    /// </summary>
    public abstract string Name { get; set; }

    /// <summary>
    /// Stores the type of the method's output (as defined by the method), and the name of the output, if there is one,
    /// defined by the player.
    /// </summary>
    public abstract SpellComponent Output { get; set; }

    /// <summary>
    /// Markers for components required by this method to execute.
    /// i.e. The information that is contained in ReduceHealth(Object target, Mana fuel)
    /// Used by MethodUI to know what variables need to go into the method.
    /// </summary>
    public abstract SpellComponent[] MethodComponents { get; set; }

    /// <summary>
    /// Contains the names of the actual variables that will be fed into the method when it is run.
    /// Stored as a dictionary where key=Name of the component, value=Name of the component as a variable.
    /// </summary>
    public abstract Dictionary<string, string> VarNames { get; set; }

    /// <summary>
    /// Executes whatever function is performed by this method, using the arguments given to it.
    /// </summary>
    public abstract Variable Cast(SpellStack variables);

    /// <summary>
    /// Returns a string that should be printed above the spellcaster as they cast this method.
    /// </summary>
    public abstract string GetVoice(SpellStack variables);

    /// <summary>
    /// Initializes the value of every variable in the method by setting them to an empty string.
    /// </summary>
    protected void InitVarNames()
    {
        foreach (SpellComponent comp in MethodComponents)
        {
            VarNames[comp.name] = "";
        }
    }
}
