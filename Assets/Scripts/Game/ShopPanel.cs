using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace New
    { 

    public class ShopPanel : MonoBehaviour
    {
        public static ShopPanel Instance { get { return instance; } }
        static ShopPanel instance;
        int clickCount = 0;

        Produce lastProduce;
        /// <summary>
        /// 获得商品并存储
        /// </summary>
        /// 

        public void GetProduceEvent()
        {

            MaiDian();
            PlayerData.Instance.AddProduce(currentProduce.item_id, count);
            var panel = UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel;
            if (!GuideManager.Instance.isFirstGame)
            {
                AndroidAdsDialog.Instance.UploadDataEvent("finish_new_redpacket_tanchuang");
                #region
               
                    var zhibo = panel.GetKonXianZhuBo();
                    if (zhibo != null)
                    {
                        //var produceInfo = StockManager.Instance.produceInfos.Find(s => s.produceDate.item_id == currentProduce.item_id);
                        zhibo.Sell(currentProduce.item_id);
                        print("自动售卖完成直播间是++" + zhibo.index);
                        print("自动售卖完成商品" + currentProduce.item_name);
                    
                }
                #endregion
            }
            else
            {
                //ToggleManager.Instance.ShowPanel(1);
                //    AndroidAdsDialog.Instance.ShowTableVideo("0");
                //panel.RecoverGuideStates();//恢复导航栏
                //panel. peopleEffect.ShowMask(0.5f, () => panel. peopleEffect.SetTips(panel.daohanlanGo.transform.GetChild(1).GetComponent<RectTransform>(), panel.daohanlanGuideTf.position, true));
                #region
                clickCount++;
                if (clickCount == 1)
                {
                    Guide(panel);
                    AndroidAdsDialog.Instance.UploadDataEvent("new_course_3");
                }
                else if(clickCount == 2)
                {
                    //GuideManager.Instance.AchieveGuide();
                    GuideManager.Instance.RecoverZhiBoStatus();
                    PeopleEffect.Instance.ShowMask(false);
                    //PeopleEffect.Instance.HideMask();

                    //AndroidAdsDialog.Instance.UploadDataEvent("finishshangjinredpacket_jiaocheng");
                    ToggleManager.Instance.ShowPanel(1);
               //     ToggleManager.Instance.ShowAdward(_redCount, null, awardCount, false,
               //() => PeopleEffect.Instance.ShowMask(0.8f, () => PeopleEffect.Instance.SetTips(
               //      (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).daohanlanGo.transform.GetChild(1).GetComponent<RectTransform>(),
               //      (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).daohanlanGuideTf.position, true)));
                    PeopleEffect.Instance.ShowMask(0.8f, () => PeopleEffect.Instance.SetTips(
                     (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).daohanlanGo.transform.GetChild(1).GetComponent<RectTransform>(),
                     (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).daohanlanGuideTf.position, true));
                    AndroidAdsDialog.Instance.UploadDataEvent("new_course_10");
                    //Time.timeScale = 0;
                }

            }
            #endregion
            if (isXuanPing)
            {
                AndroidAdsDialog.Instance.UploadDataEvent("finish_item_video_suc");
                (UIManager.Instance.GetPanel("Panel_Shop") as HLDH.ShopPanel).RefreshProduce();
            }
            print("自动售卖完成商品" + currentProduce.item_name);
        }

        private void Guide(ZhiBoPanel panel)
        {
            PeopleEffect.Instance.HideTips();
            ToggleManager.Instance.ShowPanel(1);
            PeopleEffect.Instance.HideMask();
            //peopleEffect.ShowMask(()=>peopleEffect.SetTips(boPanel.zhibojianList[0].proRect, boPanel.zhibojianList[0].guideTarget1.position));
            panel.RefreshGuideShow();
           // AndroidAdsDialog.Instance.UploadDataEvent("jiaocheng3");
            //AudioManager.Instance.PlaySound("jiaocheng6");
        }

        private void MaiDian()
        {
            print("埋点");
            //AndroidAdsDialog.Instance.OnGameStart(currentProduce.item_id);
            //if (lastProduce != null)
            //{
            //    AndroidAdsDialog.Instance.OnGameEnd(1, lastProduce.item_id);
            //}
            //lastProduce = currentProduce;

           // AndroidAdsDialog.Instance.UploadDataEvent(currentProduce.item_id.ToString());
            //AndroidAdsDialog.Instance.UploadDataEvent("finish_redpacket");
        }

        /// <summary>
        /// 设置商品
        /// </summary>
        public void SetShopInfo()
        {


            if (GuideManager.Instance.isFirstGame)
            {

              

                //SetCount(8000);
                SetCount(8000);
          
#if UNITY_EDITOR

              //  SetCount(8000);

#elif UNITY_ANDROID
   
#endif
            }
            else
            {
                //AndroidAdsDialog.Instance.ShowRewardVideo("-10");
              //  AndroidAdsDialog.Instance.UploadDataEvent("item_video_show");
                print("播放广告");
#if UNITY_EDITOR

                SetCount(8000);

#elif UNITY_ANDROID
   
#endif
            }

            //else
            //{
            //    if (GuideManager.Instance.isFirstGame)
            //    {
            //        //  peopleEffect.HideTips();
            //        AndroidAdsDialog.Instance.UploadDataEvent("jiaocheng2");

            //        SetCount(8000);

            //        //peopleEffect.SetTips(rectTransform2, targetGuide2.position, false);
            //    }
            //    else
            //    {
            //        SetCount(8000);
            //    }
            //}

        }
        /// <summary>
        /// 获取数量并显示ui
        /// </summary>
        /// <param name="countValue"></param>
        /// 传值说明是选品商店调用的
        /// 
        public void SetCount(int countValue,Produce produce=null)
        {if (produce != null)
            { currentProduce = produce;
                isXuanPing = true;
            }
            else
            {
                isXuanPing = false;
            }
            AudioManager.Instance.PlaySound("bubble1");
            count = countValue;
            Shop.Instance.gameObject.SetActive(false);
            RedManager.Instance.ShowUI();
            // Time.timeScale = 0;
        }
        bool isXuanPing = false;

        Produce currentProduce;
        /// <summary>
        /// 当前商品
        /// </summary>
        public Produce CurrentProduce
        {
            get
            {
                //produceList = ConfigManager.Instance.GetProduce();
                if(GuideManager.Instance.isFirstGame)
                currentProduce = ProduceList.Find(s=>s.item_id==8);
                else
                currentProduce = ProduceList[UnityEngine.Random.Range(0, ProduceList.Count)];

                return currentProduce;
            }
        }
        List<Produce> produceList;
        int currentLevel;
        public List<Produce> ProduceList
        {
            get
            {
                print("currentlevel++" + currentLevel);
                print("PlayerDate.Instance.actor_maxlevel++" + PlayerData.Instance.actor_maxlevel);
                if (currentLevel == PlayerData.Instance.actor_maxlevel)
                    return produceList;
                else
                {
                    produceList = ConfigManager.Instance.GetProduces(PlayerData.Instance.actor_maxlevel);
                    currentLevel = PlayerData.Instance.actor_maxlevel;
                    print("currentlevel++" + currentLevel);
                    return produceList;
                }
            }
        }
        private void Start()
        {
            StartCoroutine(InitStart());
        }

        private IEnumerator InitStart()
        {
            yield return null;
            produceList = ConfigManager.Instance.GetProduces(PlayerData.Instance.actor_maxlevel);
            currentLevel = PlayerData.Instance.actor_maxlevel;
        }

        private void Awake()
        {
            instance = this;
        }
        public int Count
        {
            get { return count; }
        }
        int count;
    }
}