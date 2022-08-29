using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AwardManagerNew : PanelBase
{
    //public static AwardManagerNew Instance;
    public static AwardManagerNew Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("AwardManagerNew")) as AwardManagerNew;
                instance.gameObject.SetActive(false);
                return instance;
            }
            return instance;
        }

    }
    static AwardManagerNew instance;
    public bool isShow = false;
  //  public string ClickAward = "isFirstClickAward", DropSellProduce= "isFirstDropSellProduce";
    public GameObject closeBt;
   // public Text infos;
    public Text countText;
   // public Image goldImage;
  //  public string[] stringArrys = new string[] { "万人享受带货分红", "赏金提现无限制", "多卖多得送赏金" };
   // public string[] stringArrys = new string[1] { "可以获得" };
    public GameObject iconGo;
    public GameObject tipsGo;
    public CanvasGroup canvasGroup1;
    public GameObject smallBt;
    protected override void Awake()
    {
        instance = this;
        JavaCallUnity.Instance.BannerCallBackAction += SetAnimTime;
        //gameObject.SetActive(false);
    }
    float animTime = 0.3f;
    bool isAnim = false;
    public void SetAnimTime(bool value)
    {
        if (gameObject.activeInHierarchy)
        {
            Debug.Log("改变动画时间");
            if (value)
            {
                animTime = 0.3f;

            }
            else
            {
                animTime = 0.5f;
            }
            if (!isAnim)
            {
                base.Animation(null, 1f, animTime);
                isAnim = true;
            }
        }
    }
    public override void Show()
    {
        //gameObject.SetActive(true);
        transform.SetParent(UIManager.Instance.showRootMain);
        //transform.SetSiblingIndex(3);
    }

    int typesGold;
    string spriteName;
    private UnityEngine.Events.UnityAction unityAction1;
    private UnityEngine.Events.UnityAction CloseunityAction1;
    int type = 0;
    RectTransform rectTransform;
    public void SetTransForm()
    {
        if (rectTransform == null)
        {
            rectTransform = backTf.GetComponent<RectTransform>();
        }
        rectTransform.anchorMin = new Vector2(0.5f, 1);
        rectTransform.anchorMax = new Vector2(0.5f, 1);
        rectTransform.DOAnchorPosY(-365, 0.8f);
    }
    // public Image imagetips;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="count">货币值</param>
    /// <param name="count1"></param>
    /// <param name="type"></param>
    /// <param name="unityAction"></param>
    /// <param name="CloseunityAction"></param>
    public void ShowUI(int count,int count1,int type=0, UnityEngine.Events.UnityAction unityAction=null, UnityEngine.Events.UnityAction CloseunityAction = null)
    {
        if (isShow) return;
        isShow = true;
       isAnim = false;
        animTime = 0.3f;
        gameObject.SetActive(true);
       backTf.localScale = Vector3.zero;
        AndroidAdsDialog.Instance.ShowBannerAd();
        AudioManager.Instance.PlaySound("show_redpacket");
        AndroidAdsDialog.Instance.UploadDataEvent("show_new_fahuo_redpacket");
        transform.SetAsLastSibling();
        canvasGroup1.alpha = 0;
        unityAction1 = null;
        CloseunityAction1 = null;
      unityAction1 = unityAction;
        CloseunityAction1 = CloseunityAction;
      
       // SetTransForm();
       
       
        ButtonAnim();
        this.type = type;
        //infos.text = stringArrys[0];
        ShowClose();
        countValue1 = count;
        countValue2 = count1;
        typesGold = type;
        if (type == 0)
        {
            countText.text = "+100" + "元";
            spriteName = "金币";
         if(!GuideManager.Instance.isFirstGame)
          canvasGroup1.DOFade(1, 1.5f).SetUpdate(true);
        }
        else
        {
            countText.text ="+"+ Random.Range(40f, 60f).ToString("f3") + "元";
            spriteName = "钻石";
        }
        AndroidAdsDialog.Instance.CloseFeedAd();

        if (!AndroidAdsDialog.Instance.isBroadcast)
        {
            base.Animation(null, 1f, animTime);
        }
     
    }
 public   int countValue1, countValue2;
    void ShowClose()
    {
        if (!GuideManager.Instance.isFirstGame)
        {

           closeBt.SetActive(false);
            smallBt.SetActive(true);

        }
        else
        {
            smallBt.SetActive(false);
            closeBt.SetActive(false);
           
        }
        if (GuideManager.Instance.isFirstGame)
        {
            //iconGo.SetActive(false);
            //PeopleEffect.Instance.SetTips2(null, iconGo.transform.position,UIManager.Instance.showRootMain);
            tipsGo.SetActive(true);
            //iconGo.SetActive(true);
            //tipsGo.SetActive(false);
        }
        else
        {
            //iconGo.SetActive(true);
            tipsGo.SetActive(false);
        }
    }
    public Transform buttonTf;
    Sequence queen;
    public void ButtonAnim()
    {
        queen = DOTween.Sequence();
        queen.Append(buttonTf.DOScale(1.2f, 1.0f));
        queen.AppendInterval(0.3f);
        queen.Append(buttonTf.DOScale(1.0f, 1.0f));
        queen.SetLoops(-1);
    }
    public void StopButtonAnim()
    {
        if (queen != null)
        {
            queen.Kill();
            buttonTf.localScale = Vector3.one;
        }
    }
    public void CloseUI()
    {
        StopButtonAnim();
        Time.timeScale = 1;
        //AndroidAdsDialog.Instance.ShowTableVideo("1");
        print("关闭ui");
        gameObject.SetActive(false);
        isShow = false;
   
        //if (typesGold == 0)
        //{
        //    spriteName = "金币";
        //    PlayerData.Instance.GetGold(countValue1,false);
        //}
        //else
        //{
        //    spriteName = "钻石";
        //    PlayerData.Instance.GetDiamond(countValue1);
        //}
      //  TipsShowBase.Instance.Show("TipsShow4", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
      //{
      //      string.Format("+{0}",countValue1)
      //    }, new Sprite[]
      //    {
      //          ResourceManager.Instance.GetSprite(spriteName)
      //    }, null,()=> { if (typesGold == 0) { PlayerData.Instance.AddWangDianGold(countValue1);  } });

      CloseunityAction1?.Invoke();
        CloseunityAction1 = null;
        unityAction1 = null;
        AndroidAdsDialog.Instance.CloseBanner();
        if (GuideManager.Instance.isFirstGame)
        {
            clickGuide1++;
            if (clickGuide1 == 1)
            {
                AndroidAdsDialog.Instance.UploadDataEvent("new_guide_7_close");
            }
        }
        AndroidAdsDialog.Instance.ShowTableVideo("0");
    }
    public void GetSmallAward()
    {
        StopButtonAnim();
        AndroidAdsDialog.Instance.CloseBanner();
        GetAdwardManagerNew.Instance.ShowUI((int)((0.1f * MoneyManager.redProportion)), countValue1, typesGold, unityAction1);
        if (type == 0)
        {
            UnityActionManager.Instance.DispatchEvent<int>("GetFaHuoRed", 1);
        }
        gameObject.SetActive(false);
       
    }
        //unityAction1 = null;
       
        public void GetAward()
    {
        if (type == 0)
        {
            AndroidAdsDialog.Instance.UploadDataEvent("click_new_fahuo_redpacket");
        }
        AndroidAdsDialog.Instance.CloseBanner();
        if (!GuideManager.Instance.isFirstGame)
        { AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_GOLD);
#if UNITY_EDITOR
            
                JavaCallUnityEvent();
            //JavaCallUnityEvent();

#elif UNITY_ANDROID
       
#endif

        }
        else
        {
            //if (PlayerDate.Instance.ClickFaHuoRedCount <= 0)
            //{ JavaCallUnityEvent(); PeopleEffect.Instance.HideTips(); }
            //else
            //{
                PeopleEffect.Instance.HideTips();
                AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_GOLD);
#if UNITY_EDITOR

            JavaCallUnityEvent();
            //JavaCallUnityEvent();

#elif UNITY_ANDROID
       
#endif
            //}
        }




    }
    int clickGuide = 0;
    int clickGuide1 = 0;
    public void JavaCallUnityEvent()
    {
        StopButtonAnim();
        GetAdwardManagerNew.Instance.ShowUI(NumberGenenater.GetRedCount(), countValue1*2,typesGold,unityAction1,"发货红包奖励",true);
        if (type == 0)
        {
            UnityActionManager.Instance.DispatchEvent<int>("GetFaHuoRed", 1);
        }
       //unityAction1 = null;
        gameObject.SetActive(false);
        if (GuideManager.Instance.isFirstGame)
        {
            clickGuide++;
            if (clickGuide == 1)
            {
                AndroidAdsDialog.Instance.UploadDataEvent("new_version_guide6");
            }
        }
    }
}
public enum VideoType
{
    ShengJiZhuBo=0,
    Defult=1
}