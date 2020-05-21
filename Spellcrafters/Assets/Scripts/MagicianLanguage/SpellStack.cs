using System.Collections;
using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// Object that acts as active memory for a spell.
/// Tracks variable objects, their names, and provides methods for changing and accessing them.
/// </summary>
public class SpellStack
{
    private Dictionary<string, Variable> data = new Dictionary<string, Variable>();

    /// <summary>
    /// Get the variable with the specified name.
    /// </summary>
    public Variable Get(string varName)
    {
        return data[varName];
    }

    /// <summary>
    /// Store the variable with the specified name.
    /// </summary>
    public void Set(string varName, Variable val)
    {
        if (data.ContainsKey(varName))
            data.Remove(varName);
        data[varName] =  val;
    }

    /// <summary>
    /// Initialize the stack with the given variables.
    /// </summary>
    public void Initialize(List<Variable> vars)
    {
        foreach (Variable v in vars)
            data[v.varName] = v;
    }

    /// <summary>
    /// Returns the amount of magic stored in all Mana variables in the stack.
    /// </summary>
    public float GandalfTotal()
    {
        float storedMagic = 0;
        foreach (KeyValuePair<string, Variable> entry in data)
        {
            if (entry.Value is Mana)
            {
                Mana m = (Mana)entry.Value;
                UnityEngine.Debug.Log(m.varName);
                UnityEngine.Debug.Log(m.gandalfs);
                storedMagic += m.gandalfs;
            }
        }
        return storedMagic;
    }

    /// <summary>
    /// Remove all variables from the stack.
    /// </summary>
    public void Clear()
    {
        data.Clear();
    }
}
