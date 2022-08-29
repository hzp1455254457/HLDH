using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardManager : PanelAnimation
{
    public static AwardManager Instance;
   public ZhiBoJian currentZhiBoJian;
    public bool isShow = false;
    public string ClickAward = "isFirstClickAward", DropSellProduce= "isFirstDropSellProduce";
    private void Awake()
    {
        Instance = this;

        gameObject.SetActive(false);
    }
    public void GetisFirstGame()
    {
        if (DataSaver.Instance.HasKey(ClickAward) == false)
        {
            isFirstClickAward = true;

        }
        else
            isFirstClickAward = false;
        if (DataSaver.Instance.HasKey(DropSellProduce) == false)
        {
            isFirstDropSellProduce = true;

        }
        else
            isFirstDropSellProduce = false;

    }
    public bool isFirstClickAward = true;
    public bool isFirstDropSellProduce = true;
    public void SetisFirstClickAward()
    {
        print("设置为不是第一次点击打赏界面");
        DataSaver.Instance.SetInt(ClickAward, 1);
        isFirstClickAward = false;
    }
    public void SetisFirstDropSellProduce()
    {
        print("设置为不是第一次拖到商品");
        DataSaver.Instance.SetInt(DropSellProduce, 1);
        isFirstDropSellProduce = false;
    }
    public void ShowUI(ZhiBoJian zhiBoJian)
    {if (isShow) return;
        isShow = true;
        gameObject.SetActive(true);
        base.Animation();
        currentZhiBoJian = zhiBoJian;
      //  AndroidAdsDialog.Instance.UploadDataEvent("show_shangjinredpacket");

    }
   public void CloseUI()
    {
        print("关闭ui");
        gameObject.SetActive(false);
        isShow = false;
        if (currentZhiBoJian!=null)
        currentZhiBoJian.RecorveSell();
        if (isFirstClickAward)
        {
            
            AndroidAdsDialog.Instance.OpenTiXianUI();
            if (GuideManager.Instance.isFirstGame)
            {
                //ToggleManager.Instance.ShowPanel(0);
                GuideManager.Instance.AchieveGuide();
                PeopleEffect.Instance.HideTips();
                PeopleEffect.Instance.HideMask();
            }
            SetisFirstClickAward();
        }
        //AndroidAdsDialog.Instance.UploadDataEvent("close_shangjinredpacket");
    }
    public void GetAward()
    {
        //AndroidAdsDialog.Instance.UploadDataEvent("click_shangjinredpacket");
        //AndroidAdsDialog.Instance.ShowRewardVideo("-50");
        if (GuideManager.Instance.isFirstGame)
        {
           // AndroidAdsDialog.Instance.UploadDataEvent("getshangjinredpacket_jiaocheng");
          
        }
#if UNITY_EDITOR
        GetAdwardManager.Instance.ShowUI(1000);
        gameObject.SetActive(false);

       #elif UNITY_ANDROID
       
        #endif
    }
}
