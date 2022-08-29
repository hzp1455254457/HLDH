using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaimondTaskManager : MonoBehaviour
{
    public DaimondTask daimondTask;
    public Text daimondCount, info, progress;
    public Sprite[] sprites;
    public GameObject[] statusGo;
    public Image tipsImg;
    public List<DaimondTaskObj> daimondTasklist = new List<DaimondTaskObj>();
    public void RefreshTask()
    {
        if (PlayerData.Instance.Data.daimondTasks.Count > PlayerData.Instance.Data.currentTask)
        {
            daimondTask = PlayerData.Instance.Data.daimondTasks[PlayerData.Instance.Data.currentTask];
            int target = 0;
            switch (daimondTask.main_mission_type)
            {
                case 1:
                    target = daimondTask.main_mission_item_id_num;
                    break;
                case 2:
                    target = daimondTask.main_mission_shengji_index;
                    break;
                case 3:
                    target = daimondTask.main_mission_redpacket_index;
                    break;
            }
            RefreshText(daimondTask.main_mission_context, string.Format("{0}/{1}", daimondTask.progress, target), "+" + daimondTask.main_mission_reward);
            Refreshstatus();

        }
        else
        {
            gameObject.SetActive(false);

        }
    }
    public void ClickEvent()
    {

        int target = 0;
        switch (daimondTask.main_mission_type)
        {
            case 1:
                target = daimondTask.main_mission_item_id_num;
                break;
            case 2:
                target = daimondTask.main_mission_shengji_index;
                break;
            case 3:
                target = daimondTask.main_mission_redpacket_index;
                break;
        }
        PanelTask.Instance.ShowUI(daimondTask.main_mission_name, daimondTask.main_mission_context, string.Format("任务进度:{0}/{1}", daimondTask.progress, target), "+" + daimondTask.main_mission_reward, daimondTask.main_mission_isdone, GotoTips);

    }
    private void RefreshText(string value1, string value2, string value3)
    {
        info.text = value1;
        progress.text = value2;
        daimondCount.text = value3;
    }
    void AddListening()
    {
        daimondTasklist[PlayerData.Instance.Data.currentTask].Init();
    }
    private void Refreshstatus()
    {
        if (daimondTask.main_mission_isdone == 0)
        {
            Settips(0);

        }
        else
        {
            Settips(1);

        }
    }
    private void Settips(int index)
    {

        //tipsImg.sprite = sprites[index];
        if (index == 0)
        {
            //tipsImg.gameObject.SetActive(false);
            statusGo[0].SetActive(true);
            statusGo[1].SetActive(false);
        }
        else
        {
            //tipsImg.gameObject.SetActive(true);
            statusGo[1].SetActive(true);
            statusGo[0].SetActive(false);
        }
    }
    private void Awake()
    {
        RefreshTask();
        for (int i = 0; i < PlayerData.Instance.Data.daimondTasks.Count; i++)
        {
            DaimondTaskObj daimondTask = new DaimondTaskObj(PlayerData.Instance.Data.daimondTasks[i], this);
            daimondTasklist.Add(daimondTask);
        }
        if (!GuideManager.Instance.isFirstGame)
        {
            AddListening();
        }
        GuideManager.Instance.achieveGuideAction += AddListening;
    }
    public void GotoTips()
    {
        //UIManager.Instance.SetUIStates(true);
        ToggleManager.Instance.GoGame();
        switch (daimondTask.main_mission_type)
        {
            case 1:
                if (daimondTask.main_mission_isdone == 0)
                {
                    var level = ConfigManager.Instance.GetProduce(daimondTask.main_mission_item_id).produce_level;
                    if (level > ZhiBoPanel.Instance.currentIndex)
                    {
                        AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, "升级主播，解锁高楼层的主播才能卖哦!", Color.black, null, null, 0.8f);
                        ZhiBoPanel.Instance.MoveZhuBo(ZhiBoPanel.Instance.currentIndex);
                        UnityActionManager.Instance.DispatchEvent<int>("ShengJiTips", ZhiBoPanel.Instance.currentIndex);
                    }
                    else
                    {
                        level = UnityEngine.Random.Range(level, ZhiBoPanel.Instance.currentIndex + 1);

                        ZhiBoPanel.Instance.MoveZhuBo(level);
                        ZhiBoPanel.Instance.zhibojianList[level].OpenProduceInfoEvent(daimondTask.main_mission_item_id);


                    }
                }
                else
                {
                    AchiveFun();
                }
                break;
            case 2:
                if (daimondTask.main_mission_isdone == 0)
                {
                    ZhiBoPanel.Instance.MoveZhuBo(ZhiBoPanel.Instance.currentIndex);
                    UnityActionManager.Instance.DispatchEvent<int>("ShengJiTips", ZhiBoPanel.Instance.currentIndex);
                }
                else
                {
                    AchiveFun();
                }
                break;
            case 3:
                if (daimondTask.main_mission_isdone == 0)
                {
                    ToggleManager.Instance.ShowPanel(2);

                    UnityActionManager.Instance.DispatchEvent("FahuoTips");
                }
                else
                {
                    AchiveFun();
                }
                break;
                //case 4:
                //    ToggleManager.Instance.ShowPanel(2);
                //    break;
                //case 5:
                //    UnityActionManager.Instance.DispatchEvent("RefreshTips");
                //    break;
        }
    }

    private void AchiveFun()
    {
        TipsShowBase.Instance.Show("TipsShow4", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
   {
            string.Format("+{0}",daimondTask.main_mission_reward)
       }, new Sprite[]
       {
                ResourceManager.Instance.GetSprite("钻石")
       }, null, null);

        PlayerData.Instance.AchiveDaimondTask();
        RemoveLastTaskAction();
        AddListening();
        RefreshTask();
        AndroidAdsDialog.Instance.UploadDataEvent(string.Format("finish_mission_{0}", daimondTask.main_mission_id));

    }
    public void RemoveLastTaskAction()
    {
        UnityActionManager.Instance.CleanKey("AddProduce");
        UnityActionManager.Instance.CleanKey("ShengJiZhuBo");
        UnityActionManager.Instance.CleanKey("GetFaHuoRed");
    }
}
