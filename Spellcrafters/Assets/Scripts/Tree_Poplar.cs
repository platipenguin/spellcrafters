using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Poplar : WorldObject
{
    public Tree_Poplar() : base(100, 50, 10, 100, 50, 10, 0.5f, true, false, 0, 0.1f) { }

    void Update()
    {
        UpdateObject();
    }
}
