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
      

    [Header("����Ԥ��")]
    public GameObject GoodsPrefab;

    [Header("�����򿨳����ɻ����ִ����ƶ�·��")]
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
    /// ���󷢻�
    /// </summary>
    /// <param name="goodsTarget">����Ŀ�ĵ�</param>
    /// <param name="goodsValue">��Ʒ��ֵ</param>
    public GameObject CreateGoods(TransportationType goodsTarget,int goodsValue)
    {
        RefreashCount();

        GameObject goodsObj = GameObject.Instantiate(GoodsPrefab);

        goodsObj.AddComponent<Goods>().Value = goodsValue;

        var cart = goodsObj.GetComponent<CinemachineDollyCart>();        

        //��ֵ·��
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
        cart.m_Speed = 4; //��ʼ�ƶ�
        return goodsObj;
    }

    //TODO  ����ʱҲҪˢ��
    public void RefreashCount()
    {
        //DaiFaHuoTxt.text = PlayerDate.Instance.datas3D.DaiFaHuo.Count + "��";
        //TODO TEST
        DaiFaHuoTxt.text = BigWorldData.DaiFaHuo.Count + "��";
    }
}
[Serializable]
public enum TransportationType
{
    KaChe=1,
    FeiJi=2,
    LunChuan=3
}
