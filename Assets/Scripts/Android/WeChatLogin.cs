using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeChatLogin : MonoSingleton<WeChatLogin>
{
    public Image touxiangBack;
    public Image touxiang;
    public Text wangdainLevel;
    public Button button;
  public GameObject  wangdianlevelGo;
    public GameObject dengluGo;
    public Sprite[] sprites;
    public void SetLevelText()
    {
        wangdainLevel.text = string.Format("{0}级", PlayerData.Instance.storeData.level);

    }

    public void Login()
    {
        print("微信登录");
        AndroidAdsDialog.Instance.UploadDataEvent("click_zhibo_touxiang");
        if (!islogined)
        { AndroidAdsDialog.Instance.RequestBindWechat();
            //JavaCallUnity.Instance.LoginWeChat("https://pic1.zhimg.com/v2-d58ce10bf4e01f5086c604a9cfed29f3_r.jpg?source=1940ef5c");
        }
        else
        {
            ShopTaskManager.Instance.ShowUI(true);

        }
     
    }
    private void Start()
    {
        //PlayerDate.Instance.storeData.level = AndroidAdsDialog.Instance.GetRedLevel();
        SetLoginStatus(false);
        button.onClick.AddListener(Login);
        PlayerData.Instance.addStoreLevelAction += SetLevelText;
        SetLevelText();
       
        AndroidAdsDialog.Instance.NotiyUnityWXInfo();
       // JavaCallUnity.Instance.LoginWeChat("https://pic1.zhimg.com/v2-d58ce10bf4e01f5086c604a9cfed29f3_r.jpg?source=1940ef5c");
    }
    private IEnumerator SetWeChat()
    {
        yield return new WaitForSeconds(0.1f);
        if (isLogined)
        {
            print("有登录过");
            if (!string.IsNullOrEmpty(chatInfo.openId))
            {
                print("有登录过+" + chatInfo.headUrl);
                SetWeChatImage(chatInfo.headUrl);

            }
        }
    }
    public void SetWeChatImage(string url)
    {
        SetLoginStatus(true);
        //touxiangBack.sprite = sprites[0];
        if (!string.IsNullOrEmpty(url))
        StartCoroutine(Global. UnityWebRequestGetData(touxiang, url));

    }
    bool islogined = false;
    private void SetLoginStatus(bool value)
    {
        islogined = value;
        touxiang.gameObject.SetActive(value);
        wangdianlevelGo.SetActive(value);
        dengluGo.SetActive(!value);
      //  button.interactable = !value;
        if(value)touxiangBack.sprite = sprites[0];
        else touxiangBack.sprite = sprites[1];
    }
    public void GetKey(string key)
    {
        isLogined = true;
        url = key;
        chatInfo = JsonMapper.ToObject<WeChatInfo>(url);
      StartCoroutine(  SetWeChat());
        //_key = key;
        Debug.Log("GetKey" + key);
        Debug.Log("GetKey" + isLogined);
    }
    public bool isLogined = false;
    public string url;
    public WeChatInfo chatInfo;
}
