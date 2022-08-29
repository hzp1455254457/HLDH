using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoxResult : BoxPannel
{
    public Image OpendBoxImage,BonusImage;
    public Sprite BonusZuanShiSprite, BonusHongBaoSprite;
    public Text BonusText;

    public Sprite[] BoxSprites;
    public override void OnEnable()
    {
        IsHongBao = false;
        int boxType = PlayerPrefs.GetInt(BoxGame.NextBoxType, 1);

        OpendBoxImage.sprite = BoxSprites[boxType-1];

        //下一个快递领取时间
        DateTime nextTime = DateTime.Now.AddMinutes(boxType == 3 ? 30 : 40);
        PlayerPrefs.SetString(BoxGame.NextTime, nextTime.ToString());

        //是否是高级快递
        if (boxType == 3)
        {
            AndroidAdsDialog.Instance.requestCKDData((bool tf) => { CheckAndSetResult(tf); });
        }
        //普通快递和中级快递直接给钻石
        else
        {
            SetZuanShiBouns();
            boxType++;
            PlayerPrefs.GetInt(BoxGame.NextBoxType, boxType);
        }



        base.OnEnable();

        //Audio
        StartCoroutine(PlaySound());

        //信息流
        AndroidAdsDialog.Instance.ShowFeedAd(540);
    }
    IEnumerator PlaySound()
    {
        AudioManager.Instance.PlaySound("yeah");
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlaySound("success_deliever");
    }

    /// <summary>
    /// 查询并且设置高级快递奖励
    /// </summary>
    /// <param name="tf"> 是否领取过现金红包,true表示领取过</param>
    public void CheckAndSetResult(bool tf)
    {
        if (tf)
        {
            SetZuanShiBouns();
        }
        //领取过现金红包，一律给钻石
        else
        {
            SetHongBaoBonus();
            IsHongBao = true;
        }
    }

    private int zuanShiValue;
    bool IsHongBao = false;
    void SetZuanShiBouns()
    {
        int boxType = PlayerPrefs.GetInt(BoxGame.NextBoxType, 1);
        BonusImage.sprite = BonusZuanShiSprite;
        float _zuanShiValue = GetBonusValue(boxType);
        _zuanShiValue = _zuanShiValue * (1+PlayerData.Instance.actorDateList.Count/(float)10)*PlayerPrefs.GetInt(BoxGame.EnterTimes,1);
        zuanShiValue = (int)_zuanShiValue;
        BonusText.text = "x " + zuanShiValue;
    }

    void SetHongBaoBonus()
    {
        BonusImage.sprite = BonusZuanShiSprite;
        BonusText.text = "0.3元";
    }
    int GetBonusValue(int boxType)
    {
        switch (boxType)
        {
            case 1:
                return UnityEngine.Random.Range(800, 1200);
            case 2:
                return UnityEngine.Random.Range(2400, 3600);
            case 3:
                return UnityEngine.Random.Range(2400, 3600);
            default:
                return 0;
        }
    }

    public void NoTksBtn()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("rejecet_reward_chaikuaidi");
        AndroidAdsDialog.Instance.CloseFeedAd();
        //this.gameObject.SetActive(false);
        Quit();
    }
    public void YesBtn()
    {
#if UNITY_EDITOR
        int _boxType = PlayerPrefs.GetInt(BoxGame.NextBoxType, 1);
        _boxType++;
        if (_boxType<=3)
        {
            PlayerPrefs.SetInt(BoxGame.NextBoxType, _boxType);
        }
        

        Debug.Log("已设置NextBox:"+ PlayerPrefs.GetInt(BoxGame.NextBoxType, 1));
        return;
#endif
        AndroidAdsDialog.Instance.UploadDataEvent("show_video_chaikuaidi");
        AndroidAdsDialog.Instance.CloseFeedAd();

        AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_KUAIDI, () =>
        {
            AndroidAdsDialog.Instance.UploadDataEvent("finish_video_chaikuaidi");

          int nowBoxType=  PlayerPrefs.GetInt(BoxGame.NextBoxType, 1);
            int nextBoxType = nowBoxType + 1 > 3 ? nowBoxType : nowBoxType++;

            if (IsHongBao)
            {
                //加现金红包
                AndroidAdsDialog.Instance.requestGetCKDWithDraw(null);
            }
            else
            {
                //加钻石
                PlayerData.Instance.GetDiamond(zuanShiValue);
            }
            //this.gameObject.SetActive(false);
            Quit();
        });
    }

    void Quit()
    {

        AudioManager.Instance.PlayMusic("bgm");

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("BigWorld"));
        UIManager.Instance.SetUIStates(true);
        CamareManager.Instance.SetStates(true);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("OpenBox"));

    }
}
