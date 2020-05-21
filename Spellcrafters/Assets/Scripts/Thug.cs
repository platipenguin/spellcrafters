using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thug : WorldObject
{
    public Thug() : base(100, 0, 0, 100, 0, 0, 1, true, false, 7, 0) { }

    void Update()
    {
        UpdateObject();
    }
}
