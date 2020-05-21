using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : WorldObject
{
    public Rock() : base(0, 0, 100, 0, 0, 100, 1, false, true, 0, 0) { }

    void Update()
    {
        UpdateObject();
    }
}
