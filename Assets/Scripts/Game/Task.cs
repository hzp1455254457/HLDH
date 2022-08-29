using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    // Start is called before the first frame update
    //public Image image;
    public Text textInfo,jindu,reward,rewardInfo,buttonText;
    public Image buttonImg;
    public Button button;
    public Sprite[] sprites;//0是可以提现，1是不可提现
    public int index;
    public Slider slider;
    public Mission_redpacket mission_Redpacket;
    public TaskType taskType;

    private void Start()
    {

        InitShow();
        button.onClick.AddListener(ButtonClickEvent);
        RefreshTask();
    }
    private void Awake()
    {
        //InitShow();
        //button.onClick.AddListener(ButtonClickEvent);
    }
    private void OnEnable()
    {
        RefreshTask();
    }
    public void ButtonClickEvent()
    {
        //AndroidAdsDialog.Instance.UploadDataEvent()
        //switch (mission_Redpacket.tid)
        //{
        //    case 2369:
                
        //        AndroidAdsDialog.Instance.UploadDataEvent("0.3_mission_redpacket");
        //        break;
        //    case 2371:
        //        AndroidAdsDialog.Instance.UploadDataEvent("0.31_mission_redpacket");
        //        break;
        //    case 2373:
        //        AndroidAdsDialog.Instance.UploadDataEvent("0.32_mission_redpacket");
        //        break;
        //    case 2375:
        //        AndroidAdsDialog.Instance.UploadDataEvent("0.33_mission_redpacket");
        //        break;
        //    case 2377:
        //        AndroidAdsDialog.Instance.UploadDataEvent("0.34_mission_redpacket");
        //        break;
        //    case 2383:
        //        AndroidAdsDialog.Instance.UploadDataEvent("0.35_mission_redpacket");
        //        break;
        //    case 2385:
        //        AndroidAdsDialog.Instance.UploadDataEvent("0.5_mission_redpacket");
        //        break;
           
        //}
        AndroidAdsDialog.Instance.RequestTaskReward(mission_Redpacket.tid);
        button.interactable = false;
        mission_Redpacket.status = 1;
        RefreshGetStatus();

#if UNITY_EDITOR

        //TaskManager.Instance.RefreshTaskObject(PlayerDate.Instance.mission_RedpacketsList);

#elif UNITY_ANDROID
       
#endif
    }
    public void RefreshTask()
    {
       
        Init();
        RefeshStates();

    }

    private void InitShow()
    {
        textInfo.text = mission_Redpacket.desc;

        switch (mission_Redpacket.tid)
        {
            case 2369:
                reward.text = "0.3元";
                // taskType = TaskType.ZHUBOCOUNT;
                break;
            case 2371:
                reward.text = "0.31元";
                //  taskType = TaskType.KUAIDIYUANCOUNT;
                break;
            case 2373:
                reward.text = "0.32元";
                // taskType = TaskType.KUAIDIYUANCOUNT;
                break;
            case 2375:
                reward.text = "0.33元";
                //taskType = TaskType.ZHUBOLEVEL;
                break;
            case 2377:
                reward.text = "0.34元";
                textInfo.text = "拥有8个7级主播";
                // taskType = TaskType.ZHUBOLEVEL;
                break;
            case 2383:
                reward.text = "0.35元";
                // taskType = TaskType.DASHANGLEVEL;
                break;
            case 2385:
                reward.text = "0.5元";
                gameObject.SetActive(false);
                break;
            case 0:
                reward.text = "1元";
                textInfo.text = "拥有40个51级主播";
                jindu.text = "0%";
                break;
            case 1:
                reward.text = "188元";
                textInfo.text = "完成全部任务";
                jindu.text = "0%";
                break;
        }

        rewardInfo.text = string.Format("{0}现金奖励", reward.text);
    }

    public void Init()
    {
        if (mission_Redpacket.status == 0)
        {
            buttonImg.sprite = sprites[1];
            buttonText.text = "提现";
            button.interactable = false;
          
        }
        else
        {

            buttonImg.sprite = sprites[1];
            buttonText.text = "已提现";
            button.interactable = false;
        }
    }
    public void RefreshGetStatus()
    {
        InitShow();
        if (mission_Redpacket.status == 1)
        {
            buttonImg.sprite = sprites[1];
            buttonText.text = "已提现";
            button.interactable = false;
        }
    }
    public void RefeshStates()
    {
        
            switch (mission_Redpacket.tid)
            {
                case 2369:
                    reward.text = "0.3";
                    slider.value = PlayerData.Instance.actorDateList.Count / 15f;
                    jindu.text = ((int)(slider.value * 100)) + "%";
                    if (15> PlayerData.Instance.actorDateList.Count)
                    {

                    }
                    else
                    {
                        SetCanTixian();

                    }
                    break;
                case 2371:
                    reward.text = "0.31";
                    slider.value = PlayerData.Instance.actorDateList.Count / 30f;
                    jindu.text = ((int)(slider.value * 100)) + "%";
                    if (30 > PlayerData.Instance.actorDateList.Count)
                    {

                    }
                    else
                    {
                        SetCanTixian();

                    }
                    // taskType = TaskType.KUAIDIYUANCOUNT;
                    break;
                case 2373:
                    reward.text = "0.32";
                    slider.value = PlayerData.Instance.courierDateList.Count / 15f;
                    jindu.text = ((int)(slider.value * 100)) + "%";
                    if (15 > PlayerData.Instance.courierDateList.Count)
                    {

                    }
                    else
                    {
                        SetCanTixian();

                    }
                    //taskType = TaskType.KUAIDIYUANCOUNT;
                    break;
                case 2375:
                    slider.value = PlayerData.Instance.courierDateList.Count / 30f;
                    jindu.text = ((int)(slider.value * 100)) + "%";
                    if (30> PlayerData.Instance.courierDateList.Count)
                    {

                    }
                    else
                    {
                        SetCanTixian();

                    }
                    break;
                case 2377:

                    var list = PlayerData.Instance.actorDateList.FindAll(s => s.actor_level >= 7);
                    if (list == null)
                    {
                        slider.value = 0 / 8f;
                        jindu.text = ((int)(slider.value * 100)) + "%";
                    }
                    else
                    {
                        slider.value = list.Count / 8f;
                        jindu.text = ((int)(slider.value * 100)) + "%";
                    }
                   
                     if (list != null && list.Count >= 8)
                    {
                        SetCanTixian();
                    }
                    break;
                case 2383:

                    var list1 = PlayerData.Instance.actorDateList.FindAll(s => s.actor_level >= 6);
                    if (list1 == null)
                    {
                        slider.value = 0 / 9f;
                        jindu.text = ((int)(slider.value * 100)) + "%";
                    }
                    else
                    {
                        slider.value = list1.Count / 9f;
                        jindu.text = ((int)(slider.value * 100)) + "%";
                    }
                 
                    if (list1 != null && list1.Count >= 9)
                    {
                        SetCanTixian();
                    }
                    break;
                case 2385:
                   slider.value = 1 / 4f;
                    jindu.text = ((int)(slider.value * 100)) + "%";
                    //if (4 > PlayerDate.Instance.burseConfig.wallet_level)
                    //{

                    //}
                    //else
                    //{
                    //    SetCanTixian();
                    //}

                    break;
                case 0:
                    var list2 = PlayerData.Instance.actorDateList.FindAll(s => s.actor_level >= 51);
                    if (list2 == null)
                    {
                        slider.value = 0 / 40f;
                        jindu.text = ((int)(slider.value * 100)) + "%";
                    }
                    else
                    {
                        slider.value = list2.Count / 40f;
                        jindu.text = ((int)(slider.value * 100)) + "%";
                    }
                  
                     if (list2 != null && list2.Count >= 40)
                    {
                        SetCanTixian();
                    }
                    break;
                case 1:
                    var task = TaskManager.Instance.taskList.FindAll(s => s.mission_Redpacket.status == 1);
                    if (task == null)
                    {
                        slider.value = 0 / TaskManager.Instance.taskList.Count + 1;
                        jindu.text = ((int)(slider.value * 100)) + "%";
                    }
                    else
                    {
                        slider.value = task.Count / (float)(TaskManager.Instance.taskList.Count + 1);
                        print("task.Count+++" + task.Count);
                        print("TaskManager.Instance.taskList.Count+1+++" + TaskManager.Instance.taskList.Count + 1);
                        jindu.text = ((int)(slider.value * 100)) + "%";
                    }
                    break;

            
        }
            
        }
    //}

    private void SetCanTixian()
    {
        if (mission_Redpacket.status == 0)
        {
            buttonImg.sprite = sprites[0];
            buttonText.text = "提现";
            button.interactable = true;
        }
    }
}
