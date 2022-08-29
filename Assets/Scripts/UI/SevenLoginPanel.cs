using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SevenLoginPanel : PanelBase
{
    public static SevenLoginPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("SevenLoginPanel")) as SevenLoginPanel;
                instance.Hide();
                //instance. Init();
                return instance;
            }
            return instance;
        }
    }
    static SevenLoginPanel instance;
    public Button sureButton, closeButton;
    public SevenLoginItem[] sevenLoginItems;
    public CanvasGroup canvasGroup1;
    public GameObject guideGo;
    protected override void Awake()
    {
        LoadData();

        Debug.LogError(".....current" + currentCount + ".....last" + lastCount);
    }

    public void RefreshStatus()
    {
        IsGet = false;
        SetCount();
    }

    public void ShowGuide(bool value)
    {if(guideGo.activeSelf==!value)
        guideGo.SetActive(value);
    }
    public override void Show()
    {
        //transform.SetParent(UIManager.Instance.showRootMain);
        //transform.SetAsLastSibling();
    }
    public override void Hide()
    {
        canvasGroup1.alpha = 0;
        canvasGroup1.blocksRaycasts = false;
        canvasGroup1.interactable = false;
        SaveData();
       //emoveAction();
    }
    public void ShowUI( Transform parentTf)
    {
        canvasGroup1.alpha = 1;
        canvasGroup1.blocksRaycasts = true;
        canvasGroup1.interactable = true;
        transform.SetParent(parentTf);
        transform.SetAsLastSibling();
        base.Animation();
        gameObject.SetActive(true);
        AndroidAdsDialog.Instance.ShowBannerAd();
        InitUI();
        //if (PlayerData.Instance.IsSet)
        //{
        //    isGet = false;
        //    SetCount();
        //}




    }
    private void OnDestroy()
    {
        instance = null;
    }
    public void HideUI()
    {
        Hide();
        AndroidAdsDialog.Instance.ShowTableVideo("0");
        AndroidAdsDialog.Instance.CloseBanner();
    }
    public void RemoveAction()
    {
        sureButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();
    }
    

    private void InitUI()
    {
        sevenLoginItems = GetComponentsInChildren<SevenLoginItem>();
        int i = 0;
        foreach (var item in sevenLoginItems)
        {
            item.index = i;
            item.sevenLoginData = PlayerData.Instance.SevenLoginDatas[i++];
            item.Init();
        }
    }

    public   bool isGet = false;
   public bool IsGet
    {
        set { isGet = value;
            UnityActionManager.Instance.DispatchEvent("RefreshSevenIcon");

        }
        get { return isGet; }
    }

    public int lastCount = 0;
    public int currentCount = 0;
    public void AddCount()
    {
        currentCount++;
    }
    public void SetCount()
    {
        lastCount = currentCount;
        currentCount = 0;
        Debug.LogError(".....current" + currentCount + ".....last" + lastCount);
    }
    private void LoadData()
    {
        isGet = DataSaver.Instance.GetInt("isGetSeven", 0) == 1 ? true : false;
        lastCount = DataSaver.Instance.GetInt("lastCount", 0);
        currentCount= DataSaver.Instance.GetInt("currentCount", 0);
    }
    private void SaveData()
    {
        int value = isGet == true ? 1 : 0;
        DataSaver.Instance.SetInt("isGetSeven", value);
        DataSaver.Instance.SetInt("lastCount", lastCount);
        DataSaver.Instance.SetInt("currentCount", currentCount);
        
    }
    private void OnApplicationQuit()
    {

        SaveData();

    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();

        }
    }
}
