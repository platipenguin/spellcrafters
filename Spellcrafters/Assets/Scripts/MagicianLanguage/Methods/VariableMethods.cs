using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This file contains methods attached to non-WorldObject variables, such as accessing parameters and firing object-related methods

/// <summary>
/// Returns the number of gandalfs stored in a mana variable
/// </summary>
public class GetGandalfs : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public GetGandalfs()
    {
        Name = "GetGandalfs";
        Output = new SpellComponent("", Types.Number);
        MethodComponents = new SpellComponent[] { new SpellComponent("mana", Types.Mana) };
        InitVarNames();
    }

    public override Variable Cast(SpellStack variables)
    {
        Mana m = (Mana)variables.Get(VarNames["mana"]);
        return new Number(Output.name, m.gandalfs);
    }

    public override string GetVoice(SpellStack variables)
    {
        Mana m = (Mana)variables.Get(VarNames["mana"]);
        return $"{Output.name} = {m.gandalfs}";
    }
}

/// <summary>
/// Method for constructing a new mana variable by channeling
/// </summary>
public class ChannelMana : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public ChannelMana()
    {
        Name = "ChannelMana";
        Output = new SpellComponent("", Types.Mana);
        MethodComponents = new SpellComponent[] { new SpellComponent("amount", Types.Number) };
        InitVarNames();
    }

    /// <summary>
    /// Creates and returns a new Area variable
    /// </summary>
    public override Variable Cast(SpellStack variables)
    {
        Number amount = (Number)variables.Get(VarNames["amount"]);
        if (amount.value < 0)
            throw new AberrationException(AberrationException.NEGATIVE_GANDALFS);
        Obj caster = (Obj)variables.Get("=player");
        float gandalfs = caster.ConsumeMana(amount.value);
        return new Mana(Output.name, gandalfs);
    }

    public override string GetVoice(SpellStack variables)
    {
        Number amount = (Number)variables.Get(VarNames["amount"]);
        return $"{Output.name} = self.Channel({amount.GetVoice()} gandalfs)";
    }
}

/// <summary>
/// Method for transfering all magic from one variable to another
/// </summary>
public class TransferAll : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public TransferAll()
    {
        Name = "TransferAll";
        Output = new SpellComponent("", Types.Null);
        MethodComponents = new SpellComponent[] {
            new SpellComponent("from", Types.Mana),
            new SpellComponent("to", Types.Mana),
            new SpellComponent("amount", Types.Number)
        };
        InitVarNames();
    }

    /// <summary>
    /// Creates and returns a new Area variable
    /// </summary>
    public override Variable Cast(SpellStack variables)
    {
        Mana from = (Mana)variables.Get(VarNames["from"]);
        Mana to = (Mana)variables.Get(VarNames["to"]);
        to.gandalfs += from.gandalfs;
        from.gandalfs = 0;
        return null;
    }

    public override string GetVoice(SpellStack variables)
    {
        return $"{VarNames["from"]}.TransferAll({VarNames["to"]})";
    }
}

/// <summary>
/// Returns the x coordinate of a point
/// </summary>
public class GetPointX : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public GetPointX()
    {
        Name = "GetPointX";
        Output = new SpellComponent("", Types.Number);
        MethodComponents = new SpellComponent[] { new SpellComponent("p", Types.Point) };
        InitVarNames();
    }

    public override Variable Cast(SpellStack variables)
    {
        Point p = (Point)variables.Get(VarNames["p"]);
        return new Number(Output.name, p.x);
    }

    public override string GetVoice(SpellStack variables)
    {
        Point p = (Point)variables.Get(VarNames["p"]);
        return $"{Output.name} = {p.x}";
    }
}

/// <summary>
/// Returns the y coordinate of a point
/// </summary>
public class GetPointY : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public GetPointY()
    {
        Name = "GetPointY";
        Output = new SpellComponent("", Types.Number);
        MethodComponents = new SpellComponent[] { new SpellComponent("p", Types.Point) };
        InitVarNames();
    }

    public override Variable Cast(SpellStack variables)
    {
        Point p = (Point)variables.Get(VarNames["p"]);
        return new Number(Output.name, p.y);
    }

    public override string GetVoice(SpellStack variables)
    {
        Point p = (Point)variables.Get(VarNames["p"]);
        return $"{Output.name} = {p.y}";
    }
}

/// <summary>
/// Returns a group containing all the WorldObjects currently inside this area
/// </summary>
public class GetObjectsInside : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public GetObjectsInside()
    {
        Name = "GetObjectsInside";
        Output = new SpellComponent("", Types.Group);
        MethodComponents = new SpellComponent[] { new SpellComponent("a", Types.Area) };
        InitVarNames();
    }

    public override Variable Cast(SpellStack variables)
    {
        Area a = (Area)variables.Get(VarNames["a"]);
        List<Variable> returnData = new List<Variable>();
        Collider2D[] hits = Physics2D.OverlapAreaAll(new Vector2(a.originX, a.originY), new Vector2(a.endX, a.endY));
        foreach(Collider2D hit in hits)
        {
            WorldObject w = hit.gameObject.GetComponent<WorldObject>();
            if (w != null)
            {
                returnData.Add(new Obj("", w));
            }
        }
        return new Group(Output.name, Types.Object, returnData);
    }

    public override string GetVoice(SpellStack variables)
    {
        Area a = (Area)variables.Get(VarNames["a"]);
        return $"{Output.name} = {VarNames["a"]}.ObjectsInside()";
    }
}
