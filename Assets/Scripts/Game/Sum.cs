using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sum : MonoBehaviour
{

    public int value;
    public int day;
    public int currentCount;
    public TaskType taskType;

    void Start()
    {
      //  taskType = TaskType.ZHUBOLEVEL | TaskType.KUAIDIYUANLEVEL;
    }
    public void ClickFun(bool value)
    {
        if (value)
        {
            //TiXianManager.Instance.currentSum = this;

        }
        
    }



}
