using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellbookUIEntry : MonoBehaviour
{
    /// <summary>
    /// The index of the spellbook this object represents
    /// </summary>
    public int index;

    /// <summary>
    /// The text on this button for displaying the name of the spell
    /// </summary>
    public Text titleText;

    /// <summary>
    /// The text on this button for displaying the component types of the spell
    /// </summary>
    public Text componentText;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    /// <summary>
    /// Initialize this spellbookentry
    /// </summary>
    public void Init(int _index, string _title, string _components)
    {
        index = _index;
        titleText.text = _title;
        componentText.text = _components;
    }

    private void HandleClick()
    {
        SpellbookEntryClickedEventArgs args = new SpellbookEntryClickedEventArgs();
        args.index = index;
        SpellbookEntryClickedHandler handler = EntryClicked;
        if (handler != null)
        {
            handler(this, args);
        }
    }

    public event SpellbookEntryClickedHandler EntryClicked;

}

public class SpellbookEntryClickedEventArgs : EventArgs
{
    public int index { get; set; }
}

public delegate void SpellbookEntryClickedHandler(object sender, SpellbookEntryClickedEventArgs e);
