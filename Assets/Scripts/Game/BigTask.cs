using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigTask : MonoBehaviour
{
    public Big_Redpacket my_Shop_Mission;
    public Sprite[] sprites;//��0��δ��ȡ��1����ȡ
    public Image buttonImg;//��ťimage
    public Button button;//��ť
    public GameObject[] buttonGos;//0��δ��ɣ�1�������
    public Text buttonText, expText, infosText, awardText;
    //ZhiBoPanel boPanel;
    public int index;
    void Start()
    {
        //infosText.text = string.Format("{0}({1}/{2})", my_Shop_Mission.shop_need_exp, my_Shop_Mission.processValue, my_Shop_Mission.mission_index);
        //awardText.text = string.Format("{0:F}Ԫ���ȯ�ʹ�������", my_Shop_Mission.mission_reward_redpacket / 1000f);
        RefreshTask();
        RefeshStates();
    }

    private void RefeshStates()
    {
       // throw new NotImplementedException();
    }

    private void RefreshTask()
    {
        //throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
