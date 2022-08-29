using UnityEngine;

public class LockMgr : MonoBehaviour
{
    public BigWorld BigWorld;
    public BigWorldData BigWorldData;
    //��ͬ���乤�ߵĽ����������֡�������ť
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
        //�����δ7�յ�½��ȫ������
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

    #region ��������
    /// <summary>
    /// ��ť�������
    /// </summary>
    /// <param name="transportationType">�������TransportationTypeö�ٵ�intֵ</param>
    public void UnLockArea(int transportationType)
    {
        //TODO ��ʱȫ�����ν�������
        return;
        TransportationType type = (TransportationType)transportationType;
        //�������Ƿ���ã�40�����������
        if (!BigWorldData.IsBigWorldUnlocked)
        {
            //Debug.Log("ǰ����������");
            //BigWorld.GoGame();

            //��������ã�ֱ�Ӻ��Ե��
            return;
        }
        //����Ѵﵽ40��������������δ�������򣬵������պ���ҳ��
        if (type == TransportationType.FeiJi || type == TransportationType.LunChuan)
        {
            Debug.Log("���պ���");
            SevenLoginPanel.Instance.ShowUI(UIManager.Instance.showRootMain);
        }
    }
    #endregion

    #region �����ȼ�����

    public void UnlockKaChe( int costCoins)
    {
      
        //PanelGoldBox.Instance.ShowUI
        Debug.Log("��������������"+costCoins);

        //���Կ۳����
        bool tf= PlayerData.Instance.Expend(costCoins);

        if (tf)
        {
            //�۳��ɹ�������
            BigWorldData.CarJieSuoLevel += 1;
        }
        else
        {
            //���ý�Ҳ��㵯��
            AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget1, "��Ҳ���", Color.black, null, null, 1.5f); 
            //PanelGoldBox.Instance.ShowUI(costCoins, (int)PlayerDate.Instance.gold, null, null);
        }
       
    }
    public void UnlockFeiJi(int costCoins)
    {
        //PanelGoldBox.Instance.ShowUI
        Debug.Log("�����ɻ�������" + costCoins);

        //���Կ۳����
        bool tf = PlayerData.Instance.Expend(costCoins);

        if (tf)
        {
            //�۳��ɹ�������
            BigWorldData.FeiJIJieSuoLevel += 1;
        }
        else
        {
            //���ý�Ҳ��㵯��
            //TODO �����Ҳ��㵯���Ļص�
            PanelGoldBox.Instance.ShowUI(costCoins, costCoins-(int)PlayerData.Instance.gold, null, null);
        }
    }

    #endregion
}
