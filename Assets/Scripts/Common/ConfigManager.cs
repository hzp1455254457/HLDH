using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

public class ConfigManager : MonoSingleton<ConfigManager>
{
  public  List<Produce> produceList;
    Dictionary<int, Produce> produceDic;
 //public   List<FreeProduce> freeProduceList;
    public List<ActorDate> actorList;
    public List<Actor_Skill> skillList;
    public List<Floor_Up> floorList;
    public List<Mission_redpacket> taskList;
   public List<BurseConfig> burseList;
    public List<CourierDate> courierDateList;
    public List<Courier_Up> courier_UpsList;
    public string[] animName = new string[] { "walk","walk2"};
    public List<My_Shop> shopList;
    public List<My_Shop_Mission> shop_MissinList;
    public List<RedConFig> redConFigs;
    public WeChatInfo chatInfo;
  
    public List<NewWangDianData> NewWangDianDatas
    {
        get
        {
            if (newWangDianDatas == null)
            {
                newWangDianDatas = JsonMapper.ToObject<List<NewWangDianData>>(Resources.Load<TextAsset>("Config/NewWangDianData").text);
            }
            return newWangDianDatas;
        }
    }
    [HideInInspector]
    List<NewWangDianData> newWangDianDatas;
    public List<Sign_up> Sign_Ups
    {
        get
        {
            if (sign_Ups == null)
            {
                sign_Ups = JsonMapper.ToObject<List<Sign_up>>(Resources.Load<TextAsset>("Config/sign_up").text);
            }
            return sign_Ups;
        }
    }
    [HideInInspector]
   List<Sign_up> sign_Ups;

    public List<DaimondTask> DaimondTaskList
    {
        get
        {
            if (daimondTaskList == null)
            {
                daimondTaskList = JsonMapper.ToObject<List<DaimondTask>>(Resources.Load<TextAsset>("Config/DaimondTask").text);
                print(daimondTaskList);
            }
            return daimondTaskList;
        }
    }
    [HideInInspector]
    List<DaimondTask> daimondTaskList;
//public List<Big_Redpacket> Big_Redpackets
//{
//    get
//    {
//        if (big_Redpackets == null)
//        {
//            big_Redpackets= JsonMapper.ToObject<List<Big_Redpacket>>(Resources.Load<TextAsset>("Config/Big_Redpacket").text);
//        }
//        return big_Redpackets;
//    }
//}
//List<Big_Redpacket> big_Redpackets;
public override void Init()
    {
        base.Init();
        //chatInfo= JsonMapper.ToObject<WeChatInfo>(Resources.Load<TextAsset>("Config/WeChatInfo").text);
        produceList = JsonMapper.ToObject<List<Produce>>(Resources.Load<TextAsset>("Config/Produce").text);
      
        produceList.Sort(new Produce());
        produceList.Reverse();
        produceDic = new Dictionary<int, Produce>();
        for (int i = 0; i < produceList.Count; i++)
        {
            //if(!produceDic.ContainsKey(produceList[i].item_id))
            produceDic.Add(produceList[i].item_id, produceList[i]);
        }
       //freeProduceList = JsonMapper.ToObject<List<FreeProduce>>(Resources.Load<TextAsset>("Config/FreeProduce").text);
       actorList = JsonMapper.ToObject<List<ActorDate>>(Resources.Load<TextAsset>("Config/ActorDate").text);
        skillList= JsonMapper.ToObject < List < Actor_Skill>>(Resources.Load<TextAsset>("Config/Actor_Skill").text);
        floorList = JsonMapper.ToObject < List<Floor_Up>>(Resources.Load<TextAsset>("Config/Floor_Up").text);
        redConFigs= JsonMapper.ToObject<List<RedConFig>>(Resources.Load<TextAsset>("Config/RedConFig").text);
        // Debug.LogError( GetProduce(6).item_name);
        // taskList = JsonMapper.ToObject < List<Mission_redpacket>>(Resources.Load<TextAsset>("Config/Mission_redpacket").text);
        //burseList = JsonMapper.ToObject < List<BurseConfig>>(Resources.Load<TextAsset>("Config/BurseConfig").text);
        //courierDateList = JsonMapper.ToObject<List<CourierDate>>(Resources.Load<TextAsset>("Config/CourierDate").text);
        // courier_UpsList= JsonMapper.ToObject<List<Courier_Up>>(Resources.Load<TextAsset>("Config/Courier_Up").text);
        // shopList= JsonMapper.ToObject<List<My_Shop>>(Resources.Load<TextAsset>("Config/My_Shop").text);
        //shop_MissinList= JsonMapper.ToObject<List<My_Shop_Mission>>(Resources.Load<TextAsset>("Config/My_Shop_Mission").text);


        // big_Redpackets = JsonMapper.ToObject<List<Big_Redpacket>>(Resources.Load<TextAsset>("Config/Big_Redpacket").text);

    }
    public BurseConfig GetBurse(int day)
    {
        if(day> burseList.Count)
        {
           return burseList[burseList.Count - 1];
        }
       return burseList.Find(s => s.days== day);
    }
    public RedConFig GetRedConfig(int id)
    {
        if(id>redConFigs.Count)
        {
            return null;
        }
        return redConFigs.Find(s => s.zs_mission_id== id);
    }
   // int countClick = 0;
   /// <summary>
   /// 通过小于level值得商品列表
   /// </summary>
   /// <param name="level"></param>
   /// <returns></returns>
        public List<Produce> GetProduces(int level)
    {

        return  produceList.FindAll(p => p.produce_level <= level);
              //  return list;
            
        
    }
    /// <summary>
    /// 高于level值得商品列表
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<Produce> GetHightLevelProduces(int level)
    {
       return produceList.FindAll(p => p.produce_level > level);
    }
    public Floor_Up GetCurrentLevel(int level)
    {
        if (level <= floorList.Count - 1)
            return floorList.Find(s => s.floor_total == level);
        else
        {
            return floorList[floorList.Count - 1];
        }
    }
    public Courier_Up GetCurrentCourier_Up(int count)
    {
        var up = courier_UpsList.Find(s => s.kuaidiyuan_num == count);
        if (up == null)
        {
            return courier_UpsList[courier_UpsList.Count-1];
        }
        else
        {
            return up;
        }
        //return courier_UpsList.(s => s.kuaidiyuan_num==count);
    }
    /// <summary>
    /// 通过id获取商品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Produce GetProduce(int id)
    {
        if (produceDic.ContainsKey(id))
            return produceDic[id];
        else
        {
          return  produceList.Find(s => s.item_id == id);
        }
        //return produceList.Find(p => p.item_id== id);

    }
    //public FreeProduce GetFreeProduce(int Id)
    //{
    //    return freeProduceList.Find(s => s.Days == Id);
    //}
    public ActorDate GetActor()
    {
     var actor=   actorList.Find(s => s.actor_louceng == PlayerData.Instance.actorDateList.Count + 1);
        if(actor!=null)
        return actorList.Find(s => s.actor_louceng == PlayerData.Instance.actorDateList.Count + 1);
        else
        {
            return actorList[actorList.Count - 1];
        }
    }
    public CourierDate GetCourier()
    {
    
            return new CourierDate();
      
    }
    public double GetBuff(int level)
    {
        return skillList.Find(s => s.actor_level == level).actor_level_buff;
    }
    public Actor_Skill GetSkill(int level)
    {if(level<= skillList.Count-1)
        return skillList.Find(s => s.actor_level == level);
        else
        {
            return skillList[skillList.Count - 1];
        }
    }
    public List<My_Shop_Mission> GetCurrentShop_Mission(int day)
    {
        return shop_MissinList.FindAll(s => s.days_list ==day);
    }
    public int GetLevelExp(int level)
    {
        if (level <= shopList.Count)
            return shopList[level - 1].shop_need_exp;
        else return 99999999;
        //return shopList.Find(s => s.shop_level == level).shop_need_exp;
    }
    public Sign_up GetSign_Up(int day)
    {
       return sign_Ups.Find(s => s.days == day);
    }
}
[Serializable]
public class Produce:IComparer<Produce>,IComparable<Produce>
{
    public int item_id;
    public string item_name;
    public string item_pic;
    
    public double item_profit;
    public int profit_state;
    public int produce_level;
    public int item_video;
    public int item_cost_type;
    public int item_cost_num;
    public string item_ad1;
    public int item_ad1_result;
    public string item_ad2;
    public int item_ad2_result;

  

    public int Compare(Produce x, Produce y)
    {
        return (x ).item_profit.CompareTo((y).item_profit);
    }

    public int CompareTo(Produce other)
    {
        return this.produce_level.CompareTo(other.produce_level);
    }
}
[Serializable]
public class FreeProduce
{
    public int item_id;
    public string item_name;
    public string item_pic;
    public int num_min;
    public int num_max;
    public double item_profit;
    public int profit_state;
    public int produce_level;
    public int item_video;
    public int get_State;
    public int Days;
}
[Serializable]
public class Actor_Skill
{
    public int actor_level;
    public double actor_level_buff;
    public int actorlevel_cost;
    public int actorlevel_cost_num;
    public int is_show_redpacket_shengji;
}

[Serializable]
public class Floor_Up
{
    public int floor_total; 
    public int floor_cost;
    public int floor_cost_num;
}
[Serializable]
public class Mission_redpacket
{
   
    public int tid; 
    public string desc;
    public int status;

}
[Serializable]
public class BurseConfig
{
    public int wallet_level;
    public int wallet_limit;
    public int wallet_in;
    public int days;
    public int wallet_up;
}
[Serializable]
public class CourierDate
{
    public int Delivever_id;
    public int Delivever_floor;
    public int Busy_state;
    public int item_id;
    public int deliever_item_num;
    public double sellMoney;
}
[Serializable]
public class Courier_Up
{
    public int kuaidiyuan_num;
    public int kuaidiyuan_cost;
  
}
[Serializable]
public class My_Shop
{
    public int shop_level;
    public int shop_need_exp;


}
[Serializable]
public class My_Shop_Mission: IComparer<My_Shop_Mission>
{
    public int days_list;
    public int my_shop_mission_list_id;
    public string shop_need_exp;//任务描述
    public int mission_index;
    public int mission_reward_redpacket;
    public int mission_reward_exp;
    /// <summary>
    /// 当前的任务状态0为未完成（前往完成），2：完成可领取           3：已领取
    /// </summary>
    public int shop_mission_states;
    public int mission_type;
    public int processValue;

    public int Compare(My_Shop_Mission x, My_Shop_Mission y)
    {
       return  x.days_list. CompareTo(y.days_list);
    }
}
[Serializable]
public class WeChatInfo
{
    public string openId;
    public string headUrl;
    public string nick;
    //  public List<My_Shop> shops;
}
[Serializable]
public class Sign_up
{
    public int days;
    /// <summary>
    /// 0表示未领取，1表示领取
    /// </summary>
    public int sign_state;
    public int sign_before_getnum;
    public int sign_show_num;
}
[Serializable]
public class Big_Redpacket
{
    public int redmission_id;
    public string redmission_icon_id;
    public string redmission_text;
    public int redmission_index;
    public int redmission_reward_num;
    public int redmission_state;
}
[Serializable]
public class RedConFig
{
    public int zs_mission_id;
    public int NeedCount;
    public int ShowCount;

}
