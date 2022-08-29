using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QiPaoData
{
    public int item_id;

    public int item_have;
    public double x;
    public double y;

    /// <summary>
    /// 0表示没被售卖，1表示被卖
    /// </summary>
    // public int state;
}
