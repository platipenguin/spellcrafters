using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI element that lets the player specify a direction variable
/// </summary>
public class DirectionPicker : MonoBehaviour
{
    public ComponentLoader loader;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    /// <summary>
    /// Calculate the degrees of the direction based on where the player clicked on the direction picker.
    /// Send the resulting number in degrees to the ComponentLoader.
    /// </summary>
    private void HandleClick()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 origin = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 transformPos = clickPos - origin;
        if (transformPos.x == 0 && transformPos.y == 0)
            transformPos.x = 0.001f;
        float theta = Mathf.Atan(transformPos.y / transformPos.x);
        float degrees = theta * Mathf.Rad2Deg;
        float mod = 0;
        if (transformPos.x < 0 && transformPos.y >= 0)
            mod = 180;
        else if (transformPos.x < 0 && transformPos.y < 0)
            mod = 180;
        else if (transformPos.x >= 0 && transformPos.y < 0)
            mod = 360;
        loader.GatherDirection(degrees + mod);
    }
}
