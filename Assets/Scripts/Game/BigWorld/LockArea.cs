using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockArea : MonoBehaviour
{
    //public TransportationType TransportationType;

    public  void UnLockArea(TransportationType TransportationType)
    {
        return;

        if (!BigWorldData.IsBigWorldUnlocked)
        {
            Debug.Log("ǰ����������");
            BigWorld.Instance.GoGame();
            return;
        }
        if (TransportationType== TransportationType.FeiJi || TransportationType== TransportationType.LunChuan)
        {
            Debug.Log("���պ���");
            SevenLoginPanel.Instance.ShowUI(UIManager.Instance.showRootMain);
            //PlayerDate.Instance.gold
        }
    }
}
