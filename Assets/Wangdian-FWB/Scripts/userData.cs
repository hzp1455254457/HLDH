using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

public class userData : MonoBehaviour
{
    public static userData Instance;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        Instance = this;
        //关闭UI
        

    }

    /// <summary>
    /// 网店商家信誉值
    /// </summary>
    public int xinyu
    {
        get
        {
            return _xinyu;
        }
        set
        {
            int x = _xinyu;
            _xinyu = value;
            xinyuAction?.Invoke(x, _xinyu);
        }
    }

    private int _xinyu;
    public Action<int,int> xinyuAction = null;

    public void OnEnable()
    {
        //首次进入我的网店也播插屏广告
        AndroidAdsDialog.Instance.ShowTableVideo("首次进入播放插屏");
        
    }

    public bool dataInitialed = false;
    public int num = 0;
    public void InitData()
    {
        DaimondTaskUI.Instance.Show(false);
        
        _xinyu = DataSaver.Instance.HasKey("xinyu") ? DataSaver.Instance.GetInt("xinyu") : 0;
        string shopCellStatusListString = DataSaver.Instance.HasKey("shopCellStatusList") ? DataSaver.Instance.GetString("shopCellStatusList") : null;
        //ES3.KeyExists("shopCellStatusList") ? ES3.Load<string>("shopCellStatusList") : null;
        Debug.Log(shopCellStatusListString);

        if (!string.IsNullOrEmpty(shopCellStatusListString))
            shopCellStatusList = JsonMapper.ToObject<List<shopCellStatus>>(shopCellStatusListString);
        else
            shopCellStatusList = new List<shopCellStatus>(new shopCellStatus[6] { null, null, null, null, null, null });

        //物品数据存储
        string itemDataDictionaryString = DataSaver.Instance.HasKey("itemDataDictionary") ? DataSaver.Instance.GetString("itemDataDictionary") : null;
        //ES3.KeyExists("itemDataDictionary") ? ES3.Load<string>("itemDataDictionary") : null;
        Debug.Log("物品数据存储:" + itemDataDictionaryString);
        itemDataDictionary = string.IsNullOrEmpty(itemDataDictionaryString) ? new Dictionary<string, int>() : JsonMapper.ToObject<Dictionary<string, int>>(itemDataDictionaryString);

        //当前订单的ID
        currentOrderID = DataSaver.Instance.HasKey("currentOrderID") ? DataSaver.Instance.GetInt("currentOrderID") : 1;

        //获取taskOrderList数据
        string taskOrderString = /*DataSaver.Instance.HasKey("taskOrderList") ? DataSaver.Instance.GetString("taskOrderList") : */Resources.Load<TextAsset>("taskOrder/orderInfo").text;

        Debug.Log("任务列表:" + taskOrderString);

        taskOrderList = JsonMapper.ToObject<List<taskOrder>>(taskOrderString);

        IsFirstEnter = DataSaver.Instance.HasKey("HasEnterWangDian") ? DataSaver.Instance.GetInt("HasEnterWangDian") : 0;

        num++;
        Debug.Log("初始化XXX"+num);
        dataInitialed = true;

        isFirstGrounding = !PlayerPrefs.HasKey("frist_grounding")?true:false;
    }
    public bool isFirstGrounding = true;

    public Action saveDataAction = null;
    public void OnApplicationQuit()
    {
        saveDataAction?.Invoke();

        DataSaver.Instance.SetInt("xinyu", xinyu);

        string str = JsonMapper.ToJson(shopCellStatusList);
        DataSaver.Instance.SetString("shopCellStatusList", str);

        /*
        string data = JsonMapper.ToJson(taskOrderList);
        DataSaver.Instance.SetString("taskOrderList", data);
        */

        string itemData = JsonMapper.ToJson(itemDataDictionary);
        DataSaver.Instance.SetString("itemDataDictionary", itemData);

        DataSaver.Instance.SetInt("currentOrderID", currentOrderID);
    }
    public void OnApplicationPause()
    {
        saveDataAction?.Invoke();

        DataSaver.Instance.SetInt("xinyu", xinyu);

        string str = JsonMapper.ToJson(shopCellStatusList);
        DataSaver.Instance.SetString("shopCellStatusList", str);

        /*
        string data = JsonMapper.ToJson(taskOrderList);
        DataSaver.Instance.SetString("taskOrderList", data);
        */

        string itemData = JsonMapper.ToJson(itemDataDictionary);
        DataSaver.Instance.SetString("itemDataDictionary", itemData);

        DataSaver.Instance.SetInt("currentOrderID", currentOrderID);
    }

    /// <summary>
    /// 6个商店的具体状态，包括倒计时状态
    /// </summary>
    public List<shopCellStatus> shopCellStatusList = new List<shopCellStatus>();

    /// <summary>
    /// 商品数量售卖信息
    /// </summary>
    public Dictionary<string,int> itemDataDictionary {
        get
        {
            return _itemDataDictionary;
        }
        set
        {
            _itemDataDictionary = value;
        }
    }

    private Dictionary<string, int> _itemDataDictionary = new Dictionary<string, int>();
    /// <summary>
    /// 任务列表数据
    /// </summary>
    public List<taskOrder> taskOrderList
    {
        get
        {
            if (_taskOrderList == null)
            {
                string taskOrderString = /*DataSaver.Instance.HasKey("taskOrderList") ? DataSaver.Instance.GetString("taskOrderList") : */Resources.Load<TextAsset>("taskOrder/orderInfo").text;
                _taskOrderList = JsonMapper.ToObject<List<taskOrder>>(taskOrderString);
            }

            return _taskOrderList;
        }
        set
        {
            _taskOrderList = value;
            Debug.Log("刷新订单");
            refreshOneOrder();
            //refreshFourOrders();
        }
    }
    private List<taskOrder> _taskOrderList = new List<taskOrder>();

    public List<taskOrder> currentFourOrder = new List<taskOrder>();


    /// <summary>
    /// 最有价值的订单
    /// </summary>
    public taskOrder mostValveOrder
    {
        get
        {
            return _mostValveOrder;
        }
        set
        {
            _mostValveOrder = value;
            mostValveOrderAction?.Invoke(_mostValveOrder);
        }
    }

    private taskOrder _mostValveOrder = null;

    public Action<taskOrder> mostValveOrderAction = null;

    private List<taskOrder> notFinishedOrders = new List<taskOrder>();

    public List<Produce> shopProduceList = new List<Produce>();

    public int currentOrderID = -1;

    /// <summary>
    /// 刷新4个订单
    /// </summary>
    public void refreshFourOrders()
    {
        notFinishedOrders.Clear();

        notFinishedOrders = taskOrderList.FindAll((p) => p.is_begin_mission == 0);

        if (notFinishedOrders.Count ==0)
        {
            Debug.Log("无任务");
            currentFourOrder = null;
            //shopProduceList.Clear();
            mostValveOrder = null;
            return;
            //itemDataDictionary.Clear();
        }

        if (notFinishedOrders.Count >= 4)
        {
            currentFourOrder = notFinishedOrders.GetRange(0, 4);
        }
        else
        {
            currentFourOrder = notFinishedOrders;
        }

        if (currentFourOrder != null)
        {
            int reward = 0, id = 0;
            for (int i = 0; i < currentFourOrder.Count; i++)
            {
                if (currentFourOrder[i].reward_hbq > reward)
                {
                    id = i;
                    reward = currentFourOrder[i].reward_hbq;
                }
            }

            shopProduceList.Clear();

            List<int> produceIDList = new List<int>();

            foreach (taskOrder order in currentFourOrder)
            {
                if (order.item1_id!=0&&!produceIDList.Contains(order.item1_id))
                {
                    produceIDList.Add(order.item1_id);
                }

                if (order.item2_id != 0 && !produceIDList.Contains(order.item2_id))
                {
                    produceIDList.Add(order.item2_id);
                }

                if (order.item3_id != 0 && !produceIDList.Contains(order.item3_id))
                {
                    produceIDList.Add(order.item3_id);
                }
            }

            for (int j = 0; j < produceIDList.Count; j++)
                shopProduceList.Add(ConfigManager.Instance.GetProduce(produceIDList[j]));

            //新增或者更新当前商品列表的数据
            foreach (int one in produceIDList)
            {
                if (!itemDataDictionary.ContainsKey(one.ToString()))
                {
                    itemDataDictionary.Add(one.ToString(), 0);
                }
            }

            List<int> clearList = new List<int>();

            //清除不在场景中的商品数量
            foreach (string one in itemDataDictionary.Keys)
            {
                if (!produceIDList.Contains(int.Parse(one)))
                {
                    clearList.Add(int.Parse(one));
                }
            }

            foreach (int one in clearList)
            {
                itemDataDictionary.Remove(one.ToString());
            }

            mostValveOrder = currentFourOrder[id];
        }
        else
        {
            mostValveOrder = null;
        }
    }

    public taskOrder currentOneOrder = null;
    public void refreshOneOrder()
    {
        /*
        notFinishedOrders.Clear();

        notFinishedOrders = taskOrderList.FindAll((p) => p.is_begin_mission == 0);

        if (notFinishedOrders.Count == 0)
        {
            Debug.Log("无任务");
            currentOneOrder = null;
            return;
        }
        */

        if (currentOrderID > taskOrderList.Count)
        {
            currentOneOrder = null;
            return;
        }

        currentOneOrder = taskOrderList[currentOrderID-1];

        if (currentOneOrder != null)
        {
            shopProduceList.Clear();

            List<int> produceIDList = new List<int>();

            taskOrder order = currentOneOrder;

                if (order.item1_id != 0 && !produceIDList.Contains(order.item1_id))
                {
                    produceIDList.Add(order.item1_id);
                }

                if (order.item2_id != 0 && !produceIDList.Contains(order.item2_id))
                {
                    produceIDList.Add(order.item2_id);
                }

                if (order.item3_id != 0 && !produceIDList.Contains(order.item3_id))
                {
                    produceIDList.Add(order.item3_id);
                }

            for (int j = 0; j < produceIDList.Count; j++)
                shopProduceList.Add(ConfigManager.Instance.GetProduce(produceIDList[j]));

            //新增或者更新当前商品列表的数据
            foreach (int one in produceIDList)
            {
                if (!itemDataDictionary.ContainsKey(one.ToString()))
                {
                    itemDataDictionary.Add(one.ToString(), 0);
                }
            }

            List<int> clearList = new List<int>();

            //清除不在场景中的商品数量
            foreach (string one in itemDataDictionary.Keys)
            {
                if (!produceIDList.Contains(int.Parse(one)))
                {
                    clearList.Add(int.Parse(one));
                }
            }

            foreach (int one in clearList)
            {
                itemDataDictionary.Remove(one.ToString());
            }
        }
    }

    /// <summary>
    /// 任务订单页面每4次关闭播一次插屏
    /// </summary>
    public int closeOrderTimes
    {
        get {
            return _closeOrderTimes;
        }
        set
        {
            _closeOrderTimes = value;
            if (_closeOrderTimes >= 4)
            {
                _closeOrderTimes = 0;
                AndroidAdsDialog.Instance.ShowTableVideo("任务按钮关闭4次，播放一次插屏广告");
            }
        }
    }

    private int _closeOrderTimes = 0;

    /// <summary>
    /// 是否是首次进入这个页面，0表示是首次，1表示不是
    /// </summary>
    public int IsFirstEnter = 2;

    public bool isInJiaoCheng = false;

    //是否开始采集统计数据
    public bool isCollectingOrder = false;
}
