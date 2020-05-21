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
    private string manaName;

    public GetGandalfs(string outputName, string _manaName) : base("Get Gandalfs", outputName)
    {
        manaName = _manaName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Mana m = (Mana)variables[manaName];
        return new Number(outputName, m.gandalfs);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Mana m = (Mana)variables[manaName];
        return $"{outputName} = {m.gandalfs}";
    }
}

/// <summary>
/// Method for constructing a new mana variable by channeling
/// </summary>
public class ChannelMana : Method
{
    private string casterName;

    private string numberName;

    public ChannelMana(string outputName, string _casterName, string _numberName) : base("Channel Mana", outputName)
    {
        casterName = _casterName;
        numberName = _numberName;
    }

    /// <summary>
    /// Creates and returns a new Area variable
    /// </summary>
    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Number amount = (Number)variables[numberName];
        if (amount.value < 0)
            throw new AberrationException(AberrationException.NEGATIVE_GANDALFS);
        Obj caster = (Obj)variables[casterName];
        float gandalfs = caster.ConsumeMana(amount.value);
        return new Mana(outputName, gandalfs);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Number amount = (Number)variables[numberName];
        return $"{outputName} = self.Channel({amount.GetVoice()} gandalfs)";
    }
}

/// <summary>
/// Method for transfering all magic from one variable to another
/// </summary>
public class TransferAll : Method
{
    private string fromMana;

    private string toMana;

    public TransferAll(string _fromMana, string _toMana) : base("Transfer Mana", "")
    {
        fromMana = _fromMana;
        toMana = _toMana;
    }

    /// <summary>
    /// Creates and returns a new Area variable
    /// </summary>
    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Mana from = (Mana)variables[fromMana];
        Mana to = (Mana)variables[toMana];
        to.gandalfs += from.gandalfs;
        from.gandalfs = 0;
        return null;
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        return $"{fromMana}.TransferAll({toMana})";
    }
}

/// <summary>
/// Returns the x coordinate of a point
/// </summary>
public class GetPointX : Method
{
    private string pointName;

    public GetPointX(string outputName, string _pointName) : base("Get Point X", outputName)
    {
        pointName = _pointName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Point p = (Point)variables[pointName];
        return new Number(outputName, p.x);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Point p = (Point)variables[pointName];
        return $"{outputName} = {p.x}";
    }
}

/// <summary>
/// Returns the y coordinate of a point
/// </summary>
public class GetPointY : Method
{
    private string pointName;

    public GetPointY(string outputName, string _pointName) : base("Get Point Y", outputName)
    {
        pointName = _pointName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Point p = (Point)variables[pointName];
        return new Number(outputName, p.y);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Point p = (Point)variables[pointName];
        return $"{outputName} = {p.y}";
    }
}

/// <summary>
/// Returns the origin point of an area variable
/// </summary>
public class GetOrigin : Method
{
    private string areaName;

    public GetOrigin(string outputName, string _areaName) : base("Get Origin", outputName)
    {
        areaName = _areaName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Area a = (Area)variables[areaName];
        return new Point(outputName, a.originX, a.originY);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Area a = (Area)variables[areaName];
        return $"{outputName} = ({a.originX}, {a.originY})";
    }
}

/// <summary>
/// Returns the end point of an area variable
/// </summary>
public class GetEnd : Method
{
    private string areaName;

    public GetEnd(string outputName, string _areaName) : base("Get End", outputName)
    {
        areaName = _areaName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Area a = (Area)variables[areaName];
        return new Point(outputName, a.endX, a.endY);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Area a = (Area)variables[areaName];
        return $"{outputName} = ({a.endX}, {a.endY})";
    }
}

/// <summary>
/// Returns a group containing all the WorldObjects currently inside this area
/// </summary>
public class GetObjectsInside : Method
{
    private string areaName;

    public GetObjectsInside(string outputName, string _areaName) : base("Get Objects Inside", outputName)
    {
        areaName = _areaName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Area a = (Area)variables[areaName];
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
        return new Group(outputName, Types.Object, returnData);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Area a = (Area)variables[areaName];
        return $"{outputName} = {areaName}.ObjectsInside()";
    }
}

/// <summary>
/// Returns the size of a group variable
/// </summary>
public class GetSize : Method
{
    private string groupName;

    public GetSize(string outputName, string _groupName) : base("Get End", outputName)
    {
        groupName = _groupName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Group g = (Group)variables[groupName];
        return new Number(outputName, g.size);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        return $"{outputName} = {groupName}.Size()";
    }
}

/// <summary>
/// Returns the item stored in the group at the specified index
/// </summary>
public class GetItem : Method
{
    private string groupName;

    private string indexName;

    public GetItem(string outputName, string _groupName, string _indexName) : base("Get Item", outputName)
    {
        groupName = _groupName;
        indexName = _indexName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Group g = (Group)variables[groupName];
        Number n = (Number)variables[indexName];
        Variable toReturn = g.GetItem(Mathf.RoundToInt(n.value));
        toReturn.varName = outputName;
        return toReturn;
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Number n = (Number)variables[indexName];
        return $"{outputName} = {groupName}.GetItem({Mathf.RoundToInt(n.value)})";
    }
}
