using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRedpacketPanel : PanelBase
{
    // Start is called before the first frame update
    public static BigRedpacketPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("BigRedpacketPanel")) as BigRedpacketPanel;
                instance.HideUI();
                //instance. Init();
                return instance;
            }
            return instance;
        }
    }

    static BigRedpacketPanel instance;
    public Transform parentTf;
    public List<BigTask> bigTasks;
    protected override void Awake()
    {
        //base.Awake();
        instance = this;
    }
    public override void Init()
    {
        base.Init();
}
public void HideUI()
{
    gameObject.SetActive(false);
}
    public override void Show()
    {
        gameObject.SetActive(true);
        transform.SetParent(UIManager.Instance.canvas_Main.transform);
        transform.SetSiblingIndex(8);
    }
   
    public void ShowUI()
    {
        base.Animation();
        gameObject.SetActive(true);
    }
    private void Start()
    {
        //if (PlayerDate.Instance.shop_MissinList == null || PlayerDate.Instance.shop_MissinList.Count == 0)
        //{
        //    PlayerDate.Instance.shop_MissinList = ConfigManager.Instance.GetCurrentShop_Mission(PlayerDate.Instance.day);
        //}
        for (int i = 0; i < PlayerData.Instance.shop_MissinList.Count; i++)
        {
            var go = ResourceManager.Instance.GetProGo("ShopTask");
            var pro = Instantiate(go, parentTf).GetComponent<BigTask>();
            //pro.my_Shop_Mission = PlayerDate.Instance.shop_MissinList[i];
            bigTasks.Add(pro);
            //pro.index = 0;
        }
    }
}
