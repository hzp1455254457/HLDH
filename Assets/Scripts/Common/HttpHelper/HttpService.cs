using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif
using UnityEngine.Networking;
using LitJson;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Net;
using System.IO;

public class HttpService : MonoBehaviour
{
    public static bool isTesting = false;
    public static string serverUrl = isTesting ? "http://121.201.120.153:38090" : "https://diamond.inveno.com";

    #region 重要数据
    private string suuid = "";
    private string jwt = "-1";
    private string uid = "-1";
#if UNITY_IOS
    public static string package_name = "com.ywns.hldhs";
#elif UNITY_ANDROID
    public static string package_name = "com.ywns.hldh";
#endif

    private static string tm = "";
    //openid，若未绑定微信，则为空
    private string openID = "";
    private string nickName = "";
    private string headimgurl = "";

    private static int versionCode = 2004;
    private static string channel = "st01";

    private static string idfv = "";
    #endregion

    #region 微信数据
    private static string code = "001syP100TMLSN1RmX100daP422syP1V";
    private static string access_token = "";
    private static string AppID = "wxae6fd001136a784e";
    private static string AppSecret = "b73d7c8b0e1e951253cd41559396a9be";

    #endregion
    private Dictionary<string, List<Delegate>> actionDictionary = new Dictionary<string, List<Delegate>>();

    public static HttpService Instance;
    // Start is called before the first frame update

    private void Awake()
    {
       Instance = this;
        //#if UNITY_EDITOR
        //        suuid = "DoNewscde33b84-6bde-4fed-91be-da8be5e38771";
        //#elif UNITY_ANDROID
        //        AndroidJavaClass duoniuClass = new AndroidJavaClass("com.donews.dnsuuid_lib.DnObtainSuuidUtils");
        //        AndroidJavaClass mainClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //        var activity = mainClass.GetStatic<AndroidJavaObject>("currentActivity");
        //        var applicationContext = activity.Call<AndroidJavaObject>("getApplicationContext");
        //        AndroidJavaObject duoniuObject = duoniuClass.CallStatic<AndroidJavaObject>("getInstance");
        //        duoniuObject.Call("init", applicationContext);
        //        string suuid = duoniuObject.Call<string>("obtainSuuid", applicationContext);
        //        Debug.Log("suuid:"+suuid);
        //#elif UNITY_IOS
        //        if (Device.advertisingTrackingEnabled)
        //            idfv = Device.advertisingIdentifier;
        //        suuid = Device.vendorIdentifier;//"00008020-000C45682E8A002E"
        //#endif
        //        isFirstRequestScore = PlayerPrefs.HasKey("isFirstRequestScore") ? bool.Parse(PlayerPrefs.GetString("isFirstRequestScore")) : true;
        //        isFirstAddScore = PlayerPrefs.HasKey("isFirstAddScore") ? bool.Parse(PlayerPrefs.GetString("isFirstAddScore")) : true;
        AndroidAdsDialog.Instance.GetUserInfo();
    }
    private void Start()
    {
      
    }
    public void SetUserData(bodydata body)
    {
        Debug.Log("拿到body"+body);
        uid = body.uid;
        print("uid:"+uid);
        suuid = body.suuid;
        print("suuid:" + suuid);
        jwt = body.jwt;
        print(" jwt :" + jwt);
        package_name = body.package_name;
        print("package_name:" + package_name);
        versionCode = body.versionCode;
        print("versionCode:" + versionCode);

    }
    public void AddListener<T>(string type, Action<T> action)
    {
        List<Delegate> actions = null;
        if (actionDictionary.TryGetValue(type, out actions))
        {
            actions.Add(action);
        }
        else
        {
            actions = new List<Delegate>();
            actions.Add(action);
            actionDictionary.Add(type, actions);
        }
    }

    public void AddListener(string type, Action action)
    {
        List<Delegate> actions = null;

        if (actionDictionary.TryGetValue(type, out actions))
        {
            actions.Add(action);
        }
        else
        {
            actions = new List<Delegate>();
            actions.Add(action);
            actionDictionary.Add(type, actions);
        }
    }

    public void RemoveListener<T>(string type, Action<T> action)
    {
        List<Delegate> actions = null;
        if (actionDictionary.TryGetValue(type, out actions))
        {
            actions.Remove(action);
            if (actions.Count == 0)
            {
                actionDictionary.Remove(type);
            }
        }
    }

    public void RemoveAllListener(string type)
    {
        if (actionDictionary.ContainsKey(type))
        {
            actionDictionary.Remove(type);
        }
    }

    /// <summary>
    /// 分发事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <param name="t"></param>
    public void DisPathEvent<T>(string type, T arg)
    {
        List<Delegate> actions = null;

        if (actionDictionary.ContainsKey(type))
        {
            if (actionDictionary.TryGetValue(type, out actions))
            {
                foreach (Delegate one in actions)
                {
                    one.DynamicInvoke(arg);
                }
            }
        }
    }
    public void DisPathEvent(string type)
    {
        List<Delegate> actions = null;

        if (actionDictionary.ContainsKey(type))
        {
            if (actionDictionary.TryGetValue(type, out actions))
            {
                foreach (Delegate one in actions)
                {
                    one.DynamicInvoke();
                }
            }
        }
    }

    public enum requestType
    {
        POST = 0,
        GET = 1
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type">请求链接，也作为callback的关键字</param>
    /// <param name="isAbsolutePath">是否是绝对路径链接</param>
    /// <param name="items"></param>
    /// <param name="requestType">POST还是GET</param>
    private void SendHttpRequest(string type, Dictionary<string, object> items = null, bool isAbsolutePath = false, requestType requestType = requestType.POST)
    {
        string url = !isAbsolutePath ? serverUrl + type : type;
        tm = ((System.DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000).ToString();
        bodydata data = new bodydata();
#if UNITY_EDITOR
        device d = new device("ANDROID", suuid, "", "", "", "", "");
        data.InitBodyData(suuid, jwt, versionCode, channel, uid, "", package_name, tm, "", 1, "", "", d);
#elif UNITY_ANDROID
        device d = new device("ANDROID", suuid, "", "", "", "", "");
        data.InitBodyData(suuid, jwt, versionCode, channel, uid, "", package_name, tm, "", 1, "", "", d);
#elif UNITY_IOS

        device d = new device("IOS", suuid, idfv, "", "", "", "");
        data.InitBodyData(suuid, jwt, versionCode, channel, uid, "", package_name, tm, "", 1, "", "", d);
#endif
        UnityWebRequest request = new UnityWebRequest(url, requestType == requestType.POST ? UnityWebRequest.kHttpVerbPOST : UnityWebRequest.kHttpVerbGET);
        request.timeout = 3;

        string st = JsonMapper.ToJson(data);

        if (items != null)
        {
            string st2 = JsonMapper.ToJson(items);
            //Debug.Log("携带的数据是:" + st2);
            st2 = st2.Replace('{', ',');
            st2 = st2.Remove(st2.Length - 1);
            st = st.Insert(st.Length - 1, st2);
        }
        //Debug.Log("数据body是：" + st.ToString());

        byte[] bytes = Encoding.UTF8.GetBytes(st.ToString());
        UploadHandlerRaw uH = new UploadHandlerRaw(bytes);
        DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
        request.downloadHandler = dH;
        request.uploadHandler = uH;
        addHeader(request, st.ToString());
        StartCoroutine(sendRequest(request, type));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="type">请求的链接</param>
    /// <returns></returns>
    IEnumerator sendRequest(UnityWebRequest request, string type)
    {
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError ||
            request.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.Log(type+" Error is : " + request.error);
            string result = request.downloadHandler.text;
            //Debug.Log("downloadHandler Text : " + request.downloadHandler.text);
            DisPathEvent(type, result);
        }
        else
        {
            string result = request.downloadHandler.text;
            //Debug.Log("downloadHandler Text : " + request.downloadHandler.text);
            DisPathEvent(type, result);
            Debug.Log(type + "achive is : " + result);
        }
    }

    public static string loginURL = "/diamond/v1/user/login";
    /// <summary>
    /// 发送用户请求登录或者注册请求
    /// </summary>
    public void SendHttpLoginRequest()
    {
        SendHttpRequest(loginURL);
    }

    public static string bindURL = "/diamond/v1/user/bind";
    /// <summary>
    /// 微信绑定
    /// </summary>
    /// <param name="type"></param>
    public void SendWeChatBindRequest()
    {
        Dictionary<string, object> items = new Dictionary<string, object>();
        items.Add("openId", openID);
        items.Add("nickName", nickName);
        items.Add("headimgurl", headimgurl);
        SendHttpRequest(bindURL, items);
    }

    public void setAccountInfo(string uid, string jwt, string openID, string nickName, string headimgurl)
    {
        this.uid = uid;
        this.jwt = jwt;
        this.openID = openID;
        this.nickName = nickName;
        this.headimgurl = headimgurl;
    }

    public void setWeChatInfo(string openID, string nickName, string headimgurl)
    {
        this.openID = openID;
        this.nickName = nickName;
        this.headimgurl = headimgurl;
    }

    public static string activeDaysUrl = "/diamond/v1/coin/user/queryscore";
    /// <summary>
    /// 查询活跃天数
    /// </summary>
    public void SendActiveDaysRequest()
    {
        SendHttpRequest(activeDaysUrl);
    }


    public static string scoreRequiryURL = "/diamond/v1/coin/xtask/score/v2/get";
    private bool isFirstRequestScore;
    /// <summary>
    /// 积分查询请求
    /// </summary>
    public void SendScoreRequest()
    {
        if (isFirstRequestScore)
        {
            isFirstRequestScore = !isFirstRequestScore;
            PlayerPrefs.SetString("isFirstRequestScore", isFirstRequestScore.ToString());
            UploadEventRequest(UploadEventType.frist_request_score.ToString(), "第一次请求积分");
        }
        Dictionary<string, object> items = new Dictionary<string, object>();
        items.Add("score_type", "new_redpacket");
        SendHttpRequest(scoreRequiryURL, items);
    }
    public static string adControlURL = "/diamond/v1/obs/event/curtimequery";

    public void SendAdControlRequest()
    {
        //Dictionary<string, object> items = new Dictionary<string, object>();
        //items.Add("score_type", "new_redpacket");
        SendHttpRequest(adControlURL);
    }

    public static string scoreAddURL = "/diamond/v1/coin/xtask/score/v2/increment";
    private bool isFirstAddScore;
    /// <summary>
    /// 积分增加请求
    /// </summary>
    /// <param name="number"></param>
    /// <param name="description"></param>
    public void SendScoreAddRequest(int number, string description = null)
    {
        if (isFirstAddScore)
        {
            isFirstAddScore = !isFirstAddScore;
            PlayerPrefs.SetString("isFirstAddScore", isFirstAddScore.ToString());
            UploadEventRequest(UploadEventType.frist_add_score.ToString(),"第一次增加积分");
        }
        string trade_no = System.DateTime.Now.ToString() + UnityEngine.Random.Range(-1000.0f, 1000.0f).ToString();
        string score_type = "new_redpacket";
        int score_value = number;
        string score_desc = description;

        Dictionary<string, object> items = new Dictionary<string, object>();
        items.Add("trade_no", trade_no);
        items.Add("score_type", score_type);
        items.Add("score_value", score_value);
        items.Add("score_desc", score_desc);
        SendHttpRequest(scoreAddURL, items);
    }

    public static string tixianListUrl = "/diamond/v1/cash/configs";
    /// <summary>
    /// 提现列表接口
    /// </summary>
    public void SendTiXianListRequest()
    {
        SendHttpRequest(tixianListUrl);
    }

    public static string tixianUrl = "/diamond/v1/cash/withdrawals";
    /// <summary>
    /// 提现接口
    /// </summary>
    /// <param name="type"></param>
    public void SendTiXianRequest(int cid)
    {

        string trade_no = System.DateTime.Now.ToString() + UnityEngine.Random.Range(-1000.0f, 1000.0f).ToString();
        string mid = "0";
        int taskCid = cid;
        string openid = openID;

        Dictionary<string, object> items = new Dictionary<string, object>();
        items.Add("trade_no", trade_no);
        items.Add("mid", mid);
        items.Add("cid", taskCid);
        items.Add("openid", openid);
        SendHttpRequest(tixianUrl, items);

    }

    public static string tixianRecordUrl = "/diamond/v1/cash/record";
    /// <summary>
    /// 提现记录接口
    /// </summary>
    public void SendTiXianRecordRequest()
    {
        SendHttpRequest(tixianRecordUrl);
    }

    public static string tixianControlUrl = "https://monetization.tagtic.cn/rule/v1/calculate/key-withdraw-control-prod";
    /// <summary>
    /// 提现控制接口
    /// </summary>
    public void GetTiXianControlRequest()
    {
        string url = tixianControlUrl + "?package_name=" + package_name;
        SendHttpRequest(url, null, true, requestType.GET);
    }

    public static string taskUrl = "https://xtasks.xg.tagtic.cn/xtasks/task/list";
    /// <summary>
    /// 任务获取
    /// </summary>
    /// <param name="type"></param>
    public void GetTaskListRequest()
    {
        string url = taskUrl + "?app_name=" + package_name;
        SendHttpRequest(url, null, true, requestType.GET);
    }

    public static string taskUpdateUrl = "https://xtasks.xg.tagtic.cn/xtasks/task/update";
    /// <summary>
    /// 任务状态更新
    /// </summary>
    /// <param name="taskID">任务ID状态更新</param>
    public void SendTaskUpdateRequest(int taskID)
    {
        int id = taskID;
        int is_append = 0;

        Dictionary<string, object> items = new Dictionary<string, object>();
        items.Add("id", id);
        items.Add("is_append", is_append);
        SendHttpRequest(taskUpdateUrl, items, true);
    }

    public static string taskFinishUrl = "https://xtasks.xg.tagtic.cn/xtasks/score/add";
    /// <summary>
    /// 任务完成
    /// </summary>
    /// <param name="taskID"></param>
    public void SendTaskFinishRequest(int taskID)
    {
        int id = taskID;
        int is_append = 0;

        Dictionary<string, object> items = new Dictionary<string, object>();
        items.Add("id", id);
        items.Add("is_append", is_append);
        SendHttpRequest(taskFinishUrl, items, true);
    }

    /*event_id 说明
    frist_add_score 第一次增加积分
    frist_request_score 第一次查询积分
    user_active 用户激活
    user_keyhavior 关键行为
    app_open 打开app
    user_login 登录成功获取UID
    wx_login_success 微信登录成功
    system_recycle 系统回收了app
    video_play_start 激励视频开始播放
    video_play_fail 激励视频播放失败
    video_play_click 激励视频点击触发
    video_play_end 激励视频播放完成
    insertad_show 插屏展示
    insertad_fail 插屏展示失败
    insertad_click 插屏点击*/

    public static string uploadEventUrl = "/diamond/v1/obs/event/report";

    public enum UploadEventType
    {
        frist_add_score,
        frist_request_score,
        user_active,
        user_keyhavior,
        app_open,
        user_login,
        wx_login_success,
        video_play_start,
        video_play_fail,
        video_play_click,
        video_play_end,
        insertad_show,
        insertad_fail,
        insertad_click
    }
    /// <summary>
    /// 上报事件
    /// </summary>
    /// <param name="event_id">事件ID</param>
    /// <param name="event_desc">事件描述</param>
    public void UploadEventRequest(string event_id, string event_desc)
    {
        Debug.Log("event_id:" + event_id);
        Dictionary<string, object> items = new Dictionary<string, object>();
        items.Add("event_id", event_id);
        items.Add("event_desc", event_desc);
        SendHttpRequest(uploadEventUrl, items);
    }

    public static string uploadRewardVideoEventUrl = "/diamond/v1/coin/user/seead";
    /// <summary>
    /// 激励视频上报事件
    /// </summary>
    /// <param name="ecpm">ecpm值</param>
    /// <param name="event_desc">描述</param>
    public void UploadRewardVideoEventRequest(double ecpm, string event_desc = null)
    {
        string trade_no = System.DateTime.Now.ToString() + UnityEngine.Random.Range(-1000.0f, 1000.0f).ToString();

        Dictionary<string, object> items = new Dictionary<string, object>();
        items.Add("trade_no", trade_no);
        items.Add("ecpm", ecpm);
        items.Add("desc", event_desc);
        SendHttpRequest(uploadRewardVideoEventUrl, items);
    }

    public static string uploadTableVideoEventUrl = "/diamond/v1/coin/user/ad/insert/report";
    /// <summary>
    /// 插屏上报事件
    /// </summary>
    /// <param name="ecpm">ecpm值</param>
    /// <param name="event_type">1是展示3是点击</param>
    /// <param name="event_desc">描述</param>
    public void UploadTableVideoEventRequest(double ecpm, int event_type, string event_desc = null)
    {
        string trade_no = System.DateTime.Now.ToString() + UnityEngine.Random.Range(-1000.0f, 1000.0f).ToString();

        Dictionary<string, object> items = new Dictionary<string, object>();
        items.Add("trade_no", trade_no);
        items.Add("event_type", event_type);
        items.Add("ecpm", ecpm);
        items.Add("desc", event_desc);
        SendHttpRequest(uploadTableVideoEventUrl, items);
    }

    private void addHeader(UnityWebRequest request, string body)
    {
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("uid", uid);
        request.SetRequestHeader("Authorization", jwt);
        request.SetRequestHeader("packagename", package_name);
        request.SetRequestHeader("tm", tm);
        string tk = generateTK(uid, tm, body);
        request.SetRequestHeader("tk", tk);
    }

    private static string deadValve = "093a964e515d2732067cb247ae101494";
    /// <summary>
    /// 生成tk
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="tm"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    string generateTK(string uid, string tm, string body)
    {
        //tk=MD5(固定字符串 + : + uid + : + tm + : + json格式的请求体)
        string t = deadValve + ":" + uid + ":" + tm + ":" + body;
        t = EncryptMD5_32(t);
        Debug.Log("MD5:" + t);
        return t;
    }

    private static string EncryptMD5_32(string _encryptContent)
    {
        string content_Normal = _encryptContent;
        string content_Encrypt = "";
        MD5 md5 = MD5.Create();

        byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(content_Normal));

        for (int i = 0; i < s.Length; i++)
        {
            content_Encrypt = content_Encrypt + s[i].ToString("X2");
        }
        return content_Encrypt;
    }

    /// <summary>
    /// JSON是否成功返回
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool CheckCallBackJson(string result)
    {
        if (result.Contains("code"))
        {
            string resultString = result.Split(',')[0];
            string codeString = resultString.Split(':')[1];
            if (int.Parse(codeString) == 0)
            {
                Debug.Log("成功");
                return true;
            }
            else
            {
                Debug.Log("失败");
                return false;
            }
        }
        else
        {
            Debug.LogError("返回json失败");
            return false;
        }
    }

    #region 微信登录获取
    private static string openidURL = "https://api.weixin.qq.com/sns/oauth2/access_token";
    public void GetWeChatOpenID()
    {
        RemoveAllListener(openidURL);
        AddListener<string>(openidURL, WechatOpenidCallBack);
        string url = openidURL + "?appid=" + AppID + "&" + "secret=" + AppSecret + "&code=" + code + "&grant_type=" + "authorization_code";
        UnityWebRequest request = UnityWebRequest.Get(url);
        DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
        StartCoroutine(sendRequest(request, openidURL));
    }

    void WechatOpenidCallBack(string result)
    {
        Debug.Log("微信openid返回数据：" + result);
        OpenIDData t = JsonMapper.ToObject<OpenIDData>(result);
        if (!string.IsNullOrEmpty(t.openid) && !string.IsNullOrEmpty(t.access_token))
        {
            openID = t.openid;
            access_token = t.access_token;
            GetWeChatUserInfo();
        }
    }
    private class OpenIDData
    {
        public string access_token;
        public int expires_in;
        public string refresh_token;
        public string openid;
        public string scope;
        public string unionid;
    }

    private static string userInfoURL = "https://api.weixin.qq.com/sns/userinfo";
    /// <summary>
    /// 获取微信用户数据
    /// </summary>
    public void GetWeChatUserInfo()
    {
        RemoveAllListener(userInfoURL);
        AddListener<string>(userInfoURL, WechatUserInfoCallBack);
        string url = userInfoURL + "?access_token=" + access_token + "&openid=" + openID;
        UnityWebRequest request = UnityWebRequest.Get(url);
        DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
        StartCoroutine(sendRequest(request, userInfoURL));
    }

    private void WechatUserInfoCallBack(string result)
    {
        Debug.Log("微信用户信息返回数据：" + result);
        UserInfoData t = JsonMapper.ToObject<UserInfoData>(result);
        if (!string.IsNullOrEmpty(t.nickname) && !string.IsNullOrEmpty(t.openid) && !string.IsNullOrEmpty(t.headimgurl))
            setAccountInfo(uid, jwt, t.openid, t.nickname, t.headimgurl);
    }

    private class UserInfoData
    {
        public string openid;
        public string nickname;
        public int sex;
        public string language;
        public string city;
        public string province;
        public string country;
        public string headimgurl;
        public List<string> privilege;
        public string unionid;
    }
    #endregion

    /// <summary>
    /// 获取用户id
    /// </summary>
    /// <returns></returns>
    public string GetUID()
    {
        return uid;
    }

    /// <summary>
	/// 获取微信头像地址
	/// </summary>
	/// <returns></returns>
    public string GetHeadingUrl()
    {
        return headimgurl;
    }

    /// <summary>
	/// 获取微信昵称
	/// </summary>
	/// <returns></returns>
    public string GetNickName()
    {
        return nickName;
    }

    /// <summary>
	/// 获取微信OpenID
	/// </summary>
	/// <returns></returns>
    public string GetOpenID()
    {
        return openID;
    }
}
