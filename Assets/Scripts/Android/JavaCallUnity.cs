using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.Events;
using System;
public class JavaCallUnity : MonoBehaviour
{
    
    public static JavaCallUnity Instance;
    // public Transform born1, born2;
    //  public Camera uicamera;
    //public Camera mainCamere;

    //public GameObject main, ui;
    public bool IsNewUser;


    public TextAsset textAsset;
    public List<TiXianData> tiXianDatas;
    public List<TiXianData> EWaitiXianDatas;
    public  UnityAction shengjiAction;
    private void Awake()
    {
        Instance = this;
        AndroidAdsDialog.Instance.ReqeustNew();
        //uicamera.orthographicSize = (float)(Screen.height) / 200;
        //mainCamere.orthographicSize = (float)(Screen.height) / 200;
        List<TiXianData> list = new List<TiXianData>();
       
        list.Add(new TiXianData()
        {
            amount = 30,
           
           count = 1

        });
        list.Add(new TiXianData()
        {
            amount =40,
          
            count = 1

        });
        list.Add(new TiXianData()
        {
            amount = 50,
            


            count = 1

        });
        print(JsonMapper.ToJson(list));
        tiXianDatas = list;

        //SetLogin("https://pic1.zhimg.com/v2-d58ce10bf4e01f5086c604a9cfed29f3_r.jpg?source=1940ef5c");
        //EWaitiXianDatas = list;
       // SetLogin(textAsset.text);
    }
    private void Start()
    {
        //LoginWeChat("https://pic1.zhimg.com/v2-d58ce10bf4e01f5086c604a9cfed29f3_r.jpg?source=1940ef5c");
        AndroidAdsDialog.Instance.NotiyUnityWXInfo();
        IsFirstGetRed = PlayerPrefs.HasKey("Red_IsFirstGetRed");
        Debug.Log("IsFirstGetRed:" + IsFirstGetRed);
        //SetSound("1");
    }
    public void GetGoldBoxAdward(string count)//激励视频回调-60
    {
        if (showCountAction != null)
        {
            showCountAction();
        }
        PanelGoldBox.Instance.GetAdward();

    }
    public void GetDaimondBoxAdward(string count)//激励视频回调-70
    {
        StartCoroutine(Delay(0.8f, () =>
        {
            PanelDaimondTips.Instance.JavaCallUnityEvent();
        }));
    }
    /// <summary>
    /// 获取是不是新手用户
    /// </summary>
    /// <param name="value"></param>
    public void GetUserIsNew(string value)
    {
        if (value == "1")
        {
            print("GetUserIsNewtrue++" + value);
            IsNewUser = true;
        }
        else
        {
            print("GetUserIsNewfalse++" + value);
            IsNewUser = false;
        }
    }
    public void SetMemberDialogText(string value)
    {
        print("SetMemberDialogText++"+value);
      string[] valueArry=  value.Split('+');
        // valueArry[0];
       // ZhiBoPanel.Instance.wangDianDaRenUI.SetText(valueArry[0], valueArry[1]);
    }
    /// <summary>
    /// 设置抽奖次数
    /// </summary>
    /// <param name="value"></param>
    public void SetChouJiangCount(string value)
    {
     //string[] arry=   value.Split('+');
     //   int count;
     //   int dayCount;
     // if(int.TryParse(arry[0], out count))
     //   {
     //       MyShopPanel.Instance.chouJiangTiXian.SetCount(count);
     //   }
     //   else
     //   {
     //       Debug.LogError("安卓传值++" + arry[0]);
     //   }
     //   if (int.TryParse(arry[1], out dayCount))
     //   {
     //       PlayerDate.Instance.ChouJiangCount = dayCount;
     //   }
     //   else
     //   {
     //       Debug.LogError("安卓传值++" + arry[1]);
     //   }
    }
   
    
    public void JavaSendMessage(string tag)
    {
        int count;
        print("UNITY++" + tag);
       if( int.TryParse(tag, out count))
        {
            print("UNITY++" + count);
            if (count >= 0)
            {
                ShenJiZhuBo(tag);
                print("UNITY++SHENGJI");
            }
            else
            {
                CallBack(tag);
            }
        }
        else
        {
            CallBack(tag);
        }
        
    }

    private void CallBack(string tag)
    {
        print("UNITY++QITA");
        switch (tag)
        {
            case AndroidAdsDialog.TAG_ADDZHUBO_WANGDIAN:
                AndroidAdsDialog.Instance.UploadDataEvent("finish_myshop_mission_get_video");
                ShenJIlouCeng(tag);
                break;
            case AndroidAdsDialog.TAG_ADDZHUBO:
                ZhiBoPanel.Instance.creactZhuBoTiming.ClickEvent();
                AndroidAdsDialog.Instance.UploadDataEvent("finish_new_zhaomu");
                ShenJIlouCeng(tag);
                break;
            case AndroidAdsDialog.TAG_ChouJiang:
                ChouJiangEvent(tag);
                break;
            case AndroidAdsDialog.TAG_GetDaiMond:
                GetDaiMond(tag);
                break;
            case AndroidAdsDialog.TAG_GetGoldBoxAdward:
                GetGoldBoxAdward(tag);
                break;
            case AndroidAdsDialog.TAG_GOLD:
                GetGold(tag);
                break;
            case AndroidAdsDialog.TAG_PRODUCE:
                GetProduceCount(tag);
                break;
            case AndroidAdsDialog.TAG_SHENGJIRED:
               // ShengJIRed(tag);
                break;

            case AndroidAdsDialog.TAG_GetDaimondBoxAdward:
                GetDaimondBoxAdward(tag);
                break;
            case AndroidAdsDialog.TAG_GetWANGDIANEXP:
                GetWangdiangExp(tag);
                break;
            case AndroidAdsDialog.TAG_KUAIDI:
                AndroidAdsDialog.Instance.RewardAction?.Invoke();
                AndroidAdsDialog.Instance.RewardAction = null;
                break;
            case AndroidAdsDialog.TAG_ADDVALUE:
                AndroidAdsDialog.Instance.RewardAction?.Invoke();
                AndroidAdsDialog.Instance.RewardAction = null;
                break;
            default:
                AndroidAdsDialog.Instance.RewardAction?.Invoke();
                AndroidAdsDialog.Instance.RewardAction = null;

                break;
        }
    }

    /// <summary>
    /// 增加钻石得接口
    /// </summary>
    /// <param name="count">数量</param>
    public void AddDaimond(string count)
    {
        int value;
        if(int.TryParse(count,out value))
        PlayerData.Instance.GetDiamond(value);
    }
    /// <summary>
    /// 开启额外提现
    /// </summary>
    /// <param name="value"></param>
    public void SetEWaiTiXianStatus(string value)
    {
        //if (!PlayerData.Instance.IsShowEWaiTiXianUI)
        //{ 
        //    PlayerData.Instance.IsShowEWaiTiXianUI = true;
        //    UnityActionManager.Instance.DispatchEvent("ShowEWaiTiXianIcon");
        //}
        //EWaiTiXianPanel.Instance.ShowUI(UIManager.Instance.showRootMain);

    }
    public event UnityAction feedCallBackAction;
    public void DoFeedCallBack(string value)
    {
        feedCallBackAction?.Invoke();
       
    }
    public event UnityAction<bool> BannerCallBackAction;
    public void DoBannerCallBack(string value)
    {
        bool isShow = value =="1" ? true : false;
        BannerCallBackAction?.Invoke(isShow);
    }
    /// <summary>
    ///设置钻石任务次数
    /// </summary>
    /// <param name="count"></param>
    public void SetRemainderCount(string count)
    {
        //int value;
        //if (int.TryParse(count, out value))
        //   DaimondTaskUI.Instance.SetRemainderCount(value);
        //else
        //{
        //    DaimondTaskUI.Instance.SetText(count);
        //}
    }
    public void SetQiaoDao(string value)
    {
        //int value1 = int.Parse(value);
        //SetQiaoDaoAction?.Invoke(value1);
    }
    public void ShowPiaoChuang(string value)
    {
        StartCoroutine(Global.Delay(0.1f, () => { AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget1, value, Color.black, null, null, 0.8f); }));
        //AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget1, value, Color.black, null, null, 1f);
    }
  public  UnityAction<int> SetQiaoDaoAction;
    /// <summary>
    /// count 为设置冷却时间
    /// </summary>
    /// <param name="count"></param>
    public void SetDaimonTaskColdTime(string count)
    {
        //int value = int.Parse(count);
        //DaimondTaskUI.Instance.SetColdTime(value);
    }
    /// <summary>
    /// "1"为显示 "0"为隐藏
    /// </summary>
    /// <param name="value"></param>
    public void ShowRecoverBtn(string value)
    {
        //bool status = value == "1" ? true : false;
        //DaimondTaskUI.Instance.ShowRecoverBtn(status);
    }
    public void ChouJiangEvent(string count)//激励视频回调-100
    {
        AndroidAdsDialog.Instance.UploadDataEvent("finish_myshop_showvideo_new");
        StartCoroutine(  Delay(1f, MyShopPanel.Instance.ChouJiangEvent));
       // MyShopPanel.Instance.ChouJiangEvent();

    }
    public void SetDaimondTaskStatus(string count)//
    {
        //print("unity++SetDaimondTaskStatus"+count);
        //if (!PlayerData.Instance.IsShowDaimondTaskUI)
        //{
        //    PlayerData.Instance.IsShowDaimondTaskUI = true;
        //    DaimondTaskUI.Instance.gameObject.SetActive(PlayerData.Instance.IsShowDaimondTaskUI);
        //    DaimondTaskUI.Instance.ShowDaimondTipsPanel();
        //    PlayerData.Instance.getDaimondCount = 0;
        //}
    }
  public void SetEWaiTiXianValue(string count)
    {
        //int value = int.Parse(count);
        //print("unity++SetEWaiTiXianValue" + count);
        //PlayerData.Instance.TixianValues[value] = 0;
        //EWaiTiXianPanel.Instance.RefreshValue();
    }
    public void GetGold(string count)//激励视频回调-50
    {
        if (showCountAction != null)
        {
            showCountAction();
        }
        AndroidAdsDialog.Instance.UploadDataEvent("finish_video_in_fahuo");
        AwardManagerNew.Instance.JavaCallUnityEvent();
    }
    public void GetWangdiangExp(string count)//激励视频回调-50
    {
        if (showCountAction != null)
        {
            showCountAction();
        }
        AndroidAdsDialog.Instance.RewardAction?.Invoke();
        AndroidAdsDialog.Instance.RewardAction = null;
    }
    public void ShowAdwardUI(string value)
    {if(!GuideManager.Instance.isFirstGame)
        AwardManagerNew.Instance.ShowUI(1000,NumberGenenater.GetRedCount(),1);
    }
    public void ShowWangDian(string value)
    {
        if (!GuideManager.Instance.isFirstGame)
            NewWangDianPanel.Instance.ShowUI(UIManager.Instance.showRootMain);
    }
    public void ShowWangDianScene(string value)
    {
        if (!GuideManager.Instance.isFirstGame)
        { if (PlayerData.Instance.red >= PlayerData.Instance.RedBiaoZhu)
                ToggleManager.Instance.ShowPanel(0);
        
        else
        {
            ToggleManager.Instance.ShowTusi();
        }
    }
        else
        {
           
        }

    }
    public void ShowZhiBo(string value)
    {
        if (!GuideManager.Instance.isFirstGame)
        { 
            
            ToggleManager.Instance.ShowPanel(1); }
       
    }
    public void ShowFaHuo(string value)
    {
        if (!GuideManager.Instance.isFirstGame)
            ToggleManager.Instance.ShowPanel(2);
    }
    public void ShowRedUI(string value)
    {
        if (!GuideManager.Instance.isFirstGame)
            AwardManagerNew.Instance.ShowUI(666, NumberGenenater.GetRedCount(), 1);
    }
    public void GetProduceCount(string count)//激励视频回调-10
    {
        print("GetProduceCount" + count);
     

        if (showCountAction != null)
        {
            showCountAction();
        }

       
        Shop.Instance.CallBack();
        

    }
   
    float tixianValue = 0;
   public void SetTiXianValue(string count)
    {
        print("tixianValue+++" + count);
        tixianValue=  float.Parse(count);
    }
    public void SetFirstTableECPM(string count)
    {
        //if (!PlayerData.Instance.IsShowEWaiTiXianUI) return;
        print("SetFirstTableECPM+++" + count);
        //if (PlayerData.Instance.FirstTableEcpm <= 0)
        //{
        //    PlayerData.Instance.FirstTableEcpm = int.Parse(count);
        //}
        //if (PlayerData.Instance.FirstTableEcpm <= 20)
        //{



        //}
        //else
        //{
        //    PlayerData.Instance.AddTiXIanCount_Table++;

        //}
        // AddTiXianValue(int.Parse(count));
        int ecpm = int.Parse(count);
        PlayerData.Instance.FirstTableEcpm+= ecpm;
      
            HttpService.Instance.UploadTableVideoEventRequest(ecpm, 1, "unity插屏回调");

    }
    
    public void AddTiXianTaskValue(string value)
    {
      //double count=  double.Parse(value);
      //  ShangJinTaskPanel.Instance.AddTiXianValue(count);
    }
    public void SetSound(string value1)
    {
        settingPanel.Instance.ShowUI();
    }
    public void requestCKDData(string count)
    {
        print("unityrequestCKDData");
        bool value = count == "1" ? true : false;
        AndroidAdsDialog.Instance.requestCKDDataAction?.Invoke(value);

    }
    public void requestSJHBWithDrawList(string count)
    {
        print("unityrequestSJHBWithDrawList");
        tiXianDatas = JsonMapper.ToObject<List<TiXianData>>(count);
        UnityActionManager.Instance.DispatchEvent("RefreshZhiBo");
    }
    public void requestEWTXWithDrawList(string count)
    {
        print("requestEWTXWithDrawList");
        var list = JsonMapper.ToObject<List<TiXianData>>(count);
        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                EWaitiXianDatas[i] = list[i];
            }
        }
    }
  
    public int jiliEcpm { get; set; }
    public void SetCurrentEcpm(string count)
    {
        Debug.Log("SetCurrentEcpm" + count);
        int value1 = int.Parse(count);
        jiliEcpm = value1;
        SevenLoginPanel.Instance.AddCount();
        HttpService.Instance.UploadRewardVideoEventRequest((double)value1, "unity激励视频回调");
        PlayerData.Instance._videoClickRedAdwrdCount++;

    }
    public void ShengJIRed(string count)//激励视频回调-11
    {
        PlayerData.Instance.IsShowShengJiRed =true;
        AndroidAdsDialog.Instance.CloseBanner();
        AndroidAdsDialog.Instance.UploadDataEvent("finish_ad_shengjihb_news");
        AndroidAdsDialog.Instance.UploadDataEvent("finsh_shengjihblast");
        AudioManager.Instance.PlaySound("show_shengjihb");
        var skill = ConfigManager.Instance.GetSkill(ZhiBoPanel.Instance.zhibojianList[ZhiBoPanel.Instance.currentZhuBoIndex].actorDate.actor_level + 1);

        int daimondCount = 0;
      
           
        
        print("ShengJIRed+++" + count);
        if (showCountAction != null)
        {
            showCountAction();
        }
        hongbao3.Instance.Hide();
        //string[] arry = count.Split('+');

        int value1 = int.Parse(count);
        if (PlayerData.Instance.firstShengJiEcpm <= 0)
        {
            PlayerData.Instance.firstShengJiEcpm = value1;
        }

        //        int index = GetDangwei();
        //    float value2 = 0.3f;
        //    if (index <= 2)
        //    {
        //        value2 = NumberGenenater.GetTiXianValue(value1, (float)tiXianDatas[index].amount / 100f, PlayerData.Instance.ShengJiRedValue) + PlayerData.Instance.ShengJiRedValue;

        //    }
        //    else
        //    {
        //        value2 = NumberGenenater.GetTiXianValue(value1, 0.5f, PlayerData.Instance.ShengJiRedValue) + PlayerData.Instance.ShengJiRedValue;
        //    }
        //    if (value2 > 1)
        //    {
        //        value2 = 1;
        //    }
        //    if (PlayerData.Instance.firstShengJiEcpm >= 50)

        //    {
        //        daimondCount = (int)(skill.actorlevel_cost_num * 1.5f);
        //        hongbao5.Instance.tiXianItems[index].SetStates("已领取", 1);
        //        if (PlayerData.Instance.ClickShengJiRedCount == 0)
        //        {
        //            AndroidAdsDialog.Instance.UploadDataEvent("sjhb_ecpm_high");
        //            AndroidAdsDialog.Instance.UploadDataEvent("big_ecpm_shengjihblast");
        //        }
        //            // int red = NumberGenenater.GetRedCount();
        //            //if(PlayerDate.Instance.red + red>100* MoneyManager.redProportion)
        //            //{
        //            //    hongbao4.Instance.NeedToMakeHongBaoNumberText.gameObject.SetActive(false);
        //            //}
        //            //else
        //            //{
        //            //    hongbao4.Instance.NeedToMakeHongBaoNumberText.gameObject.SetActive(true);
        //            //}
        //            hongbao5.Instance.InitHongBao(PlayerData.Instance.ShengJiRedValue, value2, index, daimondCount, () =>
        //        {//PlayerDate.Instance.GetRed(red);
        //        hongbao5.Instance.Hide(); 
        //            AudioManager.Instance.PlaySound("finish_redpacket");
        //            AndroidAdsDialog.Instance.RewardAction?.Invoke();
        //            hongbao3.Instance.RecoverCanShow();
        //            PlayerData.Instance.GetDiamond(daimondCount);
        //            TipsShowBase.Instance.Show("TipsShow4", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
        //{
        //        string.Format("+{0}",daimondCount)
        //    }, new Sprite[]
        //    {
        //            ResourceManager.Instance.GetSprite("钻石"),
        //    }, null, null);
        //        }
        //       , () =>
        //       {
        //           if (value2 >= 1 && index <= 2)
        //           {
        //               print("提现");
        //               AndroidAdsDialog.Instance.requestWithDraw(tiXianDatas[index].mid, tiXianDatas[index].cid, (double)(tiXianDatas[index].amount / 100d));
        //               tiXianDatas[index].count = 0;
        //               hongbao5.Instance.NextDangWei();
        //               hongbao5.Instance.tiXianItems[index].SetStates("已领取", 3);


        //           }
        //       }
        //       );
        //    }
        //    else
        //    {
        //        daimondCount = (int)(skill.actorlevel_cost_num * 1.8f);
        //        if (PlayerData.Instance.ClickShengJiRedCount == 0)
        //        {
        //            AndroidAdsDialog.Instance.UploadDataEvent("sjhb_ecpm_low");
        //            AndroidAdsDialog.Instance.UploadDataEvent("small_ecpm_shengjihblast");
        //        }
        int redCount =(int)( 0.1f * MoneyManager.redProportion);
        bool isShowVideo = false;
        if (value1 > 0)
        {
            redCount = NumberGenenater.GetRedCount();
            isShowVideo = true;
        }
        daimondCount = (int)(skill.actorlevel_cost_num * 1.8f);
        //UnityAction unityAction = AndroidAdsDialog.Instance.RewardAction;
        //AndroidAdsDialog.Instance.RewardAction = null;
        GetAdwardManagerNew.Instance.ShowUI(redCount, daimondCount, 1,()=> {

              AndroidAdsDialog.Instance.UploadDataEvent("finish_ad_shengjihb_news");
              AndroidAdsDialog.Instance.UploadDataEvent("finish_small_ecpm_shengjihblast");
 
             hongbao3.Instance.RecoverCanShow();
            shengjiAction?.Invoke();
            shengjiAction = null;
              PlayerData.Instance.ClickShengJiRedCount++;
          },"升级红包奖励", isShowVideo);
    //    }
    }
    public string GetDangWeiValue()
    {
        if (GetDangwei() < tiXianDatas.Count)
            return (tiXianDatas[GetDangwei()].amount / 100f).ToString() + "元";
        else return "5元";
    }
  public int GetDangwei()
    {
        int index = 0;
        // List<TiXianData> list = JsonMapper.ToObject<List<TiXianData>>(arry[1]);
        for (int i = 0; i < tiXianDatas.Count; i++)
        {
            hongbao5.Instance.tiXianItems[i].SetJinE(((float)tiXianDatas[i].amount / 100f).ToString() + "元");
            if (tiXianDatas[i].count == 0)
            {
                index = i + 1;
                hongbao5.Instance.tiXianItems[i].SetStates("已领取", 3);
                //hongbao5.Instance.tiXianItems[i+1].SetStates("已领取", true);

            }
            else
            {
                // hongbao5.Instance.tiXianItems[i].SetStates("已领取", false);
            }
        }

        return index;
    }

    float[] values = new float[3];
    public bool IsFirstGetRedValue()
    {
      return  tiXianDatas[0].count > 0&&PlayerData.Instance.ClickShengJiRedCount<=0;
    }

    public void ShowRed(string value)
    {
      ResourceManager.Instance.LoadRed(  int.Parse(value));
    }
    public void AchiveTiXianGuide(string value)
    {
        ToggleManager.Instance.ShowUI(); PeopleEffect.Instance.ShowMask();
              PeopleEffect.Instance.SetTips(ToggleManager.Instance.guiTaget3, ToggleManager.Instance.guiTaget4.position, true, RotaryType.TopToBottom);
    }
    public void AddTiXianValue(string value)
    {
       // if (!PlayerData.Instance.IsShowEWaiTiXianUI) return;
       //int value1 = int.Parse(value);
       // for (int i = 0; i < EWaitiXianDatas.Count; i++)
       // {
       //     values[i] = NumberGenenater.GetTiXianValue1(value1, EWaitiXianDatas[i].amount / 100f);
       // }
      
       // PlayerData.Instance.AddTiXiProcese(values, value1 >= 70);
      
        
       // NumberGenenater.GetTiXianValue1(value, EWaitiXianDatas)
    }
    public void AddTiXianValue(int value)
    {

        //int value1 = value;
        //for (int i = 0; i < EWaitiXianDatas.Count; i++)
        //{
        //    values[i] = NumberGenenater.GetTiXianValue1(value1, EWaitiXianDatas[i].amount / 100f,true);
        //}

        //PlayerData.Instance.AddTiXiProcese(values, value1 >= 70);


   
    }
    public void AddTiXianValueBtnFun(string value)
    {

        //int value1 = int.Parse(value);
        //if (value1 >= 100)
        //{
        //    EWaiTiXianPanel.Instance.time = 180;
        //}
        //else
        //{
        //    EWaiTiXianPanel.Instance.time = 300;
        //}
        //for (int i = 0; i < EWaitiXianDatas.Count; i++)
        //{
        //    values[i] = NumberGenenater.GetTiXianValue1(value1, EWaitiXianDatas[i].amount / 100f) * 2;
        //}
        //PlayerData.Instance.AddTiXiProcese(values, value1 >= 70);
       

        //AndroidAdsDialog.Instance.UploadDataEvent("finish_zengjiajindu");
    }
        public void ShenJIlouCeng(string value)//激励视频回调-20
    {
      
        print("ShenJIlouCeng+++" + value);
        StartCoroutine(Delay(0.8f, () =>
        {
            (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).PlayVideoShenJiEvent();
        }));
        // (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).PlayVideoShenJiEvent();
    }
    public void ShenJiZhuBo(string value)//激励视频回调为主播index
    {
        print("ShenJiZhuBo+++" + value);
       // AndroidAdsDialog.Instance.UploadDataEvent("finish_actor_levelup");
        int index = int.Parse(value);
        StartCoroutine(Delay(0.8f, () =>
        {
            if (shenJiCallBack != null)
            {
                shenJiCallBack(index,false);
            }
        }));
        //if (shenJiCallBack != null)
        //{
        //    shenJiCallBack(index);
        //}
    }
    //public void OnGUI()
    //{
    //    if (GUILayout.Button("Ceshi"))
    //    {
    //        PlayerData.Instance.GetGold(100000);
    //    }
    //    if (GUILayout.Button("Ceshi1"))
    //    {
    //        PlayerData.Instance.ShengJiCount = 21;
    //    }
    //}
    public void GetDaiMond(string value)//激励视频回调为主播-40
    {
        print("GetDaiMond+++" + value);
        //AndroidAdsDialog.Instance.UploadDataEvent("finish_double_zuanshi");
        StartCoroutine(Delay(0.8f, () =>
        {
            DaimondManager.Instance.ShowGetUI(true);
        }
        )); 
    }
    public void LoginWeChat(string value)
    {
        if (!string.IsNullOrEmpty(value))
            StartCoroutine(Global.UnityWebRequestGetSprite(wangDianSprite, value));
    }
    public void GetDay(string value)
    {
        print("javacallunity++"+value);
        int day = int.Parse(value);
     
        if (PlayerData.Instance.day != day)
        {
            print("javacallunity++重置次数" + value);
            PlayerData.Instance.ChouJiangCount +=4;
            //if (PlayerDate.Instance.day < day)
            //{
            //    var shop = ConfigManager.Instance.GetCurrentShop_Mission(day);
            //    if (shop != null && shop.Count != 0)
            //    {
            //        PlayerDate.Instance.shop_MissinList.InsertRange(0, shop);
            //        ShopTaskManager.Instance.CreactTask(shop);
            //    }
            //}
            PlayerData.Instance.day = day;
            PlayerData.Instance.SetSevenLoginDate();
            SevenLoginPanel.Instance.RefreshStatus();
        }
        PlayerData.Instance.day = day;
        UnityActionManager.Instance.DispatchEvent("RefreshDay");
        //MyShopPanel.Instance.SetDays();


        //PlayerDate.Instance.burseConfig = ConfigManager.Instance.GetBurse(day);
        //BurseManager.Instance.RefreshLevel();
    }
    public UnityAction<int ,bool> shenJiCallBack;
 
    /// <summary>
    /// 激励视频回调成功次数。因为每个都会执行delay延时
    /// </summary>
    public UnityAction showCountAction;
    IEnumerator Delay(float time,UnityAction unityAction)
    {
        if (showCountAction != null)
        {
            showCountAction();
        }
        yield return new WaitForSeconds(time); ;
        // yield return new WaitForSeconds(0.8f);
        print("延时0.8f执行激励视频回调");
        unityAction?.Invoke();
        //yield return time;
    }
  
   public void SetLogin(string value)
    {
        Debug.Log("SetLogin" + value);
        GetKey(value);
    }
    public Sprite wangDianSprite;
    public Sprite GetWangDianSpriteAndName(out string Name)
    {
        Name = wangDianName;
        if (Global.sprite1 != null)
            return Global.sprite1;
        return null;
    }
    public void GetKey(string key)
    {
        isLogined = true;
        Debug.Log("GetKey" + key);
        chatInfo = JsonMapper.ToObject<WeChatInfo>(key);
        wangDianName = chatInfo.nick;
        url = chatInfo.headUrl;
        if (!string.IsNullOrEmpty(url))
            StartCoroutine(Global.UnityWebRequestGetSprite(wangDianSprite, url));
        //StartCoroutine(SetWeChat());
        //_key = key;
        Debug.Log("GetKey" + url);
        Debug.Log("GetKey" + wangDianName);
    }
    public string wangDianName;
    public bool isLogined = false;
    public string url;
    public WeChatInfo chatInfo;
    public void GetTaskList(string value)
    {
        print("通知unity更换任务列表");
     var obj=   JsonMapper.ToObject<List<Mission_redpacket>>(value);
        //PlayerDate.Instance.mission_RedpacketsList = new List<Mission_redpacket>(7);
        for (int i = 0; i < PlayerData.Instance.mission_RedpacketsList.Count; i++)
        {
            PlayerData.Instance.mission_RedpacketsList[i] = obj[i];
        }
       // PlayerDate.Instance.mission_RedpacketsList=JsonMapper.ToObject<List<Mission_redpacket>>(value);
        foreach (var item in PlayerData.Instance.mission_RedpacketsList)
        {
            print("通知unity更换任务列表" + item.tid);
            print("通知unity更换任务列表" + item.status);
        }
      
        TaskManager.Instance.RefreshTaskObject(PlayerData.Instance.mission_RedpacketsList);

    }
    bool isFirst = true;
    public void RefreshRed(string gold)
    {
      int count=  int.Parse(gold);
        Debug.Log("红包值:" + PlayerData.Instance.red);
        Debug.Log("安卓红包值:" + count);
        if (isFirst)
        {
            PlayerData.Instance.red = count;
            MoneyManager.Instance.SetRed();
            isFirst = false;
        }
        else
        {
            if (PlayerData.Instance.red > count)
            {
                Debug.Log("扣除时红包值:" + PlayerData.Instance.red);
                Debug.Log("扣除时安卓红包值:" + count);
                PlayerData.Instance.red = count;
                MoneyManager.Instance.SetRed();
            }
        }
        if (!IsFirstGetRed)
        {
            HttpService.Instance.UploadEventRequest("frist_request_score", "第一次查询积分");
            IsFirstGetRed = true;
            PlayerPrefs.SetInt("Red_IsFirstGetRed", 1);
            Debug.Log("第一次查询积分");
        
        }
       
    }
    bool IsFirstGetRed = false;
    public void RefreshAdward(string gold)
    {
        int count = int.Parse(gold);
        PlayerData.Instance.moneyReward = count;
        UnityActionManager.Instance.DispatchEvent("award");
      
    }
    public void RefreshLevel(string tag)
    {
        //int count = int.Parse(tag);
       // PlayerDate.Instance.storeData.level= count;
     // ShopTaskManager.Instance.ShenJi();
        //(UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).SetLevelText();
        //WeChatLogin.Instance.SetLevelText();
    }
    
}
public class Free
{
    public string item_id;

    public int count;
}
[Serializable]
public class TiXianData
{
    //public int id;
    //public double count;
    ///// <summary>
    ///// 0未提现 1已提现
    ///// </summary>
    //public int states;
   public int amount; //单位：分
    public int type;
    public string desc;
    public string remarks;
    public int cid;
    public int count;  //剩余次数
    public int tag;
    public int mid;    //提现的id
    public int tid;
    public int day;
    public int needProgress;   //需要网店的等级
    public int lottery;        //需要抽奖次数
    

}
