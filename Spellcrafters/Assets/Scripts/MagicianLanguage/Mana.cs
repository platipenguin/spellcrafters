using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A variable representing a quantum of magical energy
/// </summary>
public class Mana : Variable
{
    /// <summary>
    /// The amount of magical energy stores in this Mana variable
    /// </summary>
    public float gandalfs;

    public Mana(string _name, float _num)
    {
        varName = _name;
        magicianType = Types.Mana;
        gandalfs = _num;
    }

    public Mana(string _name) : this(_name, 0) { }

    /// <summary>
    /// Transfer all the magic from this variable to the supplied mana variable
    /// </summary>
    /// <param name="m"></param>
    public void TransferAll(Mana m)
    {
        m.gandalfs += this.gandalfs;
        this.gandalfs = 0;
    }

    /// <summary>
    /// Transfer n gandalfs of magic from this variable to the supplied variable
    /// </summary>
    /// <param name="n"></param>
    /// <param name="m"></param>
    public void Transfer(float n, Mana m)
    {
        this.gandalfs -= n;
        m.gandalfs += n;
    }

    public override Variable Clone()
    {
        return new Mana(varName);
    }

    public override string GetVoice()
    {
        return $"{gandalfs} gandalfs";
    }
}
