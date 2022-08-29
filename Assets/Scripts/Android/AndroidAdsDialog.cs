using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidAdsDialog : MonoSingleton<AndroidAdsDialog>
{
  
    AndroidJavaObject topOnJob;
    AndroidJavaClass topOnClass;
   
    AndroidJavaClass httpHelperClass;
    AndroidJavaClass DialogClass;
    AndroidJavaClass jc;
    AndroidJavaObject activity;
    AndroidJavaClass DataHelper;
    public bool isBroadcast = false;

   



    //升级红包
    public const string TAG_SHENGJIRED = "-11";
    //金币钻石红包
    public const string TAG_GOLD = "-50";
    //商品红包
    public const string TAG_PRODUCE = "-10";
    //升级主播
    public const string TAG_SHENGJIZHUBO = "5";

    //增加主播
    public const string TAG_ADDZHUBO = "-20";

    public const string TAG_ADDZHUBO_WANGDIAN = "-30";
    //抽奖
    public const string TAG_ChouJiang = "-100";
    /// <summary>
    /// 获取双倍钻石
    /// </summary>
    public const string TAG_GetDaiMond = "-40";
    //获取金币宝箱
    public const string TAG_GetGoldBoxAdward = "-60";
    //获取钻石宝箱
    public const string TAG_GetDaimondBoxAdward = "-70";
    //获取网店经验
    public const string TAG_GetWANGDIANEXP = "-80";
    public const string TAG_KUAIDI = "-110";
    public const string TAG_ADDVALUE = "-111";
    private void Awake()
    {
                //Debug.unityLogger.logEnabled = false;
#if UNITY_EDITOR



#elif UNITY_ANDROID
  
   
          //Debug.unityLogger.logEnabled =false;
      
        
#endif
    }
    public void GetInstance()
    {

    }
    public override void Init()
    {
        //canvas = FindObjectOfType<Canvas>();
        //scale = canvas.scaleFactor;
        // print("scale++++++++++" + scale);
#if UNITY_EDITOR



#elif UNITY_ANDROID
  isBroadcast = true;
   
          //Debug.unityLogger.logEnabled =false;
      
        
#endif
      
        if (isBroadcast)
        {
            topOnClass = new AndroidJavaClass("com.unity3d.player.TopOnHelper");
            topOnJob = topOnClass.CallStatic<AndroidJavaObject>("getInstance");
            jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            httpHelperClass = new AndroidJavaClass("com.unity3d.player.HttpHelper");
            DialogClass = new AndroidJavaClass("com.unity3d.player.DialogHelper");
            DataHelper = new AndroidJavaClass("com.unity3d.player.DataHelper");
            IsFirstAdd = PlayerPrefs.HasKey("AndroidAdsDialog" + "IsFirstAdd");
            Debug.Log("IsFirstAdd:" + IsFirstAdd);
        }
    }
    Canvas canvas;
    /// <summary>
    /// 第五次开心收下得调用
    /// </summary>
    public void showReward88Dialog()
    {
        Debug.LogError("showReward88Dialog");
        if (isBroadcast)
        {
            DialogClass.CallStatic("showReward88Dialog");
        }
    }
// public   float scale;
    /// <summary>
    /// 获取任务列表
    /// </summary>
    public void RequestTaskList()
    {
        if (isBroadcast)
        { httpHelperClass.CallStatic("requestTaskList"); }
    }
    public void requestCalendarPermission()
    {
        Debug.Log("requestCalendarPermission");
        if (isBroadcast)
        {
            DialogClass.CallStatic("requestCalendarPermission");
        }
    }
    /// <summary>
    /// 获取上次钻石插屏的ecpm
    /// </summary>
    /// <returns></returns>
    public int LAST_TABLE_VIDEO_ECPM()
    {
        print("LAST_TABLE_VIDEO_ECPM");
        if (isBroadcast)
        {
            double value = DataHelper.GetStatic<double>("LAST_TABLE_VIDEO_ECPM");
            print("LAST_TABLE_VIDEO_ECPM++" + value);
            return (int)value;
        }
        return 0;
    }
    public string GetVERSION_NAME()
    {
        print("VERSION_NAME++");
        if (isBroadcast)
        {
         string value= DataHelper.GetStatic<string>("VERSION_NAME");
            print("VERSION_NAME++" + value);
            return value;
        }
        return null;
    }
 public void unlockProduct(string value)
    {
       
        if (isBroadcast)
        {
             DataHelper.CallStatic("unlockProduct",value);
           
           
        }
        else
        {
           
        }
    }
    public void ShowDiamondDialog()
    {
        print("unity打开ShowDiamondDialog++count++");
        if (isBroadcast)
        {
            DialogClass.CallStatic("showDiamondDialog");
        }
    }
    public void RequestAddDiamondClick()
    {
        print("unity++RequestAddDiamondClick");
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("requestAddDiamondClick");
        }
    }
    public void ShowSignDialog()
    {
        AudioManager.Instance.PlaySound("android_tc");
        print("unity打开ShowSignDialog");

        if (isBroadcast)
        {
            if(PlayerData.Instance.IsWangDianUI)
            DialogClass.CallStatic("showSignDialog");
                }

    }
    /// <summary>
    ///
    /// </summary>
    public void ReqeustNew()
    {
        print("请求是否是新用户");
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("reqeustNew");
        }
    }

    public void RequestLastUnWithDraw()
    {
        print("请求获取最近提现额度");
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("requestLastUnWithDraw");
        }
       
    }
    /// <summary>
    /// 查询抽奖次数
    /// </summary>
    public void RequestQueryDrawCount()
    {
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("requestQueryDrawCount");
        }
    }
    /// <summary>
    /// 上报抽奖次数
    /// </summary>
    public void RequestPostDrawCount()
    {
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("requestPostDrawCount");
        }
    }

    public void unityInit()
    {
        if (isBroadcast)
        {
            DataHelper.CallStatic("unityInit");
        }
    }
    public void GetUserInfo()
    {
        Debug.Log("GetUserInfo");
        if (isBroadcast)
        {
            StartCoroutine(SetData());
        }
    }
    private IEnumerator SetData()
    {
        yield return new WaitUntil(Fun);
       Debug.Log("GetUserInfo:" + data);
        HttpService.Instance.SetUserData(LitJson.JsonMapper.ToObject<bodydata>(DataHelper.GetStatic<string>("USER_DATA")));
    }
    string data;
    private bool Fun()
    {
        data = DataHelper.GetStatic<string>("USER_DATA");
        Debug.Log("设置uid"+data);
        return (string.IsNullOrEmpty(data)==false);
    }
    /// <summary>
    /// 领取抽奖奖励接口
    /// </summary>
    public void RequestDrawWithDraw()
    {
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("requestDrawWithDraw");
        }
    }
    /// <summary>
    /// 获取红包卷接口
    /// </summary>
    public void RequestAddScoreHBQ()
    {
        if (isBroadcast)
        { httpHelperClass.CallStatic("requestAddScoreHBQ"); }
    }
    /// <summary>
    /// 1:发货红包次数    2：抽奖次数  3：主播个数   4：升级主播次数    5：钻石次数 6：推销时刻  7：获取暴力商品次数
    /// </summary>
    /// <param name="type"></param>
    public void AddSignDataCount(int type)
    {
       
        if (isBroadcast)
        {
            if (PlayerData.Instance.IsWangDianUI)
            {
                print("unity++AddSignDataCount" + "type" + type);
               // DataHelper.CallStatic("addSignDataCount", type);
            }
            else
            {
                print("unity++AddSignDataCount false" + "type" + type);
            }
        }
    }
    /// <summary>
    /// 上报钻石点击次数
    /// </summary>
    public void addDiamondClick()
    {
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("addDiamondClick");
        }
    }
    /// <summary>
    /// 发货红包统计次数
    /// </summary>
    public void addRedPacketCount()
    {
        if (isBroadcast)
        {
            DataHelper.CallStatic("addRedPacketCount");
        }
    }
    /// <summary>
    /// 打开网店达人弹窗
    /// </summary>
    public void ShowMemberDialog()
    {
        AudioManager.Instance.PlaySound("android_tc");

       print("unity++ShowMemberDialog" );
        if (isBroadcast)
        {
            DialogClass.CallStatic("showMemberDialog");
                }
    }
    public int GetRedLevel()
    {
        if (isBroadcast)
        {
            int level = httpHelperClass.GetStatic<int>("currentLevle");


            return level; }
        return PlayerData.Instance.storeData.level;
    }
    public void setIsDiamondClickLocked(bool value)
    {
        if (isBroadcast)
        {
            DataHelper.CallStatic("setIsDiamondClickLocked",value);
        }
    }
    public int GetFirstECPM()
    {
        if (isBroadcast)
        {
            return (int)(httpHelperClass.GetStatic<double>("FIRST_REWARD_ECPM"));
        }
        else
        {
            return 50;
        }
    }
    /// <summary>
    ///获取点击几次播放插屏
    /// </summary>
    public int GetPER_DIAMOND_CLICK_COUNT_SHOW_TABLE()
    {
        if (isBroadcast)
        {
            int value = httpHelperClass.GetStatic<int>("PER_DIAMOND_CLICK_COUNT_SHOW_TABLE");
            print("PER_DIAMOND_CLICK_COUNT_SHOW_TABLE+++"+value);
            return value;
        }
        else
        {
            return 3;
        }
    }
    /// <summary>
    /// 点击钻石弹出面板
    /// 
    /// </summary>
    public void showDiamondTipDialog()
    {
        print("unity打开showDiamondTipDialog");
        if (isBroadcast)
        {
            DialogClass.CallStatic("showDiamondTipDialog");
        }
    }
    /// <summary>
    /// 获取任务奖励
    /// </summary>
    /// <param name="tid"></param>
    /// <param name="mid"></param>
    public void RequestTaskReward(int tid)
    {
        if (isBroadcast)
        { httpHelperClass.CallStatic("requestTaskReward",tid);
            print("任务提现++++" + tid);
        }
    }
    public void FinishTutorial()
    {
        if (isBroadcast)
        { httpHelperClass.CallStatic("finishTutorial"); }
    }
    /// <summary>
    /// 上传货币
    /// </summary>
    /// <param name="count">数量</param>
    /// <param name="isSJ">是否为赏金，不是则是红包卷</param>
    public void RequestAddScore(int count,bool isSJ)
    {
        if (isBroadcast)

        {if(isSJ)
            httpHelperClass.CallStatic("requestAddScoreSJ", count);
            else
            {
                httpHelperClass.CallStatic("requestAddScoreHBQ", count);
                if (!IsFirstAdd)
                {
                    HttpService.Instance.UploadEventRequest("frist_add_score", "第一次添加积分");
                    Debug.Log("第一次添加积分");
                    IsFirstAdd = true;
                    PlayerPrefs.SetInt("AndroidAdsDialog" + "IsFirstAdd",1);
                   
                }
            }
        }
        

    }
    bool IsFirstAdd = false;
    /// <summary>
    /// 获取金币，校验
    /// </summary>
    public void RequestQueryScore()
    {
        if (isBroadcast)
        { httpHelperClass.CallStatic("requestQueryScore"); }
    }

    /// <summary>
    /// 升级红包接口提现列表
    /// </summary>
    public void requestSJHBWithDrawList()
    {
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("requestSJHBWithDrawList");
        }
    }
    public void requestWithDraw(int mid, int cid, double money)
    {
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("requestWithDraw",mid,cid,money);
        }
        //DialogHelper.checkWXLogin(mid, cid, money);
    }


    /// <summary>
    /// 获取高低拆快递数据
    /// </summary>
    /// 
    public UnityEngine.Events.UnityAction<bool> requestCKDDataAction;
    public void requestCKDData(UnityEngine.Events.UnityAction<bool> Action)
    {
        requestCKDDataAction = Action;
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("requestCKDData");
        }
        else
        {
            JavaCallUnity.Instance.requestCKDData("1");
        }
    }
    public UnityEngine.Events.UnityAction requestGetCKDWithDrawAction;
    /// <summary>
    /// 提现拆快递奖励
    /// </summary>
    public void requestGetCKDWithDraw(UnityEngine.Events.UnityAction Action)
    {
        requestGetCKDWithDrawAction = Action;
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("requestGetCKDWithDraw");
        }
        else
        {
            print("提现成功");
        }
    }
        /// <summary>
        /// 每日登录
        /// </summary>
    public void RequestActiveDay()
    {
        if (isBroadcast)
            httpHelperClass.CallStatic("requestActiveDay");
    }
    /// <summary>
    /// 提现列表
    /// </summary>
     public void RequestWithDrawList()
    {
        if (isBroadcast)
            httpHelperClass.CallStatic("requestWithDrawList");
    }
    /// <summary>
    /// 打开赏金面板和红包卷面板
    /// </summary>
    /// <param name="Sj"></param>
    public void OpenTiXianUI(bool Sj=true,int progress1=0,int progress2=0,int pro3=0,int level=0,bool isFirst=false)
    {
        //if (isFirst)
        //{
        //    Debug.LogError("进入新手");
        //}
        //else
        //{
        //    Debug.LogError("进入提现");
        //}
        AudioManager.Instance.PlaySound("android_tc");
        if (isBroadcast)
        {if(Sj)
            DialogClass.CallStatic("showWithDrawDialogSJ");
            else
            {
                DialogClass.CallStatic("showWithDrawDialog",progress1,progress2,pro3,level, isFirst);
            }
        }
    }
    public void OpenJiaTiXian()
    {
        AudioManager.Instance.PlaySound("android_tc");
        if (isBroadcast)
        {
            DialogClass.CallStatic("showWithDrawDialog300");
        }

    }
    public void OpenJiangLi(long money)
    {
        AudioManager.Instance.PlaySound("android_tc");
        if (isBroadcast)
        {
           DialogClass.CallStatic("showBigRedPacketDialog",money);
        }
    }
    public void RefreshJiangLi(long money)
    {
        if (isBroadcast)
        {
            DialogClass.CallStatic("refreshBigRedPacketDialog", money);
        }
    }

    public void RequestNextLevel(int level)
    {
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("requestNextLevel", level);
        }
    }
    /// <summary>
    /// 提现
    /// </summary>
    public void RequestWithDraw(int mid)
    {
        if (isBroadcast)
            httpHelperClass.CallStatic("requestWithDraw", mid);
    }
    public void RequestCheckSignReward()
    {
        if (isBroadcast)
            httpHelperClass.CallStatic("requestCheckSignReward");
    }
    /// <summary>
    /// 绑定微信
    /// </summary>
    public void RequestBindWechat()
    {
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("requestBindWechat");
        }
    }
    //通知unity微信头像有更新
         public void NotiyUnityWXInfo()
    {
        if (isBroadcast)
        {
            httpHelperClass.CallStatic("notiyUnityWXInfo");
        }
    }

    /// <summary>
    /// 友盟埋点
    /// </summary>
    /// <param name="eventName"></param>
    public void UploadDataEvent(string eventName)
    {
        print(eventName);
        if (isBroadcast)
        {
            topOnJob.Call("uploadDataEvent", eventName);
           // print(eventName);
        }
    }
    
    public void PlayAudio(string text)
    {
        //if (isBroadcast)
        //{
        //    topOnJob.Call("playAudio", text);
        //}
      //  AudioManager.Instance.PlaySound(text);
    }
    public void CacheAudio(string text)
    {
        //if (isBroadcast)
        //{
        //    topOnJob.Call("cacheAudio", text);
        //}
    }
 public void SetBannerAdHeight(int height)
    {
        topOnJob.Call("setBannerAdHeight",  height); 
    }
    /// <summary>
    /// banner 广告
    /// </summary>
    public void ShowBannerAd()
    {
        print("Unity+++++打开banner");
        if (isBroadcast)
            topOnJob.Call("doBannerContainer", true);
    }
    public void CloseBanner()
    {
        print("Unity+++++关闭banner");
        if (isBroadcast)
            topOnJob.Call("doBannerContainer", false);

    }
    /// <summary>
    /// 信息流广告
    /// </summary>
    public void ShowFeedAd(int top)
    {
        if (isBroadcast)
            topOnJob.Call("showFeedAd", 1, (int)(FindObjectOfType<Canvas>().scaleFactor *600));
    }
    public void CloseFeedAd()
    {
        if (isBroadcast)
            topOnJob.Call("doFeedContainer", false);
        print("关闭信息流");
    }
    /// <summary>
    /// 插屏
    /// </summary>
    public void ShowTableVideo(string tag)
    {
        print("执行播放插屏广告+Unity");
        if (isBroadcast)
        {
            topOnJob.Call("showTableVideo", activity, tag);
            print("执行播放插屏广告+Unity");
        }
        else
        {


            JavaCallUnity.Instance.SetFirstTableECPM("10");


        }
    }
    public void CloseTableVideo()
    {

    }
    /// <summary>
    /// 全屏广告
    /// </summary>
    public void ShowFullVideo(string tag)
    {
        print("执行播放全屏广告+Unity");
        if (isBroadcast)
            topOnJob.Call("showFullVideo", activity, tag);
    }
    public void CloseFullVideo()
    {

    }
    /// <summary>
    /// 激励
    /// </summary>
    public void ShowRewardVideo(string tag)
    {
        print("播放激励视频");
        if (isBroadcast)
            topOnJob.Call("showRewardVideo", activity, tag);
    }
    public void ShowRewardVideo(string tag,UnityEngine.Events.UnityAction unityAction)
    {
        print("播放激励视频");
        RewardAction = unityAction;
       // if (tag == TAG_SHENGJIRED && JavaCallUnity.Instance.IsFirstGetRedValue()) return;
        if (isBroadcast)
        {
           
            topOnJob.Call("showRewardVideo", activity, tag);
        }
        else
        {
            if (tag != TAG_SHENGJIRED)
            { unityAction?.Invoke();
                RewardAction = null;
                JavaCallUnity.Instance.SetCurrentEcpm("50");
            }
        }
        
    }

    public UnityEngine.Events.UnityAction RewardAction;
    public void CloseRewardVideo()
    {
       
    }
    /// <summary>
    /// 开屏
    /// </summary>
    public void ShowSplash()
    {
        print("播放全屏视频");
        if (isBroadcast)
            topOnJob.Call("showSplash", activity,  "b6135ced638f6f");
    }
    public void CloseSplash()
    {

    }
    /// <summary>
    /// 激励视频播放完回调
    /// </summary>
    /// <param name="tag"></param>
    public void ReceiveCallBack(string tag)
    {
        //  Red.OpenAward(tag);
        //LevelManager.Instance.LoadScene();
    }
   
    public void OnGameStart(int level)
    {
        if (isBroadcast)
        {
            topOnJob.Call("onGameStart", level);
        }
    }
    public void OnGameEnd(int status,int level)
    {
        if (isBroadcast)
        {
            topOnJob.Call("onGameEnd",status, level);
        }
    }
    public void ShowToasts(string value, Sprite sprite,Color color , UnityEngine.Events.UnityAction unityAction = null)
    {

        //var effect = GameObjectPool.Instance.CreateObject("TipsShow", ResourceManager.Instance.GetProGo("TipsShow"), ToggleManager.Instance.effectBorn, Quaternion.identity);
        //effect.GetComponent<TipsEffect>().Show(ToggleManager.Instance.effectTarget, value, sprite, unityAction, color);
        TipsShowBase.Instance.Show("TipsShow4", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
     {
              value
         }, new Sprite[]
         {
                   sprite
         }, new Color[] { color }, unityAction);
    }
    public void ShowToasts(Transform bornTf, Transform targetTf,  string[] value, Sprite[] sprite,  Color[] color, UnityEngine.Events.UnityAction unityAction = null,float scale=1f)
    {
       
        var effect = GameObjectPool.Instance.CreateObject("TipsShow2", ResourceManager.Instance.GetProGo("TipsShow2"), bornTf, Quaternion.identity);
        effect.GetComponent<TipsEffectBase>().Show(targetTf, value, sprite, unityAction, color, scale);
    }
    public void ShowToasts(Transform bornTf, Transform targetTf, string value,  Color color, UnityEngine.Events.UnityAction unityAction = null, Sprite sprite = null, float scale = 1f)
    {
        
        var effect = GameObjectPool.Instance.CreateObject("TipsShow2", ResourceManager.Instance.GetProGo("TipsShow2"), bornTf, Quaternion.identity);
        effect.GetComponent<TipsEffectBase>().Show(targetTf,new string[] { value },new Sprite[] { sprite },unityAction, new Color[] { color }, scale);
    }
    public void ShowRed(string count, Produce produce, string count1, string count2, string count3, Transform effectBorn, Transform effectTarget)
    {
     var   effectGo = GameObjectPool.Instance.CreateObject("GetProduceAndRed", ResourceManager.Instance.GetProGo("GetProduceAndRed"), effectBorn, Quaternion.identity);
        effectGo.GetComponent<RedAndProduceAdward>().Show(effectTarget, count, ResourceManager.Instance.GetSprite(produce.item_pic), count1, count2, count3);

    }
    public void SetAnchorLevel(int value)
    {
        if (isBroadcast)
        {
            DialogClass.CallStatic("SetAnchorLevel", value);
        }
    }
}



