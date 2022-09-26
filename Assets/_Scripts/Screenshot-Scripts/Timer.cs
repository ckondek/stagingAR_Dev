using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    public float timeGone = 0;
    public bool timerIsRunning = false;
    public TextMeshPro timeText;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            timeGone += Time.deltaTime;
            DisplayTime(timeGone);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float hours = Mathf.FloorToInt(timeToDisplay / 3600);
        float minutes = Mathf.FloorToInt(timeToDisplay / 60) - hours*3600;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        string output = string.Format("Diese Erinnerung entstand vor {0:00} Stunden, {1:00} Minuten und {2:00} Sekunden, ", hours, minutes, seconds);
        timeText.text = output.ToString();
    }
}