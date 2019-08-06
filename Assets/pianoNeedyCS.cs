using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class pianoNeedyCS : MonoBehaviour
{
    static int ModuleIdCounter = 1;
    int ModuleId;

    public KMAudio Audio;
    public KMSelectable[] buttons;
    public SpriteRenderer[] noteRenderers;
    public Sprite[] notes;
    public KMNeedyModule Module;

    private int index = 0;
    private int noteNumber = 0;
    bool isActive = false;

    void Awake()
    {
        GetComponent<KMNeedyModule>().OnNeedyActivation += OnNeedyActivation;
        GetComponent<KMNeedyModule>().OnNeedyDeactivation += OnNeedyDeactivation;

        ModuleId = ModuleIdCounter++;

        foreach (KMSelectable button in buttons)
        {
            KMSelectable pressedButton = button;
            button.OnInteract += delegate () { buttonPressed(pressedButton); return false; };
        }

        GetComponent<KMNeedyModule>().OnTimerExpired += OnTimerExpired;
    }

    protected bool buttonPressed(KMSelectable pressedButton)
    {
        Audio.PlaySoundAtTransform(pressedButton.name.ToString(), transform);

        pressedButton.AddInteractionPunch(0.5f);
        if (isActive)
        {
            if (pressedButton.name == noteRenderers[noteNumber].sprite.name)
            {
                if (noteNumber < 4)
                {
                    noteNumber++;
                }
                else
                {
                    noteNumber = 0;
                    GetComponent<KMNeedyModule>().OnPass();
                    isActive = false;
                }
            }
            else
            {
                noteNumber = 0;
                GetComponent<KMNeedyModule>().OnStrike();
                GetComponent<KMNeedyModule>().OnPass();
                isActive = false;
            }
        }
        return false;
    }

    protected void OnNeedyActivation()
    {
        isActive = true;
        for (int i = 0; i < 5; i++)
        {
            index = UnityEngine.Random.Range(0, 12);
            noteRenderers[i].sprite = notes[index];
        }
    }

    protected void OnNeedyDeactivation()
    {
        isActive = false;
    }

    protected void OnTimerExpired()
    {
        GetComponent<KMNeedyModule>().OnStrike();
        isActive = false;
    }

#pragma warning disable 414
#pragma warning disable 649
#pragma warning disable IDE0044 // Add readonly modifier
    private readonly string TwitchHelpMessage = @"Press the keys with !{0} press 1. Keys start from one and continue in reading order.";
    private bool TwitchPlaysActive;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore 414
#pragma warning restore 649

    IEnumerator ProcessTwitchCommand(string cmd)
    {
        var parts = cmd.ToLowerInvariant().Split(new[] { ' ' });
        if (parts.Length == 2 && parts[0] == "press" && parts[1].Length == 1)
        {
            if (parts[1] == "1")
            {
                buttonPressed(buttons[0]);
            }
            else if (parts[1] == "2")
            {
                buttonPressed(buttons[1]);
            }
            else if (parts[1] == "3")
            {
                buttonPressed(buttons[2]);
            }
            else if (parts[1] == "4")
            {
                buttonPressed(buttons[3]);
            }
            else if (parts[1] == "5")
            {
                buttonPressed(buttons[4]);
            }
            else if (parts[1] == "6")
            {
                buttonPressed(buttons[5]);
            }
            else if (parts[1] == "7")
            {
                buttonPressed(buttons[6]);
            }
            else if (parts[1] == "8")
            {
                buttonPressed(buttons[7]);
            }
            else if (parts[1] == "9")
            {
                buttonPressed(buttons[8]);
            }
            else if (parts[1] == "10")
            {
                buttonPressed(buttons[9]);
            }
            else if (parts[1] == "11")
            {
                buttonPressed(buttons[10]);
            }
            else if (parts[1] == "12")
            {
                buttonPressed(buttons[11]);
            }
            else
            {
                yield break;
            }
        }
        else
        {
            yield break;
        }
    }

    void DebugMsg(string msg)
    {
        Debug.LogFormat("[Needy Piano #{0}] {1}", ModuleId, msg);
    }
}