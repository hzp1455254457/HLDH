using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShengJiAdwardRedPanel : PanelBase
{
    // Start is called before the first frame update
    public static ShengJiAdwardRedPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("ShengJiAdwardRedPanel")) as ShengJiAdwardRedPanel;
                instance.HideUI();
                //instance. Init();
                return instance;
            }
            return instance;
        }
    }
    static ShengJiAdwardRedPanel instance;
    public ZhiBoJian currentZhiBoJian;
    public Text redCount, haveRedCount;
    int _redCount = 0;
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
    public void ShowUI(ZhiBoJian zhiBoJian)
    {
        transform.SetAsLastSibling();
        currentZhiBoJian = zhiBoJian;
        base.Animation();
        gameObject.SetActive(true);
        _redCount = NumberGenenater.GetRedCount();
        redCount.text = _redCount.ToString();
        haveRedCount.text = string.Format("领取后余额≈{0:F}元", (PlayerData.Instance.red + _redCount) / MoneyManager.redProportion);
        AndroidAdsDialog.Instance.ShowFeedAd(540);
       
    }
    public void HideUI()
    {
        //currentShopUI = null;
        gameObject.SetActive(false);
    }
    public void GetProduce()
    {
        ShengJiRedPanel.Instance.isShow = false;
        gameObject.SetActive(false);
        PlayerData.Instance.GetRed(_redCount);
        AndroidAdsDialog.Instance.CloseFeedAd();
       
        currentZhiBoJian.ShenJiEvent(currentZhiBoJian.index,true);
        currentZhiBoJian = null;
        
    }
}
