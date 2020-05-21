using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Collection object that stores variables
/// </summary>
public class Group : Variable
{
    public List<Variable> data;

    /// <summary>
    /// The type of item stored in this group
    /// </summary>
    public Types itemType;

    /// <summary>
    /// Number of items in this group
    /// </summary>
    public int size {
        get => data.Count;
    }

    public Group(string name, Types t, int size)
    {
        varName = name;
        magicianType = Types.Group;
        itemType = t;
        data = new List<Variable>(size);
    }

    public Group(string name, Types t, List<Variable>_data)
    {
        varName = name;
        magicianType = Types.Group;
        itemType = t;
        data = _data;
    }

    /// <summary>
    /// Get the item stored at the specified index in this group
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public Variable GetItem(int n)
    {
        return data[n];
    }

    /// <summary>
    /// Assign the specified item to the index in this group
    /// </summary>
    public void SetItem(int n, Variable newItem)
    {
        data[n] = newItem;
    }

    public override Variable Clone()
    {
        return new Group(varName, itemType, size);
    }

    public override string GetVoice()
    {
        string toReturn = "[";
        for (int i = 0; i < data.Count; i++)
        {
            toReturn += data[i].GetVoice();
            if (i != data.Count - 1)
                toReturn += ", ";
        }
        toReturn += "]";
        return toReturn;
    }
}
