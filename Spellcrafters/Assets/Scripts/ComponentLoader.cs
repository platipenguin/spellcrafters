using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/// <summary>
/// When the player begins casting a spell, the player object passes this class a list of components necessary to cast.
/// This class then handles popping up the UI elements to generate those components.
/// Once the components have been set, this class hands the components back to the player object to begin casting the spell.
/// </summary>
public class ComponentLoader : MonoBehaviour
{
    public NumberPicker numberPicker;

    public DirectionPicker directionPicker;

    public Player player;

    public Texture2D cursorDefault;

    public Texture2D cursorPoint;

    public Texture2D cursorArea;

    public Texture2D cursorObject;

    public GameObject[] areaSelect = new GameObject[4];

    /// <summary>
    /// Keeps track of which component needs to be generated next
    /// </summary>
    private int nextIndex = -1;

    /// <summary>
    /// The components that need to be gathered
    /// </summary>
    private List<SpellComponent> toGather;

    /// <summary>
    /// The list of variables that will be passed to the Player object when it is complete
    /// </summary>
    private List<Variable> toPass;

    /// <summary>
    /// Set to true if the next component to be gathered requires a click
    /// </summary>
    private bool waitingForClick = false;


    /// <summary>
    /// Set to true if the player is dragging out an area to select
    /// </summary>
    private bool dragging = false;

    private Vector2 areaStart;

    void Update()
    {
        if (waitingForClick && Input.GetMouseButtonDown(0))
        {
            switch (toGather[nextIndex].type)
            {
                case Types.Object:
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
                    if (hit.collider != null)
                    {
                        WorldObject entity = hit.collider.gameObject.GetComponent<WorldObject>();
                        Obj objVar = new Obj(toGather[nextIndex].name, entity);
                        waitingForClick = false;
                        NewComponent(objVar);
                    }
                    break;
                case Types.Point:
                    Vector3 pt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Point pointVar = new Point(toGather[nextIndex].name, pt.x, pt.y);
                    waitingForClick = false;
                    NewComponent(pointVar);
                    break;
                case Types.Area:
                    foreach (GameObject bar in areaSelect)
                        bar.SetActive(true);
                    waitingForClick = false;
                    dragging = true;
                    areaStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    break;
            }
        }
        else if (dragging && Input.GetMouseButton(0))
        {
            UpdateAreaBars();
        }
        else if (dragging && Input.GetMouseButtonUp(0))
        {
            Vector2 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (endPoint.x > areaStart.x && areaStart.y > endPoint.y)
            {
                dragging = false;
                foreach (GameObject bar in areaSelect)
                    bar.SetActive(false);
                Area areaVar = new Area(toGather[nextIndex].name, areaStart.x, areaStart.y, endPoint.x, endPoint.y);
                NewComponent(areaVar);
            }
            else
            {
                waitingForClick = true;
            }
        }
    }

    /// <summary>
    /// Begin gathering the specified components
    /// </summary>
    public void GatherComponents(List<SpellComponent> components)
    {
        nextIndex = 0;
        toGather = components;
        toPass = new List<Variable>();
        if (toGather.Count == 0)
        {
            player.StartCasting(toPass);
            return;
        }
        ReadyNextGather();
    }

    /// <summary>
    /// Called by the number picker to give this object a Number
    /// </summary>
    public void GatherNumber(float n)
    {
        numberPicker.gameObject.SetActive(false);
        NewComponent(new Number(toGather[nextIndex].name, n));
    }

    /// <summary>
    /// Called by the direction picker to give this object a Direction
    /// </summary>
    public void GatherDirection(float n)
    {
        directionPicker.gameObject.SetActive(false);
        NewComponent(new Direction(toGather[nextIndex].name, n));
    }

    /// <summary>
    /// Add the component to the list, then decide if it's ready to pass to the player, or move to the next component
    /// </summary>
    private void NewComponent(Variable newVar)
    {
        toPass.Add(newVar);
        nextIndex += 1;
        if (nextIndex == toGather.Count)
        {
            nextIndex = -1;
            player.StartCasting(toPass);
            toPass = null;
            toGather = null;
            Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            ReadyNextGather();
        }
    }

    /// <summary>
    /// Activates whatever UI elements are required to gather the next component, if any
    /// </summary>
    private void ReadyNextGather()
    {
        switch (toGather[nextIndex].type)
        {
            case Types.Number:
                numberPicker.gameObject.SetActive(true);
                break;
            case Types.Direction:
                directionPicker.gameObject.SetActive(true);
                break;
            case Types.Object:
                waitingForClick = true;
                Cursor.SetCursor(cursorObject, Vector2.zero, CursorMode.Auto);
                break;
            case Types.Point:
                waitingForClick = true;
                Cursor.SetCursor(cursorPoint, Vector2.zero, CursorMode.Auto);
                break;
            case Types.Area:
                waitingForClick = true;
                Cursor.SetCursor(cursorArea, Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    /// <summary>
    /// Stretch the area bars to match the mouse input
    /// </summary>
    private void UpdateAreaBars()
    {
        Vector2 currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool showBars = currentPoint.x > areaStart.x && areaStart.y > currentPoint.y;
        foreach (GameObject bar in areaSelect)
            bar.SetActive(showBars);
        if (showBars)
        {
            float scaleToLength = 10;
            float width = currentPoint.x - areaStart.x;
            float height = areaStart.y - currentPoint.y;
            areaSelect[0].transform.position = new Vector2(areaStart.x + (width / 2), areaStart.y);
            areaSelect[0].transform.localScale = new Vector2(width * scaleToLength, 1);
            areaSelect[1].transform.position = new Vector2(currentPoint.x, currentPoint.y + (height / 2));
            areaSelect[1].transform.localScale = new Vector2(1, height * scaleToLength);
            areaSelect[2].transform.position = new Vector2(areaStart.x + (width / 2), currentPoint.y);
            areaSelect[2].transform.localScale = new Vector2(width * scaleToLength, 1);
            areaSelect[3].transform.position = new Vector2(areaStart.x, currentPoint.y + (height / 2));
            areaSelect[3].transform.localScale = new Vector2(1, height * scaleToLength);
        }
    }
}
