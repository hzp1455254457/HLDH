using UnityEngine;
using Cinemachine;
/// <summary>
/// ���乤�ߵĸ���
/// </summary>
[RequireComponent(typeof(CinemachineDollyCart))]
public abstract class Transportation : MonoBehaviour
{

    public BigWorld BigWorld;
    public BigWorldData BigWorldData;

    /// <summary>
    /// �ڼ���
    /// </summary>
    public int Index;

    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    public int MoveSpeed;

    [HideInInspector]
    public CinemachineDollyCart CinemachineDollyCart;

    /// <summary>
    /// ����װ�صĿ���
    /// </summary>
    public static GameObject LoadingKache;
    /// <summary>
    /// ����װ�صķɻ�
    /// </summary>
    public static GameObject LoadingFeiJi;

    /// <summary>
    /// ���䵥Ԫ��״̬
    /// </summary>
    public TransportationState TransportationState;

    /// <summary>
    /// �Ƿ��ѽ���
    /// </summary>
    public bool IsUnLocked;

    /// <summary>
    /// װ������
    /// </summary>
    public int Capacity;

    /// <summary>
    /// �ϴ�δ��ȡ�Ľ��
    /// </summary>
    [HideInInspector]
    public int LastGoodsValue;

    /// <summary>
    /// ����ȡ���(���ػ����ֵ)
    /// </summary>
    [HideInInspector]
    public int GoodsValue;

    /// <summary>
    /// ����UI������ȡ����Ч��
    /// </summary>
    public GameObject JieSuoUI, DaiLingQu;

    /// <summary>
    /// ������Ҫ�������
    /// </summary>
    public int UnLockCost;


    /// <summary>
    /// �Ƿ���ʾ������ʾ���д���ȡ��ң��´����н�ҹ������ͻ���ʾ������
    /// </summary>
    public bool IsCoinsFull;

    public virtual void Awake()
    {
        LoadingKache = null;
        LoadingFeiJi = null;

        CinemachineDollyCart = GetComponent<CinemachineDollyCart>();

        //�������
        BigWorld.LockAreaChange += Refresh;
        //���乤�߽���
        BigWorld.TransportationUnlockStateChange += Refresh;
    }


    //״̬��顢UI��ʾ
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
        ////����������ſ���ˢ�º���ʾUI
        //if (this is KaChe || (this is FeiJi && PlayerDate.Instance.Datas3D.IsJieSuoFeiJi) || (this is LunChuan && PlayerDate.Instance.Datas3D.IsJieSuoLunChuan))
        //{
        //    RefreshUnLockState();
        //    RefreshUIState();
        //}
        //TODO TEST
        //����������ſ���ˢ�º���ʾUI
        if (this is KaChe || (this is FeiJi && BigWorldData.IsJieSuoFeiJi) || (this is LunChuan && BigWorldData.IsJieSuoLunChuan))
        {
            RefreshUnLockState();
            RefreshUIState();
        }
    }

    //����ˢ�½���״̬
    public virtual void RefreshUnLockState()
    {

        //����
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

    //ˢ��UI״̬��������ʾ����������ȡ�����ǲ���ʾ
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
            //��ֵ�����ֵ
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
            //�����յ�
            if (CinemachineDollyCart.m_Position == CinemachineDollyCart.m_Path.PathLength)
            {
                //���ջ���
                LastGoodsValue = GoodsValue;
                DaiLingQu.SetActive(true);
                //��ֵ�����ֵ
                DaiLingQu.GetComponent<DaiLingQu>().CoinValue = LastGoodsValue;
                GoodsValue = 0;
                //������ʼ��
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
/// ���䵥Ԫ��״̬
/// </summary>
public enum TransportationState
{
    /// <summary>
    /// δ����
    /// </summary>
    Locked = 1,
    /// <summary>
    /// ͣ���ȴ�
    /// </summary>
    Waiting = 2,
    /// <summary>
    /// ����װ��
    /// </summary>
    Loading = 3,
    /// <summary>
    /// ��������
    /// </summary>
    Transporting = 4
}
