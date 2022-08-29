using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class TiXianManager : MonoSingleton<TiXianManager>
//{
//    // Start is called before the first frame update
//    public Text gold;
//    public Slider slider;
//    public Button button,exit;
//    public Sum[] sums;
//    public Sum currentSum;

//    private void Start()
//    {
//        sums = transform.Find("sumGroup").GetComponentsInChildren<Sum>();

//        exit.onClick.AddListener(CloseTixian);
//        PlayerDate.Instance.goldAction += Refresh;
//        Refresh();
//    }

//    private void CloseTixian()
//    {
//        gameObject.SetActive(false);
//    }
//    /// <summary>
//    /// 刷新金币
//    /// </summary>
//    public void Refresh()
//    {
//        gold.text = string.Format("{0}元", PlayerDate.Instance.gold);
//    }
//    //刷新slider
//    public void RefreshTips()
//    {if(currentSum.taskType==TaskType.ZHUBOLEVEL)
//        slider.value =(float) PlayerDate.Instance.gold /(float) currentSum.value;
//    }
//    //点击事件
//    public void RedPacketClickFun()
//    {
//        if (currentSum.taskType == TaskType.ZHUBOLEVEL)
//        {

//        }

//    }
//}
[System.Flags]
public enum TaskType
{
    ZHUBOLEVEL = 1,
    ZHUBOCOUNT=2,
    KUAIDIYUANLEVEL=4,
    KUAIDIYUANCOUNT=8,
    DASHANGLEVEL=16,


}
