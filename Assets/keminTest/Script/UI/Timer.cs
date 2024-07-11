using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace keastmin
{
    public class Timer : MonoBehaviour
    {
        TextMeshProUGUI timerText;
        string timerString;
        float miliseconds = 0;
        int hour = 0;
        int minute = 0;
        int second = 0;

        void Start()
        {
            timerText = GetComponentInChildren<TextMeshProUGUI>();
        }

        void Update()
        {
            UpdateTime();
            SetTimer();
        }

        void UpdateTime()
        {
            miliseconds += Time.deltaTime;
            if(miliseconds >= 1f)
            {
                second++;
                miliseconds = 0;
            }
            if(second >= 60)
            {
                minute++;
                second = 0;
            }
            if(minute >= 60)
            {
                hour++;
                minute = 0;
            }
        }

        void SetTimer()
        {
            timerString = (hour > 0) ? hour.ToString() + ":" : "";
            timerString += (minute >= 10) ? minute.ToString() + ":" : "0" + minute.ToString() + ":";
            timerString += (second >= 10) ? second.ToString() : "0" + second.ToString();
            timerText.text = timerString;
        }
    }
}