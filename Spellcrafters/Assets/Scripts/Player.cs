using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Player : Spellcaster
{
    /// <summary>
    /// The object that gathers components when the player is preparing a spell
    /// </summary>
    public ComponentLoader componentLoader;

    /// <summary>
    /// The object that creates the UI spellbook
    /// </summary>
    public SpellbookController spellBookController;

    public GameObject spellbookScroller;

    public Player() : base(100, 100, 100, 0, 100, 100, 0, 1, true, false, 13, 1) { }

    void Awake()
    {
        InitSpellcaster(1);

        List<Variable> lesserHarmInit = new List<Variable>();
        lesserHarmInit.Add(new Obj("_player", this));
        lesserHarmInit.Add(new Number("_num1", 50));
        List<SpellComponent> lesserHarmComponents = new List<SpellComponent>();
        lesserHarmComponents.Add(new SpellComponent("enemy", Types.Object));
        List<Method> lesserHarmLines = new List<Method>();
        lesserHarmLines.Add(new ChannelMana("m", "_player", "_num1"));
        lesserHarmLines.Add(new ReduceHealth("leftover", "enemy", "m"));
        Spell lesserHarm = new Spell("Lesser Harm", lesserHarmLines, lesserHarmComponents, lesserHarmInit);
        spellBook.Add(lesserHarm);

        List<Variable> amplifiedHarmInit = new List<Variable>();
        amplifiedHarmInit.Add(new Obj("_player", this));
        amplifiedHarmInit.Add(new Number("_num1", 50));
        List<SpellComponent> amplifiedHarmComponents = new List<SpellComponent>();
        amplifiedHarmComponents.Add(new SpellComponent("drainee", Types.Object));
        amplifiedHarmComponents.Add(new SpellComponent("enemy", Types.Object));
        List<Method> amplifiedHarmLines = new List<Method>();
        amplifiedHarmLines.Add(new ChannelMana("m", "_player", "_num1"));
        amplifiedHarmLines.Add(new ReduceMagic("n", "drainee", "m"));
        amplifiedHarmLines.Add(new ReduceHealth("leftover", "enemy", "n"));
        Spell amplifiedHarm = new Spell("Amplified Harm", amplifiedHarmLines, amplifiedHarmComponents, amplifiedHarmInit);
        spellBook.Add(amplifiedHarm);

        List<Variable> variableHarmInit = new List<Variable>();
        variableHarmInit.Add(new Obj("_player", this));
        List<SpellComponent> variableHarmComponents = new List<SpellComponent>();
        variableHarmComponents.Add(new SpellComponent("amount", Types.Number));
        variableHarmComponents.Add(new SpellComponent("enemy", Types.Object));
        List<Method> variableHarmLines = new List<Method>();
        variableHarmLines.Add(new ChannelMana("m", "_player", "amount"));
        variableHarmLines.Add(new ReduceHealth("leftover", "enemy", "m"));
        Spell variableHarm = new Spell("Variable Harm", variableHarmLines, variableHarmComponents, variableHarmInit);
        spellBook.Add(variableHarm);

        List<Variable> continuousDrainInit = new List<Variable>();
        continuousDrainInit.Add(new Obj("_player", this));
        continuousDrainInit.Add(new Number("_num1", 30));
        List<SpellComponent> continuousDrainComponents = new List<SpellComponent>();
        continuousDrainComponents.Add(new SpellComponent("enemy", Types.Object));
        List<Method> continuousDrainLines = new List<Method>();
        continuousDrainLines.Add(new ChannelMana("m", "_player", "_num1"));
        continuousDrainLines.Add(new IsAlive("yn", "enemy"));
        continuousDrainLines.Add(new Loop(3, 6, "yn"));
        continuousDrainLines.Add(new ReduceHealth("leftover", "enemy", "m"));
        continuousDrainLines.Add(new TransferAll("leftover", "m"));
        continuousDrainLines.Add(new IsAlive("yn", "enemy"));
        Spell ContinuousDrain = new Spell("Continuous Drain", continuousDrainLines, continuousDrainComponents, continuousDrainInit);
        spellBook.Add(ContinuousDrain);

        List<Variable> megaDrainInit = new List<Variable>();
        megaDrainInit.Add(new Obj("=player", this));
        megaDrainInit.Add(new Number("=number1", 0));
        megaDrainInit.Add(new YeaNay("=yeanay2", false));
        megaDrainInit.Add(new Number("=number3", 1));
        List<SpellComponent> megaDrainComponents = new List<SpellComponent>();
        megaDrainComponents.Add(new SpellComponent("input", Types.Number));
        megaDrainComponents.Add(new SpellComponent("drainArea", Types.Area));
        megaDrainComponents.Add(new SpellComponent("enemy", Types.Object));
        List<Method> megaDrainLines = new List<Method>();
        megaDrainLines.Add(new NewMana("magicHolder"));
        megaDrainLines.Add(new GetObjectsInside("objectGroup", "drainArea"));
        megaDrainLines.Add(new GetSize("groupSize", "objectGroup"));
        megaDrainLines.Add(new NewNumber("i", "=number1"));
        megaDrainLines.Add(new CheckEquality("loopDecider", "i", "groupSize", "=yeanay2"));
        megaDrainLines.Add(new Loop(6, 12, "loopDecider"));
        megaDrainLines.Add(new GetItem("object", "objectGroup", "i"));
        megaDrainLines.Add(new ChannelMana("m", "=player", "input"));
        megaDrainLines.Add(new ReduceMagic("temp", "object", "m"));
        megaDrainLines.Add(new TransferAll("temp", "magicHolder"));
        megaDrainLines.Add(new AddNumbers("i", "i", "=number3"));
        megaDrainLines.Add(new CheckEquality("loopDecider", "i", "groupSize", "=yeanay2"));
        megaDrainLines.Add(new ReduceHealth("temp", "enemy", "magicHolder"));
        Spell megaDrain = new Spell("Mega Drain", megaDrainLines, megaDrainComponents, megaDrainInit);
        spellBook.Add(megaDrain);

        List<Variable> throwInit = new List<Variable>();
        throwInit.Add(new Obj("=player", this));
        List<SpellComponent> throwComponents = new List<SpellComponent>();
        throwComponents.Add(new SpellComponent("amount", Types.Number));
        throwComponents.Add(new SpellComponent("direction", Types.Direction));
        throwComponents.Add(new SpellComponent("enemy", Types.Object));
        List<Method> throwLines = new List<Method>();
        throwLines.Add(new ChannelMana("m", "=player", "amount"));
        throwLines.Add(new Move("enemy", "direction", "m"));
        Spell throwEnemy = new Spell("Throw", throwLines, throwComponents, throwInit);
        spellBook.Add(throwEnemy);

        List<Variable> conditionalHarmInit = new List<Variable>();
        conditionalHarmInit.Add(new Obj("=player", this));
        conditionalHarmInit.Add(new Number("=number1", 100));
        conditionalHarmInit.Add(new YeaNay("=bool2", true));
        List<SpellComponent> conditionalHarmComponents = new List<SpellComponent>();
        conditionalHarmComponents.Add(new SpellComponent("num1", Types.Number));
        conditionalHarmComponents.Add(new SpellComponent("enemy", Types.Object));
        List<Method> conditionalHarmLines = new List<Method>();
        conditionalHarmLines.Add(new GetMagic("currentMagic", "=player"));
        conditionalHarmLines.Add(new CheckEquality("equals", "currentMagic", "num1", "=bool2"));
        conditionalHarmLines.Add(new Conditional(3, 5, "equals"));
        conditionalHarmLines.Add(new ChannelMana("m", "=player", "num1"));
        conditionalHarmLines.Add(new ReduceHealth("temp", "enemy", "m"));
        Spell conditionalHarm = new Spell("Conditional Harm", conditionalHarmLines, conditionalHarmComponents, conditionalHarmInit);
        spellBook.Add(conditionalHarm);

        List<Variable> testInit = new List<Variable>();
        testInit.Add(new Obj("=player", this));
        List<SpellComponent> testComponents = new List<SpellComponent>();
        testComponents.Add(new SpellComponent("num1", Types.Number));
        testComponents.Add(new SpellComponent("num2", Types.Number));
        List<Method> testLines = new List<Method>();
        testLines.Add(new ChannelMana("m", "=player", "num1"));
        testLines.Add(new ChannelMana("n", "=player", "num2"));
        Spell test = new Spell("test", testLines, testComponents, testInit);
        spellBook.Add(test);

        List<SpellbookUIEntry> spellbookEntries = spellBookController.InitializeSpellbook(spellBook);
        foreach (SpellbookUIEntry entry in spellbookEntries)
            entry.EntryClicked += SetSpell;
        spellbookScroller.SetActive(false);
        spellBookController.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateObject();
        UpdateSpellcaster();
        HandleCasting();
        ToggleSpellbook();
    }

    void FixedUpdate()
    {
        HandleMovementPhysics();
    }

    /// <summary>
    /// Called by the spellbook UI to set the currently selected spell
    /// </summary>
    private void SetSpell(object sender, SpellbookEntryClickedEventArgs args)
    {
        currentSpell = args.index;
    }

    /// <summary>
    /// Called by the ComponentLoader when all the components are ready.
    /// Starts casting the spell.
    /// </summary>
    public void StartCasting(List<Variable> spellComponents)
    {
        preparing = false;
        casting = true;
        spellBook[currentSpell].BeginCast(spellComponents);
        voice.Speak(spellBook[currentSpell].SpeakNext(), castSpeed);
    }

    /// <summary>
    /// Show/hide the spellbook UI
    /// </summary>
    private void ToggleSpellbook()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            bool newState = !spellbookScroller.activeSelf;
            spellbookScroller.SetActive(newState);
            spellBookController.gameObject.SetActive(newState);
        }
    }

    /// <summary>
    /// Handle player movement by applying force to the game object
    /// </summary>
    private void HandleMovementPhysics()
    {

        Vector2 f = Vector2.zero;
        float rot = -1;
        if (Input.GetKey(KeyCode.W))
        {
            f.y += acceleration;
            rot = 90;
        }
        if (Input.GetKey(KeyCode.A))
        {
            f.x -= acceleration;
            rot = 180;
        }
        if (Input.GetKey(KeyCode.S))
        {
            f.y -= acceleration;
            rot = 270;
        }
        if (Input.GetKey(KeyCode.D))
        {
            f.x += acceleration;
            rot = 0;
        }
        GetComponent<Rigidbody2D>().AddForce(f);
        if (rot != -1)
            transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    private void HandleCasting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && preparing == false && casting == false)
        {
            preparing = true;
            if (spellbookScroller.activeSelf == true)
            {
                spellbookScroller.SetActive(false);
                spellBookController.gameObject.SetActive(false);
            }
            componentLoader.GatherComponents(spellBook[currentSpell].components);
        }
        else if (casting)
        {
            castTicker += Time.deltaTime;
            if (castTicker > castSpeed)
            {
                castTicker = 0;
                ConsumeMana(WorldObject.CASTING_COST);
                CastResult res = spellBook[currentSpell].CastNext();
                //Debug.Log(res.storedMagic);
                if (res.storedMagic > channelLimit)
                {
                    HandleAberration("GandalfOverflowAberration!", res.storedMagic);
                }
                else if (res.aberration != "")
                {
                    HandleAberration(res.aberration, res.storedMagic);
                }
                else
                {
                    if (spellBook[currentSpell].currentLine == -1)
                    {
                        casting = false;
                    }
                    else
                    {
                        voice.Speak(spellBook[currentSpell].SpeakNext(), castSpeed);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Cancels casting after an aberration occurs and puts the aberration to voice
    /// </summary>
    private void HandleAberration(string message, float magicOverflow)
    {
        casting = false;
        Explode(magicOverflow);
        voice.SpeakInstant(message, 2);
    }
}


