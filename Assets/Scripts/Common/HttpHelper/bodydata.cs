using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodydata
{
    public string suuid;
    public string jwt;
    public string version_code;
    public string channel;
    public int versionCode;
    public string uid;
    public string inviteCode;
    public string package_name;

    public string tm;
    public string imei;
    public int middle_ground;
    public string androidid;
    public string oaid;
    public device device;

    public void InitBodyData( string suuid, string jwt, int versionCode, string channel, string uid,
        string inviteCode, string package_name, string tm, string imei, int middle_ground, string androidid, string oaid, device device)
    {
        this.suuid = suuid;
        this.jwt = jwt;
        this.version_code = versionCode.ToString();
        this.channel = channel;
        this.versionCode = versionCode;
        this.uid = uid;
        this.inviteCode = inviteCode;
        this.package_name = package_name;

        this.tm = tm;
        this.imei = imei;
        this.middle_ground = middle_ground;
        this.androidid = androidid;
        this.oaid = oaid;
        this.device = device;
    }
    
}

public class device
{
    public string os;
    public string suuid;
    public string idfa;
    public string imei;
    public string mac;
    public string androidId;
    public string oaid;

    public device(string os, string suuid, string idfa, string imei, string mac, string androidId, string oaid)
    {
        this.os = os;
        this.suuid = suuid;
        this.idfa = idfa;
        this.imei = imei;
        this.mac = mac;
        this.androidId = androidId;
        this.oaid = oaid;
    } 
}

public class LoginOrRegisterBodyData : bodydata
{
}
public class BindWechatBodyData : bodydata
{
    public string openId;
    public string nickName;
    public string headimgurl;
}
//查询积分接口数据结构
public class ScoreRequiryBodyData : bodydata
{
    public string score_type;
}

//增加积分接口数据结构
public class ScoreAddBodyData : bodydata
{
    public string trade_no;
    public string score_type;
    public int score_value;
    public string score_desc;
}
public class TixianBodyData : bodydata
{
    public string trade_no;
    public string mid;
    public string cid;
    public string openid;
}
public class TaskListBodyData : bodydata
{
    public string app_name;
}
public class TaskUpdateBodyData : bodydata
{
    public int id;
    public int is_append;
}

