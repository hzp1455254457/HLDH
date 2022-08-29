using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using DragonBones;
public class CanKu : MonoBehaviour
{
    [Header("仓库索引")]
    public int index;
    //  public ProduceDate currentProDate;
    // public bool isKonXian=true;
    [Header("仓库数据")]
    public CourierDate courier;
    //CanvasGroup canvasGroup;
    [Header("快递员")]
    public GameObject people;
    [Header("快递员动画和皮带动画控制脚本")]
    public  PeopleAnimationEvent peopleAnimationEvent;

    Produce produce;
    // public Button setSpeedBt;
    [Header("发货商品img")]
    public Image pro;
    [Header("仓库背景img")]
    public Image cankuGroup;
    [Header("剩余数量")]
    public Text proCount;
    [Header("发货显示")]
    public GameObject startSellGo;
    [Header("已赚多少钱")]
    public Text sellCount;
    [Header("仓库Panel")]
    public CanKuPanel kuPanel;
    [Header("开始发货动画得物体")]
    public GameObject fahuoAnim;
    [Header("开始发货动画")]
    public SkeletonGraphic skeletonGraphic;
    [Header("龙骨皮带动画")]
    public UnityArmatureComponent armatureComponent;
    [Header("空闲状态显示")]
    public GameObject konxianGo;
    [Header("升级动画1")]
    public SkeletonGraphic sonHuoAnim;
    [Header("升级动画2")]
    public SkeletonGraphic sonHuoingAnim;
    
    public void Sell(int id,int count,bool isStart=false)
    {if (id == 0) return;
        print("卖货");

        courier.item_id = id;
        courier.deliever_item_num = count;
        courier.Busy_state = 1;
        StartSell();
        produce = ConfigManager.Instance.GetProduce(id);
          pro.sprite = ResourceManager.Instance.GetSprite(produce.item_pic);
        RefreshCount();
        startSellGo.SetActive(true);
        kuPanel.RemoveKonXianCangku(this);
        //kuPanel.RefreshKonXianCanKu();//空闲
        if (!isStart)
        {
            kuPanel.SetKonxianCount(-1);
          
        }
        FaHuoAnim();
        armatureComponent.animation.Play("idel");
        konxianGo.SetActive(false);
       
       // PlayerDate.Instance.SaveCourierDate();
        //if (courier.deliever_item_num > 30)
        //{
        //    setSpeedBt.gameObject.SetActive(true);
        //}
    }
    public void ShowFaHuoWin()
    {
        sonHuoAnim.gameObject.SetActive(true);
        sonHuoAnim.AnimationState.SetAnimation(0, "yangshi1", false).Complete+=s => sonHuoAnim.gameObject.SetActive(false);
    }
    public void FaHuoAnim()
    {
        fahuoAnim.gameObject.SetActive(true);
        skeletonGraphic.AnimationState.SetAnimation(0, "start_sending", false).Complete+=(s)=> fahuoAnim.SetActive(false);
        
    }
    public void FaHuoIngAnim()
    {
        sonHuoingAnim.gameObject.SetActive(true);
        sonHuoingAnim.AnimationState.SetAnimation(0, "sendingwarning", false).Complete += (s) => sonHuoingAnim.gameObject.SetActive(false);
    }
   public void RefreshCount()
    {
        sellCount.text = string.Format("{0}", (int)courier.sellMoney);
        proCount.text = string.Format("还剩<color=#32B0FF>{0}</color>个", courier.deliever_item_num);
    }


    //public void SetSpeed()
    //{
    //    setSpeedBt.gameObject.SetActive(false);
    //}
    public void JiaSu()
    {
        peopleAnimationEvent.SetSudu();
    }
    public void RecorveSuDu()
    {
        peopleAnimationEvent.RecoverSudu();
    }
    int anim = 0;
    public void StartSell()
    {
        //people.gameObject.SetActive(true);
        anim = UnityEngine.Random.Range(0, 2);
        peopleAnimationEvent.peopleAnim.skeletonDataAsset = ResourceManager.Instance.GetSkeleton(anim);
        if (anim == 0)
        {
            cankuGroup.sprite = ResourceManager.Instance.GetSprite("齿轮动画背景1");
        }
        else
        {
            cankuGroup.sprite = ResourceManager.Instance.GetSprite("齿轮动画背景2");
        }
        people.gameObject.SetActive(true);

    }
    internal void Init()
    {
        //setSpeedBt.gameObject.SetActive(false);
    }
    int i = 0;
    public void SellProduce(int count)
    {
        CanKuPanel.AddSelledCount(count);
        double price = produce.item_profit * (double)count;
        if (GuideManager.Instance.isFirstGame)
        {
          
            i++;
            if (i <= 4)
            {
                price = 5000d;
                if (i == 4)
                {
                  StartCoroutine( DelayGuide(1f));
                }
                //kuPanel.peopleEffect.HideMask();
                //   kuPanel.peopleEffect.ShowMask(0.5f);
                //AudioManager.Instance.PlaySound("jiaocheng9");
                courier.sellMoney += price;
                PlayerData.Instance.GetGold(price);//存储到钱包
                //AndroidAdsDialog.Instance.RequestAddScore(price, true);
            }
            else
            {
                courier.sellMoney += price;
                PlayerData.Instance.GetGold(price);//存储到钱包
               //AndroidAdsDialog.Instance.RequestAddScore(price, false);
            }

            
        }
        else
        {
            courier.sellMoney += price;
            PlayerData.Instance.GetGold(price);//存储到钱包
           // AndroidAdsDialog.Instance.RequestAddScore(price, false);
        }
       
        if (isStop)
        {
            StopSellEvent();
        }
        RefreshCount();
        //PlayerDate.Instance.SaveCourierDate();
    }
    IEnumerator DelayGuide(float time)
    {
        yield return new WaitForSeconds(time);
        //kuPanel.peopleEffect.HideMask();
        //kuPanel.peopleEffect.ShowMask(0.5f,()=>kuPanel.peopleEffect.PlayCanKuAnimation(()=> { kuPanel.peopleEffect.HideMask(0.5f
        //    ,() => {


        //        // AndroidAdsDialog.Instance.UploadDataEvent("showshangjinredpacket_jiaocheng");
        //        Shop.Instance.ShowUI(true);
        //        AndroidAdsDialog.Instance.UploadDataEvent("new_course_9");
        //        //if (GuideManager.Instance.achieveGuideAction != null)
        //        //{
        //        //    GuideManager.Instance.achieveGuideAction();
        //        //}
        //        //StartCoroutine(Shop.Instance.Delay(2f));
        //        kuPanel.peopleEffect.HideCanku();
        //    }); 
        //}));
       // AudioManager.Instance.PlaySound("jiaocheng9");
    }
   public bool isStop = false;
    public void StopSell()
    {
        peopleAnimationEvent.StopAnimation();
        //StopSellEvent();
    }
    public void StopSellEvent()
    {
        SetEmpryProduce();
        
    }
    
    private void Start()
    {
      
        armatureComponent.animation.Stop("idel");
        RefreshCount();
        kuPanel = UIManager.Instance.GetPanel("Panel_CanKu") as CanKuPanel;
        var rect = startSellGo.GetComponent<RectTransform>();
        rect.anchoredPosition=new Vector2(-((kuPanel.canvas.rect.width - 750)/ UIManager.Instance.canvas.transform.localScale.x), rect.anchoredPosition.y)   ;
        if (courier.Busy_state == 1)
        {
            Sell(courier.item_id, courier.deliever_item_num,true);
        }
        peopleAnimationEvent = people.GetComponent<PeopleAnimationEvent>();
    }
  void SetEmpryProduce()
    {
        courier.Busy_state = 0;
        startSellGo.SetActive(false);

       // kuPanel.RefreshKonXianCanKu();//空闲

        kuPanel.SetKonxianCount(1);//
        kuPanel.AddKonXianCangku(this);
        armatureComponent.animation.Stop("idel");
        konxianGo.SetActive(true);
    }
}
