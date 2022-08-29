using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreactZhuBoTiming : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public GameObject maskGo;
    int time;
    public void SetTime()
    {

        DataSaver.Instance.SetInt("CreactZhuBoTime", time);
    }
    //  public bool isFirstGame = true;
    public void GetTime()
    {
        if (DataSaver.Instance.HasKey("CreactZhuBoTime") == false)
        {
            time = 0;

        }
        else
            time = DataSaver.Instance.GetInt("CreactZhuBoTime");
    }
    void Start()
    {
        GetTime();
        if (time > 0)
        {
            Timing(time);
        }
        else
        {
            TimedEvent();
        }
    }
    void TimedEvent()
    {
        maskGo.SetActive(false);
    }
  public void ClickEvent()
    {
       
        Timing(180);
    }
    public void Timing(int time)
    {
        maskGo.SetActive(true);
       StartCoroutine( Timing(text, TimedEvent, time));
    }
    private void OnApplicationQuit()
    {
        SetTime();
    }
    private void OnApplicationPause(bool pause)
    {
        SetTime();
    }
    public  IEnumerator Timing(Text text, UnityEngine.Events.UnityAction action, int time1 = 180)
    {
         this.time = time1;
        text.text = string.Format("{0}",Global. GetMinuteTime(time));
        while (time >= 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            text.text = string.Format("{0}", Global.GetMinuteTime(time));
        }
        time = 0;
        text.text = string.Format("{0}", Global.GetMinuteTime(time));
        if (action != null)
        {
            action();
        }
    }
}
