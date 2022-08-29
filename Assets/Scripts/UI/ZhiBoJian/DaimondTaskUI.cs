using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaimondTaskUI : MonoBehaviour
{
    public static DaimondTaskUI Instance;
    //public GameObject[] tipGOs;
    //public Text countText,coldTime;
    //public Button button;
    //public Image image;
    //int count;
    //public GameObject tipsRedGo;

    //public Button recoverBtn;
    //public GameObject coldTimeGo;
    public CanvasGroup canvasGroup;
    private void Awake()
    {
        Instance = this;
       
    }
    //public void ShowTips(bool value)
    //{
    //    if (value)
    //    {
    //        if (!tipsRedGo.activeSelf)
    //        {
    //            tipsRedGo.SetActive(value);
    //        }

    //    }
    //    else
    //    {
    //        if (tipsRedGo.activeSelf)
    //        {
    //            tipsRedGo.SetActive(value);
    //        }
    //    }
    //}
    public void Show(bool value)
    {
        if (value)
        {
            canvasGroup.alpha = 1;

            canvasGroup.interactable = true;

            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.interactable = false;
              canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts =false;
        }
    }
    private void Start()
    {
        //AndroidAdsDialog.Instance.unityInit();
        //button.onClick.AddListener(ClickEvent);
        //count = PlayerPrefs.GetInt("DaimondRemainderCount",65);
        //SetRemainderCount(count);
        //  PlayerDate.Instance.s
        if (GuideManager.Instance.isFirstGame)
        {
            Show(false);
        }
       //else
       // {
       //     if (PlayerData.Instance.coldTime > 0)
       //     {
       //         SetColdTimeStatus(true);
       //         StartTime();
       //         AndroidAdsDialog.Instance.setIsDiamondClickLocked(true);
       //     }
       //     else
       //     {
       //         AndroidAdsDialog.Instance.setIsDiamondClickLocked(false);
       //     }

       // }
       // gameObject.SetActive(PlayerData.Instance.IsShowDaimondTaskUI);
       // recoverBtn.onClick.AddListener(StopTime);
       //JavaCallUnity.Instance.SetRemainderCount("5");
        
       //  JavaCallUnity.Instance.SetDaimonTaskColdTime("500");
     //  JavaCallUnity.Instance.ShowRecoverBtn("1");
    }
  //  public IEnumerator Timing()
  //  {
  //      // this.time = time;
  //      //text.text = string.Format("{0}", GetMinuteTime(time));
       
  //      coldTime.text = string.Format("{0}", Global.GetMinuteTime(PlayerData.Instance.coldTime));
  //      while (PlayerData.Instance.coldTime >= 0)
  //      {
  //          yield return new WaitForSeconds(1f);
  //          PlayerData.Instance.coldTime--;
  //          coldTime.text = string.Format("{0}",Global. GetMinuteTime(PlayerData.Instance.coldTime));
  //      }
  //      //PlayerDate.Instance.coldTime?.Invoke();
  //      PlayerData.Instance.coldTime = 0;
  //      AndroidAdsDialog.Instance.setIsDiamondClickLocked(false);
  //      coldTime.text = string.Format("{0}", Global.GetMinuteTime(PlayerData.Instance.coldTime));
  //      SetColdTimeStatus(false);
  //      ShowRecoverBtn(false);
  //  }
  //  public void SetColdTime(int time)
  //  {
  //      if (PlayerData.Instance.coldTime > 0) return;
  //      PlayerData.Instance.coldTime = time;
  //      if (PlayerData.Instance.coldTime > 0)
  //      {
  //          AndroidAdsDialog.Instance.UploadDataEvent("enter_daojishi_zuanshi");
  //          SetColdTimeStatus(true);
  //          StartTime();
  //      }
  //      else
  //      {
  //          SetColdTimeStatus(false);
  //      }

  //  }

  //  public void SetColdTimeStatus(bool value)
  //  {
  //      coldTimeGo.SetActive(value);
  //  }
  //  public void ShowRecoverBtn(bool value)
  //  {
  //      recoverBtn.gameObject.SetActive(value);
  //  }
  //  public void StartTime()
  //  {
  //      StartCoroutine(Timing());
  //  }
  //  public void StopTime()
  //  {
  //      AndroidAdsDialog.Instance.UploadDataEvent("show_ad_zuanshi_daojishi");
  //      AndroidAdsDialog.Instance.ShowRewardVideo("恢复冷却时间为0", () =>
  //      {
  //          PlayerData.Instance.coldTime = 0;
  //          AndroidAdsDialog.Instance.setIsDiamondClickLocked(false);
  //          AndroidAdsDialog.Instance.UploadDataEvent("finish_ad_zuanshi_daojishi");
  //      });
       
  //  }
  //  public void SetRemainderCount(int value)
  //  {
  //      count = value;
  //      countText.text =string.Format("还差<size=30><color=red>{0}</color></size>次可提现",count) ;
  //      if (count <= 0)
  //      {
  //          ShowTiXian(true);
  //      }
  //      else
  //      {
  //          ShowTiXian(false);
  //      }
  //      //PlayerPrefs.SetInt("PlayerPrefs.GetInt",count);
  //  }
  //  public void SetText(string value)
  //  {
  //      countText.text = value;
  //      ShowTiXian(false);


  //  }
  //public void ShowDaimondTipsPanel()
  //  {
  //    Instantiate( ResourceManager.Instance.GetProGo("DaimondTipPanel"),UIManager.Instance.showRootMain,false);
  //  }
  //  private void ShowTiXian(bool value)
  //  {
  //     // button.interactable = value;
  //      SetStatus(value);
  //     // image.raycastTarget = value;
  //  }
  //  private void ClickEvent()
  //  {
  //      print("打开钻石任务");
  //      AndroidAdsDialog.Instance.ShowDiamondDialog();
  //      ShowTips(false);
  //      //if (count > 1)
  //      //{ count--;
  //      //    SetRemainderCount(count);
  //      //}
  //  }
  //  private void SetStatus(bool value)
  //  {
  //      tipGOs[0].SetActive(value);
  //      tipGOs[1].SetActive(!value);
  //  }
    

}
