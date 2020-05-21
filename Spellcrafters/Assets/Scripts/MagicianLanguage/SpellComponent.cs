using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Marker used to identify a component required by a spell.
/// Contains a type, and a name for the component as set by the user.
/// </summary>
public class SpellComponent
{
    /// <summary>
    /// The type of this component
    /// </summary>
    public Types type;

    /// <summary>
    /// The name this component will be referred by
    /// </summary>
    public string name;

    public SpellComponent(string n, Types t)
    {
        this.name = n;
        this.type = t;
    }
}

public enum Types
{
    Number,
    YeaNay,
    Direction,
    Object,
    Point,
    Area,
    Mana,
    Group,
    Loop,
    Conditional
}