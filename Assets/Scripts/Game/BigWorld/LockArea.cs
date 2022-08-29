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
            Debug.Log("前往发货中心");
            BigWorld.Instance.GoGame();
            return;
        }
        if (TransportationType== TransportationType.FeiJi || TransportationType== TransportationType.LunChuan)
        {
            Debug.Log("七日好礼");
            SevenLoginPanel.Instance.ShowUI(UIManager.Instance.showRootMain);
            //PlayerDate.Instance.gold
        }
    }
}
