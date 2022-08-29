using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaChe : Transportation
{

    public override void Start()
    {
        base.Start();
        //if (!PlayerDate.Instance.GetIsSevenRedCount())
        //{
        //    return;
        //}
        //TODO TEST
        if (!BigWorldData.IsBigWorldUnlocked)
        {
            return;
        }
        ShowUI();
    }

    void ShowUI()
    {
        //var unlockLevel = PlayerDate.Instance.Datas3D.CarJieSuoLevel;

        ////是否 已解锁
        //IsUnLocked = Index >= unlockLevel;
        //if (!IsUnLocked)
        //{
        //    //待解锁
        //    GameObject UIObj = GameObject.Instantiate(JieSuoUI, UIPositionObj.transform.position, UIPositionObj.transform.rotation, GameObject.Find("WorldCanvas").transform);
        //    TransportationState = TransportationState.Waiting;
        //}
        //else
        //{
        //    int goldValue;
        //    switch (Index)
        //    {
        //        case 1:
        //            goldValue = PlayerDate.Instance.Datas3D.carGold1;
        //            break;
        //        case 2:
        //            goldValue = PlayerDate.Instance.Datas3D.carGold2;
        //            break;
        //        case 3:
        //            goldValue = PlayerDate.Instance.Datas3D.carGold3;
        //            break;
        //        case 4:
        //            goldValue = PlayerDate.Instance.Datas3D.carGold4;
        //            break;
        //        default:
        //            goldValue = 0;
        //            break;
        //    }
        //    //判断是否有未领取金币
        //    if (goldValue > 0)
        //    {
        //        //TODO: 需要判断是否已满

        //        //待领取
        //        GameObject UIObj = GameObject.Instantiate(LingQuUI, UIPositionObj.transform.position, UIPositionObj.transform.rotation, GameObject.Find("WorldCanvas").transform);
        //    }

        //}
    }
    public override void Move()
    {
        base.Move();
    }

    public override void OnSceneLoaded()
    {
        throw new System.NotImplementedException();
    }

    public override void Reset()
    {
        base.Reset();
    }

    public override void Stop()
    {
        base.Stop();
    }

 
}
