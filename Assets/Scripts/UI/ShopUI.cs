using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using HLDH;
public class ShopUI : MonoBehaviour
{
    public int index;
    public Image produceImg, statesImg;
    public Text prodceName, profit, countText;
    public Produce currentProduce;
    public Button getProduce;
    public int count;
    public ShopInfo info = new ShopInfo();
    public int states;
  public  HLDH. ShopPanel shopPanel;
    public GameObject proGo;
    public GameObject[] xiaoHaoShowGo;

    public Image xiaoHaoImg;
    public Text xiaoHaoText;
    public GameObject refresh;
    private void Start()
    {
       shopPanel = UIManager.Instance.GetPanel("Panel_Shop") as HLDH.ShopPanel;
        //if (GuideManager.Instance.isFirstGame)
        //{
        //    refresh.SetActive(false);
        //}
    }
    public void SetProduce(Produce produce)
    {
        currentProduce = produce;
        RefreshXiaoHaoShow();
        produceImg.sprite = ResourceManager.Instance.GetSprite(produce.item_pic);
        prodceName.text = produce.item_name;
        // count = procount;
        // info.count = count;
        info.itemId = produce.item_id;
        profit.text = string.Format("利润:{0}元/个", produce.item_profit);
        countText.text = string.Format("{0}个", count);
      
        //暴利状态
        states = produce.profit_state;
        info.states = produce.profit_state;
        if (states == 2)
        {
            (UIManager.Instance.GetPanel("Panel_Shop") as HLDH.ShopPanel).isHaveBaoli = true;
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
        shopPanel.currentShopUI = this;
        if (GuideManager.Instance.isFirstGame)
        {
            clickCount++;
            //shopPanel.SetShopInfo(this);
            if (clickCount == 1)
            {
                New.ShopPanel.Instance.SetCount(4000, currentProduce);
                AndroidAdsDialog.Instance.UploadDataEvent("new_course_2");
            }
            else if (clickCount == 2)
            { GetProduceCount(); }
            else if(clickCount == 3)
            {
                GetProduceCount();
                PeopleEffect.Instance.HideTips();
                PeopleEffect.Instance.SetTips(ToggleManager.Instance.guiTaget3, ToggleManager.Instance.guiTaget4.position,true,RotaryType.TopToBottom);
            }
        }
        else
        {
            GetProduceCount();

        }


    }

    private void GetProduceCount()
    {
        if (currentProduce.item_cost_type == 1)
        {
            //shopPanel.currentShopUI = this;
            Shop.Instance.ShowUI(false);

            //shopPanel.currentShopUI = this;
        }

        else if (currentProduce.item_cost_type == 2)
        {

            if (PlayerData.Instance.diamond >= currentProduce.item_cost_num)
            {
                AndroidAdsDialog.Instance.UploadDataEvent("get_item_suc");

                // SetCount(NumberGenenater.GetProduceCount());
                PlayerData.Instance.ExpendDiamond(currentProduce.item_cost_num);
                int red = NumberGenenater.GetRedCount(false);
                var go = GameObjectPool.Instance.CreateObject("GetProduceAndRed", ResourceManager.Instance.GetProGo("GetProduceAndRed"), ToggleManager.Instance.effectBorn, Quaternion.identity);
                int producecount = NumberGenenater.GetProduceCount();
               // go.GetComponent<RedAndProduceAdward>().Show(ToggleManager.Instance.effectTarget, red.ToString(), ResourceManager.Instance.GetSprite(currentProduce.item_pic), producecount.ToString());
                GetProduceCount(producecount);

                shopPanel.AddProduceAndRefesh();
                PlayerData.Instance.GetRed(red);

            }
            else
            {
                AndroidAdsDialog.Instance.ShowToasts("钻石数量不足", ResourceManager.Instance.GetSprite("钻石不足"), Color.red);
                Debug.LogError("钻石不足");
            }
        }
        else if (currentProduce.item_cost_type == 3)
        {
            if (PlayerData.Instance.gold >= currentProduce.item_cost_num)
            {
                AndroidAdsDialog.Instance.UploadDataEvent("get_item_suc");
                //SetCount(NumberGenenater.GetProduceCount());
                PlayerData.Instance.Expend(currentProduce.item_cost_num);
                int red = NumberGenenater.GetRedCount(false);
                var go = GameObjectPool.Instance.CreateObject("GetProduceAndRed", ResourceManager.Instance.GetProGo("GetProduceAndRed"), ToggleManager.Instance.effectBorn, Quaternion.identity);
                int producecount = NumberGenenater.GetProduceCount();
                //go.GetComponent<RedAndProduceAdward>().Show(ToggleManager.Instance.effectTarget, red.ToString(), ResourceManager.Instance.GetSprite(currentProduce.item_pic), producecount.ToString());
                GetProduceCount(producecount);
                shopPanel.AddProduceAndRefesh();
               
                PlayerData.Instance.GetRed(red);
            }
            else
            {
                //PlayerDate.Instance.AddGoldNotEnoughCount(currentProduce.item_cost_num, (int)(currentProduce.item_cost_num-PlayerDate.Instance.gold),null);
                AndroidAdsDialog.Instance.ShowToasts("金币不足", ResourceManager.Instance.GetSprite("金币不足"), Color.red);
                Debug.LogError("金币不足");
            }
        }
    }

    public void GetProduceCount(int count)
    {
        this.count = count;
        info.count = count;
    }


    //   public void AddProduceAndRefesh()
    //    {
    //        PlayerDate.Instance.AddProduce(this);

    //        RefreshProduce();
    //    }

       public void RefreshProduce()
    {
        if (GuideManager.Instance.isFirstGame) return;
        shopPanel.SetShopProduce(this);
        
        // if (GuideManager.Instance.isFirstGame) return;
        if (!ToggleManager.Instance.toggles[0].isOn)
        {
            ToggleManager.Instance.SetTips(0);
        }

        //AndroidAdsDialog.Instance.ShowTableVideo("0");
    }

    public void RefreshXiaoHaoShow()
    {
        if (currentProduce.item_cost_type == 1)
        {
            xiaoHaoShowGo[0].SetActive(true);
            xiaoHaoShowGo[1].SetActive(false);
        }
        else if (currentProduce.item_cost_type == 2)
        {
            xiaoHaoShowGo[0].SetActive(false);
            xiaoHaoShowGo[1].SetActive(true);
            xiaoHaoImg.sprite = ResourceManager.Instance.GetSprite("钻石");
            xiaoHaoText.text = currentProduce.item_cost_num.ToString();
        }
        else if (currentProduce.item_cost_type == 3)
        {
            xiaoHaoShowGo[0].SetActive(false);
            xiaoHaoShowGo[1].SetActive(true);
            xiaoHaoImg.sprite = ResourceManager.Instance.GetSprite("金币");
            xiaoHaoText.text = currentProduce.item_cost_num.ToString();

        }
    }

    //float time;

    private void Update()
    {
    }

}

    /// <summary>
    /// 商店位置和数量
    /// </summary>

    [Serializable]
    public class ShopInfo
    {
        public int itemId;
        public int count;
        public int states;
    }
