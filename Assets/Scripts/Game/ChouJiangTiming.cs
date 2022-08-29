using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChouJiangTiming : MonoBehaviour
{
    [Header("抽奖倒计时text组件")]
    public Text timeText;
    public GameObject[] statesChouJiang;
    //int time = 0;
    public GameObject tipsGo;

  //  public void SetisFistGame()
  //  {

  //      DataSaver.Instance.SetInt("time", time);
  //  }
  ////  public bool isFirstGame = true;
  //  public void GetisFirstGame()
  //  {
  //      if (DataSaver.Instance.HasKey("time") == false)
  //      {
  //          time = 0;

  //      }
  //      else
  //          time = DataSaver.Instance.GetInt("time");
  //  }
    int times = 90;
    private void Start()
    {
        //GetisFirstGame();
        
        UnityActionManager.Instance.AddAction("timing", ()=>TimingEvent(times));
        if (PlayerData.Instance.time != 0)
        {
            TimingEventStart(PlayerData.Instance.time);
            ShowStatus(false);
        }
        else
        {

            ShowStatus(true);
        }
    }

  public bool IsTiming()
    {
        if (PlayerData.Instance.time == 0)
        {
            return true;
        }
        return false;
    }
    IEnumerator Timing( UnityEngine.Events.UnityAction action, int time= 180)
    {
        PlayerData.Instance.time = time;
        timeText.text = string.Format("{0}", Global.GetMinuteTime(PlayerData.Instance.time));
        while (PlayerData.Instance.time >= 0)
        {
            yield return new WaitForSeconds(1f);
            PlayerData.Instance.time--;
            timeText.text = string.Format("{0}", Global.GetMinuteTime(PlayerData.Instance.time));
        }
        PlayerData.Instance.time = 0;
        timeText.text = string.Format("{0}", Global.GetMinuteTime(PlayerData.Instance.time));
        if (action != null)
        {
            action();
        }
    }
    private void Timinged()
    {
        ShowStatus(true);
        tipsGo.SetActive(false);
        MyShopPanel.Instance.ShowQiPao(false);
        MyShopPanel.Instance.rotaryTablePanel.ShowSelect(null);
        ToggleManager.Instance.SetTips(0);
    }
   int GetTimes()
    {
        switch (PlayerData.Instance.ChouJiangCount)
        {
            case 1:return 30;
            case 2: return 60;
            case 3: return 90;
            default: return 90;

        }
    }
    private void ShowStatus(bool value)
    {
        statesChouJiang[0].SetActive(value);
        statesChouJiang[1].SetActive(!value);
    }

    private void StartTiming(bool value)
    {
     ZhiBoPanel.Instance.wangDianDaRenUI.   PlayAnim(false);
        ShowStatus(false);
        if (value)
        {
            MyShopPanel.Instance.ChouJiangedEvent1(0, value);
           // tipsGo.SetActive(true);
        }
        else
        {

        }
    }
    
    private void TimingEvent(int time)
    {
        times = GetTimes();
        StartTiming(false);
        StartCoroutine(Timing(Timinged, times));


    }
    private void TimingEventStart(int time)
    {

        StartTiming(true);
        StartCoroutine(Timing(Timinged, time));

    }
    //private void OnApplicationQuit()
    //{
    //    SetisFistGame();
    //}
    //private void OnApplicationPause(bool pause)
    //{
    //    SetisFistGame();
    //}
}
