using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class hongbao6 : PanelBase
{
    // Start is called before the first frame update
  //  public Text titleText, hongbaoNumberText,leftHongbaoNumberText,NeedToMakeHongBaoNumberText;
   // public Image lightImage;

    public static hongbao6 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("hongbao6")) as hongbao6;
                instance.gameObject.SetActive(false);
                return instance;
            }
            return instance;
        }

    }
    static hongbao6 instance;

    int awardCount;
    public Text countText, redCount, countName, topName;
    int _redCount = 0;
    public Image goldImage;
    public GameObject tips;
    public RectTransform rectTransform;
    protected override void Awake()
    {
        //Instance = this;
        //gameObject.SetActive(false);
        JavaCallUnity.Instance.feedCallBackAction += SetTransForm;
    }
    private void OnDestroy()
    {
        JavaCallUnity.Instance.feedCallBackAction -= SetTransForm;
    }
    int typesGold;
    public override void Show()
    {
        gameObject.SetActive(true);
        transform.SetParent(UIManager.Instance.showRootMain);
        //transform.SetSiblingIndex(3);
    }
    string spriteName;
    UnityEngine.Events.UnityAction unityAction1 = null;
    public void ShowUI(int count1, int count2, int type = 0, UnityEngine.Events.UnityAction unityAction = null, string name = "发货红包奖励")
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        RecoverTransForm();
        topName.text = name;
        transform.SetAsLastSibling();
        unityAction1 = unityAction;
        gameObject.SetActive(true);
        _redCount = count1;
        redCount.text ="+"+ (_redCount / MoneyManager.redProportion).ToString("f3") + "元";
        awardCount = count2;
        countText.text = string.Format("+{0}个", count2);
        //tixianCount.text=string.Format("领取后余额≈{0:F}元", (PlayerData.Instance.red + count1) / MoneyManager.redProportion);
        //Show();
        base.Animation(()=> {
           // SetTransForm();
            ShowFeed();
        }, 1.15f);
        ButtonAnim();
       
        //AndroidAdsDialog.Instance.ShowFeedAd(540);
        typesGold = type;
        if (type == 0)
        {
            spriteName = "金币";
            countName.text = "金币";
        }
        else
        {
            spriteName = "钻石";
            countName.text = "钻石";
        }
        if (count2 == 0)
        {
            tips.SetActive(false);
        }
        else
        {
            tips.SetActive(true);
        }
        goldImage.sprite = ResourceManager.Instance.GetSprite(spriteName);

    }
    private void ShowFeed()
    {
        if (gameObject.activeInHierarchy)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            coroutine= StartCoroutine(  Global.Delay(0.5F, () => {
                if (gameObject.activeInHierarchy)
                    AndroidAdsDialog.Instance.ShowFeedAd(540);
            }));

        }
    }
    Coroutine coroutine;
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
    RectTransform rectTransform1;
    float time = 0.8f;
    public void SetTransForm()
    {
        if (gameObject.activeInHierarchy)
        {
            if (rectTransform1 == null)
            {
                rectTransform1 = backTf.GetComponent<RectTransform>();
            }
            rectTransform.DOAnchorPosY(-10, time);
            rectTransform.DOAnchorMax(new Vector2(0.5f, 1), time);
            rectTransform.DOAnchorMin(new Vector2(0.5f, 1), time);
            rectTransform.DOPivotY(1, time);
        }
    }
    public void RecoverTransForm()
    {
        if (rectTransform == null)
        {
            rectTransform = backTf.GetComponent<RectTransform>();
        }
        rectTransform.anchorMin = new Vector2(0.5f, 0f);
        rectTransform.anchorMax = new Vector2(0.5f, 0f);
        rectTransform.pivot = new Vector2(0.5f, 0f);
        rectTransform.anchoredPosition = new Vector2(0, 400);
        // rectTransform.DOAnchorPosY(-365, 1.3f);
    }
    public void GetAward()
    {
        StopButtonAnim();
        Time.timeScale = 1;
        AudioManager.Instance.PlaySound("finish_redpacket");

        PlayerData.Instance.GetRed(_redCount);
        gameObject.SetActive(false);
        unityAction1?.Invoke();//发货场景发货红包增加发货点击次数
        unityAction1 = null;
        if (typesGold == 0)
        {
            AndroidAdsDialog.Instance.UploadDataEvent("finish_new_fahuo_redpacket");
            PlayerData.Instance.GetGold(awardCount, false);//涉及到网店增加金币
            AndroidAdsDialog.Instance.AddSignDataCount(1);
            AndroidAdsDialog.Instance.addRedPacketCount();
            //if (PlayerDate.Instance.ClickFaHuoRedCount == 2)
            //{
            //    ToggleManager.Instance.isFirstShowChouJiang = true;
            //    PeopleEffect.Instance.ShowMask(0.5f, () => { PeopleEffect.Instance.SetTips(ToggleManager.Instance.guiTaget5, ToggleManager.Instance.guiTaget6.position, true, RotaryType.TopToBottom); });
            //}
            if (PlayerData.Instance.ClickFaHuoRedCount == 1 && GuideManager.Instance.isFirstGame)
            {
                AndroidAdsDialog.Instance.UploadDataEvent("new_version_guide7");
            }
        }
        else
        {
            if (awardCount > 0)
                PlayerData.Instance.GetDiamond(awardCount);
        }
        if (awardCount > 0)
        {
            TipsShowBase.Instance.Show("TipsShow4", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
      {
            string.Format("+{0}",awardCount)
          }, new Sprite[]
          {
              ResourceManager.Instance.GetSprite( spriteName)
          }, null,


          () => { if (typesGold == 0) { PlayerData.Instance.AddWangDianGold(awardCount); } });
        }
        AndroidAdsDialog.Instance.CloseFeedAd();

        AwardManagerNew.Instance.isShow = false;


    }


}
    

