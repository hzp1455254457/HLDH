using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using UnityEngine.Networking;
using UnityEngine.Events;
public class ZhiBoPanel : PanelBase
{
    public static ZhiBoPanel Instance;
    public Transform parentTf;
    public Button creactFloor/*,kongxianBt*/;
    //public  Text kongxianCount;
    [Header("所有直播间")]
    public List<ZhiBoJian> zhibojianList = new List<ZhiBoJian>();
    [Header("空闲直播间")]
    public List<ZhiBoJian> kongXianzhibojianList = new List<ZhiBoJian>();
    public ScrollManager scrollManager;
    //public ScrollRect scrollRect;
    Transform task;
    public Transform houseTf;
    public float scale;
    float vector;
    Transform mask;
    public PeopleEffect peopleEffect;
    public GameObject daohanlanGo;
    public RectTransform daohanlanGuideTf;
    public int konxianCount;
    public Image shengJiFloorImg;
    public Button creactZhuBo;
    public Text shengjiText;
    // public GameObject wangdianlevelGo;
    public static int ShengJICount;//主播升级次数
    public static int getDaimondCount;//钻石获取数量
                                      //public WeChatInfo chatInfo;
    public Animator Creactanimator;
    public WangDianDaRenUI wangDianDaRenUI;

    public CreactZhuBoTiming creactZhuBoTiming;

    public Sprite qipaoSprite;
    public Sprite[] fanjianSprites;
    public Sprite[] zhuoziSprites;

    public GameObject zhiBoItemTips;
    CanvasGroup canvas;
    [Header("直播间滑动脚本")]
    public ZhiBoJianRect zhiBoJianRect;
    public EWaiTiXianIcon eWaiTiXianIcon;
    public DaoHangLanManager daoHangLanManager;
    public BigWorldIcon bigWorldIcon;
    public TuiXiaoIcon tuiXiaoIcon;
    public ZhiBoPiaoChuan zhiBoPiaoChuan;

    public void ShowShengJiTips()
    {
        ZhiBoPanel.Instance.zhiBoItemTips.SetActive(true);
    }
    public void StopShengJiTips()
    {
        zhiBoItemTips.SetActive(false);
    }

    // public Image image;//测试;
    public static void GetDaimondCount(int count)
    {
        getDaimondCount += count;
        print(getDaimondCount);
        if (addDaimondCountAction != null)
        {
            addDaimondCountAction(count);
        }
        if (addDaimondCountAction1 != null)
        {
            addDaimondCountAction1();
        }
    }
    public static UnityAction<int> addDaimondCountAction;
    public static UnityAction addDaimondCountAction1;
    public static void AddShengJICount(int count,UnityAction unityAction)
    {
        ShengJICount += 1;
       // if(PlayerData.Instance.IsShowShengJiRed)
        ShowRed(count, unityAction);

    }

    private static void ShowRed(int count,UnityAction unityAction)
    {
        
           
        hongbao3.Instance.InitHongBao(count, () =>
        {
            AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_SHENGJIRED);
            JavaCallUnity.Instance.shengjiAction = unityAction;
            PlayerData.Instance.IsShowShengJiRed = false;
#if UNITY_EDITOR
            // if (PlayerDate.Instance.ClickShengJiRedCount >= 1)
            JavaCallUnity.Instance.ShengJIRed("50");

#elif UNITY_ANDROID

      
        
#endif


            //if (JavaCallUnity.Instance.IsFirstGetRedValue())
            //{

            //    JavaCallUnity.Instance.ShengJIRed("100");
            //}
                //AndroidAdsDialog.Instance.RequestLastUnWithDraw();
                AndroidAdsDialog.Instance.UploadDataEvent("click_shengjihb_news");
            AndroidAdsDialog.Instance.UploadDataEvent("click_shengjihblast");
        }, () => { hongbao3.Instance.HideUI();  AndroidAdsDialog.Instance.UploadDataEvent("close_shengjihb_news");
            PlayerData.Instance.IsShowShengJiRed = false;
        },
        () =>
        {
            JavaCallUnity.Instance.shengjiAction = unityAction;
            hongbao3.Instance.HideUI();
            JavaCallUnity.Instance.ShengJIRed("-50");
            PlayerData.Instance.IsShowShengJiRed = false;
        }
        );



        //print(ShengJICount);
        //if (addShengJICountAction != null)
        //{
        //    addShengJICountAction();
        //}
    }

   //public static UnityAction addShengJICountAction;
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        AndroidAdsDialog.Instance.requestSJHBWithDrawList();
        //  creactZhuBo.onClick.AddListener(()=>CreactZhiBoJian());
    }
    private void Start()
    {
        scrollManager.isStart = false;
        //eWaiTiXianIcon.SetParent(UIManager.Instance.showRootMain);
        AndroidAdsDialog.Instance.RequestActiveDay();
        //PlayerDate.Instance.expenddiamondAction += ShowShengJiTips;
        //PlayerDate.Instance.expengdGoldAction += ShowShengJiTips;
        
        //UnityActionManager.Instance.AddAction("AddZhiBoJian", PlayVideoShenJiEvent);
        StartCoroutine(InitStart());
        //creactFloor.onClick.AddListener(()=>CreactZhiBoJian());
        UnityActionManager.Instance.AddAction<bool>("AddZhiBoJian", PlayVideoShenJiEvent);
        if (GuideManager.Instance.isFirstGame)
        {

            daohanlanGo.SetActive(false);
            zhiBoJianRect.enabled = false;
            MoneyManager.Instance.gameObject.SetActive(false);
        }
        
    }
    private void OnDestroy()
    {
        Instance = null;
        //PlayerDate.Instance.expenddiamondAction -= ShowShengJiTips;
        //PlayerDate.Instance.expengdGoldAction -= ShowShengJiTips;
    }
    private IEnumerator InitStart()
    {

        yield return null;
        //scrollRect = scrollManager.GetComponent<ScrollRect>();
       // task = UIManager.Instance.canvas_Main.transform.Find("Panel_Task");
        //JavaCallUnity.Instance.GetDay("5");
        if (GuideManager.Instance.isFirstGame)
        {
            
            daohanlanGo.SetActive(false);
            var mask = UIManager.Instance.canvas_Main.transform.Find("Mask");
            mask.gameObject.SetActive(true);
            peopleEffect = mask.GetComponent<PeopleEffect>();
            vector = houseTf.localPosition.y;
            //scrollRect.enabled = false;
            scale = FindObjectOfType<Canvas>().scaleFactor;
            houseTf.localPosition = new Vector3(0, vector - 1334, 0);
            houseTf.DOLocalMoveY(vector, 1f).onComplete += () => StartCoroutine(Guide());
            //for (int i = 0; i < 50; i++)
            //{
            //    AddZhiBoJian(ConfigManager.Instance.GetActor());
            //}
            AddZhiBoJian(ConfigManager.Instance.GetActor());
            AddZhiBoJian(ConfigManager.Instance.GetActor());
            canvas = zhibojianList[0].transform.GetChild(2).GetComponentInChildren<CanvasGroup>();
            
            canvas.blocksRaycasts = false;
            canvas.alpha = 0;
            canvasGroup.ignoreParentGroups = true;
        }
        else
        {
            if (PlayerData.Instance.actorDateList.Count <= 0)
            {
                //for (int i = 0; i < 50; i++)
                //{
                //    AddZhiBoJian(ConfigManager.Instance.GetActor());
                //}
                AddZhiBoJian(ConfigManager.Instance.GetActor());
                AddZhiBoJian(ConfigManager.Instance.GetActor());
            }
            else
            {
              yield return StartCoroutine(  LoadDate());
            }

           

        }
        //PlayerDate.Instance.diamondAction += RefreshShenJIShow;
        //PlayerDate.Instance.expenddiamondAction += RefreshShenJIShow;
        // PlayerDate.Instance.fullTemporaryDaimondAction += FullBurse;
      //  RefreshShenJIShow();
        var zhibojian = zhibojianList.FindAll(s => s.actorDate.item_id == 0);
        if (zhibojian == null || zhibojian.Count == 0)
        {

            konxianCount = 0;
            // kongxianBt.interactable = false;
            //kongxianCount.text = "0";
        }
        else
        {
            konxianCount = zhibojian.Count;

            //  kongxianBt.interactable = true;
            //kongxianCount.text = zhibojian.Count.ToString();
        }
        //zhibojianList[currentZhuBoIndex].ShowZhiBoJian(true);
        RefreshKonXianZhuBo();
      //  if(MoneyManager.Instance.shengJiTips!=null)
       // MoneyManager.Instance.shengJiTips.RefreshStatus();
    }

    
    /// <summary>
    /// 增加空闲直播间
    /// </summary>
    /// <param name="canKu"></param>
    public void AddKonXianZhiBo(ZhiBoJian canKu)
    {
        if (!kongXianzhibojianList.Contains(canKu))
            kongXianzhibojianList.Add(canKu);
    }
    /// <summary>
    /// 移除空闲直播间
    /// </summary>
    /// <param name="canKu"></param>
    public void RemoveKonXianZhiBo(ZhiBoJian canKu)
    {
        if (kongXianzhibojianList.Contains(canKu))
            kongXianzhibojianList.Remove(canKu);
    }
    public void SetKonxianCount(int count)
    {
        konxianCount += count;
        RefreshKonXianZhuBo();

    }
    public void RecoverGuideStates()
    {
       // scrollRect.enabled = true;
        daohanlanGo.SetActive(true);
        zhiBoJianRect.enabled = true;
        scrollManager.RefreshScrollRect();
    }
    public void RefreshGuideShow()
    {
        //var canvas = zhibojianList[0].GetComponentInChildren<CanvasGroup>();

        //canvas.blocksRaycasts = true;

        //canvas.alpha = 1;
        StartCoroutine(AnimZhiBojian());
    }
    
    IEnumerator AnimZhiBojian()
    {

        peopleEffect.ShowMask(0.5f/*, ()=>peopleEffect.PlayZhiBoAnimation(null)*/);//添加动画
        yield return new WaitForSeconds(1f);
        PeopleEffect.Instance.SetTips2(PeopleEffect.Instance.jixuGo.GetComponent<RectTransform>(), PeopleEffect.Instance.jixuGo.transform.GetChild(0).position);
        PeopleEffect.Instance.jixuGo.gameObject.SetActive(true);
        AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, "点击大楼开始带货！", Color.black, null, null, 1f);
        PeopleEffect.Instance.jixuGo.onClick.AddListener(() =>
        {
            ToggleManager.Instance.GoGame();
            PeopleEffect.Instance.HideTips();
            PeopleEffect.Instance.SetTips(zhibojianList[0].proRect, Vector2.zero, false);
            MoneyManager.Instance.gameObject.SetActive(true);
            MoneyManager.Instance.redTf.gameObject.SetActive(true);
            PeopleEffect.Instance.jixuGo.gameObject.SetActive(false);
            AndroidAdsDialog.Instance.UploadDataEvent("new_version_guide2");
        });
        //peopleEffect.SetTips(zhibojianList[0].proRect, Vector2.zero,false);
        //var canvas = zhibojianList[0].GetComponentInChildren<CanvasGroup>();
        canvasGroup.ignoreParentGroups = false;
        canvas.blocksRaycasts = true;
       // peopleEffect.SetTips(zhibojianList[0].proRect, zhibojianList[0].guideTarget2.position,false);
        //canvas.alpha = 1;
        while (canvas.alpha < 1)
        {
            canvas.alpha += 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
       
       //  scrollRect.enabled = true;
        //peopleEffect.ShowMask();
        //peopleEffect.SetTips(zhibojianList[0].proRect, zhibojianList[0].guideTarget2.position);
    }
    AudioSource audioClip;
    public void StopJiaoCheng1Sound()
    {
        if (audioClip.isPlaying)
        {
            audioClip.Stop();
        }
    }
    private IEnumerator Guide()
    {
        yield return new WaitForSeconds(1);
        peopleEffect.gameObject.SetActive(true);
        audioClip= AudioManager.Instance.PlaySound("jiaocheng1");
        AndroidAdsDialog.Instance.UploadDataEvent("new_version_guide1");
        //maskEffect.Init();
        peopleEffect.LeftShow("欢迎来到欢乐带货<color=#FF058B>极速赚钱版</color> ",()=> { peopleEffect.HideMask(); RefreshGuideShow(); }, null);
     Produce produce1=   ConfigManager.Instance.GetProduce(8);
        Produce produce2 = ConfigManager.Instance.GetProduce(2);
        for (int i = 0; i < 4; i++)
        {
            FaHuoPanel.Instance.faHuo.CreactQiPao(10, produce1, FaHuoPanel.Instance.fahuoBorns[i]);
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 0; i < 4; i++)
        {
           FaHuoPanel.Instance.faHuo.CreactQiPao(10, produce2, FaHuoPanel.Instance.fahuoBorns[i]);
            yield return new WaitForSeconds(0.1f);
        }
        //FaHuoPanel.Instance.faHuo.CreactQiPao(10, ConfigManager.Instance.GetProduce(8), FaHuoPanel.Instance.fahuoBorn);
    }

    int clickCount = 0;

    public override void SetHideOrShow(bool value)
    {
        //MoneyManager.Instance.ShowNew(value);
        base.SetHideOrShow(value);
        Panel_ZhuBoList.Instance.SetShowOrHide(value);
        daoHangLanManager.SetShow(value);
        if (value)
        {
         
            //countClick++;
            ToggleManager.Instance.HideTips(1);

            // ToggleManager.Instance.StartTime(true);

            //  AndroidAdsDialog.Instance.UploadDataEvent("sell_scene");
            if (!GuideManager.Instance.isFirstGame)
            { AndroidAdsDialog.Instance.UploadDataEvent("click_scene_live");
                MoneyManager.Instance.ShowShengJiTips(zhibojianList[currentIndex].actorDate.actor_name.ToString(), zhibojianList[currentIndex].actorDate.need_level_new_actor.ToString() + "级");
            }
            else
            {
                clickCount++;
                if (clickCount == 1)
                {
                    PeopleEffect.Instance.HideTips();
                    PeopleEffect.Instance.HideMask();

                    PeopleEffect.Instance.ShowMask(0.5F, 0.1F,
                   () => { PeopleEffect.Instance.SetTips(zhiBoJianRect.GetComponent<RectTransform>(),Vector2.zero,false);
                       zhiBoJianRect.enabled = true;
                       PeopleEffect.Instance.PlayZhiBoAnimation();
                   });
                }
            }
        }
        else
        {
           
            RefreshKonXianZhuBo();
            //if (GuideManager.Instance.isFirstGame)
            //{
            //    zhibojianList[0].shenjiButton.gameObject.SetActive(false);
            //}
        }
    }
    private IEnumerator LoadDate()
    {
        for (int i = 0; i < PlayerData.Instance.actorDateList.Count; i++)
        {
            var actor = Instantiate(ResourceManager.Instance.GetProGo("ZhiBoJian"), parentTf).GetComponent<ZhiBoJian>();
            actor.index = i;
            actor.actorDate = PlayerData.Instance.actorDateList[i];
            //actor.Init();
            zhibojianList.Add(actor);
            AddKonXianZhiBo(actor);
            actor.Init();
            yield return null;
           
        }
        currentIndex = PlayerData.Instance.actorDateList.Count - 1;
        scrollManager.RefreshScrollRect();
        scrollManager.RefreshPos(PlayerData.Instance.actorDateList.Count);
        floorup = ConfigManager.Instance.GetCurrentLevel(PlayerData.Instance.actorDateList.Count);
    }

 public   Floor_Up floorup;
    private void RefreshShenJIShow()
    {
       
        if (true)
        {
            shengJiFloorImg.sprite = ResourceManager.Instance.GetSprite("影片icon");
            shengJiFloorImg.SetNativeSize();
            shengJiFloorImg.rectTransform.anchoredPosition = new Vector2(-36.6f, 0);
          shengjiText.text = "招募";
            shengjiText.fontSize = 22;
            
           

        }
        //else if (floorup.floor_cost == 2)
        //{

        //    shengJiFloorImg.sprite = ResourceManager.Instance.GetSprite("钻石");
        //    shengjiText.text = floorup.floor_cost_num.ToString();
        //    shengJiFloorImg.rectTransform.anchoredPosition = new Vector2(-36.6f, 0);
        //    shengJiFloorImg.rectTransform.sizeDelta = new Vector2(22, 22);
        //    shengjiText.fontSize = 20;
        //    //if(PlayerDate.Instance.diamond< floorup.floor_cost_num)
        //    //  {
        //    //      creactFloor.interactable = false;
        //    //  }
        //    //  else
        //    //  {
        //    //      creactFloor.interactable = true;
        //    //  }
        //}
        //else if(floorup.floor_cost == 3)
        //{
        //    shengJiFloorImg.sprite = ResourceManager.Instance.GetSprite("金币");
        //    shengJiFloorImg.rectTransform.sizeDelta = new Vector2(22, 22);
        //    shengJiFloorImg.rectTransform.anchoredPosition = new Vector2(-36.6f, 0);
        //    shengjiText.text = floorup.floor_cost_num.ToString();
        //    shengjiText.fontSize = 20;
        //}
       
    }
    public void CreactZhiBoJian(UnityAction unityAction=null)
    {
        AudioManager.Instance.PlaySound("bubble1");
        AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.Warning);
    
        if (true)
        {
            //print("播放激励视频");
            AndroidAdsDialog.Instance.UploadDataEvent("click_new_zhaomu");
          
                AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_ADDZHUBO);

#if UNITY_EDITOR
            //creactZhuBoTiming.ClickEvent();
                  PlayVideoShenJiEvent();

#elif UNITY_ANDROID

      
        
#endif
            
            //else if(GuideManager.Instance.isFirstGame)
            //{
            //    AndroidAdsDialog.Instance.UploadDataEvent("new_course_11");
            //    GuideManager.Instance.AchieveGuide();
            //    PlayVideoShenJiEvent();
            //    //GuideManager.Instance.AchieveGuide();
            //    PeopleEffect.Instance.HideTips();
            //    PeopleEffect.Instance.HideMask();
            //    shengJiFloorImg.color = new Color(1, 1, 1, 1);
            //    //PeopleEffect.Instance.ShowMask(0.5F,0.1F,
            //    //    ()=> PeopleEffect.Instance.SetTips(zhibojianList[1].proRect, zhibojianList[1].guideTarget2.position, true, RotaryType.RightToLeft));
            //    PeopleEffect.Instance.SetTips( zhibojianList[1].guideTarget2, true, RotaryType.RightToLeft);

            //}
            //else if(!GuideManager.Instance.isFirstGame && isFirstClickZhiBoPanel)
            //{
            //    PlayVideoShenJiEvent();
            //    SetisFistClick();
            //    PeopleEffect.Instance.HideTips();
            //    PeopleEffect.Instance.HideMask();
            //    shengJiFloorImg.color = new Color(1, 1, 1, 1);
            //}
        }
        //else if (floorup.floor_cost == 2)
        //{
        //    if (PlayerDate.Instance.diamond < floorup.floor_cost_num)
        //    {
        //        AndroidAdsDialog.Instance.ShowToasts("钻石不够", ResourceManager.Instance.GetSprite("钻石不足"), Color.red);
        //        Debug.LogError("钻石不够");
              
        //    }
        //    else
        //    {
        //        PlayerDate.Instance.ExpendDiamond(floorup.floor_cost_num);
        //        PlayVideoShenJiEvent();

        //        if (GuideManager.Instance.isFirstGame )
        //        {
        //            AndroidAdsDialog.Instance.UploadDataEvent("new_course_11");
        //            //GuideManager.Instance.AchieveGuide();
                    
        //            PeopleEffect.Instance.HideTips();
        //            PeopleEffect.Instance.HideMask();
        //            shengJiFloorImg.color = new Color(1, 1, 1, 1);
        //            PeopleEffect.Instance.SetTips(zhibojianList[1].guideTarget2, true, RotaryType.RightToLeft);
        //        }
        //        if (unityAction != null)
        //            unityAction();
        //    }
        //}
        //else if(floorup.floor_cost == 3)
        //{
        //    if (PlayerDate.Instance.gold < floorup.floor_cost_num)
        //    {
        //        PlayerDate.Instance.AddGoldNotEnoughCount(floorup.floor_cost_num, (int)(floorup.floor_cost_num - PlayerDate.Instance.gold));
        //        AndroidAdsDialog.Instance.ShowToasts("金币不够", ResourceManager.Instance.GetSprite("金币不足"), Color.red);
        //        Debug.LogError("金币不够");
              
        //    }
        //    else
        //    {
        //        PlayerDate.Instance.Expend(floorup.floor_cost_num);
        //        PlayVideoShenJiEvent();
        //        if (GuideManager.Instance.isFirstGame )
        //        {
        //            AndroidAdsDialog.Instance.UploadDataEvent("new_course_11");
        //            //GuideManager.Instance.AchieveGuide();

        //            PeopleEffect.Instance.HideTips();
        //            PeopleEffect.Instance.HideMask();
        //            shengJiFloorImg.color = new Color(1, 1, 1, 1);
        //            PeopleEffect.Instance.SetTips(zhibojianList[1].guideTarget2, true, RotaryType.RightToLeft);
        //        }
        //        if (unityAction != null)
        //            unityAction();
        //    }
        //}
      
        //RefreshKonXianZhuBo();
        //AddZhiBoJian(ConfigManager.Instance.GetActor());

    }
    public bool isShengJi = false;
    public void PlayVideoShenJiEvent(bool isChouJiang=false)//播放视频回调
    {
        AddZhiBoJian(ConfigManager.Instance.GetActor(), isChouJiang);
        AndroidAdsDialog.Instance.UploadDataEvent("finish_add_actor");
        //RefreshShenJIShow();
        AndroidAdsDialog.Instance.AddSignDataCount(3);
     
        // AndroidAdsDialog.Instance.ShowToasts(string.Format("解锁新主播{0}",zhibojianList[zhibojianList.Count-1].actorDate.actor_name), ResourceManager.Instance.GetSprite("招募主播"), Color.black);
        isShengJi = true;
        //MoveNextZhuBo();
        if(!GuideManager.Instance.isFirstGame)
        ZhiBoPanel.Instance.ShowShengJiTips();
       // MoneyManager.Instance.shengJiTips.SetShowOrHide(false, 1f,
             // () => { MoneyManager.Instance.ShowShengJiTips(ZhiBoPanel.Instance.zhibojianList[ZhiBoPanel.Instance.currentIndex].actorDate.actor_name.ToString(), ZhiBoPanel.Instance.zhibojianList[ZhiBoPanel.Instance.currentIndex].actorDate.need_level_new_actor.ToString() + "级"); });
    }

    public UnityAction addZhiBoJianAction;

    public int currentIndex;
    public void AddZhiBoJian(ActorDate actorDate,bool value=false )
    {
        if (actorDate != null)
        {
            var actor = Instantiate(ResourceManager.Instance.GetProGo("ZhiBoJian"), parentTf).GetComponent<ZhiBoJian>();
            actor.index = PlayerData.Instance.actorDateList.Count;
            currentIndex = actor.index;
            actor.actorDate = actorDate;
            zhibojianList.Add(actor);
            PlayerData.Instance.actorDateList.Add(actorDate);
            AddKonXianZhiBo(actor);
            actor.Init();
            SetKonxianCount(1);
            // zhibojianList.Add(actor);
            floorup = ConfigManager.Instance.GetCurrentLevel(PlayerData.Instance.actorDateList.Count);
            UnityActionManager.Instance.DispatchEvent<ActorDate>("CreactZhuBoItem", actorDate);
            scrollManager.RefreshPos(PlayerData.Instance.actorDateList.Count);
            if (!GuideManager.Instance.isFirstGame)

            {
                // if (zhibojianList != null && zhibojianList.Count > 0)
                //scrollManager.Move(zhibojianList.Count - 1);
            }
            if (!value)
            {
                if (addZhiBoJianAction != null)
                {
                    addZhiBoJianAction();
                }
            }
        }
    }
    public void MoveKonXianZhuBo()
    {
        if (!scrollManager.isStart) return;
        // AndroidAdsDialog.Instance.UploadDataEvent("click_free_actor");
        AudioManager.Instance.PlaySound("bubble1");
        var zhibojian=   zhibojianList[zhibojianList.Count-1];
        
        if (zhibojian != null)
        { scrollManager.Move(zhibojian.index);
            print(zhibojian.actorDate.actor_louceng); }
        //AndroidAdsDialog.Instance.ShowTableVideo("0");
        AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.Warning);
    }
  
    public int currentZhuBoIndex = 0;
    public void MoveNextZhuBo(float time=0.2f,UnityAction unityAction=null)
    {if (!scrollManager.isStart) return;
        if (currentZhuBoIndex <= zhibojianList.Count - 2)
        {
            AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.LightImpact);            
            currentZhuBoIndex++;
            int index = currentZhuBoIndex;
            zhibojianList[currentZhuBoIndex].StopZhiBoAnim(true);
            zhibojianList[currentZhuBoIndex].levelAndlouCengAnim.gameObject.SetActive(false);
            scrollManager.MoveLerp(currentZhuBoIndex, time, ()=> {
                unityAction?.Invoke();
                //Debug.LogError("执行滑动回调");
                zhibojianList[index].levelAndlouCengAnim.gameObject.SetActive(true);
                zhibojianList[index].levelAndlouCengAnim.Animation();
                zhibojianList[index-1].StopZhiBoAnim();
                if (GuideManager.Instance.isFirstGame)
                {
                    AndroidAdsDialog.Instance.UploadDataEvent("new_version_guide8");
                    PeopleEffect.Instance.HideZhiBoAnimation();
                    zhiBoJianRect.enabled = false;
                }
            });
           
            //scrollManager.Move(zhibojian.index);
        }
        else
        {

            AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
          {
                "已经到顶啦！","升级主播开启更高楼层吧！","" }, null, new Color[]
              {
                    Color.black,Color.red,Color.black
              }, null,0.8f);
        }
    }
    public void MoveLastZhuBo(float time = 0.2f, UnityAction unityAction = null)
    {
        if (!scrollManager.isStart) return;
        if (currentZhuBoIndex >= 1)
        {
            AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.LightImpact);
            // zhibojianList[currentZhuBoIndex].ShowZhiBoJian(false);

            currentZhuBoIndex--;
            int index = currentZhuBoIndex;
            zhibojianList[currentZhuBoIndex].StopZhiBoAnim(true);
            zhibojianList[currentZhuBoIndex].levelAndlouCengAnim.gameObject.SetActive(false);
            scrollManager.MoveLerp(currentZhuBoIndex,time, () =>
            {
                unityAction?.Invoke();
                   zhibojianList[index].levelAndlouCengAnim.gameObject.SetActive(true);
                zhibojianList[index].levelAndlouCengAnim.Animation();
                zhibojianList[index+1].StopZhiBoAnim();
            });
            //zhibojianList[currentZhuBoIndex].ShowZhiBoJian(true);
        }
        else
        {

          
            AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
         {
                "已经","到底","啦!" }, null, new Color[]
             {
                    Color.black,Color.red,Color.black
             }, null);

        }
    
    }
    public void MoveZhuBo(int index,bool value=true,UnityAction unityAction=null, float time = 0.2f)
    {
        if (index == currentZhuBoIndex) return;
            if (!scrollManager.isStart) return;
        zhiBoJianRect.enabled = false;
     
        int index1 = currentZhuBoIndex;
        currentZhuBoIndex = index;

        if (value)
        {
            zhibojianList[currentZhuBoIndex].StopZhiBoAnim(true);
            scrollManager.MoveLerp(currentZhuBoIndex,time,
            
            ()=> { unityAction?.Invoke();
                zhibojianList[index1].StopZhiBoAnim();
               
                zhiBoJianRect.enabled = true;
            }); 
        
        }
        else
        {
            zhibojianList[index1].StopZhiBoAnim();
            zhibojianList[currentZhuBoIndex].StopZhiBoAnim(true);
            zhiBoJianRect.enabled = true;
            scrollManager.Move(currentZhuBoIndex);
        }
    }
    /// <summary>
    /// 播放创建主播动画
    /// </summary>
    /// <param name="value"></param>
    public void PlayCreactAnim(bool value)

    {
        //if (!Creactanimator.GetCurrentAnimatorStateInfo(0).IsName("creactkuaidiyuan"))
        Creactanimator.SetBool("walk", value);


    }
    /// <summary>
    /// 获取空闲主播
    /// </summary>
    public ZhiBoJian GetKonXianZhuBo()
    {
        //var zhibojian = zhibojianList.Find(s => s.actorDate.item_id == 0);

        //if (zhibojian != null)
        //{
        //    return zhibojian;
        //}
        //return null;
     if(kongXianzhibojianList!=null&& kongXianzhibojianList.Count != 0)
        {
            return kongXianzhibojianList[Random.Range(0, kongXianzhibojianList.Count)];
        }
        return null;


    }
    /// <summary>
    /// 统计空闲人数
    /// </summary>
    public void RefreshKonXianZhuBo()
    {
        if (GuideManager.Instance.isFirstGame) return;
        if (!ToggleManager.Instance.toggles[1].isOn)
        {
            print("zhibojiancount"+konxianCount);
            print("zhibojiancount++" + kongXianzhibojianList.Count);
            //var zhibojian = zhibojianList.FindAll(s => s.actorDate.item_id == 0);
            if (kongXianzhibojianList.Count <= 0)
            {
                
                ToggleManager.Instance.HideTips(1);
                // kongxianBt.interactable = false;
                //kongxianCount.text = "0";
            }
            else
            {
                ToggleManager.Instance.SetTips(1);
              //  kongxianBt.interactable = true;
              //kongxianCount.text = zhibojian.Count.ToString();
            }
        }
        else
        {
            if (kongXianzhibojianList.Count <= 0)
            {

              //  PlayCreactAnim(true);
            }
            else
            {
               // PlayCreactAnim(false);

            }

        }
    }
  

    public void OpenTask()
    {
        //AndroidAdsDialog.Instance.UploadDataEvent("click_mission_center");

        AudioManager.Instance.PlaySound("bubble1");
        //TaskManager.Instance.Show();
        AndroidAdsDialog.Instance.OpenJiangLi(PlayerData.Instance.SalesVolume);
        AndroidAdsDialog.Instance.UploadDataEvent("click_myshop_lingjiang_btn");
  
        AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.Warning);
    }

  public void OpenZhuBoList()
    {
        Panel_ZhuBoList.Instance.ShowUI();
        StopShengJiTips();
    }
   
}









