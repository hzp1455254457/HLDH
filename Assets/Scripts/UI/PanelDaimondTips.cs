using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelDaimondTips : PanelBase
{
    public static PanelDaimondTips Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("PanelDaimondTips")) as PanelDaimondTips;
                instance.gameObject.SetActive(false);
                return instance;
            }
            return instance;
        }
    }
    static PanelDaimondTips instance;
    public Text  awardText;
    public GameObject qipaoGo;
    public override void Show()
    {
        gameObject.SetActive(true);
    
        transform.SetParent(UIManager.Instance.showRootMain);
        //transform.SetAsLastSibling();
    }
    UnityEngine.Events.UnityAction callBack;
    // int currentIndex = 0;
    int count;
    public void ShowUI(string value, bool showQiPao,UnityEngine.Events.UnityAction unityAction)
    {
      
        awardText.text = value;
        AndroidAdsDialog.Instance.UploadDataEvent("Show_zuanshi_not_enough");
        AudioManager.Instance.PlaySound("buzu");
        //count = countTarget;
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
        base.Animation();
        qipaoGo.SetActive(showQiPao);
        AndroidAdsDialog.Instance.ShowFeedAd(540);
        callBack= unityAction;
       
        //currentIndex = index;
    }
    public void GoToTips()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("click_zuanshi_not_enough");
        //AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_GetDaimondBoxAdward);
        JavaCallUnityEvent();


    }
    public void JavaCallUnityEvent()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("finish_zuanshi_not_enough");
        //PlayerDate.Instance.GetDiamond(3*count);
        //AndroidAdsDialog.Instance.ShowToasts(string.Format("+{0}", 3 * count), ResourceManager.Instance.GetSprite("×êÊ¯"), Color.black);
        callBack?.Invoke();
        Hide();
    }
    public override void Hide()
    {
       // AndroidAdsDialog.Instance.UploadDataEvent("clos_coins_tc");
        gameObject.SetActive(false);
        callBack = null;
        AndroidAdsDialog.Instance.CloseFeedAd();
    }
    public void HideUI()
    {
        Hide();
        AndroidAdsDialog.Instance.UploadDataEvent("close_zuanshi_not_enough");

      AndroidAdsDialog.Instance.ShowTableVideo("0");
    }
}
