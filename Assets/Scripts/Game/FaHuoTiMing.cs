using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaHuoTiMing : MonoBehaviour
{
    public GameObject[] statusGos;
    [Header("倒计时text组件")]
    public Text timeText;
    public Image faHuoImg;

    public Graphic[] graphics;

   // public Graphic[] graphicbtn;
    public void StartFaHuo()
    {
        SetStatus(false);
        FaHuoPanel.Instance.faHuoToggle.OnPointerUp1();
        Global.Fade(graphics, 1, 0.1f);
      
    }
    public void RecoverFaHuo()
    {
        SetStatus(true);
        faHuoImg.fillAmount = 0;
    }
    public void AchiveFaHuo()
    {
        //SetStatus(true);
        Global.Fade(graphics, 0, 2f);
        FaHuoPanel.Instance.carManager.EndEvent();
    }
    void SetStatus(bool value)
    {
        statusGos[0].SetActive(value);

        statusGos[1].SetActive(!value);
    }
    
    void Start()
    {
       // UnityActionManager.Instance.AddAction("FaHuoEvent", ()=>TimingEvent(180));
    }
    public void TimingEvent(int time)
    {
        AndroidAdsDialog.Instance.UploadDataEvent("new_fahuo_success");
        StartFaHuo();
        //StartTiming();
        StartCoroutine(Global. Timing(timeText, AchiveFaHuo, time));

    }
    // Update is called once per frame

}
