using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Spine.Unity;
using DG.Tweening;
public class WalletManager  
{
    public Text count;
    public Button GetButton,iconBt,clearBt;
    public GameObject iconGo;
    public RectTransform guideRect;
    public Button walletBt;
    public Text walletCount;
    public SkeletonGraphic iconAnim;
    //public void SetCount()
    //{
    //    count.text = string.Format("{0}元", (int)PlayerDate.Instance.temporaryGold);
    //    if (PlayerDate.Instance.temporaryGold >= 7000)
    //    {
    //        if (iconAnim.timeScale!=1)
    //        {
    //            iconAnim.timeScale = 1;
    //            iconAnim.AnimationState.SetAnimation(0, "ketixian", true);
              

    //        }
    //    }
    //    else
    //    {
    //        if (iconAnim.timeScale == 1)
    //        { iconAnim.timeScale=0;
             
               
    //        }
    //    }
    //}
    private void OnEnable()
    {
        
    }
    private void Start()
    {
        //PlayerDate.Instance.temporaryGoldAction+= SetCount;
        //SetCount();
        GetButton.onClick.AddListener(GetGold);
        clearBt.onClick.AddListener(ClearGold);
        iconBt.onClick.AddListener(OpenIcon);
        //11.17版本移除这个按钮
        //if (GuideManager.Instance.isFirstGame)
        //{
        //    clearBt.gameObject.SetActive(false);


        //}
    }
    public void GetGold()
    {
        //print("获取金币");
        
        //AndroidAdsDialog.Instance.UploadDataEvent("get_coinbag");
        
        if (GuideManager.Instance.isFirstGame)
        {
          //GuideManager.Instance.AchieveGuide();
            PlayVidioEvent();
            //恢复所有选项
            //GuideManager.Instance.GetMask();
            //GuideManager.Instance.peopleEffect.HideMask();
            //clearBt.gameObject.SetActive(true);
            //AndroidAdsDialog.Instance.UploadDataEvent("jiaocheng8");
        }
        else
        {
           // AndroidAdsDialog.Instance.ShowTableVideo("0");
            GetAward();
        }

    }

    private void GetAward()
    {
       // PlayerDate.Instance.GetGold(PlayerDate.Instance.temporaryGold);
       
        //PlayerDate.Instance.GetTemporaryGold(-PlayerDate.Instance.temporaryGold);

       // Time.timeScale = 1;
        Hide();
       // AndroidAdsDialog.Instance.CloseFeedAd();
    }

    private  void PlayVidioEvent()
    {
#if UNITY_EDITOR

        // ToggleManager.Instance.ShowPanel(0);

        AchieveGuide();

#elif UNITY_ANDROID

        //GuideManager.Instance.isFirstGame = false;
        AndroidAdsDialog.Instance.ShowRewardVideo("-30");
        
       // print("激励视频");

#endif
        //ToggleManager.Instance.ShowPanel(0);
        //GuideManager.Instance.AchieveGuide();
    }
    public void AchieveGuide()
    {
       // ToggleManager.Instance.ShowPanel(0);
        GuideManager.Instance.GetMask();
        GuideManager.Instance.peopleEffect.HideMask();
        clearBt.gameObject.SetActive(true);
        //AndroidAdsDialog.Instance.UploadDataEvent("jiaocheng8");
        GuideManager.Instance.AchieveGuide();

        GetAward();
    }
    public void ClearGold()
    {
        //AndroidAdsDialog.Instance.UploadDataEvent("noneed_coinbag");
        //PlayerDate.Instance.GetTemporaryGold(PlayerDate.Instance.temporaryGold);
       // Time.timeScale = 1;
        //AndroidAdsDialog.Instance.ShowTableVideo("0");
        Hide();
    }
    public void OpenIcon()
    {
       // iconGo.SetActive(true);
    }
    public void Hide()
    {
        //gameObject.SetActive(false);
        walletBt.gameObject.SetActive(true);
        AndroidAdsDialog.Instance.CloseFeedAd();
    }
    
}
