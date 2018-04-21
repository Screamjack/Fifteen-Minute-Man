using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    Text clockText;

    void Awake()
    {
        clockText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }

    public void UpdateUI(int seconds)
    {
        int minutes = seconds / 60;
        int displaySeconds = seconds % 60;
        string minutesString = minutes.ToString().PadLeft(2, '0');
        string SecondsString = displaySeconds.ToString().PadLeft(2, '0');
        string toDisplay = minutesString + ":" + SecondsString;

        clockText.text = toDisplay;
    }

}
