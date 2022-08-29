using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SevenLoginItem : MonoBehaviour
{
    public SevenLoginData sevenLoginData;
    public GameObject[] stateGos;
    public Button button;
    public Transform bornTf,targetTf;
    public Text text,count;
    public Animator animator;
    //public GameObject guideGo;
    public int index;
    public GameObject go;
 
    public void Init()
    {
        text.text = string.Format("第{0}天", sevenLoginData.day);
        if (index == 2 || index == 3 || index == 1)
        {
            count.text = string.Format("{0}元", sevenLoginData.gift_num / MoneyManager.redProportion);
        }
        else
            count.text = string.Format("{0}个", sevenLoginData.gift_num);
        SetStates(sevenLoginData.states);
    }

    public void SetStates(int states)
    {
        sevenLoginData.states = states;
        for (int i = 0; i < stateGos.Length; i++)
        {
            stateGos[i].SetActive(false);
        }
        stateGos[states].SetActive(true);
        if (states == 1)
        {
            animator.SetBool("walk", true);
           
            //if (index == 2||index==3)
            //{
            //    if (go != null && !go.activeSelf)
            //       go.SetActive(true);
            //}
        }
        else 
        {
            animator.SetBool("walk", false);
            if (states == 2)
            {
                button.interactable = false;
                stateGos[0].SetActive(true);
                if (index == 2 || index == 3)
                {
                    //if (go != null && go.activeSelf)
                    //    go.SetActive(false);
                }
            }
           
           
        }
       
    }
  
    // Update is called once per frame
    public void ClickFun()
    {if (!SevenLoginPanel.Instance.IsGet)
        {
            switch (sevenLoginData.states)
            {
                case 0:
                    ShowTips("明日登录可以领取哦！", bornTf, targetTf);
                    AndroidAdsDialog.Instance.requestCalendarPermission();

                    break;
                case 1:
                    AdwardFun();
                    break;
                case 2:
                    ShowTips("已经领取过了", bornTf, targetTf);
                    break;
            }
        }
        else
        {
            ShowTips("今天已经领过奖励了哦！明天再来吧", bornTf, targetTf,0.8f);
        }
    }
    public void ShowTips(string value,Transform born,Transform target,float scale=1)
    {
        AndroidAdsDialog.Instance.ShowToasts(born, target, value, Color.black, null, null, scale);
    }

    public void AdwardFun()
    {
        switch (sevenLoginData.type)
        {
            case 1:

                SevenLoginPanel.Instance.ShowGuide(false);

                PlayerData.Instance.GetGold(sevenLoginData.gift_num,false);
                AndroidAdsDialog.Instance.ShowToasts(string.Format("+{0}", sevenLoginData.gift_num), ResourceManager.Instance.GetSprite("金币"), Color.black, () => PlayerData.Instance.AddWangDianGold(sevenLoginData.gift_num));
                break;
            case 2:
                print("获得抽奖机会" + sevenLoginData.gift_num);
                //AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget1, string.Format("获得{0}个抽奖币",sevenLoginData.gift_num), Color.black, null, null, 1.5f);
                //PlayerData.Instance.ChouJiangCount += sevenLoginData.gift_num;
                RedUIManager.ShowRed2(() =>
                {
                    PlayerData.Instance.GetRed(SevenLoginPanel.Instance.lastCount * 1000);


                    TipsShowBase.Instance.Show("TipsShow4", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
    {
            string.Format("+{0}元",SevenLoginPanel.Instance.lastCount)
    }, new Sprite[]
    {
              ResourceManager.Instance.GetSprite( "红包")
    }, null,

    null);
                }
                , sevenLoginData.day-2, SevenLoginPanel.Instance.lastCount, SevenLoginPanel.Instance.lastCount);
                //PlayerData.Instance.GetRed(SevenLoginPanel.Instance.lastCount*1000);
                break;
            case 3:
               // BigWorldData.Instance.IsJieSuoLunChuan = true;
                //AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget1, "解锁轮船", Color.black, null, null, 1.5f);
                break;
            case 4:
                //BigWorldData.Instance.IsJieSuoFeiJi = true;
                //AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget1, "解锁飞机", Color.black, null, null, 1.5f);
                break;
            case 5:
                PlayerData.Instance.GetDiamond(sevenLoginData.gift_num);
                AndroidAdsDialog.Instance.ShowToasts(string.Format("+{0}", sevenLoginData.gift_num), ResourceManager.Instance.GetSprite("钻石"), Color.black);
                break;
            case 6:
                //for (int i = 0; i < sevenLoginData.gift_num; i++)
                //{
                //    ZhiBoPanel.Instance.PlayVideoShenJiEvent(false);
                //}
               
                break;
        }
        print("获得" + sevenLoginData.gift_num);
        SetStates(2);
        SevenLoginPanel.Instance.IsGet = true;
    }
    
}
