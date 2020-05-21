using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Method for increasing the health of an object
/// </summary>
public class IncreaseHealth : Method
{
    /// <summary>
    /// The name of the object being targeted
    /// </summary>
    public string targetName;

    /// <summary>
    /// Name of the Mana variable fueling this method
    /// </summary>
    public string fuelName;

    public IncreaseHealth(string _targetName, string _fuelName) : base("Increase Health", "")
    {
        targetName = _targetName;
        fuelName = _fuelName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Obj target = (Obj)variables[targetName];
        Mana fuel = (Mana)variables[fuelName];
        target.health += (fuel.gandalfs / WorldObject.HEALTH_CONVERSION_CONSTANT);
        if (target.health > target.maxHealth)
            target.health = target.maxHealth;
        fuel.gandalfs = 0;
        return null;
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Mana fuel = (Mana)variables[fuelName];
        return $"{targetName}.IncreaseHealth({fuel.GetVoice()})";
    }
}

/// <summary>
/// Method for increasing the magic of an object
/// </summary>
public class IncreaseMagic : Method
{
    /// <summary>
    /// The name of the object being targeted
    /// </summary>
    public string targetName;

    /// <summary>
    /// Name of the Mana variable fueling this method
    /// </summary>
    public string fuelName;

    public IncreaseMagic(string _targetName, string _fuelName) : base("Increase Magic", "")
    {
        targetName = _targetName;
        fuelName = _fuelName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Obj target = (Obj)variables[targetName];
        Mana fuel = (Mana)variables[fuelName];
        target.magic += fuel.gandalfs;
        if (target.magic > target.maxMagic)
            target.magic = target.maxMagic;
        fuel.gandalfs = 0;
        return null;
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Mana fuel = (Mana)variables[fuelName];
        return $"{targetName}.IncreaseMagic({fuel.GetVoice()})";
    }
}

/// <summary>
/// Method for reducing the health of an Object
/// </summary>
public class ReduceHealth : Method
{
    /// <summary>
    /// The name of the object being targeted
    /// </summary>
    public string targetName;

    /// <summary>
    /// Name of the Mana variable fueling this method
    /// </summary>
    public string fuelName;

    public ReduceHealth(string outputName, string _targetName, string _fuelName) : base("Reduce Health", outputName)
    {
        targetName = _targetName;
        fuelName = _fuelName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Obj target = (Obj)variables[targetName];
        Mana fuel = (Mana)variables[fuelName];
        float amountRemoved = (fuel.gandalfs / WorldObject.HEALTH_CONVERSION_CONSTANT) / target.magicDefense;
        fuel.gandalfs = 0;
        float difference = amountRemoved > target.health ? target.health : amountRemoved;
        target.health -= difference;
        Mana generatedMana = new Mana(outputName);
        generatedMana.gandalfs = difference * WorldObject.HEALTH_CONVERSION_CONSTANT;
        return generatedMana;
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Mana fuel = (Mana)variables[fuelName];
        return $"{targetName}.ReduceHealth({fuel.GetVoice()})";
    }
}

/// <summary>
/// Method for reducing the magic of an Object
/// </summary>
public class ReduceMagic : Method
{
    /// <summary>
    /// The name of the object being targeted
    /// </summary>
    public string targetName;

    /// <summary>
    /// Name of the Mana variable fueling this method
    /// </summary>
    public string fuelName;

    public ReduceMagic(string outputName, string _targetName, string _fuelName) : base("Reduce Magic", outputName)
    {
        targetName = _targetName;
        fuelName = _fuelName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Obj target = (Obj)variables[targetName];
        Mana fuel = (Mana)variables[fuelName];
        Mana generatedMana = new Mana(outputName);
        float amountRemoved = fuel.gandalfs / target.magicDefense;
        fuel.gandalfs = 0;
        float difference = amountRemoved > target.magic ? target.magic : amountRemoved;
        target.magic -= difference;
        generatedMana.gandalfs = difference;
        return generatedMana;
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        Mana fuel = (Mana)variables[fuelName];
        return $"{outputName} = {targetName}.ReduceMagic({fuel.GetVoice()})";
    }
}

/// <summary>
/// Method for getting the alive state of an object
/// </summary>
public class IsAlive : Method
{
    /// <summary>
    /// The name of the object
    /// </summary>
    public string targetName;

    public IsAlive(string outputName, string _targetName) : base("Is Alive", outputName)
    {
        targetName = _targetName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Obj target = (Obj)variables[targetName];
        return new YeaNay(outputName, target.alive);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        return $"{outputName} = {targetName}.IsAlive()";
    }
}

/// <summary>
/// Method for getting the current magic of an object
/// </summary>
public class GetMagic : Method
{
    /// <summary>
    /// The name of the object
    /// </summary>
    public string targetName;

    public GetMagic(string outputName, string _targetName) : base("Get magic", outputName)
    {
        targetName = _targetName;
    }

    public override Variable Cast(Dictionary<string, Variable> variables)
    {
        Obj target = (Obj)variables[targetName];
        return new Number(outputName, target.magic);
    }

    public override string GetVoice(Dictionary<string, Variable> variables)
    {
        return $"{outputName} = {targetName}.Magic()";
    }
}
