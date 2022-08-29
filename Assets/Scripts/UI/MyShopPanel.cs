using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyShopPanel : PanelBase
{
    public static MyShopPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("MyShopPanel")) as MyShopPanel;
                instance.Hide();
                return instance;
            }
            return instance;
        }
    }

    static MyShopPanel instance;
    public ChouJiangTiXian chouJiangTiXian;
    [Header("���۶̬�仯�ű�")]
    public NumberEffect salesVolumeEffect;
    [Header("���۶�text���")]
    public Text salesVolumeText;
    [Header("����text���")]
    public Text daysText;
    [Header("�齱����text���")]
    public Text choujiangText;
    [Header("�齱����text���")]
    public Text choujiangCountText;
   // [Header("�齱����ʱtext���")]
   // public Text timeText;
    long lastSalesVolume;
    [Header("�齱ϵͳ")]
    public RotaryTablePanel rotaryTablePanel;
    public ChouJiangTiming chouJiangTiming;
    [Header("�齱��ť1")]
  public  Button buttonRotaryDaimond;
    [Header("�齱��ť2")]
   public  Button buttonRotaryFree;
    RectTransform buttonRotaryFreeRect;
    [Header("������ť")]
    public Button buttonAdward;
    [Header("ǩ��")]
    public GetFreeDaimond freeDaimond;
    int xiaoHaoDaimondCount = 1000;
    int maxChouJiangCount = 10;
    //public GameObject[] statesChouJiang;

    public MyShopGuide shopGuide;
    [Header("��������λ��")]
    public Transform parentTf;
    public List<Daimond> redQiPaos;

    public void ShowQiPao(bool value)
    {
        for (int i = 0; i < redQiPaos.Count; i++)
        {
            redQiPaos[i].gameObject.SetActive(value);
        }
    }
    public void AddQiPao(Daimond daimond)
    {
        if (!redQiPaos.Contains(daimond))
        {
            redQiPaos.Add(daimond);
        }
    }
    public void RemoveQiPao(Daimond daimond)
    {
        if (redQiPaos.Contains(daimond))
        {
            redQiPaos.Remove(daimond);
        }
    }

    public void RefreshXiaoShouE()
    {
       // salesVolumeEffect.Animation(PlayerDate.Instance.SalesVolume, "","",0.1f, lastSalesVolume);
        lastSalesVolume = PlayerData.Instance.SalesVolume;
    }
    private void RefreshXiaoHaoChouJiang()
    {if (PlayerData.Instance.ChouJiangCount > 0)
        { xiaoHaoDaimondCount = PlayerData.Instance.ChouJiangCount * 2000;
            if (PlayerData.Instance.ChouJiangCount > 4&& PlayerData.Instance.ChouJiangCount<maxChouJiangCount)
            {
                buttonRotaryDaimond.gameObject.SetActive(false);
                buttonRotaryFreeRect.anchoredPosition = new Vector2(0, buttonRotaryFreeRect.anchoredPosition.y);
               
            }
            else if (PlayerData.Instance.ChouJiangCount >=maxChouJiangCount)
            {
           
                    buttonRotaryDaimond.gameObject.SetActive(true);
               buttonRotaryFreeRect.anchoredPosition = new Vector2(168, buttonRotaryFreeRect.anchoredPosition.y);
                choujiangCountText.gameObject.SetActive(true);
            }
        }
        else
        {
            xiaoHaoDaimondCount = 1000;
        }
        choujiangText.text = xiaoHaoDaimondCount.ToString();
        choujiangCountText.text = string.Format("ÿ�ճ�ȡ {0}��( {1}/{2} )", maxChouJiangCount, PlayerData.Instance.ChouJiangCount, maxChouJiangCount);
       
    }
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    private void Start()
    {
        //ShowStatus(true);
        buttonRotaryFreeRect = buttonRotaryFree.GetComponent<RectTransform>();
        RefreshXiaoHaoChouJiang();
        salesVolumeText.text = PlayerData.Instance.SalesVolume.ToString("000,000,000,000");
        print("salesVolumeText.text++" + salesVolumeText.text);
        lastSalesVolume = PlayerData.Instance.SalesVolume;
        daysText.text = string.Format("�����{0}��", Global.NumToChinese(PlayerData.Instance.day.ToString()));
        AndroidAdsDialog.Instance.RequestActiveDay();
        UnityActionManager.Instance.AddAction("salesVolume", RefreshXiaoShouE);
        UnityActionManager.Instance.AddAction("choujiang", RefreshXiaoHaoChouJiang);
       
        buttonRotaryDaimond.onClick.AddListener(ChouJiang);
        buttonRotaryFree.onClick.AddListener(ChouJiangFree);
        buttonAdward.onClick.AddListener(OpenBigRedpacketPanel);

        //StartCoroutine(Test());

    }
    private void OpenBigRedpacketPanel()
    {
        AndroidAdsDialog.Instance.OpenJiangLi(PlayerData.Instance.SalesVolume);
        AndroidAdsDialog.Instance.UploadDataEvent("click_myshop_lingjiang_btn");
    }
    public override void Init()
    {
        base.Init();
    }
  void  HideUI()
    {
        //gameObject.SetActive(false);
    }
    public override void SetHideOrShow(bool value)
    {
        base.SetHideOrShow(value);
        if (value)
        {
         ToggleManager.Instance.HideTips(0);
            if (GuideManager.Instance.isFirstGame)
            {
                clickCount++;
                if (clickCount == 1)
                {
                    ToggleManager.Instance.HideUI();
                    PeopleEffect.Instance.HideTips();
                    PeopleEffect.Instance.HideMask();
                    shopGuide.GuideFuncEvent();
                }
                //rotaryTablePanel.SetPlayingAction(GuideEvent, GuideEvent);}
            }
            DaimondTaskUI.Instance.Show(false);
        }
        else
        {
            if (GuideManager.Instance.isFirstGame)
            {
               
                if (clickCount == 1)
                {
                   
                    shopGuide.GuideFuncEvent1();
                }
                //rotaryTablePanel.SetPlayingAction(GuideEvent, GuideEvent);}
            }
            else
            {
                RefreshChouJiangTime();
                DaimondTaskUI.Instance.Show(true);
            }
        }
    }
    public  void RefreshChouJiangTime()
    {
        if (ToggleManager.Instance.toggles[0].isOn) return;
        if (PlayerData.Instance.time <= 0)
        {
            ToggleManager.Instance.SetTips(0);
        }
        else
        {
            ToggleManager.Instance.HideTips(0);
        }
    }
    public void GuideEvent()
    {
        ChouJiangedEvent1(0.5f);
        ToggleManager.Instance.ShowUI();
            PeopleEffect.Instance.ShowMask();
            PeopleEffect.Instance.SetTips(ToggleManager.Instance.guiTaget3, ToggleManager.Instance.guiTaget4.position, true, RotaryType.TopToBottom);
        (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).MoveZhuBo(ZhiBoPanel.Instance.zhibojianList.Count-1, false);
        
    }
    public void ChouJiangedEvent()
    {
        ChouJiangedEvent1(0.5f);
        ToggleManager.Instance.ShowPanel(1);
        (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).MoveZhuBo(ZhiBoPanel.Instance.zhibojianList.Count - 1,false);
        AndroidAdsDialog.Instance.ShowTableVideo("0");
       
    }
    public void ChouJiangedEvent1(float time,bool isFirst=false)
    {

        rotaryTablePanel.HideSelect(time,() => {
            //chouJiangTiming.tipsGo.SetActive(true);
            //ShowQiPao(true);
            //if (!isFirst)
            //{
               
            //    foreach (var item in rotaryTablePanel.rewardCellArr)
            //    {
            //        CreactRed(UnityEngine.Random.Range(300, 500), item.transform);

            //    }
            //}
        });
        
        
    }
    public void SetDays()
    {
        
        daysText.text = string.Format("�����{0}��",Global.NumToChinese( PlayerData.Instance.day.ToString()));
      // freeDaimond.JavaCallUnityEvent();
    }
    IEnumerator Test()
    {
        yield return new WaitForSeconds(1f);
        JavaCallUnity.Instance.GetDay("5");
    }
    public void ChouJiangEvent()
    {
        //StartTiming();
        isChoujianging = true;
        rotaryTablePanel.OnClickDrawFunuib();
        PlayerData.Instance.ChouJiangCount += 1;
        chouJiangTiXian.AddChouJiangCount();
        AndroidAdsDialog.Instance.AddSignDataCount(2);
        AndroidAdsDialog.Instance.RequestPostDrawCount();
        ZhiBoPanel.Instance.wangDianDaRenUI.PlayAnim(true);
    }
   public bool isChoujianging = false;
    private int clickCount;

    public void ChouJiang()
    {
        if (isChoujianging) return;
        if (PlayerData.Instance.ChouJiangCount < maxChouJiangCount)
        {
            if (PlayerData.Instance.diamond >= xiaoHaoDaimondCount)
            {
              
                PlayerData.Instance.ExpendDiamond(xiaoHaoDaimondCount);
                ChouJiangEvent();
                if (GuideManager.Instance.isFirstGame)
                {
                    clickGuide++;
                    if (clickGuide == 1)
                    {
                        AndroidAdsDialog.Instance.UploadDataEvent("new_guide_8_get");
                    }
                }
            }
            else
            {
                AndroidAdsDialog.Instance.ShowToasts("��ʯ����", ResourceManager.Instance.GetSprite("��ʯ����"), Color.red);
            }
        }
        else
        {
            AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, "���ճ齱�Ѵ����ޣ�����ˢ��", Color.black, null, null, 1.5f);
            return;
        }
    }
    int clickGuide = 0;
    int clickGuide1 = 0;
    public void ChouJiangFree()
    {
        if (isChoujianging) return;
        if (PlayerData.Instance.ChouJiangCount < maxChouJiangCount)
        {
            AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_ChouJiang);
            AndroidAdsDialog.Instance.UploadDataEvent("click_myshop_showvideo_new");
            // JavaCallUnity.Instance.ChouJiangEvent("100");
            if (GuideManager.Instance.isFirstGame)
            {
                clickGuide++;
                if (clickGuide == 1)
                {
                    AndroidAdsDialog.Instance.UploadDataEvent("new_guide_8_get_video");
                }
            }
        }
        else
        {
            AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, "���ճ齱�Ѵ����ޣ�����ˢ��", Color.black, null, null, 1.5f);

        }
    }
    public void CreactRed(int count,  Transform posTf)
    {

        var go = GameObjectPool.Instance.CreateObject("MyShopRedQiPao", ResourceManager.Instance.GetProGo("MyShopRedQiPao", "Prefab/Effect/"), posTf, Quaternion.identity, false);
        go.transform.SetParent(parentTf);
        go.transform.position = posTf.position;
        go.transform.localScale = Vector3.one;
        var daimond = go.GetComponent<Daimond>();
        AddQiPao(daimond);
        daimond.SetCount(count);
        //borns.SetDaimond(daimond);
        daimond.type = 1;
       
    }
    
}