using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGame : MonoBehaviour
{
    #region PlayerPrefs
    /// <summary>
    /// 下次打开箱子的时间
    /// 中级箱子40分钟，高级箱子30分钟
    /// </summary>
    public static string NextTime = "BoxGame_NextTime";
    /// <summary>
    /// 1,2,3 分别代表低中高级箱子
    /// </summary>
    public static string NextBoxType = "BoxGame_NextBoxType";

    /// <summary>
    /// 当日进入次数
    /// </summary>
    public static string EnterTimes ;
    #endregion
    private AudioSource _audioBg;
    private void Awake()
    {
        EnterTimes = DateTime.Now.ToShortDateString();
    }

    public void Start()
    {
        AudioManager.Instance.StopMusic();
        _audioBg = AudioManager.Instance.PlaySound("choujiang_bgm");
        

        //当日进入拆快递游戏次数
        int _enterTimes = 1;
        if (PlayerPrefs.HasKey(EnterTimes))
        {
             _enterTimes = PlayerPrefs.GetInt(EnterTimes);
            _enterTimes++;
        }
            PlayerPrefs.SetInt(EnterTimes, _enterTimes);
        
    }
    public void StopAudioBg()
    {
        if (_audioBg != null && _audioBg.isPlaying)
        {
            AudioManager.Instance.StopSound(_audioBg);
        }
    }
    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            AndroidAdsDialog.Instance.UploadDataEvent("exit_chaikuaidi");

        }
    }
    public void OnApplicationQuit()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("exit_chaikuaidi");
    }

}
