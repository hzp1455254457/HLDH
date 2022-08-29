using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class StockManager : PanelBase
{
    // Start is called before the first frame update
    public Dictionary<string, ProduceDate> produceDic;
    public Transform parentTf,tudouParent;
    public List<ProduceInfo> produceInfos=new List<ProduceInfo>();
    public ZhiBoJian currentZhibojian;
    public RectTransform proinfoUi;
    public GameObject exitGo;
    public ScrollRect scrollRect;
    [Tooltip("库存为0时")]
    public GameObject tipsGo;
    [Tooltip("去进货教程目标点")]
    public RectTransform guideGoShop;
    public RectTransform guideChuShou;
    public static StockManager Instance { get
        {
            if (instance == null)
            {
                instance=(UIManager.Instance.OpenPanel("Panel_ProInfo")) as StockManager;
                instance.HideUI();
               //instance. Init();
                return instance;
            }
            return instance;
        } }
    static StockManager instance;
    protected override void Awake()
    {
       
        print("初始化");
      
    }
    public override void Init()
    {
        print("初始化Init");
        base.Init();
        scrollRect = GetComponentInChildren<ScrollRect>();
        // Instance = this;
        produceDic = PlayerData.Instance.produceDic;
        if (produceDic != null && produceDic.Count > 0)
        {
            int i = 0;
            foreach (var item in produceDic.Keys)
            {
                CreactProduceInfo(item, i);

                i++;
            }
        }
        PlayerData.Instance.creactProduceEvent += CreactProduceInfo;
    }
    public void RecoverGuideStates()
    {
        //exitGo.SetActive(true);
    }
    ProduceInfo tudou;
    int clickCount = 0;
    public void ShowUI(ZhiBoJian currentZhibojian)
    {
        
        gameObject.SetActive(true);
        this.currentZhibojian = currentZhibojian;
        if (currentZhibojian.index != 0)
        {
            if (tudou != null)
            {
                tudou.transform.SetParent(tudouParent);
            }
            if (produceInfos.Find(s => s.produceDate.item_have >0 /*&& s.produceDate.state == 0*/ && s.produceDate.item_id!=8) == null)
            {

                tipsGo.SetActive(true);
                scrollRect.verticalNormalizedPosition = 1f;

            }
            else
            {
                tipsGo.SetActive(false);
            }

        }
        else
        {
            if (tudou != null)
            {
                tudou.transform.SetParent(parentTf);
            }
            if (produceInfos.Find(s => s.produceDate.item_have > 0/*&& s.produceDate.state==0*/) == null)
            {
                tipsGo.SetActive(true);
            }
            else
            {
                tipsGo.SetActive(false);
            }
        }
        //gameObject.SetActive(true);
        proinfoUi.localScale = Vector3.zero;
     
       
        //currentZhibojian.boPanel.peopleEffect.SetTips(StockManager.Instance.proinfoUi, StockManager.Instance.produceInfos[0].guideTarget1.position, false);
        proinfoUi.DOScale(Vector3.one * 1.1f, 0.5f).onComplete
            += () => proinfoUi.DOScale(Vector3.one * 1f, 0.3f).onComplete
        += () => {
            if (GuideManager.Instance.isFirstGame)
            {
                clickCount++;
                if (clickCount == 1)
                {
                    exitGo.SetActive(false);
                    currentZhibojian.boPanel.peopleEffect.SetTips(StockManager.Instance.proinfoUi, StockManager.Instance.produceInfos[0].guideTarget1.position, true, RotaryType.BottonToTop);
                }
                else if(clickCount == 2)
                {
                    //GuideManager.Instance.AchieveGuide();
                    //PeopleEffect.Instance.HideMask();
                }
                else if (clickCount == 3)
                {
                    
                }

            }
            else
            {
                print("scrollRect.verticalNormalizedPosition" + scrollRect.verticalNormalizedPosition);
              //  if(tipsGo.activeInHierarchy)
               // scrollRect.verticalNormalizedPosition = 1f;
                exitGo.SetActive(true);
            }
        };
    }
    public override void Show()
    {
        transform.SetParent(UIManager.Instance.canvas_Main.transform);
        transform.SetSiblingIndex(6);
    }
    public override void Hide()
    {
     
    }
    public void HideUI()
    {
        this.currentZhibojian = null;
        gameObject.SetActive(false);
    }
    public void SetSellPrice()
    {
        for (int i = 0; i < produceInfos.Count; i++)
        {
            produceInfos[i].SetSellPrice(currentZhibojian);
        }
    }
    int clickCount1 = 0;
    public void Sell(ProduceInfo produceInfo)
    {if (currentZhibojian != null)
        {
            if (GuideManager.Instance.isFirstGame)
            {
                clickCount1++;
                if (clickCount1 == 1)
                {
                    AndroidAdsDialog.Instance.UploadDataEvent("new_course_5");
                    StockManager.Instance.currentZhibojian.boPanel.peopleEffect.HideTips();
                    StockManager.Instance.currentZhibojian.boPanel.peopleEffect.HideMask();
                    StockManager.Instance.currentZhibojian.boPanel.peopleEffect.ShowMask(1);
                    StockManager.Instance.currentZhibojian.boPanel.peopleEffect.SetTips(StockManager.Instance.currentZhibojian.boPanel.zhibojianList[0].guideTargetRect.GetComponent<RectTransform>(), Vector2.zero, false);
                }
                else if (clickCount1 == 2)
                {
                    //StockManager.Instance.currentZhibojian.boPanel.peopleEffect.HideTips();
                    StockManager.Instance.currentZhibojian.boPanel.peopleEffect.HideMask();
                    StockManager.Instance.currentZhibojian.boPanel.peopleEffect.SetTips(StockManager.Instance.currentZhibojian.boPanel.zhibojianList[1].guideTargetRect.GetComponent<RectTransform>(), Vector2.zero, false);
                    GuideManager.Instance.AchieveGuide();
                    //PeopleEffect.Instance.HideMask();
                }
                //AndroidAdsDialog.Instance.UploadDataEvent("jiaocheng4");
            }

            currentZhibojian.Sell(produceInfo.produceDate.item_id);
          
        }//PlayerDate.Instance. SaveActorDate(); } 
        else
        {
            Debug.LogError(" currentZhibojian没有引用");
        }
    }
    public void CreactProduceInfo(string key,int index)
    {
    var go=  ResourceManager.Instance.GetProGo("ProduceInfo");
      var pro=  Instantiate(go, parentTf).GetComponent<ProduceInfo>();
        pro.transform.SetSiblingIndex(1);
        pro.index = index;
        pro.SetProduce(produceDic[key.ToString()]);
        if (key == "8")
        {
            tudou = pro;
        }
        pro.Refresh();
        produceInfos.Add(pro);
       // return pro;
       // produceDic.Add(key, produce);
    }

   
    public void GoToShop()
    {
       // ToggleManager.Instance.ShowPanel(0);
        gameObject.SetActive(false);
        if (GuideManager.Instance.isFirstGame)
        {
         PeopleEffect.Instance.   SetTips((UIManager.Instance.GetPanel("Panel_Shop") as HLDH.ShopPanel).rectTransform, (UIManager.Instance.GetPanel("Panel_Shop") as HLDH.ShopPanel).targetGuide1.position);
        }
    }
}
