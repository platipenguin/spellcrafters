using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Variable for storing and accessing WorldObjects inside of spells.
/// </summary>
public class Obj : Variable
{
    /// <summary>
    /// The object represented by this variable
    /// </summary>
    public WorldObject obj;

    public float health {
        get { return obj.health; }
        set { obj.health = value; }
    }

    public float magic {
        get { return obj.magic; }
        set { obj.magic = value; }
    }

    public float maxHealth {
        get { return obj.maxHealth; }
        set { obj.maxHealth = value; }
    }

    public float maxMagic {
        get { return obj.maxMagic; }
        set { obj.maxMagic = value; }
    }

    public float magicDefense {
        get { return obj.magicDefense; }
        set { obj.magicDefense = value; }
    }

    public bool alive {
        get { return obj.alive; }
    }

    public Obj(string _name, WorldObject _entity)
    {
        varName = _name;
        magicianType = Types.Object;
        obj = _entity;
    }

    public float ConsumeMana(float amount)
    {
        return obj.ConsumeMana(amount);
    }

    public override Variable Clone()
    {
        return new Obj(varName, obj);
    }

    public override string GetVoice()
    {
        return varName;
    }
}
