using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine;
using Spine.Unity;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
public class ZhiBoJian : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("直播间数据")]
    public ActorDate actorDate;
    [Header("直播间索引")]
    public int index;
    [Header("直播间文字显示组件")]
    public Text jiaCheng, actorName, actorLevel,sellCount, profit,produceName,louceng;
    [Header("直播间img显示组件")]
    public Image produceImg,fangjianImg;
    [Header("直播间卖货速度")]
    public int sellSuDu;
    [Header("直播间卖货加成数值")]
    public float jiaChengValue;
    [Header("直播间卖货进度条")]
    public Slider slider;

    public IEnumerator startsell;
   // [Header("直播间卖货商品")]
  //  public ProduceInfo produce;
    [Header("升级按钮")]
    public Button shenjiButton;
    public SkeletonGraphic peopleAnim;
    [Header("刷新商品按钮")]
    public GameObject refreshGo;
    [Header("刷新商品手指")]
    public GameObject refreshTipsGo;
    Text shenjiText,shenjiCount;
    Image shenjiImg,shengjiBtImg;
    [Header("升级按钮状态精灵")]
    public Sprite[] shengjiSps;
    [Header("粉丝数量特效脚本")]
    public NumberEffect fensiCount;
    [Header("等级特效脚本")]
    public NumberEffect levelCount;
    public GameObject redShengJiGo;
    public bool isKonXian = true;
   public ZhiBoPanel boPanel;
    [Header("推销系统")]
    public TuiXiao tuiXiao;
    public RectTransform proRect;
    public Transform guideTarget1, guideTarget2;
    public RectTransform guideTargetRect;
    public GameObject konxianMask;
    public GameObject konxianBt;
    public SkeletonGraphic konxianMaskAnim, shengjiAnim,shengjiAnimNew;
    [Header("等级动画点击红包后动画")]
    public AnimationBase levelAnim;

    public AnimationBase levelAndlouCengAnim;
    public GameObject zhuoziGo;
    public Image zhuoziImage;
    float y = -273;
    public Text guideText;
    public GameObject infosGo;
    DaimondBornPosition[] bornPositionArrys;
    public GameObject awardAnim;

    [Tooltip("弹窗出生点")]
    public Transform effectBorn;
    [Tooltip("弹窗目标点")]
    public Transform effectTarget;
    [Header("主播类型")]
    public Text zhuboType;
    [Header("教程接口")]
    public ZhiBoJianGuide guideFunc;
    public GameObject tipsGo3;
    [Header("控制显隐")]
    public CanvasGroup canvasGroup;
    [Header("控制是否被布局控制")]
    public LayoutElement layoutElement;
    public ZhiBoJianAward zhiBoJianAward;
    public ZhiBoJianRed zhiBoJianRed;
    private void Start()
    {
      
        bornPositionArrys = GetComponentsInChildren<DaimondBornPosition>();
        for (int i = 0; i < bornPositionArrys.Length; i++)
        {
            bornPositionArrys[i].index =string.Format("daimond{0}-{1}",index,i);

        }
        
     
        tipsGraphs = tipsTf.GetComponentsInChildren<Graphic>();
        ShowSelledTips(false);
        //JiaCheng();
       
        shenjiButton.onClick.AddListener(()=>ShengJi());
        slider.value = 0;
        JavaCallUnity.Instance.shenJiCallBack += ShenJiEvent;
        UnityActionManager.Instance.AddAction<int>("ShengJiTips", ShowTips3);
        itemId = actorDate.item_id;
        UnityActionManager.Instance.AddAction("RefreshZhiBo",RefreshShenJIShow);

    }
    public void ShowZhiBoJian(bool value)
    {
        canvasGroup.alpha= value==true?1 : 0;
       // canvasGroup.alpha = 1;
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
        layoutElement.ignoreLayout = !value;
    }
  
    private void ShowTips3(int index )
    {if(index==this.index)
        tipsGo3.SetActive(true);
    }
    private void SetZhuBo()
    {
        SetSellAnim();
        if (actorDate.actor_sex == 1)
        {
          
            fangjianImg.sprite=ZhiBoPanel.Instance. fanjianSprites[0];
            zhuoziImage.sprite = ZhiBoPanel.Instance.zhuoziSprites[0];
        }
        else
        {
          
            fangjianImg.sprite = ZhiBoPanel.Instance.fanjianSprites[1];
            zhuoziImage.sprite = ZhiBoPanel.Instance.zhuoziSprites[1];
        }
    }
    public void Init()
    {
        if(index!=0)
        StopZhiBoAnim();
        boPanel = UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel;
        JiaCheng();
       // SetZhuBo();
        if (index != 0)
        {
            if (actorDate.actor_level < actorDate.need_level_new_actor)
            { zhiBoJianRed.Animation(actorDate.actor_level-1, actorDate.actor_level-1, 0, 0);
                zhiBoJianRed.SetCount(actorDate.need_level_new_actor-1, actorDate.actor_louceng * 25);
            }
            else
            {
                zhiBoJianRed.gameObject.SetActive(false);
            }
        }
        else
        {
            zhiBoJianRed.gameObject.SetActive(false);
        }
        if (actorDate.item_id != 0)
        {

            // produce = StockManager.Instance.produceInfos.Find(s => s.produceDate.item_id == actorDate.item_id);

            Sell(actorDate.item_id, true);


        }
        else
        {
            SetEmpryProduce();
        }
        
        //Sell(produce);
     

        actorName.text = actorDate.actor_name;
        actorLevel.text = actorDate.actor_level.ToString();
        louceng.text =string.Format("{0} 楼", actorDate.actor_louceng) ;
        //levelCount.Animation(actorDate.actor_level, "", 0.8f);
        actorLevel.text = string.Format("{0}级", actorDate.actor_level);
        zhuboType.text = actorDate.Actor_label;
        shengjiBtImg = shenjiButton.GetComponent<Image>();
        shenjiText = shenjiButton.transform.GetChild(0).GetComponent<Text>();
        shenjiImg = shenjiButton.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        shenjiCount= shenjiButton.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        RefreshShenJIShow();
        //PlayerDate.Instance.expenddiamondAction += ShowShengJiTips;
        //PlayerDate.Instance.expengdGoldAction += ShowShengJiTips;
       // PlayerDate.Instance.goldAction += RefreshShenJIShow;
        PlayerData.Instance.diamondAction += RefreshShenJIShow;
        PlayerData.Instance.expenddiamondAction += RefreshShenJIShow;
        //PlayerDate.Instance.expengdGoldAction += RefreshShenJIShow;
        //ShowZhiBoJian(false);
        if (GuideManager.Instance.isFirstGame)
        {  if (index == 0)
            shenjiButton.gameObject.SetActive(false); 
            // profit.gameObject.SetActive(false);

            // infosGo.gameObject.SetActive(false);

            //shenjiButton.gameObject.SetActive(false);
        }
        else
        {

        }
    }
   
    private void OnDestroy()
    {
        PlayerData.Instance.goldAction -= RefreshShenJIShow;
        PlayerData.Instance.diamondAction -= RefreshShenJIShow;
        PlayerData.Instance.expenddiamondAction -= RefreshShenJIShow;
        PlayerData.Instance.expengdGoldAction -= RefreshShenJIShow;
        //PlayerDate.Instance.expenddiamondAction -= ShowShengJiTips;
        //PlayerDate.Instance.expengdGoldAction -= ShowShengJiTips;
    }
    private void SetEmpryProduce()
    {
        produceImg.sprite = null;
        produceName.text = null;
        profit.text = string.Format("利润:{0}", 0);
        profit.gameObject.SetActive(false);
      isKonXian = false;
        actorDate.item_id = 0;
        refreshGo.SetActive(false);
        refreshTipsGo.SetActive(false);
        SetZhuBo();
        //proButton.gameObject.SetActive(false);
        //guideFunc.GuideFuncEvent();
        shenjiButton.gameObject.SetActive(false);
        konxianMask.SetActive(true);
            konxianMaskAnim.AnimationState.SetAnimation(0, "actor_free", false);
        StartCoroutine(Delay(1,()=> konxianBt.SetActive(true)));
   
    
      
    }

   public IEnumerator SetTips()
    {
        yield return new WaitForSeconds(0.1f);
        boPanel.peopleEffect.SetTips(proRect, guideTarget2.position);
    }

    IEnumerator Delay(float time,UnityEngine.Events.UnityAction unityAction=null)
    {
        yield return new WaitForSeconds(1f);
       if( unityAction != null)
        {
            unityAction();
        }
    }
    //Floor_Up floorup;
    /// <summary>
    /// 刷新显示消耗钻石
    /// </summary>
    public void RefreshShenJIShow()
    {
        
       // _skill = ConfigManager.Instance.GetSkill(actorDate.actor_level);
        //if (_skill.actorlevel_cost == 1)
        //{
        //    shenjiImg.sprite = ResourceManager.Instance.GetSprite("影片");
        //    shenjiImg.gameObject.SetActive(true);
        //    //shenjiImg.SetNativeSize();
        //    shenjiText.rectTransform.anchoredPosition = new Vector2(-26, 0);
        //    //if (JavaCallUnity.Instance.IsFirstGetRedValue())
        //    //{
        //    //    shenjiImg.gameObject.SetActive(false);
        //    //    shenjiText.rectTransform.anchoredPosition = new Vector2(0, 0);
        //    //}
        //    //else
        //    //{
        //    //   // shenjiImg.gameObject.SetActive(true);
        //    //}
        //    //  shenjiText.alignment = TextAnchor.MiddleCenter;
        //    shenjiImg.rectTransform.sizeDelta = new Vector2(42, 34);
        //    shenjiCount.gameObject.SetActive(false);
        //    redShengJiGo.SetActive(true);
        //    shenjiButton.interactable = true;
        //    shengjiBtImg.sprite = shengjiSps[0];
        //    SetZhuBoItemTipsShow();

        //}
        //else if (_skill.actorlevel_cost == 2)
        //{
            shenjiImg.gameObject.SetActive(true);
            shenjiImg.sprite = ResourceManager.Instance.GetSprite("钻石");
            shenjiText.rectTransform.anchoredPosition = new Vector2(-47, 0);
            shenjiImg.rectTransform.sizeDelta = new Vector2(39, 39);
            shenjiCount.text = _skill.actorlevel_cost_num.ToString();
            shenjiCount.gameObject.SetActive(true);
            redShengJiGo.SetActive(false);
            if (_skill.actorlevel_cost_num <= PlayerData.Instance.diamond)
            {

                ShengJiRecoverClick();
                //SetZhuBoItemTipsShow();
            }
            else
            {
                //ShengJiNotClick();
            }
        //}
        //else if (_skill.actorlevel_cost == 3)
        //{
        //    shenjiImg.gameObject.SetActive(true);
        //    shenjiImg.sprite = ResourceManager.Instance.GetSprite("金币");
        //    shenjiText.rectTransform.anchoredPosition = new Vector2(-47, 0);
        //    shenjiImg.rectTransform.sizeDelta = new Vector2(39, 39);
         
        //    shenjiCount.text = _skill.actorlevel_cost_num.ToString();
        //    shenjiCount.gameObject.SetActive(true);
        //    redShengJiGo.SetActive(false);
        //    if (_skill.actorlevel_cost_num<= PlayerDate.Instance.gold)
        //    {
        //        ShengJiRecoverClick();
        //        SetZhuBoItemTipsShow();
        //    }
        //    else
        //    {
        //       // ShengJiNotClick();
        //    }
        //}
    }

    private void SetZhuBoItemTipsShow()
    {
      
            ZhiBoPanel.Instance.zhiBoItemTips.SetActive(true);
    }

    public void ShengJiRecoverClick()
    {
        shenjiButton.interactable = true;
        shengjiBtImg.sprite = shengjiSps[0];

    }

    private void ShengJiNotClick()
    {
        shenjiButton.interactable = false;
       shengjiBtImg.sprite = shengjiSps[1];
    }

    int clickCount = 0;
    int openClickGuideCount = 0;
    public void OpenProduceInfo()
    {
        OpenProduceInfoEvent();
        //if (!GuideManager.Instance.isFirstGame)
        //{
        //    UnityActionManager.Instance.DispatchEvent("RefreshProduce");
        //}
    }

    public void OpenProduceInfoEvent(int id=-1)
    {
        if (index == 1 && GuideManager.Instance.isFirstGame)
        {
            openClickGuideCount++;
            if (openClickGuideCount == 2)
            {

                (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).zhibojianList[1].guideFunc.GuideFuncEvent2();

            }
        }
        zhiBoJianAward.Hide();
        AudioManager.Instance.PlaySound("bubble1");
        ShopPanelNew.Instance.ShowUI(this,id);
        print("打开面板，换商品");
    }

    public void IsFirstOpenProduceInfo()
    {

        OpenProduceInfoEvent();

    }
    void PlayAnim()
    {
        peopleAnim.AnimationState.SetAnimation(0, "02", false);
    }
    public void ShengJi(UnityEngine.Events.UnityAction unityAction=null)
    {
        print("1");
        //  AndroidAdsDialog.Instance.UploadDataEvent("actor_level_up");
        AndroidAdsDialog.Instance.UploadDataEvent("click_shengji_actor_new");
//        AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.Warning);
//        if (_skill.actorlevel_cost == 1)
//        {
//            print("激励视频");

//            //ShengJiRedPanel.Instance.ShowUI(this);
//            //Shop.Instance.ShowUI(true,this);
//            ZhiBoPanel.AddShengJICount(()=> ShenJiEvent(index,true));
//#if UNITY_EDITOR



//#elif UNITY_ANDROID

      
        
//#endif

//        }
        //else if (_skill.actorlevel_cost == 2)
        //{
            if (PlayerData.Instance.diamond >= _skill.actorlevel_cost_num)
            {
                PlayerData.Instance.ExpendDiamond(_skill.actorlevel_cost_num);
                print("_skill.actorlevel_cost_num++++" + _skill.actorlevel_cost_num);
                ShenJiEvent(index);
            
               // AndroidAdsDialog.Instance.ShowTableVideo("0");
                if (unityAction != null)
                    unityAction();
            }
            else
            {
                print(gameObject.name);
                //AndroidAdsDialog.Instance.ShowToasts("钻石不够", ResourceManager.Instance.GetSprite("钻石不足"), Color.red,null);
                PlayerData.Instance.AddDaimondNotEnoughCount();
                Debug.LogError("钻石不够");
            }
        //}



        //else if (_skill.actorlevel_cost == 3)
        //{
        //    if (PlayerDate.Instance.gold >= _skill.actorlevel_cost_num)
        //    {
        //        PlayerDate.Instance.Expend(_skill.actorlevel_cost_num);
        //        ShenJiEvent(index);
        //        if (unityAction != null)
        //            unityAction();
        //        //PlayerDate.Instance.Expend(_skill.actorlevel_cost_num);
        //    }
        //    else
        //    {
        //        PlayerDate.Instance.AddGoldNotEnoughCount(_skill.actorlevel_cost_num, (int)(_skill.actorlevel_cost_num - PlayerDate.Instance.gold), () => { ShenJiEvent(index, false); if (unityAction != null)
        //                unityAction();
        //        });
        //        AndroidAdsDialog.Instance.ShowToasts("金币不够", ResourceManager.Instance.GetSprite("金币不足"), Color.red,  null);
        //        Debug.LogError("金币不够");
        //    }
        //}
     
    }

    public void ShenJiEvent(int index,bool isRed=false)
    {
        if (index == this.index)
        {
            AudioManager.Instance.PlaySound("actor_levelup");
            AndroidAdsDialog.Instance.AddSignDataCount(4);
            PlayerData.Instance.AddShengJICount(this);
            AndroidAdsDialog.Instance.UploadDataEvent("finish_shengji_actor_new");
            if (index == 1&&GuideManager.Instance.isFirstGame)
            {
                shengjiBtImg.gameObject.SetActive(false);
                guideFunc.GuideFuncEvent1(false);
            }
            levelCount.Animation(actorDate.actor_level+1, "", "级", 0.8f,actorDate.actor_level);
            actorDate.actor_level += 1;
            if (actorDate.actor_level > PlayerData.Instance.actor_maxlevel)
            {
                PlayerData.Instance.PromoteActor_Maxlevel(actorDate.actor_level);
            }
            int red = NumberGenenater.GetRedCount(isRed);
           // AndroidAdsDialog.Instance.ShowToasts("+" + red.ToString(), ResourceManager.Instance.GetSprite("红包"), Color.red);
            PlayerData.Instance.GetRed(red);
            //  PlayerDate.Instance.SaveActorDate();

            //actorLevel.text = string.Format("{0}级", actorDate.actor_level);
            JiaCheng();
            if (index != 0)
            {
                if (actorDate.actor_level < actorDate.need_level_new_actor)
                    zhiBoJianRed.Animation(actorDate.actor_level-1, actorDate.actor_level-2, 0.5f, 3f);
                else if (actorDate.actor_level == actorDate.need_level_new_actor)
                {
                    zhiBoJianRed.Animation(actorDate.actor_level-1, actorDate.actor_level - 2, 0.5f, 1.5f, () =>
                    {
                        ZhiBoPanel.AddShengJICount(actorDate.actor_louceng * 25, () =>
                        {
                            PeopleEffect.Instance.ShowMask(0, 0);
                            StartCoroutine(Global.Delay(1.5f, () => { PeopleEffect.Instance.HideMask(); }));
                            // EventSystem.current.enabled = false;
                            ZhiBoPanel.Instance.MoveZhuBo(index+1,true, () =>
                            {
                                PeopleEffect.Instance.HideMask();
                                zhiBoJianRed.gameObject.SetActive(false);
                                //EventSystem.current.enabled = true;
                            },1.5f);
                            ZhiBoPanel.Instance.zhiBoPiaoChuan.Show(ZhiBoPanel.Instance.zhibojianList[ZhiBoPanel.Instance.zhibojianList.Count - 1].actorDate.actor_name);
                        });
                    });
                }
                else
                {
                    zhiBoJianRed.gameObject.SetActive(false);
                }
            }
            actorDate.time = 0;
        
            RefreshShenJIShow();
            
            ShengJiDongHua(isRed);
          
            refreshGo.SetActive(true);
            refreshTipsGo.SetActive(true);

            AndroidAdsDialog.Instance.ShowToasts(effectBorn, effectTarget, new string[]
          {
                "主播",actorDate.actor_name,"升级成功" }, null, new Color[]
              {
                    Color.black,new Color32(254,95,180,1),Color.black
              },

              () =>
              {

              });


        }
        if (index == ZhiBoPanel.Instance.currentIndex)
        {
            if (actorDate.actor_level >= actorDate.need_level_new_actor)
            {
                ZhiBoPanel.Instance.PlayVideoShenJiEvent(false);
               
                
            }
        }
        if (!GuideManager.Instance.isFirstGame)
        {
            UnityActionManager.Instance.DispatchEvent<int>("ShengJiZhuBo",1);
            if (tipsGo3.activeSelf)
            {
                tipsGo3.SetActive(false);

            }
           
           
        }
    }
    
    private void ShengJiDongHua(bool isRed=false)
    {
       // shengjiAnim.gameObject.SetActive(true);
        shengjiAnimNew.gameObject.SetActive(true);
        //shengjiAnim.AnimationState.SetAnimation(0, "shengji", false).Complete += (S) => shengjiAnim.gameObject.SetActive(false);
        shengjiAnimNew.AnimationState.SetAnimation(0, "animation", false).Complete += (S) => shengjiAnimNew.gameObject.SetActive(false);
        if (isRed)
        {
            levelAnim.Animation();
        }
    }

    public  Actor_Skill _skill;
    void JiaCheng()
    {
        _skill = ConfigManager.Instance.GetSkill(actorDate.actor_level);
        
           jiaChengValue = (float)_skill.actor_level_buff;
        sellSuDu =(int) (actorDate.actor_sellbase* jiaChengValue);
        SetJiaCheng();
        int fensi = (int)(500 * UnityEngine.Random.Range((float)_skill.actor_level_buff, (float)(_skill.actor_level_buff + 0.3f)));
        fensiCount.Animation(fensi, "粉丝","", 1,currentfensi);
        currentfensi = fensi;
       
    }
    int currentfensi = 0;
   void SetJiaCheng()
    {
        jiaCheng.text = string.Format("{0}%", (int)((jiaChengValue - 1f) * 100));
        sellCount.text = string.Format("卖<color=#15FF00>{0}</color>个/次", sellSuDu);
      
    }
    Produce currentProduce;
    bool isStartTuiXiao;
    int clickCount1 = 0;
    int itemId = 0;
    public void Sell(int id,bool isStart=false)
    {

        if (id == 0) return;
        //if(!GuideManager.Instance.isFirstGame||index!=0)
        //shenjiButton.gameObject.SetActive(true);
        // if (produce.produceDate.item_id == 0) return;
        refreshGo.SetActive(false);
        refreshTipsGo.SetActive(false);
        //profit.gameObject.SetActive(false);
        profit.gameObject.SetActive(true);
        actorDate.item_id = id;
        //this.produce = produce;
        currentProduce = ConfigManager.Instance.GetProduce(id);
      
        tipsImage.sprite = ResourceManager.Instance.GetSprite(currentProduce.item_pic);
        produceImg.sprite = ResourceManager.Instance.GetSprite(currentProduce.item_pic);
        produceName.text = currentProduce.item_name;
        profit.text = string.Format("利润:{0}", currentProduce.item_profit);
        if (startsell != null)
        {
            StopCoroutine(startsell);
        }
        startsell = StartSell();

        //proButton.gameObject.SetActive(false);//卖货时隐藏更换按钮
        StartCoroutine(startsell);
        boPanel.RemoveKonXianZhiBo(this);
        // produce.SetSelled(actorDate.actor_name);
        // boPanel.RefreshKonXianZhuBo();
        if (!isStart)
        {
            if (itemId == 0)
            {
                //SetZhuBoItemTipsShow();
            }
            itemId = id;
            if (currentProduce.profit_state == 2)
            {
                AndroidAdsDialog.Instance.AddSignDataCount(7);
            }
            boPanel.SetKonxianCount(-1);
            actorDate.time = 0;
            isStartTuiXiao = true;
            StopSell();
            if (GuideManager.Instance.isFirstGame)
            {
               
                if (index == 1)
                {
                    clickCount1++;
                    if(clickCount1==1)
                    StartTuiXiao(()=>guideFunc.GuideFuncEvent1(true));
                    else
                    {
                        StartTuiXiao();
                        
                    }
                }
                else
                {
                    StartTuiXiao();
                }
                    
                        }
            else
            StartTuiXiao();
        }//减少空闲主播
        HideKonxianMask();
        SetSellAnim();
        if (GuideManager.Instance.isFirstGame)
        {
            RecoverGuideStatus();

        }
    }
    public void StopZhiBoAnim(bool value=false)
    {
        IsPlayAnim = value;
        peopleAnim.enabled = value;
        if (value)
        {
            SetSellAnim();
        }
    }
  
    private void SetSellAnim()
    {
        if (!IsPlayAnim) return;
        else
        {
            if (!peopleAnim.enabled)
            {
                peopleAnim.enabled = true;
            }
                if (isSell)
            {

                SetZhuBoSellAnim();
            }
            else
            {
                SetZhuBoKongXianAnim();
            }
        }
    }

    private void SetZhuBoSellAnim()
    {
        if (!IsPlayAnim) return;
        if (actorDate.actor_sex == 1)
        {
            peopleAnim.skeletonDataAsset = ResourceManager.Instance.GetSkeleton(3);
            peopleAnim.Initialize(true);
            peopleAnim.rectTransform.anchoredPosition = new Vector2(-361, y);
            peopleAnim.AnimationState.SetAnimation(0, "sellout", true);

        }
        else if (actorDate.actor_sex == 3)
        {
            peopleAnim.skeletonDataAsset = ResourceManager.Instance.GetSkeleton(4);
            peopleAnim.Initialize(true);
            peopleAnim.rectTransform.anchoredPosition = new Vector2(-51, y);
            peopleAnim.AnimationState.SetAnimation(0, "maihuochenggong", true);
        }

        else
        {
            peopleAnim.skeletonDataAsset = ResourceManager.Instance.GetSkeleton(2);
            peopleAnim.Initialize(true);
            peopleAnim.rectTransform.anchoredPosition = new Vector2(-51, y);
            peopleAnim.AnimationState.SetAnimation(0, "sellout", true);
        }
    }

    private void SetZhuBoKongXianAnim()
    {
        if (!IsPlayAnim) return;
        if (actorDate.actor_sex == 1)
        {
            peopleAnim.skeletonDataAsset = ResourceManager.Instance.GetSkeleton(1);
            peopleAnim.Initialize(true);
            peopleAnim.AnimationState.SetAnimation(0, "daiji", true);
            peopleAnim.rectTransform.anchoredPosition = new Vector2(-361, y);
            // zhuoziGo.SetActive(true);
            // fangjianImg.sprite = ResourceManager.Instance.GetSprite("房间1");
        }
        else if (actorDate.actor_sex == 3)
        {
            peopleAnim.skeletonDataAsset = ResourceManager.Instance.GetSkeleton(4);
            peopleAnim.Initialize(true);
            peopleAnim.rectTransform.anchoredPosition = new Vector2(-51, y);
            peopleAnim.AnimationState.SetAnimation(0, "daiji", true);
        }
        else
        {
            peopleAnim.skeletonDataAsset = ResourceManager.Instance.GetSkeleton(0);
            peopleAnim.Initialize(true);
            peopleAnim.AnimationState.SetAnimation(0, "nvzhubo-daiji", true);
            peopleAnim.rectTransform.anchoredPosition = new Vector2(-51, y);
            // fangjianImg.sprite = ResourceManager.Instance.GetSprite("房间2");
            //zhuoziGo.SetActive(true);
        }
    }
    private void HideKonxianMask()
    {
        //proButton.gameObject.SetActive(true);
        konxianBt.SetActive(false);
        konxianMask.SetActive(false);
    }

   // float time = 0;
    bool isStopSell = false;
    public void RecorveSell()
    {
        isStopSell = false;
    }
    public void StopSell()
    {
        isStopSell =true;
    }
    bool isCompled = false;
    bool isSell = false;
    IEnumerator StartSell()
    {
        //actorDate.time = 0;
        isSell = true;//售卖动画状态
        float targetTime = (4f / jiaChengValue);
       // print("targetTime+++"+targetTime);
        if (GuideManager.Instance.isFirstGame)
        {
            targetTime = 2f;
           // print("targetTime+++" + targetTime);
           // yield return new WaitForSeconds(1);
           // boPanel.peopleEffect.ShowJiaSu();
        }
        while (true)
        {
            if (isStopSell)
            {
                yield return null;
                continue;
            }
            slider.value = (float)actorDate.time / targetTime;
            yield return new WaitForSeconds(0.02f);
            actorDate.time += 0.02f;
            if (actorDate.time >= targetTime)
            {
                //print("time+++" + actorDate.time);

                slider.value = (float)actorDate.time / targetTime;



                tipsText.text = string.Format("卖出{0}个", sellSuDu);


                if (ToggleManager.Instance.toggles[1].isOn)
                {
                    //AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.Warning);
                    AudioManager.Instance.PlaySound("bubble4");

                }


                ShowSelledTips(true);
               
                isSell = false;
                SetSellAnim();
                yield return new WaitForSeconds(2f);
                isSell = true;//售卖动画状态
                ShowSelledTips(false);
                PlayerData.Instance.AddSelled(this, sellSuDu);
                if (GuideManager.Instance.isFirstGame)
                {
                    if (index == 0)
                    {
                       
                        if (!isCompled)
                        {
                            isCompled = Guide1(targetTime);
                        }
                        else
                        {
                         
                            UnityActionManager.Instance.DispatchEvent("fahuo", actorDate.actor_name, sellSuDu, currentProduce);
                            yield return new WaitForSeconds(0.1f);
                            TuiXiaoAdward(targetTime);
                        }
                    }
                    else if(index == 1)
                    {
                       
                        UnityActionManager.Instance.DispatchEvent("fahuo", actorDate.actor_name, sellSuDu, currentProduce);
                        yield return new WaitForSeconds(0.1f);
                        TuiXiaoAdward(targetTime);
                    }
                    //yield  break;
                }
                else
                {
                    UnityActionManager.Instance.DispatchEvent<int, int>("AddProduce", actorDate.item_id, sellSuDu);
                    UnityActionManager.Instance.DispatchEvent("fahuo", actorDate.actor_name, sellSuDu, currentProduce);
                    yield return new WaitForSeconds(0.1f);
                    TuiXiaoAdward(targetTime);

                }
            }


        }
    }
    [HideInInspector]
    public bool IsPlayAnim =false;
    private bool Guide1(float targetTime)
    {
        bool isCompled;
        EventQueueSystem.Instance.AddEvent<string, int, Produce>(FaHuoPanel.Instance.faHuo.ShowFaHuo, new ParameterData(actorDate.actor_name, sellSuDu, currentProduce));

        boPanel.peopleEffect.HideMask();
        boPanel.peopleEffect.gameObject.SetActive(true);

        AudioManager.Instance.PlaySound("jiaocheng3");
        boPanel.peopleEffect.LeftShow("让我们一起去<color=#FF058B>发货吧</color>!", () => { boPanel.peopleEffect.SetTips(ToggleManager.Instance.guiTaget1, ToggleManager.Instance.guiTaget2.position, true, RotaryType.TopToBottom);
            
        });
        isStopSell = true;
        actorDate.time = 0;
        slider.value = (float)actorDate.time / targetTime;
        ToggleManager.Instance.ShowUI();
        isCompled = true;
        return isCompled;
    }

    private void TuiXiaoAdward(float targetTime,UnityEngine.Events.UnityAction unityAction=null)
    {
        SetSellAnim();
        actorDate.time = 0;
        slider.value = (float)actorDate.time / targetTime;
        //CreactSelledEffect(produceImg.sprite);
        if (isStartTuiXiao)
        {
            tuixiaoCount++;

            isStartTuiXiao = false;
            tuixiaoCount = 0;
            unityAction?.Invoke();
            Color color = Color.black;
            if (tuiXiao.isWin)
            {
                color = Color.red;
                AndroidAdsDialog.Instance.unlockProduct(currentProduce.item_pic);
            }
            AndroidAdsDialog.Instance.ShowToasts(effectBorn, effectTarget, tuiXiao.GetStringValue(), color, () =>
            {
                if (ZhiBoPanel.Instance.zhibojianList.Count - 5 < index)
                {
                    int type;

                    for (int i = 0; i < tuiXiao.daimondCount; i++)
                    {
                        CreactDimond(NumberGenenater.GetDaimondCount(actorDate.actor_level, out type), type);
                    }
                }

            }
            );


        }
    }

  

    int tuixiaoCount;
  
    public Transform tipsTf;
    Graphic[] tipsGraphs;
    public Text tipsText;
    public Image tipsImage;

    private void ShowSelledTips(bool value)
    {if(value)
        for (int i = 0; i < tipsGraphs.Length; i++)
        {
            tipsGraphs[i].DOFade(1, 0.5f);

        }
        else
        {
            for (int i = 0; i < tipsGraphs.Length; i++)
            {
                tipsGraphs[i].DOFade(0, 0.5f);

            }
        }
    }

   
     public void SaveDate()
    {
        //PlayerDate.Instance.SaveProduceDate();
        //PlayerDate.Instance.SaveSelledDate();
    }

    public void RecoverGuideStatus()
    {
       
        infosGo.gameObject.SetActive(true);
       
    }
    public void RecoverGuide1Status()
    {
        RecorveSell();
        actorDate.time = 0;
        shenjiButton.gameObject.SetActive(true);
      
        infosGo.gameObject.SetActive(true);
      //  guideText.gameObject.SetActive(false);
    }

    
    //public RectTransform creactDimondRange;
   public void CreactDimond(int count,int type)
    {
    var arry=    Array.FindAll<DaimondBornPosition>(bornPositionArrys, s => s.isHave == false);
        if (arry != null && arry.Length >= 1)
        {
            var borns = arry[UnityEngine.Random.Range(0, arry.Length)];
          var go = GameObjectPool.Instance.CreateObject("Daimomd", ResourceManager.Instance.GetProGo("Daimomd", "Prefab/Effect/"), borns.transform, Quaternion.identity);
            //var go = Instantiate(ResourceManager.Instance.GetProGo("Daimomd", "Prefab/Effect/"));
           // go.transform.SetParent(borns.transform,false);
            var daimond=  go.GetComponent<Daimond>();
            daimond.SetCount(count);
            borns.SetDaimond(daimond);
            daimond.type = type;
            if (type == 1)
            { daimond.image.sprite =ZhiBoPanel.Instance. qipaoSprite;
                
                //daimond.type = type;
                
            }
            else
            {
                daimond.image.sprite = ZhiBoPanel.Instance.qipaoSprite;
                //daimond.type = type;
            }
            daimond.image.SetNativeSize();
        }

    }
    public void CreactSelledEffect(Sprite sprite)
    {
     var pro=   ResourceManager.Instance.GetProGo("effectBornPos");
        var selleffect = pro.GetComponent<SellEffect>();
        selleffect.SetImage(sprite);
        GameObjectPool.Instance.CreateObject("effectBornPos", pro, ToggleManager.Instance.effectTagetTF, Quaternion.identity);
    }
    public void StartTuiXiao(UnityEngine.Events.UnityAction unityAction=null)
    {
        AndroidAdsDialog.Instance.AddSignDataCount(6);
        AndroidAdsDialog.Instance.UploadDataEvent("enter_tuixiao_time");
        tuiXiao.StartTuiXiao(currentProduce,UnityEngine.Random.Range(0,2), unityAction);
        ShengJiNotClick();
        shenjiButton.gameObject.SetActive(false);
        AudioManager.Instance.PlaySound("version3_1");
    }
}



