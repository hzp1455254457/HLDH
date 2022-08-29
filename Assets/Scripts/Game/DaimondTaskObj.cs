using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
[Serializable]
public class DaimondTaskObj
{
   DaimondTask daimondTask;
    DaimondTaskManager daimondTaskManager;
  public DaimondTaskObj(DaimondTask daimondTask, DaimondTaskManager daimondTaskManager)
    {
        this.daimondTask = daimondTask;
        this.daimondTaskManager = daimondTaskManager;
    }

    public void Init()
    {
        if (daimondTask.main_mission_isdone == 0)
        {
            switch (daimondTask.main_mission_type)
            {
                case 1:
                    UnityActionManager.Instance.AddAction<int,int>("AddProduce", AddProcess);
                    break;
                case 2:
                    UnityActionManager.Instance.AddAction<int>("ShengJiZhuBo", AddProcess);
                    break;
                case 3:
                    UnityActionManager.Instance.AddAction<int>("GetFaHuoRed", AddProcess);
                    break;
                //case 4:
                //    UnityActionManager.Instance.AddAction("GetFaHuoRed", AddProcess);
                //    break;
                //case 5:
                //    UnityActionManager.Instance.AddAction("RefreshProduce", AddProcess);
                //    break;
            }
        }
    }
    private void AddProcess(int count)
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
        if (daimondTask.progress < target)
        {
            daimondTask.progress+= count;
            if(daimondTask.progress >= target)
            {
                daimondTask.main_mission_isdone = 1;
                daimondTask.progress = target;
            }
        }
        else
        {
            daimondTask.main_mission_isdone= 1;
        }
        RefeshStates(daimondTaskManager);
    }
    private void AddProcess(int id,int count)
    {
        if (id == daimondTask.main_mission_item_id)
        {
            AddProcess(count);
        }
       
    }
    public void RefeshStates(DaimondTaskManager daimondTaskManager)
    {

        if (daimondTaskManager.daimondTask == daimondTask)
        {
           
                daimondTaskManager.RefreshTask();
            
        }
    }
}