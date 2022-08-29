using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ShopPanelNew : PanelBase
{
    public static ShopPanelNew Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("ShopPanelNew")) as ShopPanelNew;
                instance.gameObject.SetActive(false);
                //instance. Init();
                return instance;
            }
            return instance;
        }
    }
    static ShopPanelNew instance;
    public ShopUINew[] shopArry;
    public Produce[] ProduceArry;
    public bool isHaveBaoli;
    public ShopUINew currentShopUI;
    public ZhiBoJian currentZhiBoJian;
    public Text ZhuBoname;
    public Text louceng;

    public GameObject exitGo;
    public Transform parentTf;
    public Transform parentTf1;
    public List< GameObject> shopList=new List<GameObject>();
    public List<ShopUINew> shopUINewList = new List<ShopUINew>();
    public RectTransform rectTransform;
    public RectTransform gridrectTransform;
    public RectTransform grid2rectTransform;

    public ScrollRect scrollRect;
    /// <summary>
    /// 加载更多按钮
    /// </summary>
    public Button button;
    /// <summary>
    /// 商品网格
    /// </summary>
    public GridLayoutGroup gridLayoutGroup;
    public Sprite[] sprites;
    //public GameObject exitG
    protected override void Awake()
    {

       // shopArry = GetComponentsInChildren<ShopUINew>();
        button.onClick.AddListener(ShowMore);
        //ProduceArry = new Produce[shopArry.Length];
        //GameStartGetProduce();

    }
    int clickCount = 0;
    public void GameStartGetProduce()
    {
    
        var produces = ConfigManager.Instance.GetProduces(currentZhiBoJian.actorDate.actor_level);
      
        for (int i = 0; i < ProduceArry.Length; i++)
        {
            shopArry[i].index = i;
            if (i == 0)
            {
                if (GuideManager.Instance.isFirstGame&&clickCount==0)
                {
                    ProduceArry[i] = produces.Find(s => s.item_id == 2);
                }
                else
                    ProduceArry[i] = produces[UnityEngine.Random.Range(0, produces.Count)];
            }
            else
            {
                if (GuideManager.Instance.isFirstGame && clickCount == 0)
                {
                    ProduceArry[i] = produces.Find(s => s.item_id == 6);
                    clickCount++;
                }
                else
                    ProduceArry[i] = produces[UnityEngine.Random.Range(0, produces.Count)];

            }
            shopArry[i].SetProduce(ProduceArry[i]);

            if (!isHaveBaoli)
                produces.Remove(ProduceArry[i]);
            else
            {
                produces.Remove(ProduceArry[i]);
                produces = produces.FindAll(s => s.profit_state != 2);
            }
        }
    }
    public void AddProduceAndRefesh()
    {
        PlayerData.Instance.AddProduce(currentShopUI.currentProduce.item_id, currentShopUI.count);

        //RefreshProduce();
    }
    public void SetShopProduce(ShopUINew shop, bool value = true)
    { //isShow = true;

        RefreshStates();
        var produces = ConfigManager.Instance.GetProduces(currentZhiBoJian.actorDate.actor_level);
        for (int i = 0; i < ProduceArry.Length; i++)
        {
            produces.Remove(ProduceArry[i]);
        }
        // ProduceArry[shop.index]=  produces[UnityEngine.Random.Range(0, produces.Count)];
        if (value)
        {
            if (!isHaveBaoli)
            { }  //produces.Remove(ProduceArry[i]);
            else
            {
                //produces.Remove(ProduceArry[i]);
                produces = produces.FindAll(s => s.profit_state != 2);

            }
            ProduceArry[shop.index] = produces[UnityEngine.Random.Range(0, produces.Count)];
            //if (!isHaveBaoli)
            //{ shop.SetProduce(ProduceArry[shop.index], UnityEngine.Random.Range(ProduceArry[shop.index].num_min, ProduceArry[shop.index].num_max), ProduceArry[shop.index].profit_state); }
            //else
            //{
            shop.SetProduce(ProduceArry[shop.index]);
            //}
        }
        else
        {
            produces = produces.FindAll(s => s.profit_state == 2);
            shop.SetProduce(produces[shop.index]);
        }

        //shop.SetProduce();
    }
    public void RefreshProduce()
    {
       

     //PlayerDate.Instance.AddRefreshCount();
       // SetShopProduce(currentShopUI);
        //currentShopUI = null;
        // PlayerDate.Instance.AddRefreshCount();

    }
    void RefreshStates()
    {
        for (int i = 0; i < shopArry.Length; i++)
        {
            if (shopArry[i].states == 2)
            { isHaveBaoli = true; return; }
            isHaveBaoli = false;
        }

    }
    public override void Show()
    {
        transform.SetParent(UIManager.Instance.showRootMain);
       // transform.SetSiblingIndex(6);
    }
    public override void Hide()
    {

    }
    public void ShowMore()
    {
        foreach (var item in shopList)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
            }
        }
        if (maxGoldItem != null)
        {
            maxGoldItem.SetMaxTips(false);
            maxGoldItem.HideTips();
        }
        button.gameObject.SetActive(false);
        gridLayoutGroup.spacing = new Vector2(gridLayoutGroup.spacing.x, 80);
        StartCoroutine(Delay(0.2f,() =>
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, gridrectTransform.rect.height + grid2rectTransform.rect.height);
        }));
    }
    ShopUINew maxGoldItem;
    ShopUINew maxGoldItem1;
    int indexItem;
    public void ShowUI( ZhiBoJian zhiBoJian,int id )
    {
        AndroidAdsDialog.Instance.ShowBannerAd();
        gameObject.SetActive(true);
        maxGoldItem = null;
        maxGoldItem1 = null;
        bool isShowNew = false;
        bool isShowAll=false;
        button.gameObject.SetActive(true);
        gridLayoutGroup.spacing = new Vector2(gridLayoutGroup.spacing.x, 150);
        if (zhiBoJian.index >= ZhiBoPanel.Instance.currentIndex&&ZhiBoPanel.Instance.isShengJi)
        {
            isShowNew = true;
            ZhiBoPanel.Instance.isShengJi = false;
        }
        //for (int i = 0; i < shopList.Count; i++)
        //{
        //    Destroy(shopList[i].gameObject);
        //}
        shopList.Clear();
        shopUINewList.Clear();
        var produces = ConfigManager.Instance.GetProduces(zhiBoJian.actorDate.actor_louceng);

        if (produces != null && produces.Count > 0)
        {
            for (int i = 0; i < produces.Count; i++)
            {
                var go = GameObjectPool.Instance.CreateObject("ShopItem", ResourceManager.Instance.GetProGo("ShopItem"), parentTf, Quaternion.identity);
                //var go = Instantiate(ResourceManager.Instance.GetProGo("ShopItem"));
                var item = go.GetComponent<ShopUINew>();
                //item.transform.SetParent(parentTf, false);
                shopUINewList.Add(item);
                item.SetProduce(produces[i]);

                if (produces[i].item_id == id && id > 0 && i < 6)
                {
                    item.ShowTips(true);
                    isShowAll = false;
                }
                shopList.Add(go);
                if (produces[i].produce_level == zhiBoJian.actorDate.actor_level)
                {
                    if (isShowNew)
                        item.SetNew(true);
                }

                if (i < 6)
                {
                    GetMaxGoldItem(item);

                }
                if (i >= 6)
                {
                    item.gameObject.SetActive(false);
                    if (id > 0)
                    {

                        if (produces[i].item_id == id)
                        {
                            isShowAll = true;

                            if (id == item.currentProduce.item_id)
                            { item.ShowTips(true); indexItem = i; print("indexItem" + indexItem); }
                            //button.gameObject.SetActive(false);
                            //gridLayoutGroup.spacing = new Vector2(gridLayoutGroup.spacing.x, 80);


                            //StartCoroutine(Delay(0.2f, () =>
                            //{
                            //    int value1 = shopList.Count / 3;
                            //    if (shopList.Count % 3 != 0)
                            //    {
                            //        value1++;
                            //    }
                            //    int value2 = (indexItem) / 3;
                            //    if ((shopList.Count - indexItem) % 3 != 0)
                            //    {
                            //        value2++;
                            //    }
                            ////float value1 = gridrectTransform.rect.height + grid2rectTransform.rect.height;
                            //// float value2 = value1-Mathf.Abs(shopList[indexItem].transform.localPosition.y);
                            //scrollRect.verticalNormalizedPosition=1-((value2 / ((float)value1)));

                            //// scrollRect.verticalNormalizedPosition = 1;
                            //print("scrollRect.verticalNormalizedPosition" + scrollRect.verticalNormalizedPosition);
                            //    print(indexItem + "+++" + shopList.Count);
                            //}));
                        }
                        if (isShowAll)
                            item.gameObject.SetActive(true);
                    }

                }

            }
            if (produces.Count >= 6 && id > 0 && isShowAll)
            {
                ShowMore();
                StartCoroutine(Delay(0.2f, () =>
                {
                    int value1 = shopList.Count / 3;
                    if (shopList.Count % 3 != 0)
                    {
                        value1++;
                    }
                    int value2 = (indexItem) / 3;
                    if ((shopList.Count - indexItem) % 3 != 0)
                    {
                        value2++;
                    }
                    //float value1 = gridrectTransform.rect.height + grid2rectTransform.rect.height;
                    // float value2 = value1-Mathf.Abs(shopList[indexItem].transform.localPosition.y);
                    scrollRect.verticalNormalizedPosition = 1 - ((value2 / ((float)value1)));
                }));

            }
        }
        if (maxGoldItem == null)
        {
            maxGoldItem = maxGoldItem1;
        }
        if (id == -1)
        {
            maxGoldItem.SetMaxTips(true);
            maxGoldItem.ShowTips();
        }
        var produces1 = ConfigManager.Instance.GetHightLevelProduces(zhiBoJian.actorDate.actor_louceng);
        produces1.Sort();
        //produces1.Reverse();
        if (produces1 != null && produces1.Count > 0)
        {
            for (int i = 0; i < produces1.Count; i++)
            {
                if (i < 6)
                {
                    var go = GameObjectPool.Instance.CreateObject("ShopItem1", ResourceManager.Instance.GetProGo("ShopItem1"), parentTf, Quaternion.identity);
                    //var go = Instantiate(ResourceManager.Instance.GetProGo("ShopItem1"));
                    shopList.Add(go);
                    var item = go.GetComponent<ShopNllItem>();
                    item.SetText(produces1[i].item_name, produces1[i].produce_level.ToString() + "楼解锁");
                    item.transform.SetParent(parentTf1, false);
                }
            }
        }
     
        StartCoroutine( Delay(0.03f,() =>
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, gridrectTransform.rect.height + grid2rectTransform.rect.height);
            print(gridrectTransform.rect.height + grid2rectTransform.rect.height);
        }));
       
        transform.SetAsLastSibling();
        currentZhiBoJian = zhiBoJian;
        ZhuBoname.text = zhiBoJian.actorDate.actor_name;
        louceng.text = zhiBoJian.actorDate.actor_louceng.ToString() + "楼";
        base.Animation();
       
       // GameStartGetProduce();
        newGuide.GuideFuncEvent();
        
        //AndroidAdsDialog.Instance.ShowFeedAd(540);
        ShowExit();
    }

    private void GetMaxGoldItem(ShopUINew item)
    {
        if (maxGoldItem == null)
        {
            if (item.states == 1)
            {
                maxGoldItem = item;

            }

        }

        else
        {
            if (item.states == 1)
            {
                if (maxGoldItem.currentProduce.item_profit < item.currentProduce.item_profit)
                {
                    maxGoldItem = item;
                }
            }
            
        }
        if (maxGoldItem1 == null)
        {
            maxGoldItem1 = item;
        }
        else
        {

            if (maxGoldItem1.currentProduce.item_profit < item.currentProduce.item_profit)
            {
                maxGoldItem1 = item;
            }
        }
    }
  
    public ShopPanelNewGuide newGuide;
    private void ShowExit()
    {
        exitGo.SetActive(!GuideManager.Instance.isFirstGame);

    }
    public void HideUI()
    {
        //currentShopUI = null;
        gameObject.SetActive(false);
        //AndroidAdsDialog.Instance.CloseFeedAd();
        for (int i = 0; i < shopList.Count; i++)
        {
            GameObjectPool.Instance.CollectObject(shopList[i].gameObject);
        }
        scrollRect.verticalNormalizedPosition = 1;
        AndroidAdsDialog.Instance.CloseBanner();
    }
public IEnumerator Delay(float time,UnityEngine.Events.UnityAction unityAction)
    {
        yield return null;
        yield return null;
        unityAction?.Invoke(); 
    }
    int hideCount = 0;
    public void CloseUI()
    {
        HideUI();
        hideCount++;
        if(hideCount%4==0)
        AndroidAdsDialog.Instance.ShowTableVideo("0");
    }
}
