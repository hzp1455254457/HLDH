using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEngine.Networking;
using UnityEngine.Events;

[Serializable]
public class PlayerData : MonoSingleton<PlayerData>
{
    // Start is called before the first frame update
    public int actor_maxlevel = 1;
    public int goldNotEnoughCount = 0;
   
   public int daimondNotEnoughCount = 0;
   /// <summary>
   /// 变更为主播升级次数
   /// </summary>
   
  public int ShengJiCount { set; get; }
    int ClickFaHuoCount = 0;
    /// <summary>
    /// 点击钻石次数
    /// </summary>
    public int ClickDaimondCount;
    public Dictionary<string, ProduceDate> produceDic;
    public Dictionary<string, ProduceDate> selledDic;
    // public List<Produce> shopList;
    public List<ActorDate> actorDateList;
    /// <summary>
    /// 不用得列表
    /// </summary>
    #region
    [HideInInspector]
    public List<CourierDate> courierDateList;
    [HideInInspector]
    public List<My_Shop_Mission> shop_MissinList;//存储得任务列表
    [HideInInspector]
    public List<Mission_redpacket> mission_RedpacketsList;
    #endregion
    public int firstShengJiEcpm = 0;
    public int FirstTableEcpm = 0;//第一次获取的插屏ecpm
    public int TiXianCount = 0;//额外提现次数；
    public int AddTiXIanCount_Table = 0;//插屏增加次数

    public int _videoClickRedAdwrdCount { get; set; }

   // public int ShengJiCount = 0;
    bool isGetIsShowShengJiRed = false;
    public bool IsShowShengJiRed;
    public bool IsShowDaimondTaskUI = false;
    public bool IsShowEWaiTiXianUI = false;
    public bool IsWangDianUI { set; get; }

    public bool IsTiXian = false;

    public int daimondTaskID = 1;
    //==============BigWorld===============
    /// <summary>
    /// 待发货商品
    /// </summary>
    //public Queue<int> DaiFaHuo;


    public List<NewWangDianData> NewWangDianDatas
    {
        get
        {
           if (newWangDianDatas == null)
            {
                if (isHaveKey("NewWangDianDatas"))
                {
                    newWangDianDatas =JsonMapper.ToObject< List < NewWangDianData >> (Load("NewWangDianDatas"));
                }
                else
                {
                    newWangDianDatas = ConfigManager.Instance.NewWangDianDatas;
                }
            }
            return newWangDianDatas;
        }
        set
        {
            newWangDianDatas = value;
        }
    }
    List<NewWangDianData> newWangDianDatas;
    //public void AddWangDianData(int Gold)
    //{
    //    for (int i = 0; i < NewWangDianDatas.Count; i++)
    //    {
    //        NewWangDianDatas[i].myshop_needgold
    //    }
    //}
    public double[] TixianValues
    {
        get
        {
            if (tixianValues == null)
            {
                if (isHaveKey("TixianValues"))
                {
                    tixianValues = JsonMapper.ToObject<double[]>(Load("TixianValues"));
                }
                else
                {
                    tixianValues = new double[3] {0,0,0 } ;
                }
               
            }
            else
            {
                //tixianValues = new float[3];
            }
            return tixianValues;
        }
        set { tixianValues = value; }
    }
  public double[] tixianValues;


    public void AddTiXiProcese(float[] value,bool isHight70=true)
    {
        for (int i = 0; i < TixianValues.Length; i++)
        {
            if (TixianValues[i] < 1)
            {
               
                TixianValues[i] += value[i];
                if (TixianValues[i] >= 1)
                {
                   TixianValues[i] = 1;
                }
                else
                {
                  
                }
                if (i == 0)
                {
                   
                    AddTixianValue1Action?.Invoke((float)(TixianValues[i]), isHight70);
                  
                }

                AddTixianValueAction?.Invoke((float)(TixianValues[EWaiTiXianPanel.Instance.DangweiIndex]));
            }
           
           
        }
    }
    public List<Sign_up> Sign_Ups
    {
        get
        {
            if (sign_Ups == null)
            {
                if (isHaveKey("sign_Ups"))
                {
                    sign_Ups = JsonMapper.ToObject<List<Sign_up>>(Load("sign_Ups"));
                }
                else
                {
                    sign_Ups = ConfigManager.Instance.Sign_Ups;
                }
               
            }
            return sign_Ups;
        }
    }
    List<Sign_up> sign_Ups;
    public StoreData storeData;
    public int day = 1;
    public double gold =3000;
    public int diamond=2500;
    public int red =0;
    public int moneyReward = 0;
    
    public int ChouJiangCount
    {
        get
        {
            return chouJiangCount;
        }
        set
        {
            if (chouJiangCount != value)
            {
                chouJiangCount = value;
                //if (chouJiangCount >= 6)
                //{
                //    AndroidAdsDialog.Instance.UploadDataEvent("choujiang_6_counts");
                //}
                UnityActionManager.Instance.DispatchEvent("choujiang");
            }
            //chouJiangCount  =value;
        }
    }

  public  int chouJiangCount=2;
    public long SalesVolume
    {
        get
        {
           
            return salesVolume;

        }
    }
    long salesVolume = 0;
  [HideInInspector] 
   List<QiPaoData> produceQiPaoList;
    public List<QiPaoData> ProduceQiPaoList
    {
        get
        {
            if (produceQiPaoList == null )
            {
                if (isHaveKey("SaveProduceQiPaos"))
                {
                  //  print(Load("SaveProduceQiPaos"));
                    produceQiPaoList = JsonMapper.ToObject<List<QiPaoData>>(Load("SaveProduceQiPaos"));
                
                
                }
                else
                {
                    produceQiPaoList = new List<QiPaoData>();
                    //produceQiPaoList = JsonMapper.ToObject < List < QiPaoData >> (Resources.Load<TextAsset>("Config/Init").text);

                }
                
            }
            return produceQiPaoList;
        }
    }

    public List<SevenLoginData> SevenLoginDatas
    {
        get {if (sevenLoginDatas == null)
            {
                if (isHaveKey("SevenLoginDatas"))
                {
                    //  print(Load("SaveProduceQiPaos"));
                    sevenLoginDatas = JsonMapper.ToObject<List< SevenLoginData>>(Load("SevenLoginDatas"));


                }
                else
                {
                    sevenLoginDatas = new List<SevenLoginData>() {
                    new SevenLoginData()
                    {
                        day=1,
                        type=1,
                        states=1,
                        gift_num=1000
                    },
                    new SevenLoginData()
                    {
                        day = 2,
                        type = 2,
                        states = 0,
                        gift_num=15000
                    },
                     new SevenLoginData()
                    {
                        day = 3,
                        type = 2,
                        states = 0,
                        gift_num=15000
                    },
                      new SevenLoginData()
                    {
                        day = 4,
                        type = 2,
                        states = 0,
                        gift_num=15000
                    },
                       new SevenLoginData()
                    {
                        day = 5,
                        type = 5,
                        states = 0,
                        gift_num=40000
                    },
                        new SevenLoginData()
                    {
                        day = 10,
                        type = 3,
                        states = 0,
                        gift_num=8888000
                    }
                    };
                    
                }
                //return sevenLoginDatas;
            }
            return sevenLoginDatas;
        }
        
      
    }

    [HideInInspector]
  List<SevenLoginData> sevenLoginDatas;
   // public bool IsSet = false;//5.26新增

    public void SetSevenLoginDate()
    {
        //var datas = SevenLoginDatas.FindAll(s => s.states == 1);
        //if (datas != null && datas.Count > 0)
        //{
        //    foreach (var item in datas)
        //    {
        //        item.states = 0;
        //    }
        //}
        var data= SevenLoginDatas.FindAll(s => s.day<= day && s.states == 0);
        if (data != null&&data.Count>0)
        {
            foreach (var item in data)
            {
                item.states = 1;
            }
        }
        //IsSet = true;
    }
    public DaimondTaskData Data
    {
        get
        {
            if (data == null)
            {
                if (isHaveKey("DaimondTaskData"))
                {
                    //  print(Load("SaveProduceQiPaos"));
                    data = JsonMapper.ToObject<DaimondTaskData>(Load("DaimondTaskData"));
             
                   
                }
                else
                {
                    data = new DaimondTaskData()
                    {
                        currentTask = 0,
                        daimondTasks = ConfigManager.Instance.DaimondTaskList
                        //daimondTasks = new List<DaimondTask>()
                        //{
                        //     new DaimondTask()
                        //   {
                        //       info="升级任意主播1次",
                        //       progress=0,
                        //       count=1500,
                        //       status=0,
                        //       NRV=1,
                        //        type=2
                        //   },
                        //   new DaimondTask()
                        //   {
                        //       info="完成一次抽奖",
                        //       progress=0,
                        //       count=1000,
                        //       status=0,
                        //       NRV=1,
                        //       type=1
                        //   },
                          
                        //     new DaimondTask()
                        //   {
                        //       info="发货1次",
                        //       progress=0,
                        //       count=2000,
                        //       status=0,
                        //       NRV=1,
                        //        type=3
                        //   },
                        //       new DaimondTask()
                        //   {
                        //       info="领取发货红包2次",
                        //       progress=0,
                        //       count=2500,
                        //       status=0,
                        //       NRV=2,
                        //        type=4
                        //   },
                        //         new DaimondTask()
                        //   {
                        //       info="为主播更换商品1次",
                        //       progress=0,
                        //       count=2800,
                        //       status=0,
                        //       NRV=1,
                        //        type=5
                        //   }
                        //}

                    };
                }
               
               
            }
            return data;
        }
    }
    [HideInInspector]
    DaimondTaskData data;


    public void AchiveDaimondTask()
    {
        if (data.daimondTasks.Count > data.currentTask)
        {
            GetDiamond(data.daimondTasks[data.currentTask].main_mission_reward);
            data.currentTask += 1;
            if (data.daimondTasks.Count <= data.currentTask)
            {

            }
        }
        else
        {

        }
    }

    //public BurseConfig burseConfig;
    readonly string GOLD = "gold";
    readonly string DIAMOND = "diamond";
    readonly string ACTOR_MAXLEVEL = "actor_maxlevel";
    readonly string CHOUJIANGCOUNT = "ChouJiangCount";
    readonly string BIGREDPACKETS = "Big_Redpacket";
    readonly string GETDAIMONDCOUNT = "getDaimondCount";
    readonly string CLICKFAHUOCOUNT = "ClickFaHuoCount";
    readonly string DAY = "day";
    readonly string RED = "red";
    readonly string STOREDATA = "StoreData";
    readonly string MONEYREWARD = "moneyReward";
    readonly string REFRESHCOUNT = "produceRefreshCount";
    readonly string SALESVOLUME = "salesVolume";
    readonly string FirstShengjiEcpm = "FirstShengjiEcpm";
    //======================BigWorld=================
    readonly string DAIFAHUO = "DaiFaHuo";

    public void PromoteActor_Maxlevel(int level)
    {
        actor_maxlevel = level;
    }
   public void AddShengJICount(ZhiBoJian zhiBoJian)
    {if (zhiBoJian == null) return;
        ShengJiCount++;
        UnityActionManager.Instance.DispatchEvent<int>("EWaiTiXianIconAnim", ShengJiCount);
        if (ShengJiCount % 2 == 0)
        {
           var skill1= ConfigManager.Instance.GetSkill(zhiBoJian.actorDate.actor_level + 1);
            var skill2 = ConfigManager.Instance.GetSkill(zhiBoJian.actorDate.actor_level + 2);
            zhiBoJian.zhiBoJianAward.Show(NumberGenenater.GetRedCount(), skill1.actorlevel_cost_num + skill2.actorlevel_cost_num);
        }
        if (ShengJiCount == 2)
        {
          //  MoneyManager.Instance.ShowTips(true);
        }
    }
    public void AddClickFaHuoCount()
    {
     ClickFaHuoCount++;
        if (ClickFaHuoCount == 1)
        {
           // DaimondTaskUI.Instance.ShowTips(true);
            //AndroidAdsDialog.Instance.ShowSignDialog();
        }
    }
    public void AddSalesVolume(int count)
    {
        salesVolume += count;
        //UnityActionManager.Instance.DispatchEvent("salesVolume");
        AndroidAdsDialog.Instance.RefreshJiangLi(PlayerData.Instance.SalesVolume);
    }
    public void AddGoldNotEnoughCount(int init, int target, UnityEngine.Events.UnityAction unityAction = null, UnityEngine.Events.UnityAction unityAction1 = null)
    {
        goldNotEnoughCount++;
       
            PanelGoldBox.Instance.ShowUI(init, target, unityAction, unityAction1);
        
    }
    public void AddDaimondNotEnoughCount()
    {
        daimondNotEnoughCount++;
        //if (PlayerDate.Instance.daimondNotEnoughCount <= 1)
        //{
           
        //    PanelDaimondTips.Instance.ShowUI("商品掉出车外会变成钻石",true,()=>ToggleManager.Instance.ShowPanel(2));
        //}
        //else
        if (PlayerData.Instance.daimondNotEnoughCount == 5)
        {
           
            PanelDaimondTips.Instance.ShowUI("抽奖可以获得钻石哦",false, () => JavaCallUnity.Instance.ShowWangDianScene("0"));
        }
      else 
        {
            AndroidAdsDialog.Instance.ShowToasts("钻石不够", ResourceManager.Instance.GetSprite("钻石不足"), Color.red, null);
            ToggleManager.Instance.ShowZuanShiTips();
            isTiming = true;
            zuanshiBuZuCount++;
        }
        
    }
    public int RedBiaoZhu=300;
    bool isTiming = false;
    int zuanshiBuZuCount = 0;
    float zuanshiBuZutime = 0;
    float loginTime = 0;
    //float Testtime = 0;
    private void Update()
    {
        if (isTiming)
        {
            zuanshiBuZutime += Time.deltaTime;
            if (zuanshiBuZutime > 2)
            {
                if (zuanshiBuZuCount >= 3)
                {
                    isTiming = false;

                    ToggleManager.Instance.ShowZuanShiBuZuTips();
                }
                isTiming = false;
                zuanshiBuZuCount = 0;
                zuanshiBuZutime = 0;
               
            }
        }
        loginTime += Time.deltaTime;
        if (loginTime >= 100)
        {
            loginTime = 0;
            HttpService.Instance.UploadEventRequest("yx_time", "yx_time");
        }
        //Testtime += Time.deltaTime;
        //if (Testtime >= 2)
        //{
        //    Testtime = 0;

        //}
    }
    /// <summary>
    /// 提升等级
    /// </summary>
    /// <param name="count"></param>
    public void AddStoreLevel(int count)
    {
        storeData.level += count;
       //AndroidAdsDialog.Instance.RequestNextLevel(storeData.level);
        if (addStoreLevelAction != null)
        { addStoreLevelAction(); }
    }
    /// <summary>
    /// 提升经验
    /// 
    /// </summary>
    /// <param name="count"></param>
    public void AddStoreExp(int count)
    {
        
       if ( storeData.exp +count >= ConfigManager.Instance.GetLevelExp(PlayerData.Instance.storeData.level))
        {
            storeData.exp = 0;
            AddStoreLevel(1);
          //ShopTaskManager.Instance.  RefreshExp();
        }
        else
        {
            storeData.exp += count;
            
        }
        if (addStoreExpAction != null)
        { addStoreExpAction(); }
        //if(addStoreExpAction!=null)
        //  { addStoreExpAction(); }
    }


    public void GetAward(int count)
    {if (count == 0) return;
        AndroidAdsDialog.Instance.RequestAddScore(count,true);
        moneyReward += count;
        if (awardAction != null)
        {
            awardAction();
        }
    }

    int lastRed = -1;
    public void GetRed(int redCount,bool IsRequestAddScore=true)
    {
        if (redCount == 0) return;
        if (redCount == lastRed)
        {
            redCount += UnityEngine.Random.Range(-10, -1);

        }
        lastRed = redCount;
        if(IsRequestAddScore)
        AndroidAdsDialog.Instance.RequestAddScore(redCount, false);
        this.red += redCount;
        TipsShowBase.Instance.Show("redTips", MoneyManager.Instance.born, MoneyManager.Instance.target,
          new string[] { string.Format("+{0:f3}元",redCount/MoneyManager.redProportion)
          }, new Sprite[] { ResourceManager.Instance.GetSprite("红包") }, new Color[] { Color.white });
        //if (isAnimatIon)
        //{
        //    if (redAction != null)
        //    {
        //        redAction();
        //    }
        //}
        redAction?.Invoke();
        if (this.red >=RedBiaoZhu)
        {
            ToggleManager.Instance.ShowMask(false);

            //if (!IsTiXian)
            //MoneyManager.Instance.ShowTiXianAnim(true);
        }
        else
        {
            ToggleManager.Instance.ShowMask(true);
           // MoneyManager.Instance.ShowTiXianAnim(false);
        }
        if (this.red >= redBiaoZhun2)
        {
            if (!IsTiXian)
                MoneyManager.Instance.ShowTiXianAnim(true);
        }
        else
        {
            MoneyManager.Instance.ShowTiXianAnim(false);
        }
        //if (this.red >= 50000)
        //{
        //    if (!IsQiaoDaoUI)
        //    {
        //        IsQiaoDaoUI = true;
        //        UnityActionManager.Instance.DispatchEvent("ShowQiaoDaoIcon");
        //       Instantiate( ResourceManager.Instance.GetProGo("QianDaoTipPanel"),UIManager.Instance.showRootMain,false);
        //    }
        //}
    }
    public int redBiaoZhun2 = 50000;
    public void GetDiamond(int diamond,bool isAnimatIon=true)
    {
       this. diamond += diamond;

        if (isAnimatIon)
        {
            if (diamondAction != null)
            { diamondAction(); }
        }
    }
   public int clickCount = 0;
    public void AddGetDiamondCount(bool isGetAll,bool isZhibo=true)
    {if (!isGetAll)
        {
            //JavaCallUnity.Instance.SetDaimondTaskStatus("0");测试代码
            if (isZhibo)
            {
                clickCount++;

                print("clickcount" + clickCount);


                if (clickCount % 7 == 0)
                {
                    clickCount = 0;
                    AndroidAdsDialog.Instance.ShowTableVideo("钻石");


                }

            }
            else
            {
                ClickDaimondCount++;
       
                if (ClickDaimondCount % 14 == 0)
                {
                    ClickDaimondCount = 0;
                    AndroidAdsDialog.Instance.ShowTableVideo("钻石");
                }
            }
            
        }
        else
        {
           
          
            
            //if (getDaimondCount == 4)
            //{
            //    clickCount = 0;
            //    AndroidAdsDialog.Instance.ShowTableVideo("钻石");
            //    Debug.LogError("插屏");
            //}
            //else if (getDaimondCount > 4)
            //{
            //    int ecpm = AndroidAdsDialog.Instance.LAST_TABLE_VIDEO_ECPM();
            //    int playCount = 20;
            //    if (ecpm < 5)
            //    {
            //        playCount = 20;
            //        //if (clickCount % 20 == 0)
            //        //{
            //        //    AndroidAdsDialog.Instance.ShowTableVideo("钻石");
            //        //    Debug.LogError("插屏");
            //        //    clickCount = 0;
            //        //}
            //    }
            //    else if (ecpm >= 5 && ecpm < 10)
            //    {
            //        playCount = 16;
            //        //if (clickCount % 16 == 0)
            //        //{
            //        //    AndroidAdsDialog.Instance.ShowTableVideo("钻石");
            //        //    Debug.LogError("插屏");
            //        //    clickCount = 0;
            //        //}
            //    }
            //    else
            //    {
            //        playCount = 10;
            //        //if (clickCount % 10 == 0)
            //        //{

            //        //    AndroidAdsDialog.Instance.ShowTableVideo("钻石");
            //        //    Debug.LogError("插屏");
            //        //    clickCount = 0;
            //        //}
            //    }
            //    if (clickCount % playCount == 0)
            //    {

            //        AndroidAdsDialog.Instance.ShowTableVideo("钻石");
            //        Debug.LogError("插屏");
            //        clickCount = 0;
            //    }
            //}
        
        }
        getDaimondCount++;
        ClickDaimondAction?.Invoke();
    //if(PlayerData.Instance.IsShowDaimondTaskUI)
    //    AndroidAdsDialog.Instance.RequestAddDiamondClick();
    }
  public  int getDaimondCount;
    public void ExpendDiamond(int diamond)
    {
        if (this.diamond < diamond) return;
        this.diamond-= diamond;
        
        //if (diamondAction != null)
        //{ diamondAction(); }
        if (expenddiamondAction != null)
        {
            expenddiamondAction();
        }
    }
    public void GetGold(double gold,bool isAddWangDianGold=true)
    {
        this.gold += gold;
        if(isAddWangDianGold)
        AddWangDianGold((int)gold);
       if (goldAction != null)
        {
            goldAction();
        }
    }
   public void AddWangDianGold(int gold)
    {
        if (!IsWangDianUI) return;
        float value1 = 0f;
        for (int i = 0; i < NewWangDianDatas.Count; i++)
        {
            if (NewWangDianDatas[i].gold < NewWangDianDatas[i].myshop_needgold)
            {

                NewWangDianDatas[i].gold += gold;
                if (NewWangDianDatas[i].gold >= NewWangDianDatas[i].myshop_needgold)
                {
                    NewWangDianDatas[i].status = 1;
                    NewWangDianDatas[i].gold = NewWangDianDatas[i].myshop_needgold;
                    //if (storeData.level <= NewWangDianDatas[i].myshop_level)
                    //{
                        var data = NewWangDianDatas[i];
                        int value = NewWangDianDatas[i].gold * 100 / NewWangDianDatas[i].myshop_needgold;
                    UnityActionManager.Instance.DispatchEvent<int>("WangDianIcon", value);
                   // UnityActionManager.Instance.DispatchEvent("WangDianIcon1");
                    //if (NewWangDianDatas[i].myshop_level == storeData.level)
                    //{
                    //    UnityActionManager.Instance.DispatchEvent<int>("WangDianIcon", value);
                    //}
                    //Transform pt = UIManager.Instance.showRootMain;
                    //    if (ClickFaHuoRedCount == 2)
                    //    {
                    //        pt = UIManager.Instance.showRootMain1;
                    //    }
                    //value1 += data.myshop_redpacket_reward;
                    //    ToggleManager.Instance.wangDianShengJi.ShowUI(pt, (data.myshop_level + 1).ToString(), (value1 / MoneyManager.redProportion).ToString("f2"), () =>
                    //    {
                    //     int level=   data.myshop_level + 1 - storeData.level;
                    //          AddStoreLevel(level);
                    //          data.status = 2;
                    //          GetRed(data.myshop_redpacket_reward);

                    //            UnityActionManager.Instance.DispatchEvent("WangDianIcon1");
                                
                    //          NewWangDianDatas.Remove(data);
                    //          NewWangDianDatas.Add(data);
                    //        NewWangDianPanel.Instance.Hide();
                    //    }
                    //  );
                }
                else
                {

                    int value = NewWangDianDatas[i].gold * 100 / NewWangDianDatas[i].myshop_needgold;
                    if (NewWangDianDatas[i].myshop_level == storeData.level)
                    {
                        UnityActionManager.Instance.DispatchEvent<int>("WangDianIcon", value);
                    }
                }
             

            }
        }
    }


    /// <summary>
    /// 扣除金币
    /// </summary>
    /// <param name="gold"></param>
    /// <returns>是否扣除成功</returns>
    public bool Expend(int gold)
    {
        if (this.gold < gold) return false;

        this.gold -= gold;

        if (expengdGoldAction != null)
        {
            expengdGoldAction();
        }
        return true;
    }
    public  IEnumerator Timing( )
    {
        // this.time = time;
        //text.text = string.Format("{0}", GetMinuteTime(time));
        while (AddValueTime >= 0)
        {
            yield return new WaitForSeconds(1f);
            AddValueTime--;
            //text.text = string.Format("{0}", GetMinuteTime(time));
        }
        AddTixianValueTimingAction?.Invoke();
        AddValueTime = 0;
       // text.text = string.Format("{0}", GetMinuteTime(time));
       
    }
    public void StartTime()
    {
        StartCoroutine(Timing());
    }
    //public Action temporaryDaimondAction;
    //public Action fullTemporaryDaimondAction;
    public Action goldAction;
    public Action expengdGoldAction;
    //public Action temporaryGoldAction;
    //public Action fullTemporaryGoldAction;
    public Action diamondAction;
    public Action redAction;
    public Action expenddiamondAction;

    public UnityAction<string> addProduceAction;
    public UnityAction<string, int, bool> addSelledProduceAction;
    public UnityAction<string> addSelledAction;
    public UnityAction sellCountAction;
    public UnityAction<string, int> creactProduceEvent;
    public UnityAction<string, int, bool> creactSelledProduceEvent;
    public UnityAction addStoreExpAction, addStoreLevelAction;
    public UnityAction awardAction;

    public event UnityAction<float> AddTixianValueAction;
    public event UnityAction<float,bool> AddTixianValue1Action;
    public event UnityAction AddTixianValueTimingAction;
    public event UnityAction ClickDaimondAction;
    public int time;
    /// <summary>
    /// 升级红包点击次数
    /// </summary>
    public int ClickShengJiRedCount = 0;
    /// <summary>
    /// 升级红包初始值
    /// </summary>
    public float ShengJiRedValue=0f;
    /// <summary>
    /// 发货红包点击次数
    /// </summary>
    public int ClickFaHuoRedCount = 0;

    public int AddValueTime = 0;
    public int coldTime=0;

    //public DateTime lastTime;


    //1. 代发货数量
    //2. 是否已领取7个发货红包
    //3. 连续登陆天数
    //4. 车辆解锁等级（默认1）
    //1. 车辆1待领取金币
    //2.车辆2待领取金币
    //3. 车辆3待领取金币
    //5. 飞机解锁等级(默认1）
    //1. 飞机1待领取金币
    //2. 飞机2待领取金币
    //3. 飞机3待领取金币
    //6. 轮船离开时间
    //7. 轮船待领取金币
    //8. 金币领取次数

    #region
 //   public Datas3D Datas3D
 //   {

 //       get
 //       {
 //           if (datas3D == null)
 //           {
 //               if (isHaveKey("Datas3D"))
 //               {
 //                   datas3D = JsonMapper.ToObject<Datas3D>(Load("Datas3D"));
                    
 //                   if (isHaveKey(DAIFAHUO))
 //                   {
 //                       datas3D.DaiFaHuo = LoadQueue(DAIFAHUO);
 //                   }
 //                   else
 //                   {
 //                       datas3D.DaiFaHuo = new Queue<int>();
 //                   }
 //               }
 //               else
 //               {
 //                   datas3D = new Datas3D();
 //               }
 //           }
 //           return datas3D;
 //       }


 //   }

  

 //public  Datas3D datas3D;
    /// <summary>
    /// 1. 代发货数量
    /// </summary>

    public void AddDaiFaHuoCount(int count)
    {
      //  Datas3D.DaiFaHuoCount += count;
        AddDaiFaHuoCountAction?.Invoke(count);
    }
    public event UnityAction<int> AddDaiFaHuoCountAction;

    /// <summary>
    /// 是否已领取40个发货红包
    /// </summary>
    /// <returns></returns>
    public bool IsSevenRedCount { get => ClickFaHuoRedCount >= ZiDongFaHuo.count; }

    //public int DaiFaHuoCount;
    ///// <summary>
    ///// //4. 车辆解锁等级（默认1）
    ///// </summary>
    //public int CarJieSuoLevel = 1;
    ///// <summary>
    ///// //5. 飞机解锁等级(默认1）
    ///// </summary>
    //public int FeiJIJieSuoLevel = 1;

    ///// <summary>
    ///// 是否解锁飞机
    ///// </summary>
    //public bool IsJieSuoFeiJi = false;
    ///// <summary>
    ///// 是否解锁轮船
    ///// </summary>
    //public bool IsJieSuoLunChuan= false;
    ///// <summary>
    ///// 车辆待领取金币
    ///// </summary>
    //public int carGold1 = 0;
    //public int carGold2 = 0;
    //public int carGold3 = 0;
    ///// <summary>
    ///// 飞机待领取金币
    ///// </summary>
    //public int feijiGold1 = 0;
    //public int feijiGold2 = 0;
    //public int feijiGold3 = 0;
    ///// <summary>
    ///// 轮船离开时间
    ///// </summary>
    //public string lunchuangTime;
    ///// <summary>
    ///// 轮船待领取金币
    ///// </summary>
    //public int lunchuangGold1 = 0;
    ///// <summary>
    ///// 金币获取次数
    ///// </summary>
    //public int GetGoldCount = 0;

    //public void Get3DData()
    //{
    //    if (isHaveKey("DaiFaHuoCount"))
    //    {
    //        DaiFaHuoCount = LoadInt("DaiFaHuoCount");
    //    }
    //    if (isHaveKey("CarJieSuoLevel"))
    //    {
    //        CarJieSuoLevel = LoadInt("CarJieSuoLevel");
    //    }
    //    if (isHaveKey("FeiJIJieSuoLevel"))
    //    {
    //        FeiJIJieSuoLevel = LoadInt("FeiJIJieSuoLevel");
    //    }
    //    if (isHaveKey("carGold1"))
    //    {
    //        carGold1 = LoadInt("carGold1");
    //    }
    //    if (isHaveKey("carGold2"))
    //    {
    //        carGold1 = LoadInt("carGold2");
    //    }
    //    if (isHaveKey("carGold3"))
    //    {
    //        carGold1 = LoadInt("carGold3");
    //    }
    //    if (isHaveKey("feijiGold1"))
    //    {
    //        feijiGold1 = LoadInt("feijiGold1");
    //    }
    //    if (isHaveKey("feijiGold2"))
    //    {
    //        feijiGold2 = LoadInt("feijiGold2");
    //    }
    //    if (isHaveKey("feijiGold3"))
    //    {
    //        feijiGold3 = LoadInt("feijiGold3");
    //    }
    //    if (isHaveKey("lunchuangTime"))
    //    {
    //        lunchuangTime = Load("lunchuangTime");
    //    }
    //    if (isHaveKey("lunchuangGold1"))
    //    {
    //        lunchuangGold1 = LoadInt("lunchuangGold1");
    //    }
    //    if (isHaveKey("GetGoldCount"))
    //    {
    //        GetGoldCount = LoadInt("GetGoldCount");
    //    }
    //    if (isHaveKey("IsJieSuoFeiJi"))
    //    {
    //        IsJieSuoFeiJi = true;
    //    }
    //    if (isHaveKey("IsJieSuoLunChuan"))
    //    {
    //        IsJieSuoLunChuan = true;
    //    }

    //}

    public void Save3DData()
    {
        //Save("DaiFaHuoCount", DaiFaHuoCount);
        //Save("CarJieSuoLevel", CarJieSuoLevel);
        //Save("FeiJIJieSuoLevel", FeiJIJieSuoLevel);
        //Save("carGold1", carGold1);
        //Save("carGold2", carGold2);
        //Save("carGold3", carGold3);
        //Save("feijiGold1", feijiGold1);
        //Save("feijiGold2", feijiGold2);
        //Save("feijiGold3", feijiGold3);
        //Save("lunchuangTime", lunchuangTime);
        //Save("lunchuangGold1", lunchuangGold1);
        //Save("GetGoldCount", GetGoldCount);
        //if(IsJieSuoFeiJi)
        //Save("IsJieSuoFeiJi", 1);
        //if (IsJieSuoLunChuan)
        //{
        //    Save("IsJieSuoLunChuan", 1);
        //}
        //if (datas3D != null)
        //{
        //    Save("Datas3D", JsonMapper.ToJson(datas3D));
        //}
    }
    #endregion
    public override void Init()
    {
        base.Init();
        if (isHaveKey("IsTiXian"))
        {
            IsTiXian = true;
        }
        daimondTaskID= LoadInt("daimondTaskID",1);
        _videoClickRedAdwrdCount = LoadInt("videoClickRedAdwrdCount", 0);
        //if (isHaveKey("IsShowEWaiTiXianUI"))
        //{
        //    IsShowEWaiTiXianUI = true;
        //}
        if (isHaveKey("IsWangDianUI"))
        {
           IsWangDianUI=true;
        }
        if (isHaveKey("clickCount"))
        {
            clickCount = LoadInt("clickCount");
        }
        if (isHaveKey("ClickDaimondCount"))
        {
            ClickDaimondCount= LoadInt("ClickDaimondCount");
        }
        if (isHaveKey("TiXianCount"))
        {
            TiXianCount = LoadInt("TiXianCount");
        }
        if (isHaveKey("AddTiXIanCount_Table"))
        {
            AddTiXIanCount_Table = LoadInt("AddTiXIanCount_Table");
        }
        if (isHaveKey("IsShowShengJiRed"))
        {
            IsShowShengJiRed = false;
        }
        else
            IsShowShengJiRed = true;
        if (isHaveKey("daimondNotEnoughCount"))
        {
            daimondNotEnoughCount= LoadInt("daimondNotEnoughCount");
        }
       //if(isHaveKey("IsShowDaimondTaskUI"))//展示钻石任务
       // {
       //     IsShowDaimondTaskUI = true;
       //     print("unity++SetDaimondTaskStatus++IsShowDaimondTaskUI==true");
       // }
       // else
       // {
       //     print("unity++SetDaimondTaskStatus++IsShowDaimondTaskUI==false");
       // }


        //if (isHaveKey("DateTime"))
        //    {
        //    lastTime=DateTime.Parse( Load("DateTime")); }
        //else
        //{
        //    lastTime = DateTime.Now;
        //}
        if (isHaveKey("ChouJiangTime"))
        {
            chouJiangTime = LoadInt("ChouJiangTime");
            if (chouJiangTime > 0)
             startChouJiangTiming();
        }
        if (isHaveKey(FirstShengjiEcpm))
        {
            firstShengJiEcpm = LoadInt("FirstShengjiEcpm");
        }
        if (isHaveKey("FirstTableEcpm"))
        {
            FirstTableEcpm = LoadInt("FirstTableEcpm");
        }
        if (isHaveKey("AddValueTime"))
        {
            AddValueTime= LoadInt("AddValueTime");
        }
        if (isHaveKey("coldTime"))
        {
            coldTime = LoadInt("coldTime");
        }
        if (AddValueTime > 0)
        {
           
            StartTime();
        }
        if (isHaveKey("time") == false)
        {
            time = 0;
        }
        else
            time = LoadInt("time");
        if (isHaveKey("ClickShengJiRedCount"))
        {
            ClickShengJiRedCount = LoadInt("ClickShengJiRedCount");
        }
        if (isHaveKey("ShengJiRedValue"))
        {
            ShengJiRedValue = LoadFloat("ShengJiRedValue");
        }
        if (isHaveKey("ClickFaHuoRedCount"))
        {
            ClickFaHuoRedCount = LoadInt("ClickFaHuoRedCount");
        }
        //if (isHaveKey(SALESVOLUME))
        //{
        //    salesVolume= Loadlong(SALESVOLUME);
        //}
        if (isHaveKey(GETDAIMONDCOUNT))
        {
            getDaimondCount = LoadInt(GETDAIMONDCOUNT);
        }
        if (isHaveKey(CHOUJIANGCOUNT))
        {
            chouJiangCount = LoadInt(CHOUJIANGCOUNT);
        }
        // StartCoroutine(GetInternetTime());
        if (isHaveKey(REFRESHCOUNT))
        {
           ShengJiCount = LoadInt(REFRESHCOUNT);
        }
        if (isHaveKey(CLICKFAHUOCOUNT))
        {
            ClickFaHuoCount = LoadInt(CLICKFAHUOCOUNT);
        }
        //if (isHaveKey(MONEYREWARD))
        //{
        //    moneyReward = LoadInt(MONEYREWARD);
        //}
        if (isHaveKey(STOREDATA))
        {
            storeData = JsonMapper.ToObject<StoreData>(Load(STOREDATA));
        }
        else
        {
            storeData = new StoreData { level = 0, exp = 0 };
        }
        if (isHaveKey(DAY))
        {
            day = LoadInt(DAY);
        }
        if (isHaveKey(RED))
        {
            red = LoadInt(RED);
        }
        if (isHaveKey(ACTOR_MAXLEVEL))
        {
            actor_maxlevel = LoadInt(ACTOR_MAXLEVEL);
        }
        if (isHaveKey(GOLD))
        {
            gold = LoadInt(GOLD);
        }
        if (isHaveKey(DIAMOND))
        {
            diamond = LoadInt(DIAMOND);
        }
        //if (isHaveKey(TEMPORARYGOLD))
        //{
        //    temporaryGold = LoadInt(TEMPORARYGOLD);
        //}
        //if (isHaveKey(TEMPORARYDIAMOND))
        //{
        //    temporaryDiamond = LoadInt(TEMPORARYDIAMOND);
        //}
        if (isHaveKey("ProduceDate"))
        {
            produceDic = JsonMapper.ToObject<Dictionary<string, ProduceDate>>(Load("ProduceDate"));
        }
        else
        {
            produceDic = new Dictionary<string, ProduceDate>();
        }
        if (isHaveKey("SelledDate"))
        {
            selledDic = JsonMapper.ToObject<Dictionary<string, ProduceDate>>(Load("SelledDate"));
            SelledCount();
        }
        else
        {
            selledDic = new Dictionary<string, ProduceDate>();
        }

        //if (isHaveKey("ShopInfo"))
        //{
        //    infos = JsonMapper.ToObject<ShopInfo[]>(Load("ShopInfo"));
        //}
        //if (isHaveKey("My_Shop_Mission"))
        //{
        //    shop_MissinList= JsonMapper.ToObject<List<My_Shop_Mission>>(Load("My_Shop_Mission"));
        //}
        //else
        //{
        //    shop_MissinList = new List<My_Shop_Mission>();
        //   // shop_MissinList.Sort();
        //}
        //if (isHaveKey("Mission_redpacket"))
        //{
        //    mission_RedpacketsList = JsonMapper.ToObject<List<Mission_redpacket>>(Load("Mission_redpacket"));
        //}
        //else
        //{
        //    mission_RedpacketsList = new List<Mission_redpacket>();
        //    // shop_MissinList.Sort();
        //}

        if (isHaveKey("ActorDate"))
        {
            actorDateList = JsonMapper.ToObject<List<ActorDate>>(Load("ActorDate"));
        }
        else
        {
            actorDateList = new List<ActorDate>();
        }

        //if (isHaveKey("CourierDate"))
        //{
        //    courierDateList = JsonMapper.ToObject<List<CourierDate>>(Load("CourierDate"));
        //}
        //else
        //{
        //    courierDateList = new List<CourierDate>();
        //}
        //Get3DData();

        //=======================BigWorld==============
        //待发货商品 
        //if (isHaveKey(DAIFAHUO))
        //{
        //    datas3D.DaiFaHuo = LoadQueue(DAIFAHUO);
        //}
        //else
        //{
        //    datas3D.DaiFaHuo = new Queue<int>();
        //}
    }

    /// <summary>
    /// 获取钻石对象
    /// </summary>
    /// <param name="day">天数</param>
    /// <returns></returns>
    public Sign_up GetSign_Up(int day)
    {
        return Sign_Ups.Find(s => s.days == day);
    }

    public void AddProduce(int id,int count)
    {
        if (id == 0) return;
        if (produceDic.ContainsKey(id.ToString()))
        {
            produceDic[id.ToString()].item_have += count;
           if(addProduceAction != null)
            {
                addProduceAction(id.ToString());
            }
        }
        else
        {var produceDate= new ProduceDate() {
               item_id = id,
           item_have = count};
             produceDic.Add(id.ToString(), produceDate) ;
            
            //if (creactProduceEvent != null)
            //{
            //    creactProduceEvent(id.ToString(), produceDic.Count);
            //}
            creactProduceEvent?.Invoke(id.ToString(), produceDic.Count);
           // StockManager.Instance.CreactProduceInfo(shopUI.currentProduce.item_name,produceDic.Count);
        }
      //  SaveProduceDate();
       // SaveShopDate();
    }
    public void AddFreeProduce(FreeShop freeShop)
    {
        if (produceDic.ContainsKey(freeShop.id.ToString()))
        {

            produceDic[freeShop.id.ToString()].item_have += freeShop.count;
            if (addProduceAction != null)
            {
                addProduceAction(freeShop.id.ToString());
            }
        }
        else
        {
            var produceDate = new ProduceDate()
            {
                item_id = freeShop.id,
                item_have = freeShop.count
            };
            produceDic.Add(freeShop.id.ToString(), produceDate);

            if (creactProduceEvent != null)
            {
                creactProduceEvent(freeShop.id.ToString(), produceDic.Count);
            }
        }
    }
   
    List<string> list;
    int selledcount;
    public int SelledCount()
    {
     list= new List<string>(  selledDic.Keys);
        if (list == null || list.Count == 0) return 0;
        foreach (var item in list)
        {
            selledcount += selledDic[item].item_have;
        }
        return selledcount;
    }
    public int GetSelledCount()
    {
        return selledcount;
    }
    public void RemoveSelledCount(int count)//减去数量数量
    {
        selledcount -= count;
        if (sellCountAction != null)
        {
            sellCountAction();
        }
    }
    public void AddSelledCount(int count)
    {
        selledcount += count;
        if (sellCountAction != null)
        {
            sellCountAction();
        }
    }
   
    public void AddSelled(ZhiBoJian zhiBoJian,int count)
    {
        if (zhiBoJian.actorDate.item_id != 0)
        {
            AddSelledCount(count);
            AddSalesVolume(count);
           
            if (selledDic.ContainsKey(zhiBoJian.actorDate.item_id.ToString()))
            {
                selledDic[zhiBoJian.actorDate.item_id.ToString()].item_have += count;
                if (selledDic[zhiBoJian.actorDate.item_id.ToString()].item_have <= count)
                {
                    if (addSelledProduceAction != null)
                    {
                        addSelledProduceAction(zhiBoJian.actorDate.item_id.ToString(), count,false);
                    }
                    if (addSelledAction != null)
                    {
                        addSelledAction(zhiBoJian.actorDate.item_id.ToString());
                    }
                }
                else
                {
                    if (addSelledAction != null)
                    {
                        addSelledAction(zhiBoJian.actorDate.item_id.ToString());
                    }
                }


            }
            else
            {
                var produceDate = new ProduceDate()
                {
                    item_id = zhiBoJian.actorDate.item_id,
                    item_have = count
                };
                selledDic.Add(zhiBoJian.actorDate.item_id.ToString(), produceDate);
                if (creactSelledProduceEvent != null)
                { creactSelledProduceEvent(zhiBoJian.actorDate.item_id.ToString(), count,false); }
                if (addSelledAction != null)
                {
                    addSelledAction(zhiBoJian.actorDate.item_id.ToString());
                }
            }
        }
    }
    
    //public void SaveShopDate()
    //{
    //    var shopUiArry = (UIManager.Instance.GetPanel("Panel_Shop") as HLDH.ShopPanel).shopArry;
    //    infos = new ShopInfo[shopUiArry.Length];
    //    for (int i = 0; i < shopUiArry.Length; i++)
    //    {
    //        infos[i] = shopUiArry[i].info;
    //        // print(infos[i].currentProduce.item_name);
    //    }
    //    Save("ShopInfo", JsonMapper.ToJson(infos));

    //}
    private bool isHaveKey(string key)
    {
        //        print("isHaveKey+++++"+key);
        ////#if UNITY_EDITOR

        //        return PlayerPrefs.HasKey(key);

        ////#elif UNITY_ANDROID
        //// print("isHaveKey+++++ANDROID"+key);
        return DataSaver.Instance.HasKey(key);
        ////#endif
    }
    public void DeleteKey(string key)
    {
     
            DataSaver.Instance.DeleteKey(key);
        
    }
    private void Save(string key,string value)
    {
        //print("存储string类型");
//#if UNITY_EDITOR

//        PlayerPrefs.SetString(key, value);

//#elif UNITY_ANDROID
    DataSaver.Instance.SetString(key, value);
//#endif
    }
    private void Save(string key, int value)
    {
    DataSaver.Instance.SetInt(key, value);
    }
    //private void Save(string key, long value)
    //{
    //    DataSaver.Instance.Setlong(key, value);
    //}
    private void Save(string key, float value)
    {
        DataSaver.Instance.SetFloat(key, value);
    }

    //private void Save(string key, Queue<int> value)
    //{
    //    DataSaver.Instance.SetQueue(key, value);
    //}

    private string Load(string key)
    {
        //print("读取string类型数据");
//#if UNITY_EDITOR

//        return PlayerPrefs.GetString(key);

//#elif UNITY_ANDROID
       return DataSaver.Instance.GetString(key);
//#endif
        //DataSaver.Instance.GetString(key);

    }
    int LoadInt(string key,int defult=0)
    {
        //print("读取int类型数据");
//#if UNITY_EDITOR

//        return PlayerPrefs.GetInt(key);

//#elif UNITY_ANDROID
       return DataSaver.Instance.GetInt(key,defult);
//#endif
//        print("读取int类型数据");
//        DataSaver.Instance.GetInt(key);
//        return PlayerPrefs.GetInt(key);
    }
    //long Loadlong(string key)
    //{
      
    //    return DataSaver.Instance.Getlong(key);
    
    //}
    float LoadFloat(string key)
    {
        return DataSaver.Instance.GetFloat(key);
    }
    //Queue<int> LoadQueue(string Key)
    //{
    //    return DataSaver.Instance.GetQueue(Key);
       
    //}
    public void SaveProduceDate()
    {
        //print("存储商品列表");
        Save("ProduceDate", JsonMapper.ToJson(produceDic));
    }
    public void SaveSelledDate()
    {
        //print("存储已买商品列表");
        Save("SelledDate", JsonMapper.ToJson(selledDic));
    }
    public void SaveActorDate()
    {
        //print("主播列表");
        Save("ActorDate", JsonMapper.ToJson(actorDateList));
    }
    public void SaveCourierDate()
    {
        //print("快递员列表");
        Save("CourierDate", JsonMapper.ToJson(courierDateList));
    }
    public void SaveStoreData()
    {
        //print("网店数据");
      Save(STOREDATA, JsonMapper.ToJson(storeData));
    }
    public void SavaMy_Shop_Mission()
    {
        //print("网店任务");
        Save("My_Shop_Mission", JsonMapper.ToJson(shop_MissinList));
    }
    public void SaveMission_redpacket()
    {
        print("提现任务");
        //Save("Mission_redpacket", JsonMapper.ToJson(mission_RedpacketsList));
    }
    public void SaveSign_up()
    {
        //print("签到");
        //Save("sign_Ups", JsonMapper.ToJson(Sign_Ups));
    }
    public void SaveProduceQiPaos()
    {
      if(ProduceQiPaoManager.Instance!=null)
        Save("SaveProduceQiPaos", JsonMapper.ToJson(ProduceQiPaoManager.Instance.GetSaveQiPaos()));
        else
        {
            if(produceQiPaoList!=null)
            Save("SaveProduceQiPaos", JsonMapper.ToJson(produceQiPaoList));
        }
    }
    public void SaveDaimondTaskData()
    {if(data!=null)
        Save("DaimondTaskData", JsonMapper.ToJson(data));

    }
    public void SaveSevenDataS()
    {if(sevenLoginDatas!=null&&sevenLoginDatas.Count>=6)
        Save("SevenLoginDatas", JsonMapper.ToJson(sevenLoginDatas));
    }
   public void SaveTixianProcese()
    {
        if (tixianValues != null)
        {
            Save("TixianValues", JsonMapper.ToJson(tixianValues));
        }
    }
    public void SaveWangDianData()
    {
        if (newWangDianDatas != null)
        {
            Save("NewWangDianDatas", JsonMapper.ToJson(newWangDianDatas));
        }
    }
    public int chouJiangTime;

    IEnumerator chouJiangTiming()
    {
        while (chouJiangTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            chouJiangTime--;
        }

    }

    /// <summary>
    /// 开启抽奖倒计时
    /// </summary>
    public void startChouJiangTiming()
    {
        StartCoroutine(chouJiangTiming());
    }
    public void SaveIsShowEWaiTiXianBool()
    {
        if (IsShowEWaiTiXianUI)
        {
            Save("IsShowEWaiTiXianUI", "IsShowEWaiTiXianUI");
        }
    }
    //public void SaveBigTask()
    //{
    //    print("奖励任务");
    //    Save(BIGREDPACKETS, JsonMapper.ToJson(Big_Redpackets));
    //}
    /// <summary>
    /// 存储int类型
    /// </summary>
    public void SaveDate()
    {
        //SaveIsShowEWaiTiXianBool();
        if (IsWangDianUI)
        {
            Save("IsWangDianUI", "IsWangDianUI");
        }
        Save("daimondTaskID", daimondTaskID);
        if (IsTiXian)
        {
            Save("IsTiXian", "IsTiXian");
        }
        Save("videoClickRedAdwrdCount", _videoClickRedAdwrdCount);
     
        Save("clickCount", clickCount);
        Save("ClickDaimondCount", ClickDaimondCount);
        SaveProduceQiPaos();
        Save("TiXianCount", TiXianCount);
        Save("AddTiXIanCount_Table", AddTiXIanCount_Table);
        Save("daimondNotEnoughCount", daimondNotEnoughCount);//钻石不足次数
        Save(FirstShengjiEcpm, firstShengJiEcpm);
        Save("FirstTableEcpm", FirstTableEcpm);
       // if (IsShowDaimondTaskUI)
       //Save("IsShowDaimondTaskUI", "IsShowDaimondTaskUI");
        //print("存储货币");
        if(!IsShowShengJiRed)
        Save("IsShowShengJiRed", "IsShowShengJiRed");//后续是否显示升级红包
        else
        {
            DeleteKey("IsShowShengJiRed");
            
        }
        Save("coldTime", coldTime);
        Save("ChouJiangTime", chouJiangTime);
        Save("time", time);
        Save("AddValueTime", AddValueTime);
       //SaveLastTime();
        Save("ShengJiRedValue", ShengJiRedValue);
        Save("ClickShengJiRedCount", ClickShengJiRedCount);
        Save("ClickFaHuoRedCount", ClickFaHuoRedCount);
        //Save(SALESVOLUME, SalesVolume);
        Save(CHOUJIANGCOUNT, ChouJiangCount);
        Save(REFRESHCOUNT,ShengJiCount);
        Save(CLICKFAHUOCOUNT, ClickFaHuoCount);
        Save(GOLD, (int)gold);
        Save(DIAMOND, diamond);
        //Save(TEMPORARYGOLD, (int)temporaryGold);
        //Save(TEMPORARYDIAMOND, temporaryDiamond);
        Save(ACTOR_MAXLEVEL, actor_maxlevel);
        Save(DAY, day);
        Save(RED, red);
        Save(MONEYREWARD, moneyReward);
        //SaveSign_up();
        Save(GETDAIMONDCOUNT, getDaimondCount);
        //Save3DData();
        SaveTixianProcese();
        SaveWangDianData();
        //SaveBigTask();

        //=============BigWrold==========
        //if(datas3D!=null&& datas3D.DaiFaHuo!=null)
        //Save(DAIFAHUO, datas3D.DaiFaHuo);
    }
  
    private void OnApplicationQuit()
    {
        
        SaveDate();
        //print("结束游戏");
        SaveStoreData();
        SaveProduceDate();
        SaveSelledDate();
        SaveActorDate();
    
       
        SaveDaimondTaskData();
        SaveSevenDataS();
        //DataSaver.Instance.SaveData();
       
    }
    private void OnApplicationPause(bool pause)
    {
       
        if (pause)
        { 
            SaveDate();
            SaveStoreData();
            SaveProduceDate();
            SaveSelledDate();
            SaveActorDate();
            //SaveCourierDate();
            //SavaMy_Shop_Mission();
            //SaveMission_redpacket();
            SaveDaimondTaskData();
            print("暂停");
           
            SaveSevenDataS();
            //DataSaver.Instance.SaveData();
            //if(!DataSaver.Instance.isES3)
            //PlayerPrefs.Save();
            //ES3.DeleteFile("HLDH.es3");
        }
        if (!pause)
        {
            //SaveProduceDate();
            //SaveSelledDate();
            //SaveActorDate();
            //SaveCourierDate();
            print("恢复");
        }
    }
}
[Serializable]
public class ProduceDate
{
    public int item_id ;

    public int item_have;
    public int type;
 
    /// <summary>
    /// 0表示没被售卖，1表示被卖
    /// </summary>
   // public int state;
}

[Serializable]
public class StoreData
{
    public int level;
    public int exp;
    //public int expLimit;
}

[Serializable]
public class DaimondTask
{
    //public string info;
    //public int progress;
    //public int status;//0是未完成 1是已完成
    //public int count;
    //public int NRV;
    //public int type;
    public int main_mission_id;
    public int main_mission_type;
    public string main_mission_name;
    public string main_mission_context;
    public int main_mission_isdone;
    public int main_mission_reward;
    public int main_mission_item_id;
    public int main_mission_item_id_num;
    public int main_mission_shengji_index;
    public int main_mission_redpacket_index;
    public int progress;
}
[Serializable]
public class DaimondTaskData
{
   public   List<DaimondTask> daimondTasks;
    public int currentTask;

}

[Serializable]
public class Datas3D
{
    //==============BigWorld===============
    /// <summary>
    /// 待发货商品
    /// </summary>
    public Queue<int> DaiFaHuo=new Queue<int>();

    public int DaiFaHuoCount;
    /// <summary>
    /// //4. 车辆解锁等级（默认1）
    /// </summary>
    public int CarJieSuoLevel = 1;
    /// <summary>
    /// //5. 飞机解锁等级(默认1）
    /// </summary>
    public int FeiJIJieSuoLevel = 1;

    /// <summary>
    /// 是否解锁飞机
    /// </summary>
    public bool IsJieSuoFeiJi = false;
    /// <summary>
    /// 是否解锁轮船
    /// </summary>
    public bool IsJieSuoLunChuan = false;
    /// <summary>
    /// 车辆待领取金币
    /// </summary>
    public int carGold1 = 0;
    public int carGold2 = 0;
    public int carGold3 = 0;
    public int carGold4 = 0;
    /// <summary>
    /// 飞机待领取金币
    /// </summary>
    public int feijiGold1 = 0;
    public int feijiGold2 = 0;
    public int feijiGold3 = 0;
    /// <summary>
    /// 轮船离开时间
    /// </summary>
    public string lunchuangTime;
    /// <summary>
    /// 轮船待领取金币
    /// </summary>
    public int lunchuangGold1 = 0;
    /// <summary>
    /// 金币获取次数
    /// </summary>
    public int GetGoldCount = 0;

}
