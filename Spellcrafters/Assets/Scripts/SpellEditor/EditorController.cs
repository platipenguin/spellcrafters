using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorController : MonoBehaviour
{
    /// <summary>
    /// Colors for the borders of different variable types.
    /// </summary>
    public static Color ANY_COLOR = Color.white;
    public static Color NUM_COLOR = Color.red;
    public static Color YN_COLOR = Color.yellow;
    public static Color DIR_COLOR = new Color(1f, 165 / 255, 0f); //Orange
    public static Color PT_COLOR = Color.blue;
    public static Color AREA_COLOR = Color.green;
    public static Color OBJ_COLOR = Color.magenta;
    public static Color MANA_COLOR = Color.cyan;
    public static Color GROUP_COLOR = Color.gray;

    /// <summary>
    /// Returns the color the variable should display with
    /// </summary>
    public static Color GetColorByType(Types t)
    {
        switch (t)
        {
            case Types.Any:
                return ANY_COLOR;
            case Types.Number:
                return NUM_COLOR;
            case Types.YeaNay:
                return YN_COLOR;
            case Types.Direction:
                return DIR_COLOR;
            case Types.Point:
                return PT_COLOR;
            case Types.Area:
                return AREA_COLOR;
            case Types.Object:
                return OBJ_COLOR;
            case Types.Mana:
                return MANA_COLOR;
            default:
                return GROUP_COLOR;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
