using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;

/// <summary>
/// 物品计时器cell
/// </summary>
public class shopItemCellConfig : MonoBehaviour
{
    public int cellID;
    public shop_cell_status status
    {
        set {
            _status = value;
            statusChange(_status);
        }
        get {
            return _status;
        }
    }

    private shop_cell_status _status;

    #region 未解锁
    public Button lockButton;
    #endregion

    #region 解锁但是空闲
    public Button shangjiaButton;
    #endregion

    #region cell工作并且计时
    public Button workButton;
    public Image itemImage;
    public Text timerText;
    public Text itemText1, itemText2;
    public Button jiasuButton;
    #endregion

    #region 结束
    public Button shouqianButton;
    public Text enditemText1, enditemText2;
    #endregion

    #region 倒计时计数
    private int timerCount = 0;
    private int itemID = 0;
    private int kucunCount = 0;
    #endregion

    public GameObject fingerAnimation;
    private Coroutine timerCoroutine = null;

    //要设置的物品ID,点击上架后要知道物品ID
    public int preCellID = 0;

    public void Start()
    {
        lockButton.onClick.AddListener(() =>
        {
            Debug.Log("解锁了");
            //status = shop_cell_status.cellReady;
            tipsManager.Instance.createPiaoChuang(statusString);
        });

        shangjiaButton.onClick.AddListener(()=>
        {
            shangjiaButton.interactable = false;
            Debug.Log("上架页面出来");
            AudioManager.Instance.PlaySound("bubble1");
            tipsManager.Instance.openShopPanel(this,fingerAnimation.activeSelf,preCellID);

            if (fingerAnimation.activeSelf)
            {
                fingerAnimation.SetActive(false);
            }

            shangjiaButton.interactable = true;
        });

        shouqianButton.onClick.AddListener(()=>
        {
            AudioManager.Instance.PlaySound("version3_1");
            shouqianButton.interactable = false;
            shouqian();
            Invoke("ShowShouQianButton", 0.1f);
        });

        workButton.onClick.AddListener(()=>
        {
            if (!userData.Instance.isInJiaoCheng)
            {
                shopItemCellConfig[] cells = FindObjectsOfType<shopItemCellConfig>();
                foreach (shopItemCellConfig one in cells)
                {
                    one.jiasuButton.gameObject.SetActive(false);
                }
                jiasuButton.gameObject.SetActive(true);
            }
        });

        jiasuButton.onClick.AddListener(() =>
        {
            jiasuButton.gameObject.SetActive(false);
            Debug.Log("加速按钮点击");
            tipsManager.Instance.openJiaSuDialog(() =>
            {
                AndroidAdsDialog.Instance.ShowRewardVideo("点击加速播放激励视频", () =>
                 {
                     shopItemCellConfig[] cells = FindObjectsOfType<shopItemCellConfig>();
                     foreach (shopItemCellConfig one in cells)
                     {
                         one.jiasu();
                     }
                 });
            }
            );

        });

        userData.Instance.saveDataAction += () =>
        {
            shopCellStatus t = new shopCellStatus();
            t.status = (int)status;
            t.seconds = timerCount;
            t.itemID = itemID;
            t.kucunCount = kucunCount;
            userData.Instance.shopCellStatusList[cellID - 1] = t;
            Debug.Log("action 存数据:"+ itemID+">>"+LitJson.JsonMapper.ToJson(t));
        };
    }

    void ShowShouQianButton()
    {
        shouqianButton.interactable = true;
        status = shop_cell_status.cellReady;
    }

    public int lock_days = 0;

    public string statusString = "";

    public void OnEnable()
    {
        if (!userData.Instance.dataInitialed)
            userData.Instance.InitData();

        lockButton.GetComponentInChildren<Text>().text = statusString;
        //第二日解锁
        if (PlayerData.Instance.day >= lock_days && defaultStatus == shop_cell_status.cellLock)
        {
            defaultStatus = shop_cell_status.cellReady;
        }

        StartCoroutine(InitShopItemCell());
    }

    public shop_cell_status defaultStatus = shop_cell_status.cellLock;

    private int sellPerSecond;
    IEnumerator InitShopItemCell()
    {
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(()=> userData.Instance!=null&&userData.Instance.shopCellStatusList.Count == 6&&ResourceManager.Instance!=null&&ConfigManager.Instance!=null&&ConfigManager.Instance.produceList!=null);
        shopCellStatus cellStatus = userData.Instance.shopCellStatusList[cellID - 1];

        if (cellStatus != null && cellStatus.status != (int)shop_cell_status.cellLock)
        {
            timerCount = cellStatus.seconds;
            itemID = cellStatus.itemID;
            kucunCount = cellStatus.kucunCount;
            status = (shop_cell_status)cellStatus.status;

            sellPerSecond = (int)(5 * (1 + 0.2 * (itemID / 10.0f)) * 60 * 16)/ ((int)(5 * (1 + 0.2 * (itemID / 10.0f)) * 60));
        }
        else
        {
            //无状态保存，所以
            timerCount = 240;
            itemID = 0;
            status = defaultStatus;
            kucunCount = 0;
        }

    }

    private Tween scaleTweener = null;
    /// <summary>
    /// shop_cell状态变更的时候会出问题
    /// </summary>
    /// <param name="cell_status"></param>
    public void statusChange(shop_cell_status cell_status)
    {
        switch (cell_status)
        {
            case shop_cell_status.cellLock:
                lockButton.gameObject.SetActive(true);

                shangjiaButton.gameObject.SetActive(false);
                itemImage.gameObject.SetActive(false);
                shouqianButton.gameObject.SetActive(false);
                break;
            case shop_cell_status.cellReady:
                shangjiaButton.gameObject.SetActive(true);

                lockButton.gameObject.SetActive(false);
                itemImage.gameObject.SetActive(false);
                shouqianButton.gameObject.SetActive(false);
                break;
            case shop_cell_status.cellWork:
                /*if (!userData.Instance.isInJiaoCheng)
                {
                    jiasuButton.gameObject.SetActive(true);
                }*/
                lockButton.gameObject.SetActive(false);
                shangjiaButton.gameObject.SetActive(false);
                shouqianButton.gameObject.SetActive(false);

                if (!userData.Instance.isInJiaoCheng)
                {
                    shopItemCellConfig[] cells = FindObjectsOfType<shopItemCellConfig>();
                    foreach (shopItemCellConfig one in cells)
                    {
                        one.jiasuButton.gameObject.SetActive(false);
                    }
                    jiasuButton.gameObject.SetActive(true);
                }

                itemImage.gameObject.SetActive(true);
                if (p == null)
                {
                    p = ConfigManager.Instance.GetProduce(itemID);
                }

                itemImage.sprite = ResourceManager.Instance.GetSprite(p.item_pic);

                itemText1.text = p.item_name;
                itemText2.text = "库存:" + kucunCount;

                scaleTweener = timerText.transform.parent.DOScale(1.1f, 2.0f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);

                timerCoroutine = StartCoroutine(setTimer((a)=> {
                    updateTimerText(a, timerText);
                },()=> {
                    if (p == null)
                        p = ConfigManager.Instance.GetProduce(itemID);
                    kucunCount -= (int)(5*sellPerSecond);
                    if (kucunCount < 0)
                        kucunCount = 0;
                    itemText2.text = "库存:" + kucunCount;
                },()=>
                {
                    kucunCount = 0;
                    status = shop_cell_status.cellFinish; 
                }));

                
                break;
            case shop_cell_status.cellFinish:
                if (p == null)
                {
                    p = ConfigManager.Instance.GetProduce(itemID);
                }
                scaleTweener.Kill();
                timerText.transform.parent.localScale = Vector3.one;
                enditemText1.text = p.item_name;
                enditemText2.text = "售罄";

                lockButton.gameObject.SetActive(false);
                shangjiaButton.gameObject.SetActive(false);
                itemImage.gameObject.SetActive(false);

                shouqianButton.gameObject.SetActive(true);

                break;
        }
    }

    /// <summary>
    /// 计时器
    /// </summary>
    /// <param name="seconds">秒数</param>
    /// <param name="updateAction">计时器更新操作</param>
    /// <param name="endAction">计时器结束后的操作</param>
    /// <returns></returns>
    IEnumerator setTimer(Action<int> updateAction = null,Action fiveUpdateAction = null,Action endAction = null)
    {
        int num = 0;
        while (timerCount > 0)
        {
            if (num >= 5)
            {
                num = 0;
                fiveUpdateAction?.Invoke();
            }
            updateAction?.Invoke(timerCount);
            yield return new WaitForSeconds(1.0f);
            num++;
            timerCount--;
        }
        endAction?.Invoke();
    }

    /// <summary>
    /// 更新计时器读数
    /// </summary>
    /// <param name="seconds">秒数</param>
    /// <param name="timerText">计时器text</param>
    void updateTimerText(int seconds,Text timerText)
    {
        int a = seconds / 60;
        int b = seconds % 60;

        timerText.text = a.ToString("D2") + ":" + b.ToString("D2");
    }
    public void OnDisable()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }

    }


    private Produce p;

    public void refreshCell(Produce pro)
    {
        if (!userData.Instance.isInJiaoCheng)
        {
            p = pro;
            itemID = p.item_id;
            itemImage.sprite = ResourceManager.Instance.GetSprite(p.item_pic);
            timerCount = (int)(5 * (1 + 0.2 * (itemID / 10.0f)) * 60);
            itemText1.text = p.item_name;
            kucunCount = (int)(5 * (1 + 0.2 * (itemID / 10.0f)) * 60 * 16);
            sellPerSecond = kucunCount / timerCount;
            itemText2.text = "库存:" + kucunCount;
            status = shop_cell_status.cellWork;

            if (userData.Instance.isFirstGrounding)
            {
                userData.Instance.isFirstGrounding = false;
                PlayerPrefs.SetString("frist_grounding", "false");
                HttpService.Instance.UploadEventRequest("frist_grounding", "第一次上架");
            }
        }
        else
        {
            p = pro;
            itemID = p.item_id;
            itemImage.sprite = ResourceManager.Instance.GetSprite(p.item_pic);
            timerCount = 3;
            itemText1.text = p.item_name;
            kucunCount = 10;
            sellPerSecond = kucunCount / timerCount;
            itemText2.text = "库存:" + kucunCount;
            status = shop_cell_status.cellWork;
        }
    }

    /// <summary>
    /// 加速按钮点击
    /// </summary>
    public void jiasu()
    {
        if (status == shop_cell_status.cellWork)
        {
            Debug.Log("加速前库存："+kucunCount);
            kucunCount -= (480*sellPerSecond);
            timerCount -= 480;
            Debug.Log("加速后库存："+kucunCount);
            if (kucunCount < 0)
                kucunCount = 0;
            itemText2.text = "库存:" + kucunCount;
        }
    }

    /// <summary>
    /// 收钱按钮点击
    /// </summary>
    public void shouqian()
    {
        shouqianButton.interactable = false;
        ///商品数量
        int itemNum = (int)(5 * (1 + 0.2f * (itemID / 10)) * 60)/60;

        if (userData.Instance.isCollectingOrder && userData.Instance.itemDataDictionary.ContainsKey(p.item_id.ToString()))
        {
            Debug.Log("收获" + itemNum + "数量的" + p.item_id + "号商品");
            userData.Instance.itemDataDictionary[p.item_id.ToString()] += itemNum;
        }

        int num = (itemNum*5);
        userData.Instance.xinyu += num;
        Debug.Log("增加信誉值:" + num);

        int randomDiamond = UnityEngine.Random.Range(300, 1000);
        int randomHongBao = UnityEngine.Random.Range(30, 80);

        //userData.Instance.mostValveOrderAction.Invoke(userData.Instance.mostValveOrder);
        if(FindObjectOfType<taskCell>()!=null)
        FindObjectOfType<taskCell>().refreshOrder();

        tipsManager.Instance.createShouQianPiaoChuang(p,itemNum,num, randomDiamond,randomHongBao);

        PlayerData.Instance.GetDiamond(randomDiamond);
        PlayerData.Instance.GetRed(randomHongBao);

        //新增物体飞过去
        GameObject obj = new GameObject("flyItem",typeof(RectTransform),typeof(Image));
        obj.layer = LayerMask.NameToLayer("wangdian");
        obj.transform.SetParent(tipsManager.Instance.uiTransform);
        obj.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        //Camera camera = tipsManager.Instance.uiTransform.GetComponent<Canvas>().worldCamera;
        obj.transform.position = transform.position;
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        Image t = obj.GetComponent<Image>();
        t.preserveAspect = true;
        t.sprite = ResourceManager.Instance.GetSprite(p.item_pic);

        t.transform.DOMove(GameObject.Find("jiaochengRect3").transform.position, 1.0f).SetEase(Ease.OutQuint).onComplete = () =>
        {
            Destroy(obj);
            /*
            SkeletonGraphic g = FindObjectOfType<taskPanelConfig>().GetComponentInChildren<SkeletonGraphic>();

            g.AnimationState.SetAnimation(0, "shouqian", false);
            g.AnimationState.AddAnimation(0, "daiji", false,0);
            */
        };
        //如果是在教程中
        if (userData.Instance.isInJiaoCheng)
        {
            // FindObjectOfType<wangdianjiaocheng>().NextStep();
            FindObjectOfType<wangdianjiaocheng>().SetStep(4);
        }
        else
        {
            AndroidAdsDialog.Instance.ShowTableVideo("点击收钱播放插屏（教程除外）");
        }

        AndroidAdsDialog.Instance.CloseBanner();
        shouqianButton.interactable = true;
    }
}

public enum shop_cell_status
{
    /// <summary>
    /// 未解锁状态
    /// </summary>
    cellLock = 0,
    /// <summary>
    /// 可以选择售出
    /// </summary>
    cellReady = 1,
    /// <summary>
    /// 商店正在出售商品中
    /// </summary>
    cellWork = 2,
    /// <summary>
    /// 收钱状态
    /// </summary>
    cellFinish = 3,
}

[Serializable]
public class shopCellStatus
{
    public int status;
    public int seconds;
    public int kucunCount;
    public int itemID;
}
