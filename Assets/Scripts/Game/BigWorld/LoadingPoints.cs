using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 装载货物的触发点
/// 载货点具有较高的主动权
/// 负责：
/// 1. 检测是否有代发货数量
/// 2. 检测是否有等待车辆
/// 3. 调用等待车辆出发前往载货点
/// 4. 车辆停靠后，请求发货中心发货
/// 5. 货物装载上车
/// 6. 车辆满载后，指挥车辆离开，继续检测请求下一车辆
/// 7. 车辆抵达目的地后，重新回到原点时，重新调用发货检测（只有一辆车的情况）
/// </summary>
public class LoadingPoints : MonoBehaviour
{
    public BigWorld BigWorld;
    public BigWorldData BigWorldData;
    [Header("载货点类别")]
    public TransportationType LoadingPointType;

    [HideInInspector]
    //当前停靠的运输工具
    public Transportation ParkingTransportation;

    [Header("当前载货点对接的运输工具")]
    public Transportation[] TransportationsForThisPoint;

    //已装载数量
    public int LoadedCount;



    //是否有正在前往载货点的运输工具和货物
    //避免短时间多次调用发车发货
    private bool _isComingTransportation, _isComingGoods;

    //检测是否有空闲运输工具

    //载货数量
    public GameObject LoadingCountUI;
    public TextMeshPro LoadingCountTxt;

    private GameObject _movingGoods = null;
    private void Awake()
    {
        _movingGoods = null;
        BigWorld.TransportationUnlockStateChange += RequestTransportation;
        BigWorld.DaFaHuoChange += RequestTransportation;
        BigWorld.DaFaHuoChange += RequestGoods;
        BigWorld.TransportationBack += TransportationBack;
    }
    private void Start()
    {

        if (!BigWorldData.IsBigWorldUnlocked)
        {
            return;
        }
        LoadedCount = 0;

        //尝试恢复数据
        int loadedCount = 0, loadingTransportationIndex = 0;
        Resume(ref loadedCount, ref loadingTransportationIndex);
        if (loadedCount > 0)
        {
            ResumeTransportation(loadingTransportationIndex, loadedCount);
        }
        else
        {
            RequestTransportation();
        }




    }

    //还原上次的数据
    void Resume(ref int loadedCount, ref int transportationIndex)
    {
        switch (LoadingPointType)
        {
            case TransportationType.KaChe:
                loadedCount = BigWorldData.LoadingCount_KaChe;
                break;
            case TransportationType.FeiJi:
                if (!BigWorldData.IsJieSuoFeiJi) return;
                loadedCount = BigWorldData.LoadingCount_FeiJi;
                break;
            case TransportationType.LunChuan:
                if (!BigWorldData.IsJieSuoLunChuan) return;
                loadedCount = BigWorldData.LoadingCount_LunChuan;
                break;
            default:
                break;
        }
        if (loadedCount > 0)
        {
            switch (LoadingPointType)
            {
                case TransportationType.KaChe:
                    transportationIndex = BigWorldData.LoadingTransportationIndex_KaChe;
                    break;
                case TransportationType.FeiJi:
                    transportationIndex = BigWorldData.LoadingTransportationIndex_FeiJi;
                    break;
                default:
                    break;
            }


        }
    }


    /// <summary>
    /// 发货检测，满足发货条件即启动发货流程
    /// 调用时机：
    /// 1. 场景启动
    /// 2. 上一辆车归位
    /// </summary>
    public void RequestTransportation()
    {
        //待发件数
        //if (PlayerDate.Instance.datas3D.DaiFaHuo.Count==0)
        //{
        //    return;
        //}
        //待发件数
        if (BigWorldData.DaiFaHuo.Count == 0)
        {
            return;
        }

        //正在停靠的车辆
        if (ParkingTransportation != null || _isComingTransportation)
        {
            return;
        }

        //如果有，找一辆车发出来
        for (int i = 0; i < TransportationsForThisPoint.Length; i++)
        {
            if (!TransportationsForThisPoint[i].IsUnLocked || TransportationsForThisPoint[i].TransportationState != TransportationState.Waiting)
            {
                continue;
            }
            TransportationsForThisPoint[i].Move();
            //标记有车正在前往发货点
            _isComingTransportation = true;

            //将index保存下来，恢复场景使用
            switch (LoadingPointType)
            {
                case TransportationType.KaChe:
                    BigWorldData.LoadingTransportationIndex_KaChe = i;
                    break;
                case TransportationType.FeiJi:
                    BigWorldData.LoadingTransportationIndex_FeiJi = i;
                    break;
                case TransportationType.LunChuan:
                    break;
                default:
                    break;
            }
            break;
        }

    }

    public void ResumeTransportation(int index, int loadedCount)
    {
        LoadedCount = loadedCount;
        TransportationsForThisPoint[index].Move();
        _isComingTransportation = true;
    }
    public void TransportationBack(TransportationType transportationType)
    {
        if (LoadingPointType != transportationType)
        {
            return;
        }
        RequestTransportation();

    }
    //请求发货
    public void RequestGoods()
    {
        //Debug.Log("发货~~~~");

        if (!BigWorldData.IsBigWorldUnlocked)
        {
            return;
        }
        if (ParkingTransportation == null)
        {
            return;
        }

        if (_movingGoods != null || _isComingGoods)
        {
            return;
        }


        //由于轮船停靠太近，会误触发
        if (TransportationsForThisPoint[0] is LunChuan && !BigWorldData.IsJieSuoLunChuan)
        {
            return;
        }
        //补充显示轮船UI
        if (TransportationsForThisPoint[0] is LunChuan && BigWorldData.IsJieSuoLunChuan && !LoadingCountUI.activeInHierarchy)
        {
            LoadingCountUI.SetActive(true);
            LoadingCountUI.transform.DOScale(1, 0.2f);
        }

        //if (PlayerDate.Instance.datas3D.DaiFaHuo.Count>0)
        //{ 

        //待发货物>0
        if (BigWorldData.DaiFaHuo.Count > 0)
        {
            //int goodsValue=PlayerDate.Instance.datas3D.DaiFaHuo.Dequeue();

            _isComingGoods = true;
            //发货
            int goodsValue = BigWorldData.ReduceDaiFaHuo();
            if (goodsValue>0)
            {
                _movingGoods = GoodsMgr.Instance.CreateGoods(LoadingPointType, goodsValue);
            }
          

        }

    }
    //货物销毁，装载+1
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Goods"))
        {
            _movingGoods = null;
            _isComingGoods = false;
            ParkingTransportation.GoodsValue += other.GetComponent<Goods>().Value;
            //销毁
            Destroy(other.gameObject);

            Log.Print("货物装载+1");
            LoadedCount++;

            SaveLoadedCount();


            //刷新已装载数量
            RefreshLoadingCountUI();

            //判断装载量是否已满，如果已满，继续Move,如果未满，继续发货
            if (LoadedCount >= ParkingTransportation.Capacity)
            {
                //已满载
                TransportationSendAway();
            }
            else
            {
                //未满载，请求发货
                RequestGoods();
            }
        }
        //装载工具，停车等待
        else if (other.gameObject.CompareTag("Transportation"))
        {

            Log.Print("交通工具已到达");
            _isComingTransportation = false;
            ParkingTransportation = other.gameObject.GetComponent<Transportation>();
            ParkingTransportation.Stop();


            //由于轮船停靠太近，会误触发
            if (TransportationsForThisPoint[0] is LunChuan && !BigWorldData.IsJieSuoLunChuan)
            {
                return;
            }

            ParkingTransportation.TransportationState = TransportationState.Loading;
            //显示装载量和容量
            LoadingCountUI.SetActive(true);
            LoadingCountUI.transform.DOScale(1, 0.2f);
            //刷新已装载数量
            RefreshLoadingCountUI();
            //请求发货
            RequestGoods();
        }
    }
    //运输工具满载驶离
    private void TransportationSendAway()
    {
        ParkingTransportation.Move();

        //Debug.Log("运输工具驶离");
        ParkingTransportation.TransportationState = TransportationState.Transporting;
        ParkingTransportation = null;
        //清除已装载数量，隐藏装载UI
        LoadedCount = 0;
        SaveLoadedCount();
        LoadingCountUI.transform.DOScale(0, 0.2f).OnComplete(() => { LoadingCountTxt.text = ""; LoadingCountUI.SetActive(false); });
        //请求发下一辆车
        RequestTransportation();

    }
    //将载货数据保存，用于场景恢复
    void SaveLoadedCount()
    {
        switch (LoadingPointType)
        {
            case TransportationType.KaChe:
                BigWorldData.LoadingCount_KaChe = LoadedCount;
                break;
            case TransportationType.FeiJi:
                BigWorldData.LoadingCount_FeiJi = LoadedCount;
                break;
            case TransportationType.LunChuan:
                BigWorldData.LoadingCount_LunChuan = LoadedCount;
                break;
            default:
                break;
        }
    }
    //刷新装载数量
    void RefreshLoadingCountUI()
    {
        LoadingCountTxt.text = LoadedCount + "/" + ParkingTransportation.Capacity;
    }
}
