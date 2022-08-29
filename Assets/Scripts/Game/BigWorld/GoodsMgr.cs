using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using System;

public class GoodsMgr : MonoBehaviour
{
    public BigWorld BigWorld;

    public TextMeshPro DaiFaHuoTxt;
      

    [Header("货物预设")]
    public GameObject GoodsPrefab;

    [Header("货物向卡车、飞机、轮船的移动路径")]
    public CinemachinePathBase GoodsPath_KaChe, GoodsPath_FeiJi, GoodsPath_LunChuan;

    public static GoodsMgr Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        BigWorld.DaFaHuoChange += RefreashCount;
    }
   
    private void Start()
    {
        RefreashCount();
        
    }


    /// <summary>
    /// 请求发货
    /// </summary>
    /// <param name="goodsTarget">发货目的地</param>
    /// <param name="goodsValue">商品价值</param>
    public GameObject CreateGoods(TransportationType goodsTarget,int goodsValue)
    {
        RefreashCount();

        GameObject goodsObj = GameObject.Instantiate(GoodsPrefab);

        goodsObj.AddComponent<Goods>().Value = goodsValue;

        var cart = goodsObj.GetComponent<CinemachineDollyCart>();        

        //赋值路径
        switch (goodsTarget)
        {
            case TransportationType.KaChe:
                cart.m_Path = GoodsPath_KaChe;
                break;
            case TransportationType.FeiJi:
                cart.m_Path = GoodsPath_FeiJi;
                break;
            case TransportationType.LunChuan:
                cart.m_Path = GoodsPath_LunChuan;
                break;
            default:
                break;
        }
        cart.m_Speed = 4; //开始移动
        return goodsObj;
    }

    //TODO  增加时也要刷新
    public void RefreashCount()
    {
        //DaiFaHuoTxt.text = PlayerDate.Instance.datas3D.DaiFaHuo.Count + "件";
        //TODO TEST
        DaiFaHuoTxt.text = BigWorldData.DaiFaHuo.Count + "件";
    }
}
[Serializable]
public enum TransportationType
{
    KaChe=1,
    FeiJi=2,
    LunChuan=3
}
