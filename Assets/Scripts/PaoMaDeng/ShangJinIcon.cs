using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShangJinIcon : MonoBehaviour
{
    public Text text;
    void Start()
    {
        Init();
        UnityActionManager.Instance.AddAction("RefreshDay", Init);
    }

    private void Init()
    {
        text.text = "��" + PlayerData.Instance.day + "��";
    }
    public void ClickFun()
    {
      //  Debug.LogError("���");
        ShangJinTaskPanel.Instance.ShowUI();
        AndroidAdsDialog.Instance.UploadDataEvent("click_hbdjs");
    }
}
