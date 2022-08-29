using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEngine.UI;

public class ShangJinTaskPanel : PanelBase
{
    public static ShangJinTaskPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("ShangJinTaskPanel")) as ShangJinTaskPanel;
                instance.gameObject.SetActive(false);
                return instance;
            }
            return instance;
        }

    }
    static ShangJinTaskPanel instance;
    public List<ShangJinTaskData> shangJinTaskDatas;

    public Text top,target,sengYuValue,countTop,count,countLeft, countTop1;
    public GameObject mask,mask1, achiveTips, noAchiveTips,button1,button2;

    public double targetValue;
    public bool iSAchived = false;
    public int day = 1;
    public void AddTiXianValue(double value)
    {
        targetValue += value;
       
    }
    protected override void Awake()
    {
        shangJinTaskDatas = JsonMapper.ToObject<List<ShangJinTaskData>>(Resources.Load<TextAsset>("Config/ShangJinConfig").text);
        Debug.LogError("awake");
        targetValue = DataSaver.Instance.GetFloat("targetValueShangJin", 0f);
        iSAchived =  DataSaver.Instance.GetInt("iSAchivedShangJin", 0) == 1 ? true : false;
        day = DataSaver.Instance.GetInt("dayShangJin", 1) ;
       
    }

    public override void Show()
    {
        transform.SetParent(UIManager.Instance.showRootMain);
    }
    public override void Hide()
    { 

    }
  public void ShowUI()
    {
        Debug.LogError("show");
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        base.Animation();
        InitUI();
        AndroidAdsDialog.Instance.ShowFeedAd(540);
    }

    private void InitUI()
    {
            if (day != PlayerData.Instance.day)
        {
            if (iSAchived)
            {
                targetValue = 0;
                iSAchived = false;
                day = PlayerData.Instance.day;
                SetMask1(false);
                SetTips(false);
                SetMask2(false);
                InitText();
                SetButtonTips(true);
            }
            else
            {
                data = GetData(day);
                if (targetValue >= data.need_tx_nums)
                {
                    SetMask1(true);
                    SetTips(true);
                    SetMask2(false);
                    SetButtonTips(true);
                    InitText(data);
                }
                else
                {
                    SetButtonTips(true);
                    day = PlayerData.Instance.day;
                    SetMask1(false);
                    SetTips(false);
                    SetMask2(false);
                    InitText();
                }
            }
            }
            else
            {
                InitText();
                if (targetValue >= data.need_tx_nums)
                {
                SetMask1(false);

                SetTips(false);
                if (!iSAchived)
                { SetButtonTips(true);
                    SetMask2(false);
                }
                else
                {
                    SetButtonTips(false);
                    SetMask2(true);
                }
                }
                else
            {
                SetButtonTips(true);
                SetMask1(false);
                SetTips(false);
                SetMask2(false);
            }
            }

        
    }

    private void SetMask1(bool value)
    {
        mask.SetActive(value);
        
    }
    private void SetMask2(bool value)
    {
        mask1.SetActive(value);

    }
    private void SetTips(bool value)
    {
        achiveTips.SetActive(value);
        noAchiveTips.SetActive(!value);
        
    }

    private void SetButtonTips(bool value)
    {
        
        //button1.SetActive(value);
        //button2.SetActive(!value);
    }
    int clickCount;
    public void ClickHide()
    {
        HideUI();
        AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, "明日可以领取哦！", Color.black, null, null, 1f);
    }
    public void HideUI()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("close_hbdjs");
        AndroidAdsDialog.Instance.CloseFeedAd();
           clickCount++;
        if (clickCount % 3 == 0)
        { AndroidAdsDialog.Instance.ShowTableVideo("0"); 
        
        }
        gameObject.SetActive(false);
        SaveData();
    }
    public void AchiveFun()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("click_hbdjs_lingqu");
        if (data.need_tx_nums <= targetValue)
        {
            AndroidAdsDialog.Instance.UploadDataEvent("finish_hbdjs_lingqu");
            if (day != PlayerData.Instance.day)
            {
                PlayerData.Instance.GetRed(data.reward_redpackts_txdjs);
                PiaoChuang((int)(data.reward_redpackts_txdjs / MoneyManager.redProportion));
                targetValue = 0;
                day = PlayerData.Instance.day;
                InitUI();
            }
            else
            {
                iSAchived = true;
                PlayerData.Instance.GetRed(data.reward_redpackts_txdjs);
                PiaoChuang((int)(data.reward_redpackts_txdjs / MoneyManager.redProportion));
                InitUI();
            }
        }
      
       
    }
    void PiaoChuang(int count)
    {
        TipsShowBase.Instance.Show("TipsShow4", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
{
            string.Format("+{0}元",count)
}, new Sprite[]
{
              ResourceManager.Instance.GetSprite( "红包")
}, null,

null);
    
    }
    ShangJinTaskData data;
    private void InitText()
    {
        top.text = string.Format("带货直播的第{0}天", PlayerData.Instance.day);
        data = GetData(PlayerData.Instance.day);
        int value = data.show_money_nums;
        countTop.text = string.Format("明日登录可直接领取<color=yellow>{0}</color>元", value);
        countTop1.text= string.Format("明日登录可直接领取<color=yellow>{0}</color>元", value);
        count.text = string.Format("最高可得红包{0}元", value);
        countLeft.text = value.ToString() + "元";
        target.text = string.Format("若今日累计成功提现<color=yellow>{0}</color>元", data.need_tx_nums.ToString());
        double sengyuValue = data.need_tx_nums - targetValue >= 0 ? (data.need_tx_nums - targetValue):0;
        sengYuValue.text = string.Format("当前还差成功提现<color=red>{0:f2}</color>元", sengyuValue);
    }
    private void InitText(ShangJinTaskData shangJinTaskData)
    {
        top.text = string.Format("带货直播的第{0}天", PlayerData.Instance.day);
      var  data1 = shangJinTaskData;
        int value = data1.show_money_nums;
        countTop.text = string.Format("明日登录可直接领取<color=yellow>{0}</color>元", value);
        countTop1.text = string.Format("明日登录可直接领取<color=yellow>{0}</color>元", value);
        count.text = string.Format("红包{0}元", value);
        countLeft.text = value.ToString() + "元";
        target.text = string.Format("若今日累计成功提现<color=yellow>{0}</color>元", data1.need_tx_nums.ToString());
        double sengyuValue = data1.need_tx_nums - targetValue >= 0 ? (data1.need_tx_nums - targetValue) : 0;
        sengYuValue.text = string.Format("当前还差成功提现<color=red>{0:f2}</color>元", sengyuValue);
    }
    private ShangJinTaskData GetData(int day)
    {
      var d= shangJinTaskDatas.Find(s => s.days == day);
        if (d == null)
        {
            d = shangJinTaskDatas[shangJinTaskDatas.Count - 1];
        }
        return d;
    }
    private void SaveData()
    {
       
        DataSaver.Instance.SetFloat("targetValueShangJin",(float) targetValue);
     
        int value = iSAchived== true ? 1 : 0;
        DataSaver.Instance.SetInt("iSAchivedShangJin", value);
         DataSaver.Instance.SetInt("dayShangJin", day);
    }

    private void OnApplicationQuit()
    {

        SaveData();

    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();

        }
    }
}
[Serializable]
public class ShangJinTaskData
{
    public int days;
    public double need_tx_nums;
    public int reward_redpackts_txdjs;
    public int show_money_nums;
}