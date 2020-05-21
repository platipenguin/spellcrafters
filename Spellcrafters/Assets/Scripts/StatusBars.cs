using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBars : MonoBehaviour
{
    public GameObject magic;

    public GameObject health;

    public GameObject armor;

    /// <summary>
    /// Change the size of the status bars.
    /// Values must be between 0 and 1.
    /// </summary>
    public void UpdateBars(float m, float h, float a)
    {
        float yScale = magic.transform.localScale.y;
        magic.transform.localScale = new Vector3(m, yScale, 1);
        health.transform.localScale = new Vector3(h, yScale, 1);
        armor.transform.localScale = new Vector3(a, yScale, 1);
    }
}
