using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all gameobjects that can cast spells
/// </summary>
public abstract class Spellcaster : WorldObject
{
    /// <summary>
    /// How much magic this spellcaster can hold before exploding
    /// </summary>
    public float channelLimit;

    /// <summary>
    /// The spells this spellcaster can use
    /// </summary>
    public List<Spell> spellBook = new List<Spell>();

    /// <summary>
    /// 3D text object used to print lines of a spell as they are being cast
    /// </summary>
    public SpellVoice voice;

    /// <summary>
    /// Flag for whether or not the player is gathering components to cast a spell
    /// </summary>
    protected bool preparing = false;

    /// <summary>
    /// Flag for whether or not the player is actively casting a spell
    /// </summary>
    protected bool casting = false;

    /// <summary>
    /// How long it takes to cast each line in a spell
    /// </summary>
    protected float castSpeed = 1.5f;

    /// <summary>
    /// Ticker for tracking how long since the last line of a spell was cast
    /// </summary>
    protected float castTicker = 0;

    /// <summary>
    /// Index of the spell currently equipped
    /// </summary>
    protected int currentSpell = 0;

    public AberrationExplosion explosionPrefab;

    public Spellcaster(float _limit, float _health, float _magic, float _armor, float _maxHealth, float _maxMagic, float _maxArmor, float _magicDefense, bool _alive, bool _inanimate, float _acceleration, float _magicRegen) : base(_health, _magic, _armor, _maxHealth, _maxMagic, _maxArmor, _magicDefense, _alive, _inanimate, _acceleration, _magicRegen)
    {
        channelLimit = _limit;
    }

    /// <summary>
    /// Initialize this spellcaster object. Should always be called on Start.
    /// </summary>
    /// <param name="voiceOffset">How far above the spellcaster the voice should appear</param>
    protected void InitSpellcaster(float voiceOffset)
    {
        voice.SetOffset(voiceOffset);
    }

    /// <summary>
    /// Should always be called on Update. Handles updating all things spellcasters need updated each frame.
    /// </summary>
    protected void UpdateSpellcaster()
    {
        voice.UpdatePosition(transform.position);
    }

    /// <summary>
    /// Creates a magical explosion when this spellcaster triggers an aberration.
    /// Gandalfs determines how large the explosion will be.
    /// </summary>
    protected void Explode(float gandalfs)
    {
        AberrationExplosion explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.SetSize(gandalfs);
        Damage(gandalfs / 4);
        Vector3 direction = Vector3.Normalize(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0));
        float force = gandalfs / 4;
        GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
    }
}
