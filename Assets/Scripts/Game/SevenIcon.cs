using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SevenIcon : MonoBehaviour
{
    public GameObject gO;
    private void Start()
    {
      Init();
        UnityActionManager.Instance.AddAction("RefreshSevenIcon",Init);
    }

    private void Init()
    {
      gO.SetActive(!SevenLoginPanel.Instance.IsGet);
        
    }
    
    public void ClickFun()
    {
        SevenLoginPanel.Instance.ShowUI(UIManager.Instance.showRootMain);
    }
}
