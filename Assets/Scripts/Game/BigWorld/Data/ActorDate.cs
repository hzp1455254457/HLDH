using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActorDate
{
    public string actor_name;
    public int actor_id;
    public int actor_louceng;
    public int actor_level;
    public int actor_sex;
    public int actor_sellbase;
    public int item_id;
    public string Actor_label = "小主播";
    /// <summary>
    /// 解锁新主播得等级
    /// </summary>
    public int need_level_new_actor;
    public double time;
    
}