using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the target by adding force to it in the specified direction. The amount of magic applied to this
/// method determines the magnitude of the force.
/// </summary>
public class Move : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public Move()
    {
        Name = "Move";
        Output = new SpellComponent("", Types.Null);
        MethodComponents = new SpellComponent[] { 
            new SpellComponent("object", Types.Object),
            new SpellComponent("direction", Types.Direction),
            new SpellComponent("fuel", Types.Mana) };
        InitVarNames();
    }

    public override Variable Cast(SpellStack variables)
    {
        Obj target = (Obj)variables.Get(VarNames["object"]);
        Direction dir = (Direction)variables.Get(VarNames["direction"]);
        Mana fuel = (Mana)variables.Get(VarNames["fuel"]);
        float theta = dir.value * Mathf.Deg2Rad;
        Vector2 forceVector = new Vector2(Mathf.Cos(theta) * fuel.gandalfs, Mathf.Sin(theta) * fuel.gandalfs);
        target.obj.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Impulse);
        return null;
    }

    public override string GetVoice(SpellStack variables)
    {
        Direction dir = (Direction)variables.Get(VarNames["direction"]);
        Mana fuel = (Mana)variables.Get(VarNames["fuel"]);
        return $"Telekinesis.Move({VarNames["object"]}, {dir.GetVoice()}, {fuel.GetVoice()})";
    }
}
