using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ZiDongFaHuo : MonoBehaviour
{
    //public Graphic[] graphics;
    public Transform ShouDongFaHuoTf;
    public GameObject zidongFaHuoGo;
    public GameObject shoudongFaHuoGo,shouDongBtn;
    public CanvasGroup canvasGroup;
    public bool IsZiDong;
    public static int count=40;
    public Text countText;
    public Button button;
  
    private void Awake()
    {
       // graphics = ShouDongFaHuoTf.GetComponentsInChildren<Graphic>();
        //if (PlayerData.Instance.ClickFaHuoRedCount >= count)
        //{
        //    zidongFaHuoGo.SetActive(true);
        //    shoudongFaHuoGo.SetActive(false);
        //    IsZiDong = true;
        //    shouDongBtn.SetActive(false);
        //}
        //else
        //{
        //    IsZiDong = false;
        //}
    }
    public void ClickFun()
    {
        ToggleManager.Instance.ShowPanel(1);
        CamareManager.Instance.SetStates(false);
        BigWorld.Instance.GoBigWorld();

        if (!GuideManager.Instance.isFirstGame)
            ZhiBoPanel.Instance.daoHangLanManager.SetParent(UIManager.Instance.showRootMain1);
    }
    private void Start()
    {

        RefreshCount();
        BigWorld.Instance.DaFaHuoChange += RefreshCount;
        button.onClick.AddListener(ClickFun);
    }
    private void RefreshCount()
    {
        countText.text= BigWorldData.DaiFaHuo.Count.ToString() + "¼þ";
    }
    public void StartZiDongFaHuo()
    {
        canvasGroup.DOFade(0, 1).onComplete+=()=> {
            shoudongFaHuoGo.SetActive(false);
            shouDongBtn.SetActive(false);
            zidongFaHuoGo.SetActive(true);
            IsZiDong = true;
            FaHuoPanel.Instance.faHuoToggle.ZiDongFaHuoNew();
        };
        
    }
    private void OnDestroy()
    {
        BigWorld.Instance.DaFaHuoChange -= RefreshCount;

    }
}
