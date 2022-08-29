using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTask : MonoBehaviour
{
    public My_Shop_Mission my_Shop_Mission;
    public Sprite[] sprites;//，0是未领取，1已领取
    public Image buttonImg;//按钮image
    public Button button;//按钮
    public GameObject[] buttonGos;//0是未完成，1是已完成
    public Text buttonText,expText,infosText,awardText;
    ZhiBoPanel boPanel;
  public int index;
   // int processValue = 0;
  public  void Init()
    {
        boPanel = (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel);
        //AddProcess(my_Shop_Mission.processValue);
        infosText.text = string.Format("{0}({1}/{2})", my_Shop_Mission.shop_need_exp, my_Shop_Mission.processValue, my_Shop_Mission.mission_index);
        awardText.text = string.Format("{0:F}元红包券和大量经验", my_Shop_Mission.mission_reward_redpacket/1000f);
        RefreshTask();
        RefeshStates();
        if (my_Shop_Mission.shop_mission_states == 0)
        {
            switch (my_Shop_Mission.mission_type)
            {
                case 1:

                    boPanel.addZhiBoJianAction += AddProcess;
                    boPanel.addZhiBoJianAction += RefreshTask;
                    //(UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).addZhiBoJianAction += AddProcess;
                    break;
                case 2:
                    CanKuPanel.addSelledCountAction += AddProcess;
                    CanKuPanel.addSelledCountAction1 += RefreshTask;
                    //CanKuPanel.addSelledCountAction += AddProcess;
                    break;
                case 3:
                    //ZhiBoPanel.addShengJICountAction += AddProcess;
                   // ZhiBoPanel.addShengJICountAction += RefreshTask;
                    //ZhiBoPanel.addShengJICountAction += AddProcess;
                    break;
                case 4:
                    JavaCallUnity.Instance.showCountAction += AddProcess;
                    JavaCallUnity.Instance.showCountAction += RefreshTask;
                    //JavaCallUnity.Instance.showCountAction += AddProcess;
                    break;
                case 5:
                    ZhiBoPanel.addDaimondCountAction += AddProcess;
                    ZhiBoPanel.addDaimondCountAction1 += RefreshTask;
                    //ZhiBoPanel.addDaimondCountAction += AddProcess;
                    break;
            }
        }
    }
    void RefreshTask()
    {
      
        if (my_Shop_Mission.processValue >= my_Shop_Mission.mission_index)
        {
            if (my_Shop_Mission.shop_mission_states == 0)
            {
                my_Shop_Mission.shop_mission_states = 2;
                RefeshStates();
                switch (my_Shop_Mission.mission_type)
                {
                    case 1:

                        boPanel.addZhiBoJianAction -= AddProcess;
                        boPanel.addZhiBoJianAction -= RefreshTask;
                        //(UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).addZhiBoJianAction += AddProcess;
                        break;
                    case 2:
                        CanKuPanel.addSelledCountAction -= AddProcess;
                        CanKuPanel.addSelledCountAction1 -= RefreshTask;
                        //CanKuPanel.addSelledCountAction += AddProcess;
                        break;
                    case 3:
                        //ZhiBoPanel.addShengJICountAction -= AddProcess;
                        //ZhiBoPanel.addShengJICountAction -= RefreshTask;
                        //ZhiBoPanel.addShengJICountAction += AddProcess;
                        break;
                    case 4:
                        JavaCallUnity.Instance.showCountAction -= AddProcess;
                        JavaCallUnity.Instance.showCountAction -= RefreshTask;
                        //JavaCallUnity.Instance.showCountAction += AddProcess;
                        break;
                    case 5:
                        ZhiBoPanel.addDaimondCountAction -= AddProcess;
                        ZhiBoPanel.addDaimondCountAction1 -= RefreshTask;
                        //ZhiBoPanel.addDaimondCountAction += AddProcess;
                        break;
                }
            }
            
        }
       // RefeshStates();
    }
    private void AddProcess()
    {
        // infosText.text = string.Format("");
        my_Shop_Mission.processValue +=1;
        infosText.text = string.Format("{0}({1}/{2})", my_Shop_Mission.shop_need_exp, my_Shop_Mission.processValue, my_Shop_Mission.mission_index);
    }
    private void AddProcess(int count)
    {
        // infosText.text = string.Format("");
        my_Shop_Mission.processValue += count;
        infosText.text = string.Format("{0}({1}/{2})", my_Shop_Mission.shop_need_exp, my_Shop_Mission.processValue, my_Shop_Mission.mission_index);
    }
    public void RefeshStates()
    {
        ShopTaskManager.Instance.SetText();
        switch (my_Shop_Mission.shop_mission_states)
        {
            case 0:
                print("执行0+++");
                buttonGos[0].SetActive(true);
                buttonGos[1].SetActive(false);
                break;
            case 2:
                print("执行2+++");
                ChangeListPos(my_Shop_Mission, 0);
                transform.SetAsFirstSibling();
                buttonGos[0].SetActive(false);
                buttonGos[1].SetActive(true);
                buttonImg.sprite = sprites[0];
                button.interactable = true;
                buttonText.text = "可领取";
                expText.text = string.Format("经验值 {0}", my_Shop_Mission.mission_reward_exp);
                break;
            case 3:
                print("执行3+++");
                buttonGos[0].SetActive(false);
                buttonGos[1].SetActive(true);
                buttonImg.sprite = sprites[1];
                button.interactable = false;
                buttonText.text = "已领取";
                expText.text = string.Format("经验值 {0}", my_Shop_Mission.mission_reward_exp);
                break;
        }

    }
    public void GetEXP()
    {
        AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_GetWANGDIANEXP, GetEXPEvent);
#if UNITY_EDITOR

        GetEXPEvent();

#elif UNITY_ANDROID

      
        
#endif
      
        //var shopTask = PlayerDate.Instance.shop_MissinList.Find(s => s.shop_mission_states != 3);
        //if (shopTask == null)
        //{
        //   ShopTaskManager.Instance. achiveMask.SetActive(true);
        //}
        //switch (my_Shop_Mission.mission_type)
        //{
        //    case 1:
        //        AndroidAdsDialog.Instance.UploadDataEvent("mission_finish_type1");
        //        boPanel.addZhiBoJianAction -= RefreshTask;

        //        break;
        //    case 2:
        //        AndroidAdsDialog.Instance.UploadDataEvent("mission_finish_type2");
        //        CanKuPanel.addSelledCountAction -= RefreshTask;

        //        break;
        //    case 3:
        //        AndroidAdsDialog.Instance.UploadDataEvent("mission_finish_type3");
        //        ZhiBoPanel.addShengJICountAction -= RefreshTask;

        //        break;
        //    case 4:
        //        AndroidAdsDialog.Instance.UploadDataEvent("mission_finish_type4");
        //        JavaCallUnity.Instance.showCountAction -= RefreshTask;

        //        break;
        //    case 5:
        //        AndroidAdsDialog.Instance.UploadDataEvent("mission_finish_type5");
        //        ZhiBoPanel.addDaimondCountAction1 -= RefreshTask;

        //        break;
        //}
    }

    private void GetEXPEvent()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("finish_new_mission");
        PlayerData.Instance.AddStoreExp(my_Shop_Mission.mission_reward_exp);
        PlayerData.Instance.GetRed(my_Shop_Mission.mission_reward_redpacket);
        my_Shop_Mission.shop_mission_states = 3;
        transform.SetAsLastSibling();
        ChangeListPos(my_Shop_Mission, PlayerData.Instance.shop_MissinList.Count - 1);
        RefeshStates();
    }

    public void GoToTips()
    {
        ShopTaskManager.Instance.isUnity = true;
        ShopTaskManager.Instance.Hide();
        AndroidAdsDialog.Instance.UploadDataEvent("click_mission_goto");
       
        switch (my_Shop_Mission.mission_type)
        {
            case 1:
                CreactZhuBoTaskTips.Instance.ShowUI(my_Shop_Mission.mission_index-my_Shop_Mission.processValue,my_Shop_Mission.mission_reward_redpacket);
                
                break;
            case 2:
                ToggleManager.Instance.ShowPanel(2);
                break;
            case 3:
                ShengJiZhuBoTaskTips.Instance.ShowUI(boPanel.zhibojianList[Random.Range(0, boPanel.zhibojianList.Count)], 
                    my_Shop_Mission.mission_index - my_Shop_Mission.processValue, my_Shop_Mission.mission_reward_redpacket);
                break;
            case 4:
                ToggleManager.Instance.ShowPanel(1);
                break;
            case 5:
                ToggleManager.Instance.ShowPanel(1);
                break;
        }
        //AndroidAdsDialog.Instance.ShowTableVideo("0");
    }

    public void ChangeListPos(My_Shop_Mission my_Shop_Mission,int targetIndex)
    {
        var shop = my_Shop_Mission;
        PlayerData.Instance.shop_MissinList.Remove(shop);


        PlayerData.Instance.shop_MissinList.Insert(targetIndex, shop);
        //PlayerDate.Instance.shop_MissinList[targetIndex] = PlayerDate.Instance.shop_MissinList[currentIndex];
        //PlayerDate.Instance.shop_MissinList[currentIndex]=shop;
      
    }
}