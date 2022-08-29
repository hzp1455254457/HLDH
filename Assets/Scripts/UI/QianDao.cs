using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QianDao : MonoBehaviour
{
    public Button button;
    public GameObject[] gos;
    public Image image;
    void Awake()
    {
        button.onClick.AddListener(ClickEvent);
        JavaCallUnity.Instance.SetQiaoDaoAction += SetStates;
        UnityActionManager.Instance.AddAction("ShowQiaoDaoIcon", ShowUI);
        gameObject.SetActive(PlayerData.Instance.IsWangDianUI);
        //JavaCallUnity.Instance.SetQiaoDao("60");
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }
    public void SetStates(int value)
    {
        if (value >= 50)
        {
            gos[0].SetActive(false);
            gos[1].SetActive(true);
            image.fillAmount = 50 / 100f;
        }
        else
        {
            gos[0].SetActive(true);
            gos[1].SetActive(false);
        }
    }
  public   void ClickEvent()
    {
        AndroidAdsDialog.Instance.ShowSignDialog();

    }
    private void OnDestroy()
    {
        JavaCallUnity.Instance.SetQiaoDaoAction -= SetStates;
    }
}
