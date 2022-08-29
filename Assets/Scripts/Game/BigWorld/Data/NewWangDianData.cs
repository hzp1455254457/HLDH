using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NewWangDianData
{
    public int myshop_level;
    public int myshop_needgold;
    public int myshop_redpacket_reward;
    /// <summary>
    /// 0 未领取，1领取 2 已领取
    /// </summary>
    public int status;
    public int gold;
}
