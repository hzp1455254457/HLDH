using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;
using System;
using UnityEngine.SceneManagement;

public class MoneyManager: MonoSingleton<MoneyManager>
{
    // Start is called before the first frame update
    public Button redTixian;
   // public Button tixian1, redTixian1;
    //public Button SetingBtn;
    public Text gold, diamond,red;

    //public Text gold1, diamond1, red1;
    public GameObject redGo;
   // public GameObject redGo1;
   // public GameObject tipsRedGo;
    Transform tixianTf;
    int ccrentgold = 0;
   int ccrentdaimond = 0;
    public SkeletonGraphic moneyAnim,redAnim,tixianAnim;
    //public SkeletonGraphic moneyAnim1, redAnim1;
    public Transform daimondTargetTf;
    public Transform redTargetTf;
    //  public static int initCount;
    public Transform born, target;
    public static float redProportion = 1000F;
   
    public NumberEffect goldEffect, daimondEffect;
    public NumberEffect2 redEffect;
    public Transform redTf;
    public Image fillImg;
    // public ShengJiTips shengJiTips;
    //  public CanvasGroup[] status;

    // public NumberEffect redEffect, goldEffect, daimondEffect;
    //public void ShowNew(bool value)
    //{
    //    //SceneManager.MoveGameObjectToScene()
    //    if (value)
    //    {
    //        status[0].alpha = 0;
    //        status[1].alpha = 1;
    //        status[0].blocksRaycasts = false;
    //        status[1].blocksRaycasts =true;
    //        //status[0].SetActive(!value);
    //        //status[1].SetActive(value);
    //    }
    //    else
    //    {
    //        status[0].alpha = 1;
    //        status[1].alpha = 0;
    //        status[0].blocksRaycasts = true;
    //        status[1].blocksRaycasts = false;
    //        //status[1].SetActive(value);
    //        //status[0].SetActive(!value);
    //    }
    //} 
    //public void ShowTips(bool value)
    //{
    //    if (value)
    //    {
    //        if (!tipsRedGo.activeSelf)
    //        {
    //            tipsRedGo.SetActive(value);
    //        }

    //    }
    //    else
    //    {
    //        if (tipsRedGo.activeSelf)
    //        {
    //            tipsRedGo.SetActive(value);
    //        }
    //    }
    //}
    //public static void TextAnimation(int init, int targetCount,UnityEngine.Events.UnityAction unityAction)
    //{
    //    initCount = init;
    //    DOTween.To(() => initCount, x => initCount = x, targetCount, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
    //    unityAction();
    //}
    Tweener fill;
    public void FillAnim()
    {
        if (fill != null)
        {
            fill.Kill();
        }
        fill= fillImg.DOFillAmount(PlayerData.Instance.red / (50 * redProportion),1f);
    }
    public void SetGold()
    {
        // ccrentgold = 0;
        //DOTween.To(() => ccrentgold, x => ccrentgold = x, (int)PlayerData.Instance.gold, 2f).SetEase(Ease.Linear).SetUpdate(true);

        //StartCoroutine(SetText());
        goldEffect.Animation((int)PlayerData.Instance.gold, "", "", 1, ccrentgold);
        ccrentgold = (int)PlayerData.Instance.gold;
    }
    public void SetRed()
    {
        //red.text = string.Format("{0:F}元", PlayerData.Instance.red/ redProportion);
        redEffect.Animation(PlayerData.Instance.red / redProportion, "", "元", 0f, currentRed / redProportion);
        currentRed = PlayerData.Instance.red;
        FillAnim();
    }
    public void SetAnimationRed()
    {
        //currentRed = 0;
        //DOTween.To(() => currentRed, x => currentRed = x, PlayerData.Instance.red, 1f).SetEase(Ease.Linear).SetUpdate(true);

        //StartCoroutine(SetRedText());
        redEffect.Animation(PlayerData.Instance.red / redProportion, "", "元", 1, currentRed / redProportion);
        redAnim.gameObject.SetActive(true);
        redAnim.AnimationState.SetAnimation(0, "addredpacket", false).Complete += s => redAnim.gameObject.SetActive(false);
        currentRed = PlayerData.Instance.red;
        RedScaleAnim();
        FillAnim();
        TfAnim();
    }
    Tweener TfAnim1;
    Tweener TfAnim2;
    private void TfAnim()
    {
        redTf.localScale = Vector3.one;
      if(TfAnim1 != null)
        {
            TfAnim1.Kill();
        }
        if (TfAnim2 != null)
        {
            TfAnim2.Kill();
        }
        TfAnim1 = redTf.DOScale(1.2f, 0.5f);
        TfAnim1.onComplete = () =>
        {
            TfAnim2= redTf.DOScale(1, 0.5f).SetDelay(2);
        };
    }

    Tweener tweener,tweener1;
    private void RedScaleAnim()
    {
        if (tweener != null)
        {
            tweener.Kill();
           
            
        }
        if (tweener1 != null)
        {
            tweener1.Kill();
        }
        redEffect.transform.localScale = Vector3.one;
        tweener = redEffect.transform.DOScale(Vector3.one * 1.2f, 0.4f);
        tweener.onComplete =()=> {
            tweener1 = redEffect.transform.DOScale(Vector3.one, 0.4f);
            tweener1.SetDelay(0.2f);
            tweener1.onComplete = () =>
            {
                tweener1 = null;
            };
            tweener = null;
        };
    }
    private IEnumerator SetRedText()
    {
        if (ToggleManager.Instance.toggles[1].isOn)
        {
            redAnim.gameObject.SetActive(true);
            redAnim.AnimationState.SetAnimation(0, "shouxia", false).Complete += s => redAnim.gameObject.SetActive(false);
           
        }
        if (!ToggleManager.Instance.toggles[1].isOn)
        {
            //redAnim1.gameObject.SetActive(true);
            //redAnim1.AnimationState.SetAnimation(0, "shouxia", false).Complete += s => redAnim1.gameObject.SetActive(false);

        }
        //redAnim.AnimationState.SetAnimation(0, "shouxia", false);
        while (true)
        {
            red.text = string.Format("{0:F}元", currentRed/ redProportion);
        
            yield return null;
            if (currentRed >= PlayerData.Instance.red)
            {
                red.text = string.Format("{0:F}元", PlayerData.Instance.red/ redProportion);
              
                break;
            }
        }
    }

    int currentRed;
    public void SetDaimond()
    {
            //DOTween.To(() => ccrentdaimond, x => ccrentdaimond = x, PlayerData.Instance.diamond, 0.5f).SetEase(Ease.Linear).SetUpdate(true);

            //StartCoroutine(SetDaimondText());
            daimondEffect.Animation(PlayerData.Instance.diamond, "", "", 1, ccrentdaimond);
            ccrentdaimond = PlayerData.Instance.diamond;

        
  
        
    }
    IEnumerator SetDaimondText()
    {
        while (true)
        {
            diamond.text = string.Format("{0}", ccrentdaimond);
          
            yield return null;
            if (ccrentdaimond >= PlayerData.Instance.diamond)
            {
                diamond.text = string.Format("{0}", PlayerData.Instance.diamond);
              
                isXiaoHao = false;
                break;
            }
            if (isXiaoHao)
            {
                diamond.text = string.Format("{0}", PlayerData.Instance.diamond);
              
                isXiaoHao = false;
                break;
            }
        }
    }
    bool isXiaoHao = false;
    public void RefreshGold()
    {
        goldEffect.Animation((int)PlayerData.Instance.gold, "", "",0.1f, ccrentgold);
        ccrentgold = (int)PlayerData.Instance.gold;
        //ccrentgold =(int) PlayerData.Instance.gold;
        //isXiaoHaoGold =true;
    }
    bool isXiaoHaoGold = false;
    IEnumerator SetText()
    {
                AudioManager.Instance.PlaySound("item_out");
        if (ToggleManager.Instance.toggles[1].isOn)
        {
            moneyAnim.gameObject.SetActive(true);
            moneyAnim.AnimationState.SetAnimation(0, "animation", false).Complete += s => moneyAnim.gameObject.SetActive(false);
        }
        else
        {
            //moneyAnim1.gameObject.SetActive(true);
            //moneyAnim1.AnimationState.SetAnimation(0, "animation", false).Complete += s => moneyAnim1.gameObject.SetActive(false);
        }
        while (true)
        {
            gold.text = string.Format("{0}", ccrentgold);
        
           // print("gold1+"+gold1.text);
            //print("gold+" + gold.text);
            yield return null;
            if (ccrentgold >= PlayerData.Instance.gold)
            {
                gold.text = string.Format("{0}", PlayerData.Instance.gold);
                //gold1.text = string.Format("{0}", PlayerData.Instance.gold);
                isXiaoHaoGold = false;
                break;
            }
            if (isXiaoHaoGold)
            {
                gold.text = string.Format("{0}", PlayerData.Instance.gold);
                //gold1.text = string.Format("{0}", PlayerData.Instance.gold);
                isXiaoHaoGold = false;
                break;
            }
        }

        //Animation();


    }
    Sequence quence;
    void Animation()
    {
     
            quence = DOTween.Sequence();

            //1）添加动画到队列中
            quence.Append(gold.transform.DOScale(Vector3.one * 1.1f, 0.02f).SetUpdate(true));
            quence.Append(gold.transform.DOScale(Vector3.one * 0.9f, 0.02f).SetUpdate(true));
            quence.Append(gold.transform.DOScale(Vector3.one * 1f, 0.02f).SetUpdate(true));
       
            quence.SetLoops(3);
           
       
        // 2）添加时间间隔
        // quence.AppendInterval(1);

        //3）按时间点插入动画
        //第一个参数为时间，此方法把动画插入到规定的时间点
        //以这句话为例，它把DORotate动画添加到此队列的0秒时执行，虽然它不是最先添加进队列的
       // quence.Insert(0, transform.DORotate(new Vector3(0, 90, 0), 1));

    }
    public void SetDiamond() 
    {
        daimondEffect.Animation(PlayerData.Instance.diamond, "", "", 0.1f, ccrentdaimond);
        ccrentdaimond = PlayerData.Instance.diamond;
    }
    public void ShowRedTiXianGuide()
    {
        redGo.SetActive(true);
        Global.FindChild(redGo.transform, "guideAnim").SetActive(true);

    }
    private void Start()
    {
        AndroidAdsDialog.Instance.RequestQueryScore();
           tixianTf = UIManager.Instance.canvas_Main.transform.Find("Panel_Tixian");
        RefreshGold();
        SetDiamond();
        SetRed();
        isXiaoHaoGold = false;
        PlayerData.Instance.diamondAction += SetDaimond;
        PlayerData.Instance.expenddiamondAction += SetDiamond;
        PlayerData.Instance.goldAction += SetGold;
        PlayerData.Instance.expengdGoldAction += RefreshGold;
        PlayerData.Instance.redAction += SetAnimationRed;
        //tixian.onClick.AddListener(OpenTiXian);
        redTixian.onClick.AddListener(OpenRedTiXian);
        //redTixian1.onClick.AddListener(OpenRedTiXian);
        //  SetingBtn.onClick.AddListener(OpenSeting);
     
        if (GuideManager.Instance.isFirstGame)
        {
            redGo.SetActive(false);
            redTf.SetParent(UIManager.Instance.canvas_Main.transform);
            redTf.SetAsLastSibling();
            //gameObject.SetActive(false);
            redTf.gameObject.SetActive(false);
           // redGo1.SetActive(false);
            //SetingBtn.gameObject.SetActive(false);
            // tixian.GetComponent<Image>().color = Color.clear;
        }
       

        //JavaCallUnity.Instance.GetDay("15");
        // hongbao5.Instance.InitHongBao(0.1f,0.5f);
        if (PlayerData.Instance.red >= PlayerData.Instance.redBiaoZhun2)
            ShowTiXianAnim(!PlayerData.Instance.IsTiXian);
        else
        {
            ShowTiXianAnim(false);
        }
       
    }
   
public void ShowTiXianAnim(bool value)
    {

        if (value)
        {
            if (!PlayerData.Instance.IsTiXian)
                tixianAnim.AnimationState.SetAnimation(0, "manzutiaojian", true);
        }
        else
        {
            tixianAnim.AnimationState.SetAnimation(0, "daiji", false);
        }
    }
    bool isClickJIaTiXian = false;
    private void OpenRedTiXian()
    {
        //AndroidAdsDialog.Instance.OpenTiXianUI(false, 0, 0, 0, PlayerData.Instance.storeData.level);

        //if (!PlayerData.Instance.IsTiXian)
        //{
        //    ShowTiXianAnim(false);
        //    PlayerData.Instance.IsTiXian = true;
        //}
        //AndroidAdsDialog.Instance.ShowToasts("主播20级开放提现");


        if (GuideManager.Instance.isFirstGame)
        {
            if (!isClickJIaTiXian)
            {
                Global.FindChild(redGo.transform, "guideAnim").SetActive(false);
                if (!PlayerData.Instance.IsTiXian)
                {
                    ShowTiXianAnim(false);
                    PlayerData.Instance.IsTiXian = true;
                }
                AndroidAdsDialog.Instance.OpenJiaTiXian();
                isClickJIaTiXian = true;
            }
            else
            {
                AndroidAdsDialog.Instance.OpenTiXianUI(false, 0, 0, 0, PlayerData.Instance.storeData.level);
            }

        }
        else
        {
            if (PlayerData.Instance.red >= PlayerData.Instance.redBiaoZhun2)
            {
                if (!PlayerData.Instance.IsTiXian)
                {
                    AndroidAdsDialog.Instance.OpenTiXianUI(false, 0, 0, 0, PlayerData.Instance.storeData.level, false);
                    ShowTiXianAnim(false);
                    PlayerData.Instance.IsTiXian = true;
                }
                else
                {
                    AndroidAdsDialog.Instance.OpenTiXianUI(false, 0, 0, 0, PlayerData.Instance.storeData.level);
                }
            }
            else
            {
                AndroidAdsDialog.Instance.OpenTiXianUI(false, 0, 0, 0, PlayerData.Instance.storeData.level);
                //AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, "达到0.3元时解锁提现", Color.black, null, null, 1f);
            }
        }
      // ShowTips(false);
    }

    public void OpenSeting()
    {
        settingPanel.Instance.ShowUI();
    }
    public void RecoverGuideStatus()
    {
        //redGo1.SetActive(true);
        redGo.SetActive(true);
        redTf.SetParent(this.transform);
        //SetingBtn.gameObject.SetActive(true);
        //  tixian.GetComponent<Image>().color = Color.white;
    }
    public void ShowShengJiTips(string name,string value,UnityEngine.Events.UnityAction unityAction=null)
    {
        //shengJiTips.Show(name,value, unityAction);
    }

    public void OpenSevenLogoinPanel()
    {
        SevenLoginPanel.Instance.ShowUI(UIManager.Instance.showRootMain);
       
    }
}
