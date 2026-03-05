using System;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateTimeDisplay(int secondsRemaining)
    {
        int minutes = secondsRemaining / 60;
        int seconds = secondsRemaining % 60;
        
        textMesh.text =string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
