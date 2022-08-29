using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class ShopUINew : MonoBehaviour
{
    // Start is called before the first frame update
    public int index;
    public Image produceImg, statesImg;
    public Text prodceName, profit;
    public Produce currentProduce;
    public Button getProduce;
    public int count;
   // public ShopInfo info = new ShopInfo();
   /// <summary>
   /// 0表示金币不足。1表示金币足够
   /// </summary>
    public int states;
  
    public GameObject[] xiaoHaoShowGo;

    public Image xiaoHaoImg;
    public Text xiaoHaoText;

    public GameObject tipsGo;
    public GameObject baoLiGo;
    public GameObject maxGoldtipsGo;
    public GameObject tasktipsGo;
    public Image btnImg;
    // public GameObject refresh;
    public Material material;
    public SkeletonGraphic skeletonGraphic;
   
    public void SetMaxTips(bool value)
    {
        maxGoldtipsGo.SetActive(value);
    }
    public void SetProduce(Produce produce)
    {
        SetMaxTips(false);
        SetNew(false);
        HideTips();
           currentProduce = produce;
        RefreshXiaoHaoShow();
        produceImg.sprite = ResourceManager.Instance.GetSprite(produce.item_pic);
        prodceName.text = produce.item_name;
        // count = procount;
        // info.count = count;
        //  info.itemId = produce.item_id;
        profit.text = string.Format("利润:{0}元/个", produce.item_profit);
        //
        //
        //
        //countText.text = string.Format("{0}个", count);

        //暴利状态
        // SetNew(produce);
        if (currentProduce.profit_state == 2)
        {
            SetBaoli(true);
        }
        else
        {
            SetBaoli(false);
        }
        if (PlayerData.Instance.gold < produce.item_cost_num)
        {
            btnImg.sprite =ShopPanelNew.Instance. sprites[0];
            states = 0;
        }
        else
        {
            btnImg.sprite = ShopPanelNew.Instance.sprites[1];
            states = 1;
        }
        if (!GuideManager.Instance.isFirstGame)
        {
            skeletonGraphic.material = material;
        }
    }
    public void SetBaoli(bool value)
    {
        baoLiGo.SetActive(value);
    }
    public void SetTips(bool value)
    {

    }
   public void SetNew(bool value)
    {
       // states = produce.profit_state;
        //info.states = produce.profit_state;
        if (value)
        {
            //ShopPanelNew.Instance.isHaveBaoli = true;
            statesImg.gameObject.SetActive(true);
        }
        else
        {
            statesImg.gameObject.SetActive(false);
        }
    }

    public void RecoverGuideStates()
    {
        //refresh.SetActive(true);
    }
    int clickCount = 0;
    public void GetProduce()
    {
        AudioManager.Instance.PlaySound("bubble1");
       ShopPanelNew.Instance.currentShopUI= this;
            GetProduceCount();

    }

    private void GetProduceCount()
    {if(tipsGo.activeInHierarchy)
        tipsGo.SetActive(false);
        SetMaxTips(false);
        //if (currentProduce.item_cost_type == 1)
        //{
        //    //shopPanel.currentShopUI = this;
        //    Shop.Instance.ShowUI(false,currentProduce);
        //    ShopPanelNew.Instance.HideUI();
        //    //shopPanel.currentShopUI = this;
        //}

        //else if (currentProduce.item_cost_type == 2)
        //{

        //    if (PlayerData.Instance.diamond >= currentProduce.item_cost_num)
        //    {
        //        AndroidAdsDialog.Instance.UploadDataEvent("get_item_suc");

        //        // SetCount(NumberGenenater.GetProduceCount());
        //        PlayerData.Instance.ExpendDiamond(currentProduce.item_cost_num);
        //        int red = NumberGenenater.GetRedCount(false);
              
        //        PlayerData.Instance.GetRed(red);
        //        RefreshZhiboProduce(red);
        //        ShopPanelNew.Instance.newGuide.GuideFuncEvent2();
        //    }
        //    else
        //    {
        //        AndroidAdsDialog.Instance.ShowToasts("钻石数量不足", ResourceManager.Instance.GetSprite("钻石不足"), Color.red);
        //       // Debug.LogError("钻石不足");
        //    }
        //}
        //else if (currentProduce.item_cost_type == 3)
        //{
            if (PlayerData.Instance.gold >= currentProduce.item_cost_num)
            {
                AndroidAdsDialog.Instance.UploadDataEvent("get_item_suc");
                //SetCount(NumberGenenater.GetProduceCount());
                PlayerData.Instance.Expend(currentProduce.item_cost_num);
                int red = NumberGenenater.GetRedCount(false);
              
                RefreshZhiboProduce(red);
                PlayerData.Instance.GetRed(red);

                ShopPanelNew.Instance.newGuide.GuideFuncEvent2();
            }
            else
            {
                PlayerData.Instance.AddGoldNotEnoughCount(currentProduce.item_cost_num, (int)(currentProduce.item_cost_num - PlayerData.Instance.gold), GetProduceCount, ()=> { ShopPanelNew.Instance.HideUI();
                    ToggleManager.Instance.ShowPanel(2);
                });
                AndroidAdsDialog.Instance.ShowToasts("金币不足", ResourceManager.Instance.GetSprite("金币不足"), Color.red);
              //  Debug.LogError("金币不足");
                
            }
        //}
    }

    private void RefreshZhiboProduce(int red)
    {
        
       
        ShopPanelNew.Instance.currentZhiBoJian.Sell(currentProduce.item_id);
        //ShopPanelNew.Instance.currentZhiBoJian.StartTuiXiao();
        //ShopPanelNew.Instance.currentZhiBoJian.StopSell();
        AndroidAdsDialog.Instance.ShowRed(string.Format("{0}", ShopPanelNew.Instance.currentZhiBoJian.actorDate.actor_name), currentProduce, string.Format("{0}", currentProduce.item_name), "开始卖", "啦!",
            ShopPanelNew.Instance.currentZhiBoJian.effectBorn, ShopPanelNew.Instance.currentZhiBoJian.effectTarget);
      // AndroidAdsDialog.Instance.ShowToasts("+" + red.ToString(), ResourceManager.Instance.GetSprite("红包"), Color.red);
        //PlayerDate.Instance.GetRed(red);
        ShopPanelNew.Instance.HideUI();
        ShopPanelNew.Instance.AddProduceAndRefesh();
    }

    public void GetProduceCount(int count)
    {
        this.count = count;
       // info.count = count;
    }


    //   public void AddProduceAndRefesh()
    //    {
    //        PlayerDate.Instance.AddProduce(this);

    //        RefreshProduce();
    //    }

    public void RefreshProduce()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("click_change_item_new");
        //ShopPanelNew.Instance.SetShopProduce(this);

    }

    public void RefreshXiaoHaoShow()
    {
        //if (currentProduce.item_cost_type == 1)
        //{
        //    xiaoHaoShowGo[0].SetActive(true);
        //    xiaoHaoShowGo[1].SetActive(false);
        //}
        //else if (currentProduce.item_cost_type == 2)
        //{
        //    xiaoHaoShowGo[0].SetActive(false);
        //    xiaoHaoShowGo[1].SetActive(true);
        //    xiaoHaoImg.sprite = ResourceManager.Instance.GetSprite("钻石");
        //    xiaoHaoText.text = currentProduce.item_cost_num.ToString();
        //}
        //else if (currentProduce.item_cost_type == 3)
        //{
            xiaoHaoShowGo[0].SetActive(false);
            xiaoHaoShowGo[1].SetActive(true);
            xiaoHaoImg.sprite = ResourceManager.Instance.GetSprite("金币");
            xiaoHaoText.text = currentProduce.item_cost_num.ToString();

        //}
    }

    internal void ShowTips(bool value=false)
    {
        tipsGo.SetActive(true);
        if(value)
        tasktipsGo.SetActive(true);
    }
    internal void HideTips()
    {
        tipsGo.SetActive(false);
        tasktipsGo.SetActive(false);
    }
}
