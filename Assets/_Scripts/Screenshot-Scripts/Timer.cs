using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    public float timeGone = 0;
    public bool timerIsRunning = false;
    public TextMeshPro timeText;
    private DateTime startTime;
    private string hour, minute, second, timeOutput;
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        startTime = DateTime.Now;
        hour = LeadingZero( startTime.Hour );
        minute = LeadingZero( startTime.Minute );
        second = LeadingZero( startTime.Second );
        timeOutput = "um " + hour + ":" + minute + ":" + second + " Uhr.";
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
        output += timeOutput;
        timeText.text = output.ToString();
    }

    private string LeadingZero (int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}