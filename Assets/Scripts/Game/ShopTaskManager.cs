using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ShopTaskManager : PanelBase
{
    public List<ShopTask> taskList = new List<ShopTask>();
    public Transform parentTf;
    public int curretLevelExp;
    public Slider expSlider;
    public Text expText,levelText;
    //public static ShopTaskManager Instance ;
    public Transform bornPos;
    public Transform targetPos;
    [Header("任务完成遮罩")]
    public GameObject achiveMask;
    public ScrollRect scrollRect;

    public static ShopTaskManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("WangDianPanel")) as ShopTaskManager;
                instance.gameObject.SetActive(false);
                return instance;
            }
            return instance;
        }
    }
    static ShopTaskManager instance;

    protected override void Awake()
    {
        instance = this;
    }
    public override void Hide()
    {
        gameObject.SetActive(false);
       // AndroidAdsDialog.Instance.ShowTableVideo("0");
    }
    public override void Show()
    {
        //base.Animation();
        //gameObject.SetActive(true);
        transform.SetParent(UIManager.Instance.showRootMain);

    }
  public  bool isUnity = false;
    public void ShowUI(bool value=false)
    { base.Animation();
        gameObject.SetActive(true);
        // CamareManager.Instance.SetUIFirst(true);
        transform.SetAsLastSibling();
        isUnity = value;
    }
    public void HideUI()
    {
        //if(!isUnity)
        AndroidAdsDialog.Instance.OpenTiXianUI(false,(int)(PlayerData.Instance.TixianValues[0]*100), (int)(PlayerData.Instance.TixianValues[1] * 100), (int)(PlayerData.Instance.TixianValues[2] * 100),PlayerData.Instance.storeData.level);
        gameObject.SetActive(false);
      
    }
    bool isFirstError = false;
    void Start()
    {
       int level= AndroidAdsDialog.Instance.GetRedLevel();
        if (level > PlayerData.Instance.storeData.level)
        {
            if(PlayerData.Instance.shop_MissinList != null && PlayerData.Instance.shop_MissinList.Count != 0 && PlayerData.Instance.shop_MissinList.Find(s => s.days_list == level) == null)
            {
                isFirstError = true;
            }
        }
        //PlayerDate.Instance.storeData.level= AndroidAdsDialog.Instance.GetRedLevel();
        PlayerData.Instance.addStoreExpAction += SetExp;
        PlayerData.Instance.addStoreLevelAction += ShenJi;
      
        curretLevelExp = ConfigManager.Instance.GetLevelExp(PlayerData.Instance.storeData.level);
        levelText.text = string.Format("当前{0}级", PlayerData.Instance.storeData.level);
        RefreshExp();
        if (PlayerData.Instance.shop_MissinList == null || PlayerData.Instance.shop_MissinList.Count == 0)
        {
            PlayerData.Instance.shop_MissinList = ConfigManager.Instance.GetCurrentShop_Mission(PlayerData.Instance.storeData.level);
        }
        for (int i = 0; i < PlayerData.Instance.shop_MissinList.Count; i++)
        {
            var go = ResourceManager.Instance.GetProGo("ShopTask");
            var pro = Instantiate(go, parentTf).GetComponent<ShopTask>();
           
            pro.my_Shop_Mission = PlayerData.Instance.shop_MissinList[i];
            taskList.Add(pro);
            pro.Init();
            //pro.index = 0;
        }
        if (isFirstError)
        {
            var list = ConfigManager.Instance.GetCurrentShop_Mission(PlayerData.Instance.storeData.level);
            if (list != null && list.Count != 0)
            {
                PlayerData.Instance.shop_MissinList.InsertRange(0, list);
                CreactTask(list);
            }
        }
     //var shopTask=   PlayerDate.Instance.shop_MissinList.Find(s => s.shop_mission_states != 3);
     //   if (shopTask == null)
     //   {
     //       achiveMask.SetActive(true);
     //   }
     //   else
     //   {
     //       achiveMask.SetActive(false);
     //   }

        gameObject.SetActive(false);
    }
    IEnumerator Hide1()
    {
        yield return null;
        gameObject.SetActive(false);
    }
    //public void OnGUI()
    //{
    //    if (GUI.Button(new Rect(20, 40, 80, 20), "点这里！"))
    //    {
    //        PlayerDate.Instance.AddStoreLevel(1);
    //    }

    //}
    public void CreactTask(List< My_Shop_Mission>shopTasks)
    {
        if (shopTasks == null) return;
      int  i = 0;
        foreach (var item in shopTasks)
        { 
            var go = ResourceManager.Instance.GetProGo("ShopTask");
            var pro = Instantiate(go, parentTf).GetComponent<ShopTask>();
            pro.my_Shop_Mission = PlayerData.Instance.shop_MissinList[i];
            taskList.Add(pro);
            pro.Init();
            pro.transform.SetSiblingIndex(i);
            i++;
        }
        //var shopTask = PlayerDate.Instance.shop_MissinList.Find(s => s.shop_mission_states != 3);
        //if (shopTask == null)
        //{
        //    achiveMask.SetActive(true);
        //}
        //else
        //{
        //    achiveMask.SetActive(false);
        //}
    }
    /// <summary>
    /// 刷新经验显示
    /// </summary>
    public void RefreshExp()
    {


        expSlider.value = (float)PlayerData.Instance.storeData.exp / curretLevelExp;
        SetText();
    }

    public void SetText()
    {
        int noAchivedCount;
        int achivedCount;
        Getshop_MissinList(PlayerData.Instance.storeData.level, out achivedCount, out noAchivedCount);
        expText.text = string.Format("{0}/{1}", achivedCount, noAchivedCount);
    }

    public void SetExp()
    {
        SetText();
        if (PlayerData.Instance.storeData.exp == 0)
        {
            DOTween.To(() => currentExp, x => currentExp = x, curretLevelExp, 1f).SetEase(Ease.Linear).SetUpdate(true);
            DOTween.To(() => curentvalue, x => curentvalue = x, curretLevelExp / curretLevelExp, 1f).SetEase(Ease.Linear).SetUpdate(true);
            // curretLevelExp = ConfigManager.Instance.GetLevelExp(PlayerDate.Instance.storeData.level);

            StartCoroutine(SetEXPText(true));
        }
        else
        {
            DOTween.To(() => currentExp, x => currentExp = x, (int)PlayerData.Instance.storeData.exp, 1f).SetEase(Ease.Linear).SetUpdate(true);
            DOTween.To(() => curentvalue, x => curentvalue = x, (float)PlayerData.Instance.storeData.exp / curretLevelExp, 1f).SetEase(Ease.Linear).SetUpdate(true);
            // curretLevelExp = ConfigManager.Instance.GetLevelExp(PlayerDate.Instance.storeData.level);

            StartCoroutine(SetEXPText(false));
        }
        //DOTween.To(() => currentExp, x => currentExp = x, (int)PlayerDate.Instance.storeData.exp, 1f).SetEase(Ease.Linear).SetUpdate(true);
        //DOTween.To(() => curentvalue, x => curentvalue = x, (float)PlayerDate.Instance.storeData.exp / curretLevelExp, 1f).SetEase(Ease.Linear).SetUpdate(true);
        //// curretLevelExp = ConfigManager.Instance.GetLevelExp(PlayerDate.Instance.storeData.level);

        //StartCoroutine(SetEXPText());


    }
    IEnumerator SetEXPText(bool isFull=false)
    {
        while (true)
        {
            expSlider.value = curentvalue;
           // expText.text = string.Format("{0}/{1}", currentExp, curretLevelExp);

            yield return null;

            if (isFull)
            {
                if (currentExp >= curretLevelExp)
                {
                  //  expText.text = string.Format("{0}/{1}", PlayerDate.Instance.storeData.exp, curretLevelExp);
                    expSlider.value = curentvalue= 0;
                    currentExp = 0;
                    break;
                }
                
            }
            else
            {
                if (currentExp >= PlayerData.Instance.storeData.exp)
                {

                    //expText.text = string.Format("{0}/{1}", PlayerDate.Instance.storeData.exp, curretLevelExp);
                    expSlider.value = curentvalue;
                    break;
                }

              
            }
        }
    }
    int currentExp;
    float curentvalue;
   
    /// <summary>
    /// 升级
    /// </summary>
   public void ShenJi()
    {
        curretLevelExp = ConfigManager.Instance.GetLevelExp(PlayerData.Instance.storeData.level);
        levelText.text =string.Format("当前{0}级",PlayerData.Instance.storeData.level) ;
      
        var list = ConfigManager.Instance.GetCurrentShop_Mission(PlayerData.Instance.storeData.level);
        if (list != null && list.Count != 0)
        {
            PlayerData.Instance.shop_MissinList.InsertRange(0, list);
            CreactTask(list);
        }
        RefreshExp();
    }

    void Getshop_MissinList(int level,out int achivedCount,out int noAchivedCount)
    {
        achivedCount = 0;
        noAchivedCount = 0;
       var list= PlayerData.Instance.shop_MissinList.FindAll(s => s.days_list == PlayerData.Instance.storeData.level);
        if (list != null)
        {
            noAchivedCount = list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].shop_mission_states != 0)
                {
                    achivedCount++;
                }
                else
                {


                }
            }
        }
    }
}
