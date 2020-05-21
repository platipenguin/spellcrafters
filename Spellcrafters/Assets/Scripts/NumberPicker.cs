using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class NumberPicker : Picker
{
    /// <summary>
    /// The text object the number picks displays the current number to.
    /// </summary>
    public Text display;

    /// <summary>
    /// The text currently being displayed.
    /// </summary>
    private string currentNumber = "";

    /// <summary>
    /// Called by the buttons when they're clicked.
    /// If n == "b" deletes the last number clicked.
    /// If n == "-", flips the number to negative or positive.
    /// </summary>
    public void NumberClicked(string n)
    {
        if (n == "b")
        {
            if (currentNumber.Length > 0)
                currentNumber = currentNumber.Substring(0, currentNumber.Length - 1);
        }
        else if (n == "-")
        {
            if (currentNumber == "-")
                currentNumber = "";
            else if (currentNumber.IndexOf("-") == -1)
                currentNumber = "-" + currentNumber;
            else
                currentNumber = currentNumber.Substring(1, currentNumber.Length - 1);
        }
        else if (n == ".")
        {
            if (currentNumber.IndexOf(".") == -1)
                currentNumber += ".";
        }
        else
        {
            currentNumber += n;
        }
        UpdateDisplay();
    }

    /// <summary>
    /// Send the displayed number to the ComponentLoader
    /// </summary>
    public void TrySendNumber()
    {
        float n;
        try
        {
            n = float.Parse(currentNumber);
            currentNumber = "";
            UpdateDisplay();
            loader.GatherNumber(n);
        }
        catch
        {
            return;
        }
    }

    private void UpdateDisplay()
    {
        display.text = currentNumber;
    }
}
