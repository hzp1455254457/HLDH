using UnityEngine;
using Cinemachine;
/// <summary>
/// 运输工具的父类
/// </summary>
[RequireComponent(typeof(CinemachineDollyCart))]
public abstract class Transportation : MonoBehaviour
{

    public BigWorld BigWorld;
    public BigWorldData BigWorldData;

    /// <summary>
    /// 第几辆
    /// </summary>
    public int Index;

    /// <summary>
    /// 移动速度
    /// </summary>
    public int MoveSpeed;

    [HideInInspector]
    public CinemachineDollyCart CinemachineDollyCart;

    /// <summary>
    /// 正在装载的卡车
    /// </summary>
    public static GameObject LoadingKache;
    /// <summary>
    /// 正在装载的飞机
    /// </summary>
    public static GameObject LoadingFeiJi;

    /// <summary>
    /// 运输单元的状态
    /// </summary>
    public TransportationState TransportationState;

    /// <summary>
    /// 是否已解锁
    /// </summary>
    public bool IsUnLocked;

    /// <summary>
    /// 装载容量
    /// </summary>
    public int Capacity;

    /// <summary>
    /// 上次未领取的金币
    /// </summary>
    [HideInInspector]
    public int LastGoodsValue;

    /// <summary>
    /// 待领取金币(运载货物价值)
    /// </summary>
    [HideInInspector]
    public int GoodsValue;

    /// <summary>
    /// 解锁UI、待领取粒子效果
    /// </summary>
    public GameObject JieSuoUI, DaiLingQu;

    /// <summary>
    /// 解锁需要金币数量
    /// </summary>
    public int UnLockCost;


    /// <summary>
    /// 是否显示已满提示（有待领取金币，下次再有金币过来，就会显示已满）
    /// </summary>
    public bool IsCoinsFull;

    public virtual void Awake()
    {
        LoadingKache = null;
        LoadingFeiJi = null;

        CinemachineDollyCart = GetComponent<CinemachineDollyCart>();

        //区域解锁
        BigWorld.LockAreaChange += Refresh;
        //运输工具解锁
        BigWorld.TransportationUnlockStateChange += Refresh;
    }


    //状态检查、UI显示
    public virtual void OnEnable()
    {
        Refresh();
    }
    public virtual void Start()
    {
       
       
    }
    void Refresh()
    {
        //if (!PlayerDate.Instance.GetIsSevenRedCount())
        //{
        //    return;
        //}
        //TODO TEST
        if (!BigWorldData.IsBigWorldUnlocked)
        {
            return;
        }
        ////区域解锁，才可以刷新和显示UI
        //if (this is KaChe || (this is FeiJi && PlayerDate.Instance.Datas3D.IsJieSuoFeiJi) || (this is LunChuan && PlayerDate.Instance.Datas3D.IsJieSuoLunChuan))
        //{
        //    RefreshUnLockState();
        //    RefreshUIState();
        //}
        //TODO TEST
        //区域解锁，才可以刷新和显示UI
        if (this is KaChe || (this is FeiJi && BigWorldData.IsJieSuoFeiJi) || (this is LunChuan && BigWorldData.IsJieSuoLunChuan))
        {
            RefreshUnLockState();
            RefreshUIState();
        }
    }

    //用来刷新解锁状态
    public virtual void RefreshUnLockState()
    {

        //解锁
        if (this is LunChuan)
        {
            IsUnLocked = true;
            if (TransportationState == TransportationState.Locked)
            {
                TransportationState = TransportationState.Waiting;
            }
        }
        else if (this is FeiJi)
        {
            //if (PlayerDate.Instance.Datas3D.FeiJIJieSuoLevel >= Index)
            //{
            //    IsUnLocked = true;
            //}
            //TODO TEST
            if (BigWorldData.FeiJIJieSuoLevel >= Index)
            {
                IsUnLocked = true;
                if (JieSuoUI)
                {
                    JieSuoUI.SetActive(false);
                }
                if (TransportationState == TransportationState.Locked)
                {
                    TransportationState = TransportationState.Waiting;
                }
            }
        }
        else if (this is KaChe)
        {
            //if (PlayerDate.Instance.Datas3D.CarJieSuoLevel >= Index)
            //{
            //    IsUnLocked = true;
            //}
            //TODO TEST
            if (BigWorldData.CarJieSuoLevel >= Index)
            {
                IsUnLocked = true;
                if (JieSuoUI)
                {
                    JieSuoUI.SetActive(false);
                }
                if (TransportationState == TransportationState.Locked)
                {
                    TransportationState = TransportationState.Waiting;
                }
            }
        }



    }

    //刷新UI状态，决定显示解锁、待领取、还是不显示
    public virtual void RefreshUIState()
    {
        if (!IsUnLocked)
        {
            JieSuoUI.SetActive(true);
            return;
        }
        if (LastGoodsValue > 0)
        {
            DaiLingQu.SetActive(true);
            //赋值货物价值
            DaiLingQu.GetComponent<DaiLingQu>().CoinValue = LastGoodsValue;
        }
    }

    public virtual void Move()
    {
        CinemachineDollyCart.m_Speed = MoveSpeed;
    }
    public virtual void Stop()
    {
        CinemachineDollyCart.m_Speed = 0;
    }
    public virtual void Reset()
    {
        CinemachineDollyCart.m_Position = 0;
        Stop();
        TransportationState = TransportationState.Waiting;
        RefreshUnLockState();
        RefreshUIState();
    }


    public abstract void OnSceneLoaded();

    public virtual void Update()
    {
        if (TransportationState == TransportationState.Transporting)
        {
            //到达终点
            if (CinemachineDollyCart.m_Position == CinemachineDollyCart.m_Path.PathLength)
            {
                //可收获金币
                LastGoodsValue = GoodsValue;
                DaiLingQu.SetActive(true);
                //赋值货物价值
                DaiLingQu.GetComponent<DaiLingQu>().CoinValue = LastGoodsValue;
                GoodsValue = 0;
                //返回起始点
                Reset();
                TransportationType transportationType;
                if (this is KaChe)
                {
                    transportationType = TransportationType.KaChe;
                }
                else if (this is FeiJi)
                {
                    transportationType = TransportationType.FeiJi;
                }
                else
                {
                    transportationType = TransportationType.LunChuan;
                }
                BigWorld.TransportationBack?.Invoke(transportationType);
            }
        }
    }
}
/// <summary>
/// 运输单元的状态
/// </summary>
public enum TransportationState
{
    /// <summary>
    /// 未解锁
    /// </summary>
    Locked = 1,
    /// <summary>
    /// 停车等待
    /// </summary>
    Waiting = 2,
    /// <summary>
    /// 正在装货
    /// </summary>
    Loading = 3,
    /// <summary>
    /// 正在运输
    /// </summary>
    Transporting = 4
}
