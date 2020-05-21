using UnityEngine;

/// <summary>
/// Base class for all variable type objects that can be passed between methods.
/// </summary>
public abstract class Variable
{
    /// <summary>
    /// The name of this variable
    /// </summary>
    public string varName;

    /// <summary>
    /// The type of this variable in the magician language
    /// </summary>
    public Types magicianType;

    /// <summary>
    /// Return a new object that is identical to the variable
    /// </summary>
    public abstract Variable Clone();

    /// <summary>
    /// Returns a string that should be printed above the spellcaster when they cast a method with this variable.
    /// </summary>
    public abstract string GetVoice();
}
