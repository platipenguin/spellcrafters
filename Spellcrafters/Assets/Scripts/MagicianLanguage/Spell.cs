using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// Represents a spell that the player has created
/// </summary>
public class Spell
{
    /// <summary>
    /// The name of this spell
    /// </summary>
    public string name;

    /// <summary>
    /// Sequence of lines that make up this spell
    /// </summary>
    public List<Method> lines;

    /// <summary>
    /// The components required to begin casting this spell
    /// </summary>
    public List<SpellComponent> components;

    /// <summary>
    /// Literals that are pre-loaded into this spell, for instance, by a Number constructor
    /// </summary>
    public List<Variable> initial;

    /// <summary>
    /// Storage space for variables that are manipulated during spell execution
    /// </summary>
    public SpellStack memory = new SpellStack();

    /// <summary>
    /// The next line to be executed by this spell
    /// Set to -1 when the spell is not being actively cast
    /// </summary>
    public int currentLine = -1;

    /// <summary>
    /// When execution enters a loop, index of the Loop method
    /// </summary>
    private int loopStart = -1;

    /// <summary>
    /// When execution enters a loop, index where execution should return to loopStart to see if loop should continue
    /// </summary>
    private int loopCheck = -1;

    public Spell(string _name, List<Method> _lines, List<SpellComponent> _components, List<Variable>_init)
    {
        name = _name;
        lines = _lines;
        components = _components;
        initial = _init;
        Initialize();
    }

    /// <summary>
    /// Initialize the spell with any components required so that it can start casting, and sets the casting pointer to the first line
    /// </summary>
    public void BeginCast(List<Variable> componentValues)
    {
        memory.Initialize(componentValues);
        currentLine = 0;
    }

    /// <summary>
    /// Returns the text from speaking the next line of the spell
    /// </summary>
    public string SpeakNext()
    {
        return lines[currentLine].GetVoice(memory);
    }

    /// <summary>
    /// Executes the next line of the spell
    /// </summary>
    /// <returns>A CastResult object containing information about the cast</returns>
    public CastResult CastNext()
    {
        string aberration = "";
        Variable newVar = null;
        try
        {
            newVar = lines[currentLine].Cast(memory);
        }
        catch (AberrationException ae)
        {
            aberration = ae.Message;
        }
        if (newVar == null)
        {
            currentLine += 1;
            if (currentLine == loopCheck)
                currentLine = loopStart;
        }
        else if (newVar.magicianType == Types.Loop)
        {
            LoopControl loopControl = (LoopControl)newVar;
            loopStart = currentLine;
            loopCheck = loopControl.loopCheckLine;
            currentLine = loopControl.goToLine;
        }
        else if (newVar.magicianType == Types.Conditional)
        {
            ConditionalControl condControl = (ConditionalControl)newVar;
            currentLine = condControl.goToLine;
        }
        else
        {
            AddToMemory(newVar);
            currentLine += 1;
            if (currentLine == loopCheck)
                currentLine = loopStart;
        }

        float storedMagic = memory.GandalfTotal();

        if (currentLine == lines.Count || aberration != "")
            Reset();
        return new CastResult(storedMagic, aberration);
    }

    /// <summary>
    /// Return a string in the form of "Type1 component1, Type2 component2..."
    /// </summary>
    public string ComponentsToString()
    {
        string toReturn = "";
        for (int i = 0; i < components.Count; i++)
        {
            switch(components[i].type)
            {
                case Types.Number:
                    toReturn += "Num ";
                    break;
                case Types.YeaNay:
                    toReturn += "Yn ";
                    break;
                case Types.Direction:
                    toReturn += "Dir ";
                    break;
                case Types.Point:
                    toReturn += "Pt ";
                    break;
                case Types.Area:
                    toReturn += "Area ";
                    break;
                case Types.Object:
                    toReturn += "Obj ";
                    break;
            }
            toReturn += components[i].name;
            if (i != components.Count - 1)
                toReturn += ", ";
        }
        return toReturn;
    }

    /// <summary>
    /// Loads the initial variables into the spell's memory
    /// </summary>
    private void Initialize()
    {
        memory.Initialize(initial);
    }

    /// <summary>
    /// Handles adding a variable to memory so overwriting old values doesn't cause issues
    /// </summary>
    private void AddToMemory(Variable v)
    {
        memory.Set(v.varName, v);
    }

    /// <summary>
    /// Resets the variables inside this spell and sets the execution pointer back to the first line.
    /// </summary>
    public void Reset()
    {
        memory.Clear();
        Initialize();
        currentLine = -1;
    }
    
}

/// <summary>
/// Storage object for conveying information about the results of casting a line of a spell.
/// </summary>
public class CastResult
{
    /// <summary>
    /// Amount of magic held after executing the most recent line.
    /// </summary>
    public float storedMagic;

    /// <summary>
    /// Describes an aberration if one occurred, or "" if no aberration occurred during casting the last line.
    /// </summary>
    public string aberration;

    public CastResult(float _storedMagic, string _aberration)
    {
        storedMagic = _storedMagic;
        aberration = _aberration;
    }
}
