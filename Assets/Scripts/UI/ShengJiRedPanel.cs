using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShengJiRedPanel :PanelBase
{
    // Start is called before the first frame update
    public static ShengJiRedPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("ShengJiRedPanel")) as ShengJiRedPanel;
                instance.HideUI();
                //instance. Init();
                return instance;
            }
            return instance;
        }
    }
    static ShengJiRedPanel  instance;
    public ZhiBoJian currentZhiBoJian;
    protected override void Awake()
    { }
    public override void Show()
    {
        transform.SetParent(UIManager.Instance.showRootMain);
        //transform.SetAsLastSibling();
    }
    public override void Hide()
    {

    }
    public bool isShow = false;
    public void ShowUI(ZhiBoJian zhiBoJian)
    {if (isShow == true) return;
        isShow = true;
        AndroidAdsDialog.Instance.CloseFeedAd();
        transform.SetAsLastSibling();
        base.Animation();
        gameObject.SetActive(true);
        currentZhiBoJian = zhiBoJian;
        AndroidAdsDialog.Instance.ShowBannerAd();
    }
    public void HideUI()
    {
        //currentShopUI = null;
        gameObject.SetActive(false);
        currentZhiBoJian = null;
        AndroidAdsDialog.Instance.ShowTableVideo("0");
        AndroidAdsDialog.Instance.CloseBanner();
        isShow = false;
    }
    public void GetProduce()
    {
        AndroidAdsDialog.Instance.CloseBanner();
        AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_SHENGJIRED);
#if UNITY_EDITOR

        CallBack();

#elif UNITY_ANDROID
 
        
#endif

    }

   public void CallBack()
    {
        gameObject.SetActive(false);

        ShengJiAdwardRedPanel.Instance.ShowUI(currentZhiBoJian);

        currentZhiBoJian = null;
    }
}
