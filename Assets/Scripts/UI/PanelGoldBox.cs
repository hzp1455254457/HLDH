using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGoldBox : PanelBase
{
    public static PanelGoldBox Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("PanelGoldBox")) as PanelGoldBox;
                instance.gameObject.SetActive(false);
                return instance;
            }
            return instance;
        }
    }
    static PanelGoldBox instance;
    int count;
    public Text initText, targetText;
    public Button get, exit,goToTip;
    public GameObject exitGo;
    public override  void Show()
    {
        gameObject.SetActive(true);
        transform.SetParent(UIManager.Instance.showRootMain);
        //transform.SetAsLastSibling();

    }
    UnityEngine.Events.UnityAction callBack;
    UnityEngine.Events.UnityAction GoToTipscallBack;
    /// <summary>
    /// 金币不足弹窗
    /// </summary>
    /// <param name="countInit">需要多少</param>
    /// <param name="countTarget">还差多少</param>
    /// <param name="unityAction">获取金币回调</param>
    /// <param name="goToTipsAction">去发货回调</param>
    public void ShowUI(int countInit, int countTarget, UnityEngine.Events.UnityAction getCoinsAction, UnityEngine.Events.UnityAction sendGoodsTipsAction)
    {
        AudioManager.Instance.PlaySound("buzu");
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
        base.Animation();
        count = countTarget;
        //goldText.text = count.ToString() + "个";
        initText.text = countInit.ToString();
        targetText.text = countTarget.ToString();
        AndroidAdsDialog.Instance.UploadDataEvent("show_get_coins_tc");
        callBack = getCoinsAction;
        GoToTipscallBack = sendGoodsTipsAction;
        exitGo.SetActive(false);
        ShowExit();
    }
    private void ShowExit()
    {
        StartCoroutine(Global.Delay(1f, ()=>exitGo.SetActive(true)));
        goToTip.gameObject.SetActive(!GuideManager.Instance.isFirstGame);
    }
    public void GetClickEvent()
    {
        AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_GetGoldBoxAdward);
        AndroidAdsDialog.Instance.UploadDataEvent("click_coins_tc");
#if UNITY_EDITOR
        GetAdward();


#elif UNITY_ANDROID
 
      
        
#endif
    }
    public void GetAdward()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("finish_conis_tc");
        PlayerData.Instance.GetGold(count,false);
        Hide();
        AndroidAdsDialog.Instance.ShowToasts(string.Format("+{0}", count), ResourceManager.Instance.GetSprite("金币"), Color.black);
    }
    public override void Hide()
    {
        callBack?.Invoke();
        ClearnAction();
    }

    private void ClearnAction()
    {
        gameObject.SetActive(false);
        callBack = null;
        GoToTipscallBack = null;
    }

    public void GoToTip()
    {
        GoToTipscallBack?.Invoke();
        ClearnAction();
        
       
    }
    public void HideUI()
    {
        ClearnAction();
        AndroidAdsDialog.Instance.UploadDataEvent("clos_coins_tc");
        // AndroidAdsDialog.Instance.ShowTableVideo("0");
    }
    protected override void Awake()
    {
        //base.Awake();
        get.onClick.AddListener(GetClickEvent);
        exit.onClick.AddListener(HideUI);
        goToTip.onClick.AddListener(GoToTip);
    }

}
