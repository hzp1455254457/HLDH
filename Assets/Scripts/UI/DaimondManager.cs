using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaimondManager : PanelAnimation
{
    // Start is called before the first frame update
    public static DaimondManager Instance;
    public Text _countGet1, _countGet2;
    public Transform tf1, tf2;
    int count;
    Daimond currentDaimond;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ShowUI(Daimond daimond)
    {
        transform.SetAsLastSibling();
        base.Animation();
        currentDaimond = daimond;
           count = daimond.count;
        _countGet1.text = string.Format("{0}¸ö", count);
        gameObject.SetActive(true);
        tf1.gameObject.SetActive(true);
        tf2.gameObject.SetActive(false);
        //AndroidAdsDialog.Instance.UploadDataEvent("show_double_zuanshi");
       // AndroidAdsDialog.Instance.UploadDataEvent("show_big_zuanshi");
        AndroidAdsDialog.Instance.ShowFeedAd(540);
       // AndroidAdsDialog.Instance.ShowBannerAd();
    }
    public void DoubleGet()
    {
        AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_GetDaiMond);
       // AndroidAdsDialog.Instance.UploadDataEvent("click_double_zuanshi");
#if UNITY_EDITOR
        ShowGetUI(true);
#elif UNITY_ANDROID

      
        
#endif
    }
    public void Get()
    {
        //AndroidAdsDialog.Instance.UploadDataEvent("click_normal_zuanshi");
        AndroidAdsDialog.Instance.ShowTableVideo("0");
        ShowGetUI(false);
    }
    bool isdouble = false;
    public void ShowGetUI(bool isDouble)
    {
        isdouble = isDouble;
        base.Animation();
        tf1.gameObject.SetActive(false);
        tf2.gameObject.SetActive(true);
        if (isDouble)
        {
            
               count *= 2;
            currentDaimond.count = count;
           
        }
        _countGet2.text =string.Format("{0}¸ö",count) ;
    }
    public void ClickGet()
    {
        gameObject.SetActive(false);
        currentDaimond.AddDaimondAnim();
        AndroidAdsDialog.Instance.CloseFeedAd();
        if (isdouble)
        {
            AndroidAdsDialog.Instance.UploadDataEvent("finish_double_diamond");
        }
        else
        {
            AndroidAdsDialog.Instance.UploadDataEvent("click_double_diamond_normal_get");
        }
        //AndroidAdsDialog.Instance.CloseBanner();
    }
}
