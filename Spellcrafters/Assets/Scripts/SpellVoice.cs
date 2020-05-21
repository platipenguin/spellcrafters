using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellVoice : MonoBehaviour
{
    private float yOffset = 0f;

    private float ticker = 0f;

    private float duration = 0f;

    void Update()
    {
        if (duration > 0)
        {
            TextMesh voice = GetComponent<TextMesh>();

            ticker += Time.deltaTime;
            Color voiceColor = voice.color;
            voiceColor.a += Time.deltaTime / (duration * 0.25f);

            if (ticker > duration)
            {
                voiceColor.a = 0;
                voice.text = "";
                duration = 0;
                ticker = 0;
            }
            voice.color = voiceColor;
        }
    }

    private void StartSpeak(string message, float length, Color startColor)
    {
        TextMesh voice = GetComponent<TextMesh>();
        voice.text = message;
        voice.color = startColor;
        duration = length;
        ticker = 0;
    }

    /// <summary>
    /// Make the message appear over time
    /// </summary>
    public void Speak(string message, float length)
    {
        StartSpeak(message, length, new Color(0, 0, 0, 0));
    }

    /// <summary>
    /// Make the message appear instantly
    /// </summary>
    public void SpeakInstant(string message, float length)
    {
        StartSpeak(message, length, Color.black);
    }

    /// <summary>
    /// Set how far above the parent spellcaster this voice sits
    /// </summary>
    public void SetOffset(float y)
    {
        yOffset = y;
    }

    /// <summary>
    /// Update the position of this voice
    /// </summary>
    public void UpdatePosition(Vector3 objPosition)
    {
        transform.position = new Vector3(objPosition.x, objPosition.y + yOffset, 0);
    }
}
