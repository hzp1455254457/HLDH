using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isShowDebug;
    void Start()
    {
        //AndroidAdsDialog.Instance.RequestActiveDay();
        //JavaCallUnity.Instance.GetDay("1");
        Application.targetFrameRate = 60;
        StartGame();
    }
    private void Awake()
    {
        //  Debug.unityLogger.logEnabled = isShowDebug;
        
    }
    // Update is called once per frame
    public void StartGame()
    {

        UIManager.Instance.OpenPanel("Panel_ZhiBo");
     var panel=   UIManager.Instance.OpenPanel("FaHuoPanel");
        panel.Hide();
        if (!PlayerPrefs.HasKey(BoxGame.NextTime))
        {
            PlayerPrefs.SetString(BoxGame.NextTime, DateTime.Now.ToString());
        }
 
        // SceneManager.LoadScene("OpenBox", LoadSceneMode.Additive);

        //GoBigWorld();
        if (GuideManager.Instance.isFirstGame)
        {
            CamareManager.Instance.SetStates(false);
            BigWorld.Instance.GoBigWorld();
            if (!GuideManager.Instance.isFirstGame)
                ZhiBoPanel.Instance.daoHangLanManager.SetParent(UIManager.Instance.showRootMain1);
          
        }
        else
        {
            BigWorld.Instance.GoGame();
        }

//#if UNITY_EDITOR
//        return;
//#endif
       // CheckChaiKuaiDi();
    }

    private void GoBigWorld()
    {
        //yield return null;
        //ToggleManager.Instance.HideUI();
        //UIManager.Instance.SetUIStates(false);
        SceneManager.LoadScene("BigWorld", LoadSceneMode.Additive);
    }


    /// <summary>
    ///   拆快递场景载入
    /// </summary>
    void CheckChaiKuaiDi()
    {
        //获取下次领取时间
        string nextTimeStr = PlayerPrefs.GetString(BoxGame.NextTime);

        Debug.Log("检查下次领取时间：" + nextTimeStr);
        //尝试转换
        DateTime nextShowTime;
        bool tf = DateTime.TryParse(nextTimeStr, out nextShowTime);
        //如果没有领取过，也显示拆快递场景
        if (!PlayerPrefs.HasKey(BoxGame.NextTime) || string.IsNullOrEmpty(nextTimeStr))
        {
            //载入拆快递场景
            if (!GuideManager.Instance.isFirstGame)
            {
                SceneManager.LoadScene("OpenBox", LoadSceneMode.Additive);
                UIManager.Instance.SetUIStates(false);
                //CamareManager.Instance.SetStates(false);
            }
        }
        //下次载入时间已到，也显示拆快递场景
        else if (tf && nextShowTime <= DateTime.Now && !GuideManager.Instance.isFirstGame)
        {
            //载入拆快递场景
            UIManager.Instance.SetUIStates(false);
            SceneManager.LoadScene("OpenBox", LoadSceneMode.Additive);
        }
    }
}
