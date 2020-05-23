using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Method for increasing the health of an object
/// </summary>
public class IncreaseHealth : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public IncreaseHealth()
    {
        Name = "IncreaseHealth";
        Output = new SpellComponent("", Types.Null);
        MethodComponents = new SpellComponent[] { new SpellComponent("object", Types.Object), new SpellComponent("fuel", Types.Mana) };
        InitVarNames();
    }

    public override Variable Cast(SpellStack variables)
    {
        Obj target = (Obj)variables.Get(VarNames["object"]);
        Mana fuel = (Mana)variables.Get(VarNames["fuel"]);
        target.health += (fuel.gandalfs / WorldObject.HEALTH_CONVERSION_CONSTANT);
        if (target.health > target.maxHealth)
            target.health = target.maxHealth;
        fuel.gandalfs = 0;
        return null;
    }

    public override string GetVoice(SpellStack variables)
    {
        Mana fuel = (Mana)variables.Get(VarNames["fuel"]);
        return $"{VarNames["object"]}.IncreaseHealth({fuel.GetVoice()})";
    }
}

/// <summary>
/// Method for increasing the magic of an object
/// </summary>
public class IncreaseMagic : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public IncreaseMagic()
    {
        Name = "IncreaseMagic";
        Output = new SpellComponent("", Types.Null);
        MethodComponents = new SpellComponent[] { new SpellComponent("object", Types.Object), new SpellComponent("fuel", Types.Mana) };
        InitVarNames();
    }

    public override Variable Cast(SpellStack variables)
    {
        Obj target = (Obj)variables.Get(VarNames["object"]);
        Mana fuel = (Mana)variables.Get(VarNames["fuel"]);
        target.magic += fuel.gandalfs;
        if (target.magic > target.maxMagic)
            target.magic = target.maxMagic;
        fuel.gandalfs = 0;
        return null;
    }

    public override string GetVoice(SpellStack variables)
    {
        Mana fuel = (Mana)variables.Get(VarNames["fuel"]);
        return $"{VarNames["object"]}.IncreaseMagic({fuel.GetVoice()})";
    }
}

/// <summary>
/// Method for reducing the health of an Object
/// </summary>
public class ReduceHealth : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public ReduceHealth()
    {
        Name = "ReduceHealth";
        Output = new SpellComponent("", Types.Mana);
        MethodComponents = new SpellComponent[] { new SpellComponent("object", Types.Object), new SpellComponent("fuel", Types.Mana) };
        InitVarNames();
    }

    public override Variable Cast(SpellStack variables)
    {
        Obj target = (Obj)variables.Get(VarNames["object"]);
        Mana fuel = (Mana)variables.Get(VarNames["fuel"]);
        float amountRemoved = (fuel.gandalfs / WorldObject.HEALTH_CONVERSION_CONSTANT) / target.magicDefense;
        fuel.gandalfs = 0;
        float difference = amountRemoved > target.health ? target.health : amountRemoved;
        target.health -= difference;
        Mana generatedMana = new Mana(Output.name);
        generatedMana.gandalfs = difference * WorldObject.HEALTH_CONVERSION_CONSTANT;
        return generatedMana;
    }

    public override string GetVoice(SpellStack variables)
    {
        Mana fuel = (Mana)variables.Get(VarNames["fuel"]);
        return $"{VarNames["object"]}.ReduceHealth({fuel.GetVoice()})";
    }
}

/// <summary>
/// Method for reducing the magic of an Object
/// </summary>
public class ReduceMagic : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public ReduceMagic()
    {
        Name = "ReduceMagic";
        Output = new SpellComponent("", Types.Mana);
        MethodComponents = new SpellComponent[] { new SpellComponent("object", Types.Object), new SpellComponent("fuel", Types.Mana) };
        InitVarNames();
    }

    public override Variable Cast(SpellStack variables)
    {
        Obj target = (Obj)variables.Get(VarNames["object"]);
        Mana fuel = (Mana)variables.Get(VarNames["fuel"]);
        Mana generatedMana = new Mana(Output.name);
        float amountRemoved = fuel.gandalfs / target.magicDefense;
        fuel.gandalfs = 0;
        float difference = amountRemoved > target.magic ? target.magic : amountRemoved;
        target.magic -= difference;
        generatedMana.gandalfs = difference;
        return generatedMana;
    }

    public override string GetVoice(SpellStack variables)
    {
        Mana fuel = (Mana)variables.Get(VarNames["fuel"]);
        return $"{Output.name} = {VarNames["object"]}.ReduceMagic({fuel.GetVoice()})";
    }
}

/// <summary>
/// Method for getting the alive state of an object
/// </summary>
public class IsAlive : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public IsAlive()
    {
        Name = "IsAlive";
        Output = new SpellComponent("", Types.YeaNay);
        MethodComponents = new SpellComponent[] { new SpellComponent("object", Types.Object) };
        InitVarNames();
    }

    public override Variable Cast(SpellStack variables)
    {
        Obj target = (Obj)variables.Get(VarNames["object"]);
        return new YeaNay(Output.name, target.alive);
    }

    public override string GetVoice(SpellStack variables)
    {
        return $"{Output.name} = {VarNames["object"]}.IsAlive()";
    }
}

/// <summary>
/// Method for getting the current magic of an object
/// </summary>
public class GetMagic : Method
{
    public override string Name { get; set; }
    public override SpellComponent Output { get; set; }
    public override SpellComponent[] MethodComponents { get; set; }
    public override Dictionary<string, string> VarNames { get; set; }

    public GetMagic()
    {
        Name = "GetMagic";
        Output = new SpellComponent("", Types.Number);
        MethodComponents = new SpellComponent[] { new SpellComponent("object", Types.Object) };
        InitVarNames();
    }

    public override Variable Cast(SpellStack variables)
    {
        Obj target = (Obj)variables.Get(VarNames["object"]);
        return new Number(Output.name, target.magic);
    }

    public override string GetVoice(SpellStack variables)
    {
        return $"{Output.name} = {VarNames["object"]}.Magic()";
    }
}
