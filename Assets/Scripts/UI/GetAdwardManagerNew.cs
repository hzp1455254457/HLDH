using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAdwardManagerNew : PanelBase
{
    public static GetAdwardManagerNew Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("PanelAdwardNew")) as GetAdwardManagerNew;
                instance.gameObject.SetActive(false);
                return instance;
            }
            return instance;
        }
    
    }
    static GetAdwardManagerNew instance;
  
    int awardCount;
  public  Text countText, redCount,countName,topName;
    int _redCount = 0;
    public Image goldImage;
    public GameObject tips;
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
    public override   void Show()
    {
        gameObject.SetActive(true);
        transform.SetParent(UIManager.Instance.showRootMain);
        //transform.SetSiblingIndex(3);
    }
    string spriteName;
    UnityEngine.Events.UnityAction unityAction1 = null;
    bool isShowVideo = false;
    public void ShowUI(int count1,int count2,int type=0, UnityEngine.Events.UnityAction unityAction = null,string name="�����������",bool isShowVideo=false)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
       this. isShowVideo = isShowVideo;
        RecoverTransForm();
        topName.text = name;
        transform.SetAsLastSibling();
        unityAction1 = unityAction;
        gameObject.SetActive(true);
        _redCount = count1;
        redCount.text ="+"+( _redCount / MoneyManager.redProportion).ToString("f3")+"Ԫ";
        awardCount = count2;
        countText.text = string.Format("+{0}��", count2);
        //tixianCount.text=string.Format("��ȡ������{0:F}Ԫ", (PlayerData.Instance.red + count1) / MoneyManager.redProportion);
        //Show();
        base.Animation(() =>
        {
            ShowFeed();
           // SetTransForm();
        }, 1f);
        ButtonAnim();
        
        
        typesGold = type;
        if (type == 0)
        {
            spriteName = "���";
            countName.text = "���";
        }
        else
        {
            spriteName = "��ʯ";
            countName.text = "��ʯ";
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
    Coroutine coroutine;
    private void ShowFeed()
    {if (gameObject.activeInHierarchy)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine= StartCoroutine(Global.Delay(0.5F, () => {
                if (gameObject.activeInHierarchy)
                    AndroidAdsDialog.Instance.ShowFeedAd(540);
            }));


        }
    }

    public void GetAward()
    {
        Time.timeScale = 1;
        AudioManager.Instance.PlaySound("finish_redpacket");
        if (queen != null)
        {
            queen.Kill();
            buttonTf.localScale = Vector3.one;
        }
        PlayerData.Instance.GetRed(_redCount);
        gameObject.SetActive(false);
        unityAction1?.Invoke();//������������������ӷ����������
        unityAction1 = null;
        if (typesGold == 0)
        {
            AndroidAdsDialog.Instance.UploadDataEvent("finish_new_fahuo_redpacket");
            PlayerData.Instance.GetGold(awardCount,false);//�漰���������ӽ��
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
            if (PlayerData.Instance.ClickFaHuoRedCount == 1)
            {
                HttpService.Instance.UploadEventRequest("frist_deliver_goods_success", "��һ�η����ɹ�");
                Debug.Log("��һ�η����ɹ�");
            }
        }
        else
        {
            if(awardCount>0)
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


          () => { if (typesGold == 0) { PlayerData.Instance.AddWangDianGold(awardCount); if (!isShowVideo)
                  {

                      AndroidAdsDialog.Instance.ShowTableVideo("0");
                     
                  }
                } });
        }
        AndroidAdsDialog.Instance.CloseFeedAd();

        AwardManagerNew.Instance.isShow = false;
       
       
    }
    RectTransform rectTransform;
    float time = 0.8f;
    public void SetTransForm()
    {
        if (gameObject.activeInHierarchy)
        {
            if (rectTransform == null)
            {
                rectTransform = backTf.GetComponent<RectTransform>();
            }
            rectTransform.DOAnchorMax(new Vector2(0.5f, 1), time);
            rectTransform.DOAnchorMin(new Vector2(0.5f, 1), time);
            rectTransform.DOAnchorPosY(-10, time);
            rectTransform.DOPivotY(1, time);
            //rectTransform.anchorMin = new Vector2(0.5f, 1);
            //rectTransform.anchorMax = new Vector2(0.5f, 1);
            //rectTransform.pivot = new Vector2(0.5f, 1);
            //rectTransform.DOAnchorPosY(0, 1.3f);
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
      
    }
    

