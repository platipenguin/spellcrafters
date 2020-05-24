using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI element that represents a line in a spell. Gets fed a method object and creates a UI element to fill in the name of the 
/// output variable, spaces to put in variables, etc.
/// </summary>
public class LineUI : MonoBehaviour
{
    /// <summary>
    /// Font sizes of text
    /// </summary>
    public static int METHOD_FONT = 14;
    public static int COMPONENT_FONT = 12;

    /// <summary>
    /// UI object of the output variable. Used by player to name the output.
    /// </summary>
    public OutputUI outputPrefab;

    /// <summary>
    /// UI object of a component slot in the method. Used by player to fill the component for this method.
    /// </summary>
    public ComponentUI componentPrefab;

    /// <summary>
    /// Picture of an arrow to denote what is going into the output.
    /// </summary>
    public Image arrow;

    /// <summary>
    /// Used to display the name of the method, component names, etc.
    /// </summary>
    public Text textChild;

    /// <summary>
    /// Fills this line UI object with input fields so the player can fill out the method.
    /// </summary>
    public void InitWithMethod(Method m)
    {
        // Add the output field, if necessary
        if (m.Output.type == Types.Loop)
            return;
        else if (m.Output.type == Types.Conditional)
            return;
        else if (m.Output.type != Types.Null)
        {
            OutputUI output = Instantiate(outputPrefab, transform);
            output.GetComponent<Image>().color = EditorController.GetColorByType(m.Output.type);
            Instantiate(arrow, transform);
        }
        // Add the name of the method
        Text methodName = Instantiate(textChild, transform);
        methodName.text = m.Name;
        methodName.fontSize = METHOD_FONT;
        // Add fields for components
        foreach (SpellComponent c in m.MethodComponents)
        {
            Text compName = Instantiate(textChild, transform);
            compName.text = c.name;
            compName.fontSize = COMPONENT_FONT;
            ComponentUI compSlot = Instantiate(componentPrefab, transform);
            compSlot.GetComponent<Image>().color = EditorController.GetColorByType(c.type);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    // Start is called before the first frame update
    void Start()
    {
        NewNumber m = new NewNumber();
        InitWithMethod(m);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
