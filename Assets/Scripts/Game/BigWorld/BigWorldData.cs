using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWorldData : MonoBehaviour
{
    public static BigWorldData Instance;

    public BigWorld BigWorld;

    #region 主要数据

    // 大世界是否可用。读取七日红包情况。
    private static bool isBigWorldUnlocked;
    public static bool IsBigWorldUnlocked
    {
        get
        {
            isBigWorldUnlocked = PlayerData.Instance.IsSevenRedCount;
            return isBigWorldUnlocked;
            //return true;
        }
        set
        {
            isBigWorldUnlocked = value;
            BigWorld.Instance.LockAreaChange?.Invoke();
        }
    }


    //待发货
    public static List<int> DaiFaHuo;
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BigWorldData.Instance.AddDaiFaHuo(11);
            BigWorldData.Instance.AddDaiFaHuo(12);
            BigWorldData.Instance.AddDaiFaHuo(13);
            BigWorldData.Instance.AddDaiFaHuo(14);
            BigWorldData.Instance.AddDaiFaHuo(15);
            BigWorldData.Instance.AddDaiFaHuo(11);
            BigWorldData.Instance.AddDaiFaHuo(12);
            BigWorldData.Instance.AddDaiFaHuo(13);
            BigWorldData.Instance.AddDaiFaHuo(14);
            BigWorldData.Instance.AddDaiFaHuo(15);
            BigWorldData.Instance.AddDaiFaHuo(11);
            BigWorldData.Instance.AddDaiFaHuo(12);
            BigWorldData.Instance.AddDaiFaHuo(13);
            BigWorldData.Instance.AddDaiFaHuo(14);
            BigWorldData.Instance.AddDaiFaHuo(15);

            BigWorld.Instance.DaFaHuoChange?.Invoke();
        }
    }
#endif
    /// <summary>
    /// 往待发货中增加数据
    /// </summary>
    /// <param name="goodValue">货物单价</param>
    public void AddDaiFaHuo(int goodsValue)
    {
        DaiFaHuo.Add(goodsValue);
        BigWorld.DaFaHuoChange?.Invoke();
    }
    /// <summary>
    /// 发出一件货物
    /// </summary>
    /// <returns>货物价值</returns>
    public int ReduceDaiFaHuo()
    {
        int value = 0;
        if (DaiFaHuo.Count > 0)
        {
            value = DaiFaHuo[0];
            DaiFaHuo.RemoveAt(0);
            BigWorld.DaFaHuoChange?.Invoke();
        }
        return value;
    }

    // 轮船区域是否解锁
    private bool isJieSuoLunChuan;
    public bool IsJieSuoLunChuan
    {
        get => isJieSuoLunChuan;
        set
        {
            isJieSuoLunChuan = value;
            BigWorld.LockAreaChange?.Invoke();
            BigWorld.TransportationUnlockStateChange?.Invoke();
        }
    }

    //飞机区域是否解锁
    private bool isJieSuoFeiJi;

    public bool IsJieSuoFeiJi
    {
        get => isJieSuoFeiJi;
        set
        {
            isJieSuoFeiJi = value;
            BigWorld.LockAreaChange?.Invoke();
            BigWorld.TransportationUnlockStateChange?.Invoke();
        }
    }

    /// <summary>
    /// 卡车解锁等级，默认1
    /// </summary>
    private int carJieSuoLevel = 1;
    public int CarJieSuoLevel
    {
        get => carJieSuoLevel;
        set
        {
            carJieSuoLevel = value;
            BigWorld.TransportationUnlockStateChange?.Invoke();
        }
    }



    /// <summary>
    /// 飞机解锁等级，默认1
    /// </summary>
    private int feiJIJieSuoLevel = 1;
    public int FeiJIJieSuoLevel
    {
        get => feiJIJieSuoLevel;
        set
        {
            feiJIJieSuoLevel = value;
            BigWorld.TransportationUnlockStateChange?.Invoke();
        }
    }


    private int loadingCount_KaChe;
    public int LoadingCount_KaChe
    {
        get
        {
            return loadingCount_KaChe;
        }
        set
        {
            loadingCount_KaChe = value;
        }
    }

    private int loadingCount_FeiJi;
    public int LoadingCount_FeiJi
    {
        get
        {
            return loadingCount_FeiJi;
        }
        set
        {
            loadingCount_FeiJi = value;
        }
    }
    private int loadingCount_LunChuan;
    public int LoadingCount_LunChuan
    {
        get
        {
            return loadingCount_LunChuan;
        }
        set
        {
            loadingCount_LunChuan = value;
        }
    }

    private int loadingTransportationIndex_KaChe;
    public int LoadingTransportationIndex_KaChe
    {
        get
        {
            return loadingTransportationIndex_KaChe;
        }
        set
        {
            loadingTransportationIndex_KaChe = value;
        }
    }

    private int loadingTransportationIndex_FeiJi;
    public int LoadingTransportationIndex_FeiJi
    {
        get
        {
            return loadingTransportationIndex_FeiJi;
        }
        set
        {
            loadingTransportationIndex_FeiJi = value;
        }
    }


    #endregion

    #region  Key
    readonly string DAIFAHUO = "DaiFaHuo";
    readonly string ISJIESUOLUNCHUAN = "IsJieSuoLunChuan";
    readonly string ISJIESUOFEIJI = "IsJieSuoFeiJi";
    readonly string CARJIESUOLEVEL = "CarJieSuoLevel";
    readonly string FEIJIJIESUOLEVEL = "FeiJIJieSuoLevel";

    ////载货点正在装载的数量
    readonly string LOADINGCOUNT_KACHE = "LoadingCount_KaChe";
    readonly string LOADINGCOUNT_FEIJI = "LoadingCount_FeiJi";
    readonly string LOADINGCOUNT_LUNCHUAN = "LoadingCount_LunChuan";

    //载货点正在装载的运输工具index
    readonly string LOADINGTRANSPORTATIONINDEX_KACHE = "LoadingTransportationIndex_KaChe";
    readonly string LOADINGTRANSPORTATIONINDEX_FEIJI = "LoadingTransportationIndex_FeiJi";

    #endregion
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        ReadData();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }

    void SaveData()
    {
        var str = JsonMapper.ToJson(DaiFaHuo);

        //DataSaver.Instance.SetString
        DataSaver.Instance.SetString(DAIFAHUO, JsonMapper.ToJson(DaiFaHuo));
        DataSaver.Instance.SetBool(ISJIESUOFEIJI, IsJieSuoFeiJi);
        DataSaver.Instance.SetBool(ISJIESUOLUNCHUAN, IsJieSuoLunChuan);
        DataSaver.Instance.SetInt(FEIJIJIESUOLEVEL, FeiJIJieSuoLevel);
        DataSaver.Instance.SetInt(CARJIESUOLEVEL, CarJieSuoLevel);

        //载货点数据
        DataSaver.Instance.SetInt(LOADINGCOUNT_KACHE, LoadingCount_KaChe);
        DataSaver.Instance.SetInt(LOADINGCOUNT_FEIJI, LoadingCount_FeiJi);
        DataSaver.Instance.SetInt(LOADINGCOUNT_LUNCHUAN, LoadingCount_LunChuan);

        DataSaver.Instance.SetInt(LOADINGTRANSPORTATIONINDEX_KACHE, LoadingTransportationIndex_KaChe);
        DataSaver.Instance.SetInt(LOADINGTRANSPORTATIONINDEX_FEIJI, LoadingTransportationIndex_FeiJi);

    }

    void ReadData()
    {
        DaiFaHuo = JsonMapper.ToObject<List<int>>(DataSaver.Instance.GetString(DAIFAHUO, JsonMapper.ToJson(new Queue<int>())));
        if (DaiFaHuo == null)
        {
            DaiFaHuo = new List<int>();
        }
        IsJieSuoFeiJi = DataSaver.Instance.GetBool(ISJIESUOFEIJI);
        IsJieSuoLunChuan = DataSaver.Instance.GetBool(ISJIESUOLUNCHUAN);
        CarJieSuoLevel = DataSaver.Instance.GetInt(CARJIESUOLEVEL, 1);
        FeiJIJieSuoLevel = DataSaver.Instance.GetInt(FEIJIJIESUOLEVEL, 1);

        //载货点数据
        LoadingCount_KaChe = DataSaver.Instance.GetInt(LOADINGCOUNT_KACHE, 0);
        LoadingCount_LunChuan = DataSaver.Instance.GetInt(LOADINGCOUNT_LUNCHUAN, 0);
        LoadingCount_FeiJi = DataSaver.Instance.GetInt(LOADINGCOUNT_FEIJI, 0);

        LoadingTransportationIndex_KaChe = DataSaver.Instance.GetInt(LOADINGTRANSPORTATIONINDEX_KACHE, 0);
        LoadingTransportationIndex_FeiJi = DataSaver.Instance.GetInt(LOADINGTRANSPORTATIONINDEX_FEIJI, 0);
    }

}
