using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An explosion created by a spellcaster if they cause an aberration.
/// This script only handles making the explosion grow and disappear after a time, not damaging or moving objects.
/// </summary>
public class AberrationExplosion : MonoBehaviour
{
    /// <summary>
    /// Determines how large the explosion will be
    /// </summary>
    private float size = 0f;

    void Update()
    {
        if (transform.localScale.x > size)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 oldScale = transform.localScale;
            float delta = size * Time.deltaTime;
            Vector3 newScale = new Vector3(oldScale.x + delta, oldScale.y + delta, 1);
            transform.localScale = newScale;
        }
    }

    public void SetSize(float gandalfs)
    {
        size = gandalfs / 25;
    }
}
