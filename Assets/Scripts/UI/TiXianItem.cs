using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiXianItem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stateGo;
    public GameObject stateGo1;
    public GameObject stateGo2;
    public Text jine, tips;
    public Text danwei;
    public void SetJinE(string value)
    {
        jine.text = value;

    }
    /// <summary>
    /// 1 表示 当前挡位 2表示为解锁 3表示已提现
    /// </summary>
    /// <param name="value"></param>
    /// <param name="type"></param>
    public void SetStates(string value,int type)
    {
        if (type == 1)
        {
            stateGo.SetActive(false);
            stateGo2.SetActive(false);
            stateGo1.SetActive(true);
            //tips.text = value;
        }
        else if (type == 2)
        {
            stateGo1.SetActive(false);
            stateGo.SetActive(false);
            stateGo2.SetActive(true);
            danwei.text = value;
        }
        else
        {
            stateGo1.SetActive(false);
            stateGo.SetActive(true);
            stateGo2.SetActive(false);
            tips.text = value;
        }
    }
}
