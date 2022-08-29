using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAdwardManager : PanelAnimation
{
    public static GetAdwardManager Instance;
    ZhiBoJian currentZhiBoJian;
    int awardCount;
    public Text countText, tixianCount;
    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
    public void ShowUI(int count)
    {
        
           awardCount = count;
        countText.text = count.ToString();
        tixianCount.text=string.Format("领取后余额≈{0:F}元", (PlayerData.Instance.moneyReward + count) / 1000f);
        gameObject.SetActive(true);
        base.Animation();
        currentZhiBoJian = AwardManager.Instance.currentZhiBoJian;
        //infos.text = stringArrys[Random.Range(0, stringArrys.Length)];
        AndroidAdsDialog.Instance.ShowFeedAd(540);
        AndroidAdsDialog.Instance.ShowBannerAd();
    }
    public void GetAward()
    {
        PlayerData.Instance.GetAward(awardCount);
        gameObject.SetActive(false);
        if (currentZhiBoJian != null)
            currentZhiBoJian.RecorveSell();
        AwardManager.Instance.isShow = false;
        if (AwardManager.Instance.isFirstClickAward)
        {
            if (GuideManager.Instance.isFirstGame)
            {
                GuideManager.Instance.AchieveGuide();
                PeopleEffect.Instance.HideTips();
                PeopleEffect.Instance.HideMask();
               // AndroidAdsDialog.Instance.UploadDataEvent("finishshangjinredpacket_jiaocheng");
            }
            AndroidAdsDialog.Instance.OpenTiXianUI(true);
            AwardManager.Instance.SetisFirstClickAward();
        }
       // AndroidAdsDialog.Instance.UploadDataEvent("finish_shangjinredpacket");
        AndroidAdsDialog.Instance.CloseFeedAd();
        AndroidAdsDialog.Instance.CloseBanner();
    }
    
}
