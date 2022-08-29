using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EWaiTiXianPanel : PanelBase
{

    public static EWaiTiXianPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("EWaiTiXianPanel")) as EWaiTiXianPanel;
                instance.gameObject.SetActive(false);
                return instance;
            }
            return instance;
        }
    }
    static EWaiTiXianPanel instance;
    private void OnDestroy()
    {
        instance = null;
        PlayerData.Instance.AddTixianValueAction -= SetSlider;
        PlayerData.Instance.AddTixianValueTimingAction -= CanAddValueEvent;
    }
    public Slider slider;
    public Text text;
    public dangweiItem[] dangweiItems;
    public ToggleGroup toggleGroup;
    public TipsUI tipsUI;
    Tweener tweener;
    Tweener tweener1;
    /// <summary>
    /// 增加进度值相关
    /// </summary>
    public GameObject[] addValueGos;
    public Text timeText;
    public Button addvalueBtn;
    public RectTransform tixianTf;
    float init = 0;
    public void SetSlider(float value)
    {
        if (tweener != null)
        { tweener.Pause(); tweener.Kill(); }
       if (tweener1 != null)
            { tweener1.Pause(); tweener1.Kill(); }
        isAnim = true;
        tweener = slider.DOValue(value, 3.6f);
           tweener.onComplete+=()=> { isAnim = false; };
       // tweener1=   DOTween.To(() => init, x => init = x, value, 3.6f).SetEase(Ease.Linear).SetUpdate(true);
     
    }
    public override void Show()
    {
        //transform.SetParent(UIManager.Instance.showRootMain);
        //transform.SetAsLastSibling();
    }
    int clickCount = 0;
    public override void Hide()
    {
        gameObject.SetActive(false);
        clickCount++;
        //if (clickCount %3 == 0)
        //{
        //    AndroidAdsDialog.Instance.ShowTableVideo("0");
        //}
        //RemoveAction();

    }
    public void HideUI()
    {
        Hide();
        AndroidAdsDialog.Instance.UploadDataEvent("close_moretixian");
    }
    public void ShowUI(Transform parentTf)
    {
        isInit = false;
        transform.SetParent(parentTf);
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
        //if(PlayerDate.Instance.FirstTableEcpm>0&& PlayerDate.Instance.FirstTableEcpm <= 20)
        //{
            addvalueBtn.gameObject.SetActive(false);
            tixianTf.anchoredPosition= new Vector2(0, tixianTf.anchoredPosition.y);
        //}
        base.Animation();
        InitData();
    }
    public void TiXianFun()
    {
        if (PlayerData.Instance.TixianValues[dangweiIndex] >= 1)
        {
            if (DangweiIndex == 2)
            {

            }
            else
            {
                print("提现");
                var tixian = JavaCallUnity.Instance.EWaitiXianDatas[dangweiIndex];
                AndroidAdsDialog.Instance.requestWithDraw(tixian.mid, tixian.cid, tixian.amount / 100d);

               
                PlayerData.Instance.TixianValues[dangweiIndex] = 0;
               
                RefreshValue();
                // dangweiItems[DangweiIndex].toggle.isOn = false;
                //dangweiItems[DangweiIndex].toggle.enabled = false;
                //AndroidAdsDialog.Instance.requestSJHBWithDrawList();

                //dangweiItems[DangweiIndex].toggle.group = null;
                // DangweiIndex ++;
                //if (dangweiIndex<=2)
                //    dangweiItems[DangweiIndex].toggle.isOn = true;
            }
        }
        else
        {
            print("进度不足");
            tipsUI.ShowUI(Hide);
        }
    }

   public void RefreshValue()
    {
        DangweiIndex = dangweiIndex;
        UnityActionManager.Instance.DispatchEvent("EWaiTiXianIcon");
        PlayerData.Instance.TiXianCount++;
    }

    protected override void Awake()
    {
        dangweiItems = GetComponentsInChildren<dangweiItem>();
        for (int i = 0; i < dangweiItems.Length; i++)
        {
            dangweiItems[i].tiXianData = JavaCallUnity.Instance.EWaitiXianDatas[i];
            dangweiItems[i].index = i;
        }
        if (PlayerData.Instance.AddValueTime > 0)
        {
            SetMaskStates(true);
        }
        else
        {
            SetMaskStates(false);
        }
    }
    bool isTiming = false;
    bool isAnim = false;
    private void Update()
    {
        if (isTiming)
        {
            timeText.text = Global.GetMinuteTime(PlayerData.Instance.AddValueTime);
        }
      if(isAnim)
            text.text = (slider.value*100).ToString("f2") + "%";
        
    }
    private void SetMaskStates(bool value)
    {
        addValueGos[0].SetActive(value);
        addValueGos[1].SetActive(!value);
        addvalueBtn.interactable = !value;
        isTiming = value;
    }
    bool isInit = false;
    private void InitData()
    {
        for (int i = 0; i < JavaCallUnity.Instance.EWaitiXianDatas.Count; i++)
        {
            if (JavaCallUnity.Instance.EWaitiXianDatas[i].count >= 1)
            {
                dangweiItems[i].toggle.group = toggleGroup;
                if (!isInit)
                {
                    isInit = true;
                    dangweiItems[i].toggle.isOn = true;
                    DangweiIndex = i;
                }

                dangweiItems[i].toggle.enabled = true;

            }
            else
            {
                dangweiItems[i].toggle.isOn = false;
                dangweiItems[i].toggle.enabled = false;
            }
        }
    }

    public int DangweiIndex
    {
        set
        {
            if (dangweiIndex != value)
            {
                if (tweener != null)
                { tweener.Pause();
                    tweener.Kill();
                }
                if (tweener1 != null)
                { tweener1.Pause(); tweener1.Kill(); }
                isAnim = false;
            }
            if (value >= 0 && value <= 2)
            {
                dangweiIndex = value;
                slider.value = (float)PlayerData.Instance.TixianValues[value];
                text.text = (PlayerData.Instance.TixianValues[value] * 100).ToString("f2") + "%";
            }
          init = (float)PlayerData.Instance.TixianValues[value];
        }
        get
        {
            return dangweiIndex;
        }
    }
    int dangweiIndex;

    private void Start()
    {
        PlayerData.Instance.AddTixianValueAction += SetSlider;
        PlayerData.Instance.AddTixianValueTimingAction += CanAddValueEvent;
    }
    private void CanAddValueEvent()
    {
        SetMaskStates(false);
        //isTiming = false;
    }
    public void AddValue()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("click_zengjiajindu");

       // JavaCallUnity.Instance.AddTiXianValueBtnFun("50");
        AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_ADDVALUE, AddValueFun);
       

    }
    public int time=180;
    private void AddValueFun()
    {
        PlayerData.Instance.AddValueTime = time;
        PlayerData.Instance.StartTime();
        SetMaskStates(true);
    }
}