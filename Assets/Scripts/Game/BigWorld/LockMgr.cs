using UnityEngine;

public class LockMgr : MonoBehaviour
{
    public BigWorld BigWorld;
    public BigWorldData BigWorldData;
    //不同运输工具的解锁区域遮罩、解锁按钮
    public GameObject KaCheLockArea , KaCheLockAreaBtn;
    public GameObject LunChuanLockArea, LunChuanLockAreaBtn;
    public GameObject FeiJiLockArea, FeiJiLockAreaBtn;

    public static LockMgr Instance;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        BigWorld.LockAreaChange += RefreshLockState;
    }
    private void Start()
    {
       
    }

    private void OnEnable()
    {
        RefreshLockState();
    }
    public void RefreshLockState()
    {
        //如果尚未7日登陆，全部锁定
        //if (!PlayerDate.Instance.GetIsSevenRedCount())
        //{
        //    KaCheLockArea.SetActive(true);
        //    LunChuanLockArea.SetActive(true);
        //    FeiJiLockArea.SetActive(true);
        //    WorldCanvas.SetActive(true);
        //} 
        //TODO TEST
        if (!BigWorldData.IsBigWorldUnlocked)
        {
            KaCheLockArea.SetActive(true);
            KaCheLockAreaBtn.SetActive(true);
            LunChuanLockArea.SetActive(true);
            LunChuanLockAreaBtn.SetActive(true);
            FeiJiLockArea.SetActive(true);
            FeiJiLockAreaBtn.SetActive(true);
        }
        else
        {
            //FeiJiLockArea.SetActive(!PlayerDate.Instance.Datas3D.IsJieSuoFeiJi);
            //LunChuanLockArea.SetActive(!PlayerDate.Instance.Datas3D.IsJieSuoLunChuan);
            KaCheLockArea.SetActive(false);
            KaCheLockAreaBtn.SetActive(false);
            //TODO TEST
            FeiJiLockArea.SetActive(!BigWorldData.IsJieSuoFeiJi);
            FeiJiLockAreaBtn.SetActive(!BigWorldData.IsJieSuoFeiJi);
            LunChuanLockArea.SetActive(!BigWorldData.IsJieSuoLunChuan);
            LunChuanLockAreaBtn.SetActive(!BigWorldData.IsJieSuoLunChuan);
        }
    }

    #region 解锁区域
    /// <summary>
    /// 按钮点击调用
    /// </summary>
    /// <param name="transportationType">传入的是TransportationType枚举的int值</param>
    public void UnLockArea(int transportationType)
    {
        //TODO 暂时全部屏蔽解锁功能
        return;
        TransportationType type = (TransportationType)transportationType;
        //大世界是否可用（40个发货红包）
        if (!BigWorldData.IsBigWorldUnlocked)
        {
            //Debug.Log("前往发货中心");
            //BigWorld.GoGame();

            //如果不可用，直接忽略点击
            return;
        }
        //如果已达到40个发货红包，点击未解锁区域，弹出七日好礼页面
        if (type == TransportationType.FeiJi || type == TransportationType.LunChuan)
        {
            Debug.Log("七日好礼");
            SevenLoginPanel.Instance.ShowUI(UIManager.Instance.showRootMain);
        }
    }
    #endregion

    #region 解锁等级升级

    public void UnlockKaChe( int costCoins)
    {
      
        //PanelGoldBox.Instance.ShowUI
        Debug.Log("解锁卡车，消费"+costCoins);

        //尝试扣除金币
        bool tf= PlayerData.Instance.Expend(costCoins);

        if (tf)
        {
            //扣除成功，升级
            BigWorldData.CarJieSuoLevel += 1;
        }
        else
        {
            //调用金币不足弹窗
            AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget1, "金币不足", Color.black, null, null, 1.5f); 
            //PanelGoldBox.Instance.ShowUI(costCoins, (int)PlayerDate.Instance.gold, null, null);
        }
       
    }
    public void UnlockFeiJi(int costCoins)
    {
        //PanelGoldBox.Instance.ShowUI
        Debug.Log("解锁飞机，消费" + costCoins);

        //尝试扣除金币
        bool tf = PlayerData.Instance.Expend(costCoins);

        if (tf)
        {
            //扣除成功，升级
            BigWorldData.FeiJIJieSuoLevel += 1;
        }
        else
        {
            //调用金币不足弹窗
            //TODO 传入金币不足弹窗的回调
            PanelGoldBox.Instance.ShowUI(costCoins, costCoins-(int)PlayerData.Instance.gold, null, null);
        }
    }

    #endregion
}
