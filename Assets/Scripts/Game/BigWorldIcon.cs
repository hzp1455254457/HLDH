using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWorldIcon : MonoBehaviour
{
    // Start is called before the first frame update
 public void ClickFun()
    {
        //BigWorld.Instance.GoGame();
       // ToggleManager.Instance.HideUI();
       // UIManager.Instance.SetUIStates(false);
        CamareManager.Instance.SetStates(false);
        BigWorld.Instance.GoBigWorld();
        if(!GuideManager.Instance.isFirstGame)
        ZhiBoPanel.Instance.daoHangLanManager.SetParent(UIManager.Instance.showRootMain1);
    }
}
