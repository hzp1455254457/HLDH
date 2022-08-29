using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using System;

public class BoxTimer : MonoBehaviour
{
    public Text TimerText;
    DateTime nextTime;
    void Start()
    {
        int boxType = PlayerPrefs.GetInt(BoxGame.NextBoxType, 1);
        string nextTimeStr = PlayerPrefs.GetString(BoxGame.NextTime, DateTime.Now.AddMinutes(30).ToString());
        nextTime = DateTime.Parse(nextTimeStr);
    }
    void OnEnable()
    {
        InvokeRepeating("RefreshTimer",0.2f, 1);
    }
    void RefreshTimer()
    {
        if (nextTime > DateTime.Now)
        {
            //下一个快递 < color = red > 50分20秒 </ color > 后送出
            TimerText.text = "下一个快递<color=red>" + (nextTime - DateTime.Now).Minutes + "分" + (nextTime - DateTime.Now).Seconds + "秒</color>后送出";
        }
    }
    private void OnDisable()
    {
        CancelInvoke("RefreshTimer");
    }
}
