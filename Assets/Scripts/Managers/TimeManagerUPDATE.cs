﻿using Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManagerUPDATE : MonoBehaviour
{
    [Header("Day")]
    public Day GameDay;

    [Header("Timer")]

    [Tooltip("The whole day in game (In Minutes)")]
    public float wholeDayInGame = 60;

    [GreyOut]
    [SerializeField]
    float wholeDayInSeconds;

    [GreyOut]
    [SerializeField]
    float runningTime;

    [Header("Events")]
    public GameEvent StartDay;
    public GameEvent StartNight;
    public GameEvent EndDay;

    [Header("Shared Data")]
    public FloatField dayPercentage;

    private void Start()
    {
        wholeDayInSeconds = wholeDayInGame * 60;
    }

    private void Update()
    {
        runningTime += Time.deltaTime;
        CalculatePercentage();
    }

    private void CalculatePercentage()
    {
        float percentage = (wholeDayInSeconds - runningTime) / wholeDayInSeconds;
        dayPercentage.SetValue(percentage);

        if (runningTime >= wholeDayInSeconds)
        {
            StartDay.Raise();
            GameDay = (Day)(((int)GameDay + 1) % 7);
            runningTime = 0;
        }
    }

    void ResetTimer()
    {
        runningTime = 0;
    }

    public void LoadTimer(float rTime)
    {
        runningTime = rTime;
    }

    public float GetRunningTime()
    {
        return runningTime;
    }

    public float GetWholeDayInSeconds()
    {
        return wholeDayInSeconds;
    }
}
