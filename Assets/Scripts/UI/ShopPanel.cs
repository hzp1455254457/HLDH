using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Spine.Unity;
using DG.Tweening;
namespace HLDH
{
    public class ShopPanel : PanelBase
    {
        public static bool isShow = false;//弹窗是否显示中
                                          // Start is called before the first frame update
        public ShopUI[] shopArry;
        public Produce[] ProduceArry;
        public bool isHaveBaoli;
       // public GameObject proGo;
        public ShopUI currentShopUI;
        public FreeShop freeShop;
        public RectTransform rectTransform;//商店新手遮罩
        //public RectTransform rectTransform2;
        public PeopleEffect peopleEffect;
        public RectTransform targetGuide1;
       
        public ZhiBoPanel boPanel;
     
        Transform backTf;
        RectTransform backRect;
        //  public GameObject shopMask;//商品弹窗出现时遮住下面三个toggle
        protected override void Awake()
        {
            base.Awake();
            backTf = transform.GetChild(0);
            backRect = backTf.GetComponent<RectTransform>();
        }
        private void Start()
        {
            StartCoroutine(InitStart());
        }

        private IEnumerator InitStart()
        {
            yield return 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    //Global.NumberToChinese(i)
            //    print("输出数字" + Global.NumToChinese(i.ToString()));
            //}
            AndroidAdsDialog.Instance.RequestActiveDay();
           
            //AndroidAdsDialog.Instance.RequestCheckSignReward();
            boPanel = UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel;
            shopArry = GetComponentsInChildren<ShopUI>();

            ProduceArry = new Produce[shopArry.Length];
            GameStartGetProduce();
            peopleEffect = UIManager.Instance.canvas_Main.transform.Find("Mask").GetComponent<PeopleEffect>();
            //proGo.transform.SetParent(UIManager.Instance.canvas_Main.transform);
           //proGo.transform.SetSiblingIndex(7);
            // PlayerDate.Instance.SaveShopDate();
            //if (GuideManager.Instance.isFirstGame)
            //{
            //  rectTransform.
            //}
        }

        public override void Hide()
        {
            base.Hide();
            //backTf.DOLocalMoveY(-backRect.rect.height, 0.5f);

        }
        public override void Show()
        {
            base.Show();
            backTf.DOLocalMoveY(-backRect.rect.height, 0f);
            backTf.DOLocalMoveY(0, 0.5f);
        }
        public override void SetHideOrShow(bool value)
        {
            base.SetHideOrShow(value);
            if (value)
            {
               // AndroidAdsDialog.Instance.UploadDataEvent("shop_scene");
                //ToggleManager.Instance.StartTime(false);
                ToggleManager.Instance.HideTips(0);
                if (!GuideManager.Instance.isFirstGame)
                {
                    AndroidAdsDialog.Instance.UploadDataEvent("click_scene_myshop");
                }
            }
        }
        //public void ShowTomorrow(int id, int count)
        //{
        //    freeShop.SetFreeShop(id, count);
        //    //freeShop.ShowGetFreeProduce(id1, count1);
        //}
        //public void GetFreeProduce(int id, int count)
        //{
        //    freeShop.ShowGetFreeProduce(id, count);
        //}
        public void GameStartGetProduce()
        {
            //if (PlayerDate.Instance.GetShop() == null)
            var produces = ConfigManager.Instance.GetProduces(PlayerData.Instance.actor_maxlevel);
            //{
            //if (!GuideManager.Instance.isFirstGame)
            //{
            //    produces.RemoveAt(7);
            //    //var produces = ConfigManager.Instance.GetProduce();
            //}
            for (int i = 0; i < ProduceArry.Length; i++)
            {
                shopArry[i].index = i;
                if (i == 0)
                {
                    if (GuideManager.Instance.isFirstGame)
                    {
                        ProduceArry[i] = produces.Find(s => s.item_id == 8);
                    }
                    else
                    ProduceArry[i] = produces[UnityEngine.Random.Range(0, produces.Count)];
                }
                else
                {
                    ProduceArry[i] = produces[UnityEngine.Random.Range(0, produces.Count)];

                }
                shopArry[i].SetProduce(ProduceArry[i]);

                if (!isHaveBaoli)
                    produces.Remove(ProduceArry[i]);
                else
                {
                    produces.Remove(ProduceArry[i]);
                    produces = produces.FindAll(s => s.profit_state != 2);
                }
            }
            //}
            //else
            //{
            //    var produces = ConfigManager.Instance.GetProduce();
            //    var infos = PlayerDate.Instance.GetShop();
            //    for (int i = 0; i < infos.Length; i++)
            //    {
            //        shopArry[i].index = i;

            //        var produce = produces.Find(s => s.item_id == infos[i].itemId);
            //        shopArry[i].SetProduce(produce);
            //        ProduceArry[i] = produce;
            //    }
            //}

        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="value">是否一定刷新出暴利</param>
        public void SetShopProduce(ShopUI shop, bool value = true)
        { //isShow = true;
            
            RefreshStates();
            var produces = ConfigManager.Instance.GetProduces(PlayerData.Instance.actor_maxlevel);
            for (int i = 0; i < ProduceArry.Length; i++)
            {
                produces.Remove(ProduceArry[i]);
            }
            // ProduceArry[shop.index]=  produces[UnityEngine.Random.Range(0, produces.Count)];
            if (value)
            {
                if (!isHaveBaoli)
                { }  //produces.Remove(ProduceArry[i]);
                else
                {
                    //produces.Remove(ProduceArry[i]);
                    produces = produces.FindAll(s => s.profit_state != 2);

                }
                ProduceArry[shop.index] = produces[UnityEngine.Random.Range(0, produces.Count)];
                //if (!isHaveBaoli)
                //{ shop.SetProduce(ProduceArry[shop.index], UnityEngine.Random.Range(ProduceArry[shop.index].num_min, ProduceArry[shop.index].num_max), ProduceArry[shop.index].profit_state); }
                //else
                //{
                shop.SetProduce(ProduceArry[shop.index]);
                //}
            }
            else
            {
                produces = produces.FindAll(s => s.profit_state == 2);
                shop.SetProduce(produces[shop.index]);
            }
           
            //shop.SetProduce();
        }
        int i = 0;
       // int type = 0;
        /// <summary>
        /// 商店点击获取按钮
        /// </summary>
        /// <param name="shopUI"></param>
        public void SetShopInfo(ShopUI shopUI)
        {
            //this.type = type;
           // isShow = true;
            //currentShopUI = shopUI;
            // proGo.SetActive(true);
            //if (currentShopUI.currentProduce.item_cost_type == 1)
            //{
                if (GuideManager.Instance.isFirstGame)
                {
                    i++;
                    if (i == 1)
                    {
                        peopleEffect.HideTips();

                       // AndroidAdsDialog.Instance.UploadDataEvent("jiaocheng2");

                        SetCount(8000);
                       // peopleEffect.SetTips(rectTransform2, targetGuide2.position, false);
                    }
                 
                }
                else
                {
                    //AndroidAdsDialog.Instance.ShowRewardVideo("-11");
                   // AndroidAdsDialog.Instance.UploadDataEvent("item_video_show");
                    print("播放广告");
#if UNITY_EDITOR

                    SetCount(2000);

#elif UNITY_ANDROID
   
#endif
                }


            //}
            //else if (currentShopUI.currentProduce.item_cost_type == 2)
            //{
            //    if (PlayerDate.Instance.gold >= currentShopUI.currentProduce.item_cost_num)
            //    {
            //        SetCount(NumberGenenater.GetProduceCount());
            //        PlayerDate.Instance.Expend(currentShopUI.currentProduce.item_cost_num);
            //    }
            //}
            //else
            //{
            //    if (PlayerDate.Instance.diamond >= currentShopUI.currentProduce.item_cost_num)
            //    {
            //        SetCount(NumberGenenater.GetProduceCount());
            //        PlayerDate.Instance.ExpendDiamond(currentShopUI.currentProduce.item_cost_num);
            //    }
            //}
       
}
        // int count ;
        public void SetCount(int count)
        {
            New.ShopPanel.Instance. SetCount(count,currentShopUI.currentProduce);
            //AudioManager.Instance.PlaySound("bubble1");
                AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.Warning);

            
                currentShopUI.GetProduceCount(count);
                //proGoImg.sprite = ResourceManager.Instance.GetSprite(currentShopUI.currentProduce.item_pic);
                //proGoCount.text = string.Format("<color=#15FF00>{0}</color>", count);
                //proGoName.text = string.Format("{0}", currentShopUI.currentProduce.item_name);
                //ShowShopMask(true);
                //AndroidAdsDialog.Instance.ShowFeedAd(540);

                //if (!GuideManager.Instance.isFirstGame)
                //{

                //    profit.text = string.Format("卖出后可提现至微信约{0:F}元", count * currentShopUI.currentProduce.item_profit / 100000f * 1.6f);
                //}
                //else
                //{
                //    AudioManager.Instance.PlaySound("jiaocheng7");
                //    profit.text = string.Format("卖出后可提现至微信约{0:F}元", 1.32f);
                //}
            }
            //else
            //{
            //    //显示弹窗
            //    currentShopUI.GetProduceCount(count);
            //    AudioManager.Instance.PlaySound("bubble1");
            //    Shop.Instance.gameObject.SetActive(false);

            //    RedManager.Instance.ShowUI();
            //    // profit.text = string.Format("卖出后可提现至微信约{0:F}元", count * currentShopUI.currentProduce.item_profit / 100f * 1.6f);

            //}


        //private void PlayAnimation()
        //{
        //    profit.gameObject.SetActive(false);
        //    skeletonGraphic.gameObject.SetActive(false);
        //    proGoCount.transform.localScale = Vector3.zero;
        //    proGoName.color = Color.clear;
        //    proTanChuangTf.localScale = Vector3.zero;
        //    proGoImg.gameObject.SetActive(false);
        //    GetBtGo.SetActive(false);
        //    proTanChuangTf.DOScale(Vector3.one * 1.1f, 0.5f).onComplete
        //        += () => proTanChuangTf.DOScale(Vector3.one * 1f, 0.3f).onComplete
        //    += () =>
        //    {

        //    //proGoCount.gameObject.SetActive(true);
        //    proGoCount.transform.DOScale(Vector3.one, 0.4f).onComplete += () =>
        //        {
        //            proGoImg.gameObject.SetActive(true);
        //        //proGoCount.text = string.Format("{0}<color=#15FF00>{1}</color>个", currentShopUI.currentProduce.item_name, currentShopUI.count);
        //        proGoName.color = Color.white;
        //            StartCoroutine(Delay(0.4f, () =>
        //            {
        //                skeletonGraphic.gameObject.SetActive(true);
        //                skeletonGraphic.AnimationState.SetAnimation(0, "shangpinhuodehou", false);
        //                profit.gameObject.SetActive(true);
        //                GetBtGo.SetActive(true);
        //            //AudioManager.Instance.PlaySound("jiaocheng8");
        //            if (GuideManager.Instance.isFirstGame)
        //                {
        //                    if (i == 1)
        //                    { peopleEffect.SetTips(rectTransform2, targetGuide2.position); }
        //                    else
        //                    {

        //                    }
        //                }
        //            }
        //                ));
        //        };
        //    };
        //}

        private void ShowShopMask(bool value)
        {

            //if (shopMask == null)
            //{
            //    shopMask = UIManager.Instance.canvas_Main.transform.Find("shopMask").gameObject;
            //}
            //shopMask.gameObject.SetActive(value);

        }
        
      //  Produce lastProduce;
        public void GetProduceEvent(bool red = false)
        {
            //GetProduceCount(50);
            //if (!red)
            //{
            //    //AndroidAdsDialog.Instance.OnGameStart(currentShopUI.currentProduce.item_id);
            //    //if (lastProduce != null)
            //    //{
            //    //    AndroidAdsDialog.Instance.OnGameEnd(1, lastProduce.item_id);
            //    //}
            //    //lastProduce = currentShopUI.currentProduce;
            //    //AddProduceAndRefesh();

            //    // skeletonGraphic.gameObject.SetActive(false);
            //   // AndroidAdsDialog.Instance.UploadDataEvent(currentShopUI.currentProduce.item_id.ToString());
            //    AddProduceAndRefesh();
            //    AndroidAdsDialog.Instance.CloseFeedAd();
            //    //AndroidAdsDialog.Instance.CloseFeedAd();
            //    if (GuideManager.Instance.isFirstGame)
            //    {
                    
            //            peopleEffect.HideTips();
            //            ToggleManager.Instance.ShowPanel(1);
            //            peopleEffect.HideMask();
            //            //peopleEffect.ShowMask(()=>peopleEffect.SetTips(boPanel.zhibojianList[0].proRect, boPanel.zhibojianList[0].guideTarget1.position));
            //            boPanel.RefreshGuideShow();
            //          //  AndroidAdsDialog.Instance.UploadDataEvent("jiaocheng3");
            //            AudioManager.Instance.PlaySound("jiaocheng6");
                    
                   
            //    }
            //    else
            //    {  //新增每次获取时，用户自动卖商品
            //        #region
            //        if (boPanel.konxianCount > 0)
            //        {
            //            var zhibo = boPanel.GetKonXianZhuBo();
            //            if (zhibo != null)
            //            {
            //                zhibo.Sell(StockManager.Instance.produceInfos.Find(s => s.produceDate.item_id == lastProduce.item_id));
            //            }
            //        }
            //        #endregion
            //        ToggleManager.Instance.ShowPanel(1);
            //        AndroidAdsDialog.Instance.ShowTableVideo("0");

            //    }
            //    // AndroidAdsDialog.Instance.UploadDataEvent("click_kaixinshouxia");
              
            //      //  AndroidAdsDialog.Instance.UploadDataEvent("click_kaixinshouxia");
            //        proGo.SetActive(false);
            //        ShowShopMask(false);
                
               
            //    isShow = false;
            //}
        }
        public void AddProduceAndRefesh()
        {
            PlayerData.Instance.AddProduce(currentShopUI.currentProduce.item_id, currentShopUI.count);

            RefreshProduce();
        }

        public void RefreshProduce()
        {
            //PlayerDate.Instance.AddRefreshCount();
            SetShopProduce(currentShopUI);
            //currentShopUI = null;
           // PlayerDate.Instance.AddRefreshCount();

        }
        void RefreshStates()
        {
            for (int i = 0; i < shopArry.Length; i++)
            {
                if (shopArry[i].states == 2)
                { isHaveBaoli = true; return; }
                isHaveBaoli = false;
            }

        }
        /// <summary>
        /// 刷新暴利
        /// </summary>
        public void RefeshBaoli()
        {
            RefreshStates();
            if (!isHaveBaoli)
            {
                SetShopProduce(shopArry[0], false);
            }
            else
            {
                var shop = Array.Find(shopArry, s => s.states != 2);
                if (shop != null)
                {
                    SetShopProduce(shopArry[shop.index], false);

                }
                else
                {
                    SetShopProduce(shopArry[0], false);
                }
            }
           // AndroidAdsDialog.Instance.ShowTableVideo("0");
        }

    }
}