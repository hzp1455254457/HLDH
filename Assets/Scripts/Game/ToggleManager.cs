using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;
public class ToggleManager : MonoSingleton<ToggleManager>
{
    public Toggle[] toggles;
    public RectTransform guiTaget1, guiTaget2;
    public RectTransform guiTaget3, guiTaget4;
    public RectTransform guiTaget5, guiTaget6;
    string[] panelName = new string[] { "MyShopPanel", "Panel_ZhiBo", "FaHuoPanel" };
    public SkeletonGraphic skeletonGraphic;
    public SkeletonGraphic zuanshiSkeletonGraphic;
    public SkeletonGraphic faHuoSkeletonGraphic;
    public SkeletonGraphic zuanshiBuZuSkeletonGraphic;
    public GameObject[] tipsGos;
    public Transform effectTagetTF;
    [Tooltip("弹窗出生点")]
    public Transform effectBorn;
    [Tooltip("弹窗目标点")]
    public Transform effectTarget;
    [Tooltip("弹窗目标点")]
    public Transform effectTarget1;
    private GameObject effectGo;
    [Tooltip("第一个红包产生提示箭头")]
    public GameObject tipsGo;
    public GameObject fahuoGo;
    public Graphic[] graphics;
    public WangDianShengJi wangDianShengJi;
    public GameObject choujiang;
    public bool isShowFaHuoTips = false;
 public  bool isFirstShowChouJiang = false;
    public Image fahuoFill;
    public GameObject mask;

    public void SetFaHuoValue(int value)
    {
        fahuoFill.fillAmount = value/20f;
        if (value >= 20)
        {
            if (!fahuoGo.activeInHierarchy)
            {
                fahuoGo.SetActive(true);
                faHuoSkeletonGraphic.AnimationState.SetAnimation(0, "max", true);
            }
        }
        else
        {
            if (fahuoGo.activeInHierarchy)
            {
                fahuoGo.SetActive(false);
                faHuoSkeletonGraphic.AnimationState.SetEmptyAnimation(0,0);
            }
        }
    }
    void Start()
    {

        //for (int i = 0; i < toggles.Length; i++)
        //{
        //    toggles[1].onValueChanged.AddListener(UIManager.Instance.GetPanel(panelName[1]).SetHideOrShow);
        //}
        toggles[1].isOn = true;
        // toggles[0].onValueChanged.AddListener(Fun0);
        toggles[0].onValueChanged.AddListener(Fun4);
        toggles[1].onValueChanged.AddListener(Fun1);
       toggles[2].onValueChanged.AddListener(Fun2);
        // toggles[1].isOn = true;
      
        if (GuideManager.Instance.isFirstGame)
        {
            HideUI();
        }
        else
        {
            if (PlayerData.Instance.time == 0)
            {
                SetTips(0);
            }
            if (PlayerData.Instance.ProduceQiPaoList!=null)
                
            {
                SetFaHuoValue(PlayerData.Instance.ProduceQiPaoList.Count);
                if (PlayerData.Instance.ProduceQiPaoList.Count >= ProduceQiPaoManager.jianceCount)
                SetTips(2);
            }

            if (PlayerData.Instance.actorDateList != null && PlayerData.Instance.actorDateList.Count > 0)
                MoneyManager.Instance.ShowShengJiTips(PlayerData.Instance.actorDateList[PlayerData.Instance.actorDateList.Count - 1].actor_name.ToString(), PlayerData.Instance.actorDateList[PlayerData.Instance.actorDateList.Count - 1].need_level_new_actor.ToString() + "级");
        }
        transform.SetParent(UIManager.Instance._ToggleCanvas.transform);
        if (PlayerData.Instance.red >= PlayerData.Instance.RedBiaoZhu)
        {
            ShowMask(false);
        }
        else
        {
            ShowMask(true);
        }
    }
    public void ShowMask(bool value)
    {if(mask.activeInHierarchy!=value)
        mask.SetActive(value);
    }
    public void ShowTusi()
    {

        AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, "余额大于0.3元就开启我的网店哦！", Color.black, null, null, 0.8f);
    }
    public void ShowUI()
    {
        foreach (var item in graphics)
        {
            item.gameObject.SetActive(true);
            item.DOFade(1, 0.5f).SetUpdate(true);
        }
        gameObject.SetActive(true);
    }
    public void HideUI()
    {
        foreach (var item in graphics)
        {
           
            item.DOFade(0, 0f).SetUpdate(true);
        }
        gameObject.SetActive(false);
    }
    public void SetRedTips(bool value)
    {
        
        tipsGo.SetActive(value);
    }

    public void Fun0(bool value)
    {
        //var tog=  UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        //  if (tog == null) return;
        //  string name = tog.name.Split('e')[1];
        string name = "0";
        if (UnityActionManager.Instance.HaveAction<bool>(name))
        {
            UnityActionManager.Instance.DispatchEvent<bool>(name, value);
        }
        else
        {
          var panel=  UIManager.Instance.OpenPanel(panelName[int.Parse(name)]);
            //panel.Hide();
            if (panel != null)
            {
                UnityActionManager.Instance.AddAction<bool>(name,
                 panel.SetHideOrShow);
                UnityActionManager.Instance.DispatchEvent<bool>(name, value);
            }
            else
            {
                Debug.Log("panel++" + panel);
            }
        }
       // UnityActionManager.Instance.DispatchEvent<bool>(name, value);
    }

    public void Fun1(bool value)
    {
        string name = "1";
        if (value)
        {
            GoGame();
        }
        if (UnityActionManager.Instance.HaveAction<bool>(name))
        {
            UnityActionManager.Instance.DispatchEvent<bool>(name, value);
        }
        else
        {
            var panel = UIManager.Instance.GetPanel(panelName[int.Parse(name)]);
          // panel.Hide();
            if (panel != null)
            {
                UnityActionManager.Instance.AddAction<bool>(name,
                 panel.SetHideOrShow);
               UnityActionManager.Instance.DispatchEvent<bool>(name, value);
            }
            else
            {
                Debug.Log("panel++" + panel);
            }
        }
      
        // UnityActionManager.Instance.DispatchEvent<bool>(name, value);
    }
    public void Fun2(bool value)
    {
        //var tog = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        //if (tog == null) return;
        string name = "2";
        if (value)
        {
            GoGame();
        }
        if (UnityActionManager.Instance.HaveAction<bool>(name))
        {
            UnityActionManager.Instance.DispatchEvent<bool>(name, value);
        }
        else
        {
            var panel = UIManager.Instance.GetPanel(panelName[int.Parse(name)]);
            //panel.Hide();
            if (panel != null)
            {
                UnityActionManager.Instance.AddAction<bool>(name,
                 panel.SetHideOrShow);
                UnityActionManager.Instance.DispatchEvent<bool>(name, value);
            }
            else
            {
                Debug.Log("panel++" + panel);
            }
        }
     
        // UnityActionManager.Instance.DispatchEvent<bool>(name, value);
    }
    Object chouj;
    public void Fun4(bool value)
    {if (value)
        {
           
            if (chouj==null)
            chouj = Instantiate(choujiang);
            else
            {
                DaimondTaskUI.Instance.Show(false);
                tipsManager.Instance.showUI(true);
            }
            GoGame();
            CamareManager.Instance.SetStates(false);
            if (isFirstShowChouJiang)
            {
                AndroidAdsDialog.Instance.UploadDataEvent("new_version_guide10");
                PeopleEffect.Instance.HideTips();
                PeopleEffect.Instance.HideMask();
                isFirstShowChouJiang = false;
            }

        }
        else
        {if (chouj != null)
            {
                tipsManager.Instance.showUI(false);
            }
            DaimondTaskUI.Instance.Show(true);
               CamareManager.Instance.SetStates(true);
                //AudioManager.Instance.PlayMusic("bgm");
            
        }
    }
    public void SetTips(int index)
    {
        if (!tipsGos[index].activeSelf)
            tipsGos[index].SetActive(true);
        if (index == 2)
        {
            isShowFaHuoTips = true;
        }
    }
    public void HideTips(int index)
    {
        if (tipsGos[index].activeSelf)
            tipsGos[index].SetActive(false);
    }
    public void ShowTips()
    {
        skeletonGraphic.gameObject.SetActive(true);
        skeletonGraphic.AnimationState.SetAnimation(0, "liangqi", true);
    }
    public void ShowPanel(int index)
    {
        toggles[index].isOn = true;
           

    }
 public void ShowZuanShiTips()
    {
        if (!zuanshiSkeletonGraphic.gameObject.activeInHierarchy)
        {
            zuanshiSkeletonGraphic.gameObject.SetActive(true);
           
        }
        zuanshiSkeletonGraphic.AnimationState.SetAnimation(0, "animation", false);
    }
    public void ShowZuanShiBuZuTips()
    {
        if (!zuanshiBuZuSkeletonGraphic.gameObject.activeInHierarchy)
        {
            zuanshiBuZuSkeletonGraphic.gameObject.SetActive(true);

        }
        zuanshiBuZuSkeletonGraphic.AnimationState.SetAnimation(0, "qufahuo", false);
    }
    public void ShowAdward(int count, Produce produce, int count1, bool isProduce =false,UnityEngine.Events.UnityAction unityAction=null)
    {
        
    }
    public void GoGame()
    {
        //UIManager.Instance.SetUIStates(true);
        BigWorld.Instance.GoGame();
    }
}
      

