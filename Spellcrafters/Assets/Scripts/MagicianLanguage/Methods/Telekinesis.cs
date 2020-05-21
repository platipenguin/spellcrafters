using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the target by adding force to it in the specified direction. The amount of magic applied to this
/// method determines the magnitude of the force.
/// </summary>
public class Move : Method
{
    private string targetName;

    private string directionName;

    private string fuelName;

    public Move(string _target, string _direction, string _fuel) : base("Move", "")
    {
        targetName = _target;
        directionName = _direction;
        fuelName = _fuel;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Obj target = (Obj)variables[targetName];
        Direction dir = (Direction)variables[directionName];
        Mana fuel = (Mana)variables[fuelName];
        float theta = dir.value * Mathf.Deg2Rad;
        Vector2 forceVector = new Vector2(Mathf.Cos(theta) * fuel.gandalfs, Mathf.Sin(theta) * fuel.gandalfs);
        target.obj.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Impulse);
        return null;
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Direction dir = (Direction)variables[directionName];
        Mana fuel = (Mana)variables[fuelName];
        return $"Telekinesis.Move({targetName}, {dir.GetVoice()}, {fuel.GetVoice()})";
    }
}
