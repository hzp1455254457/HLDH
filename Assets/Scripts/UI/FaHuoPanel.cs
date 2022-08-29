using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FaHuoPanel : PanelBase
{
    
    public static FaHuoPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("FaHuoPanel")) as FaHuoPanel;
                instance.Hide();
                //instance. Init();
                return instance;
            }
            return instance;
        }
    }

    static FaHuoPanel instance;
    // EventQueueSystem eventQueueSystem;
    public DanBangManager danBangManager;
    public CarManager carManager;
    public ZiDongFaHuo ziDongFaHuo;
    public FaHuoTiMing faHuoTiMing;
    public FaHuo faHuo;
    public FaHuoGuide faHuoGuide;
    public FaHuoToggle faHuoToggle;
    public Transform[] fahuoBorns;
    public DaoHangLanManager daoHangLanManager;

    public GameObject[] gameObjects;
    public NewWangDianIcon newWangDianIcon;
    public void MoveDanBang()
    {
        danBangManager.MoveLeft();
        
    }
    public void RecoverDanBang()
    {
        danBangManager.MoveRight();
    }
    protected override void Awake()
    {
        base.Awake();
        instance = this;
        UnityActionManager.Instance.AddAction("FahuoTips", ShowTips);
    }
    public void HideTips()
    {
        isTips = false;
        foreach (var item in gameObjects)
        {
            item.SetActive(isTips);
        }
    }
    public void ShowTips()
    {
        isTips = true;
            foreach (var item in gameObjects)
            {
                item.SetActive(true);
            }
        
    }
    public bool isTips=false;
 
    public override void Init()
    {
        base.Init();
        
    }
    public Transform btnTf;
  void  HideUI()
    {
        gameObject.SetActive(false);
    }
    int clickCount = 0;
    public override void SetHideOrShow(bool value)
    {
     daoHangLanManager.SetShow(value);
        base.SetHideOrShow(value);
        CamareManager.Instance.SetUIFirst(!value);
        if (value)
        { if (GuideManager.Instance.isFirstGame)
            {
                clickCount++;
                if (clickCount == 1)
                {
                    PeopleEffect.Instance.HideMask();
                    PeopleEffect.Instance.HideTips();
                    ToggleManager.Instance.HideUI();
                    StartCoroutine(Global.Delay(1f, EventQueueSystem.Instance.PlayerEvent));
                    StartCoroutine(Global.Delay(3f, faHuoGuide.GuideFuncEvent));
                   
                }
                newWangDianIcon.gameObject.SetActive(false);
            }
            else
            {
                newWangDianIcon.gameObject.SetActive(true);
                PlayerData.Instance.AddClickFaHuoCount();
                ToggleManager.Instance.HideTips(2);
                if (ToggleManager.Instance.isShowFaHuoTips)
                {if (!faHuoToggle.tipsGo.activeSelf)
                    faHuoToggle.tipsGo.SetActive(true);
                }
                if (!PlayerData.Instance.IsWangDianUI)
                {
                    newWangDianIcon.transform.localScale = Vector3.zero;
                    newWangDianIcon.transform.DOScale(1, 1f).SetEase(Ease.InOutQuint);
                }
                PlayerData.Instance.IsWangDianUI = true;
            }
            btnTf.SetParent(UIManager.Instance._ToggleCanvas.transform,false);
        }
        else
        {
            ProduceQiPaoManager.Instance.RefreshQiPaoCount();
            btnTf.SetParent(faHuoTiMing.transform,false);
        }
    }
}