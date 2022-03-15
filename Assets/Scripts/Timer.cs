using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteLevel = 30f;

    float fillFraction;

    float timerValue;

    void Start()
    {
        timerValue = timeToCompleteLevel;
    }

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        fillFraction = timerValue / timeToCompleteLevel;
    }

    public float GetFillFraction()
    {
        return fillFraction;
    }

    public float GetTimerValue()
    {
        return timerValue;
    }
}
