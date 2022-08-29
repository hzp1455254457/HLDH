using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
   // public ShopUI currentShopUI;
    public Button getBotton;
    public Button closeBt;
    New.ShopPanel shopPanel;
    public Image image,back,top;
    public Text name,price,infos;
    public static Shop Instance;
    public Transform backtf;
   public Produce produce;//商品
    //public string[] stringArrys = new string[] { "带货主播都是很辛苦", "发货累了,奖励你个红包", "你的带货酬劳请收下", "快来领取你的红包!", "很快就能提现了!", "既送红包,又送货物!" };
    public string[] stringArrys = new string[1] { "可以获得"};
    //public Sprite[] backsprits;
    //public Sprite[] topsprits;
    public bool isShow = false;
    public ZhiBoJian currentZhiBoJian;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        shopPanel = New.ShopPanel.Instance ;
        closeBt.onClick.AddListener(CloseUI);
        getBotton.onClick.AddListener(GetProduce);
        gameObject.SetActive(false);
      
    }
    bool isRandom = true;
    public void ShowUI(bool IsRandom=true,Produce produce=null)
    {
        transform.SetAsLastSibling();
        if (isShow) return;
       // Time.timeScale = 0;
       // currentZhiBoJian = zhiBoJian;
        isShow = true;
        Animation();
        isRandom = IsRandom;
        closeBt.gameObject.SetActive(false);
       // AndroidAdsDialog.Instance.UploadDataEvent("show_redpacket");
        //AndroidAdsDialog.Instance.ShowFeedAd(500);
        //currentShopUI = shopUI;
        gameObject.SetActive(true);
        if(isRandom)
       this. produce = produce;
        else
        {
            this.produce = produce;
            //AndroidAdsDialog.Instance.UploadDataEvent("click_item_video_get");
            //produce = (UIManager.Instance.GetPanel("Panel_Shop") as HLDH.ShopPanel).currentShopUI.currentProduce;
        }
        image.sprite = ResourceManager.Instance.GetSprite(produce.item_pic);
        name.text = produce.item_name;
        //infos.text =  stringArrys[0];
        price.text =string.Format("利润:{0}元/个", produce.item_profit) ;
         //ShopPanel. isShow = true;
       Delay();
        AndroidAdsDialog.Instance.ShowBannerAd();
       
    }
    void Animation()
    {
        backtf.localScale = Vector3.zero;
        backtf.DOScale(Vector3.one * 1.1f, 0.5f).SetUpdate(true).onComplete
             += () => backtf.DOScale(Vector3.one * 1f, 0.3f).SetUpdate(true);
    }
   public void Delay()
    {
        //yield return new WaitForSeconds(time);
       
        if (!GuideManager.Instance.isFirstGame)
        {
            AndroidAdsDialog.Instance.UploadDataEvent("show_new_redpacket_tanchuang");
            closeBt.gameObject.SetActive(true);
            //closeBt.transform.localScale = Vector3.zero;
            //closeBt.transform.DOScale(Vector3.zero, 2f).SetUpdate(true).onComplete+=()=> closeBt.transform.localScale=Vector3.one;
        }
        else
        {

        }
       
        
    }
    int closeCount = 0;
    public void CloseUI()
    {
        Time.timeScale = 1;
        isShow = false;
        gameObject.SetActive(false);
        closeCount++;
        if (closeCount % 5 == 0)
        { AndroidAdsDialog.Instance.ShowTableVideo("0"); }
        AndroidAdsDialog.Instance.UploadDataEvent("close_new_redpacket_tanchuang");
       
        Shop.Instance.currentZhiBoJian = null;
        //Time.timeScale = 1;
        AndroidAdsDialog.Instance.CloseBanner();
        //ShopPanel.isShow = false;
    }
    public void GetProduce()
    {
        AndroidAdsDialog.Instance.CloseBanner();
        //   AndroidAdsDialog.Instance.UploadDataEvent("click_redpacket");
        AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_PRODUCE);
        //   }
#if UNITY_EDITOR

        CallBack();

#elif UNITY_ANDROID
 
        
#endif


    }

    public void CallBack()
    {
        RedManager.Instance.ShowUI();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void RecoverGuide1Status()
    {
        // shenjiButton.gameObject.SetActive(true);
        closeBt.gameObject.SetActive(true);
    }
}
