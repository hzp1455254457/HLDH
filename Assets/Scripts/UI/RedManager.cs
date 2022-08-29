using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RedManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static RedManager Instance;
    // ShopUI currentShopUI;
    public Image proImg;
    public Text proName, procount, redCount, haveRedCount;
    int _redCount = 0;
    New.ShopPanel shopPanel;
    public Transform redTf;
    public Produce produce;
    public RectTransform rectTransform2, targetGuide2;

    public GameObject iconGo;
private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
       // shopPanel = New.ShopPanel.Instance;
        gameObject.SetActive(false);
    }
    int clickCount = 0;
    public void ShowUI()
    {
        transform.SetAsLastSibling();
        Animation();
       // currentShopUI = Shop.Instance.currentShopUI;
        gameObject.SetActive(true);
       // iconGo.SetActive(true);
        if (GuideManager.Instance.isFirstGame)
        {
            //clickCount++;
            //if(clickCount==1)
            ////Guide();
            //else if(clickCount == 2)
            produce = Shop.Instance.produce;
          if(  PlayerData.Instance.ClickFaHuoRedCount == 0){
                iconGo.SetActive(false);
            }
        }
        else
        {
            produce = Shop.Instance.produce;
        }
        proImg.sprite = ResourceManager.Instance.GetSprite(produce.item_pic);
        proName.text = produce.item_name;
       //procount.text = string.Format("+{0}个", shopPanel.Count);
        _redCount = NumberGenenater.GetRedCount();
        redCount.text = (_redCount/MoneyManager.redProportion).ToString("f2")+"元";
        haveRedCount.text = string.Format("领取后余额≈{0:F}元", (PlayerData.Instance.red + _redCount) / MoneyManager.redProportion);
        AndroidAdsDialog.Instance.ShowFeedAd(540);
       // AndroidAdsDialog.Instance.ShowBannerAd();
    }

    private void Guide()
    {
        produce = (UIManager.Instance.GetPanel("Panel_Shop") as HLDH.ShopPanel).currentShopUI.currentProduce;


        PeopleEffect.Instance.HideTips();

        //AndroidAdsDialog.Instance.UploadDataEvent("jiaocheng2");


        PeopleEffect.Instance.SetTips(rectTransform2, targetGuide2.position, true);
    }

    void Animation()
    {
        redTf.localScale = Vector3.zero;
        redTf.DOScale(Vector3.one * 1.1f, 0.5f).SetUpdate(true).onComplete
             += () => redTf.DOScale(Vector3.one * 1f, 0.3f).SetUpdate(true);
    }
    public void GetProduce()
    {
        // PlayerDate.Instance.GetRed( _redCount);
        // shopPanel.GetProduceEvent();
        PlayerData.Instance.AddProduce(produce.item_id, 1);
        gameObject.SetActive(false);
        PlayerData.Instance.GetRed(_redCount);
        AndroidAdsDialog.Instance.CloseFeedAd();
        //AndroidAdsDialog.Instance.CloseBanner();
        Shop.Instance.isShow = false;
        Time.timeScale = 1;
        RefreshZhiboSell();
        if (Shop.Instance.currentZhiBoJian != null)
        {
            Shop.Instance.currentZhiBoJian.ShenJiEvent(Shop.Instance.currentZhiBoJian.index);
            Shop.Instance.currentZhiBoJian = null;
        }
        // UnityActionManager.Instance.DispatchEvent("GetRed");
        ShopPanelNew.Instance.newGuide.GuideFuncEvent2();
    }

    private void RefreshZhiboSell()
    {
        AndroidAdsDialog.Instance.ShowRed(string.Format("{0}", ShopPanelNew.Instance.currentZhiBoJian.actorDate.actor_name), produce, string.Format("{0}", produce.item_name), "开始卖", "啦!",
         ShopPanelNew.Instance.currentZhiBoJian.effectBorn, ShopPanelNew.Instance.currentZhiBoJian.effectTarget);
        AndroidAdsDialog.Instance.ShowToasts("+" + (_redCount/MoneyManager.redProportion).ToString("f2")+"元", ResourceManager.Instance.GetSprite("红包"), Color.red);
        ShopPanelNew.Instance.currentZhiBoJian.Sell(produce.item_id);
       // ShopPanelNew.Instance.currentZhiBoJian.StartTuiXiao();
        //ShopPanelNew.Instance.currentZhiBoJian.StopSell();
    }
}