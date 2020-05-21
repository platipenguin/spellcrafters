using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles creating the buttons for setting which spell is active for the player.
/// </summary>
public class SpellbookController : MonoBehaviour
{
    public GameObject spellGrid;

    public SpellbookUIEntry entryPrefab;

    /// <summary>
    /// Fill the UI spellbook with clickable spell entries. Returns those entries so the player gameobject can subscribe to their
    /// EntryClicked events.
    /// </summary>
    public List<SpellbookUIEntry> InitializeSpellbook(List<Spell> spells)
    {
        List<SpellbookUIEntry> toReturn = new List<SpellbookUIEntry>();
        for (int i = 0; i < spells.Count; i++)
        {
            Spell spell = spells[i];
            SpellbookUIEntry newEntry = Instantiate(entryPrefab);
            newEntry.Init(i, spell.name, spell.ComponentsToString());
            newEntry.transform.SetParent(spellGrid.transform);
            toReturn.Add(newEntry);
        }
        return toReturn;
    }
}
