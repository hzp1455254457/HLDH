using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelledProduce : MonoBehaviour
{
    // Start is called before the first frame update
    public int index;
    public ProduceDate produceDate;
    public Image produceImg, baoliImg;
    public Text count, produceName, profit;
    public Produce produce;
    public GameObject redTypeGo;
    [Header("出售按钮")]
    public Button button;
   // public int type = 0;
    public void SetProduce(ProduceDate produceDate)
    {
        this.produceDate = produceDate;
        produce = ConfigManager.Instance.GetProduce(produceDate.item_id);
        //if (produce.profit_state == 2)
        //    baoliImg.gameObject.SetActive(true);
        //else { baoliImg.gameObject.SetActive(false); }

      produceImg.sprite = ResourceManager.Instance.GetSprite(produce.item_pic);
        //produceName.text = produce.item_name;
       profit.text = string.Format("赚{0}元/个", produce.item_profit);
        if (produceDate.type == 1)
        {
            redTypeGo.SetActive(true);
        }
        else
        {
            redTypeGo.SetActive(false);
        }
    }
    /// <summary>
    /// 清零
    /// </summary>
    public void SetSell( )
    {
        produceDate.item_have = 0;
        SelledManager.Instance.selledInfos.Remove(this);
        PlayerData.Instance.RemoveSelledCount(this.produceDate.item_have);
        //PlayerDate.Instance.SaveSelledDate();
    }
    internal void Refresh(string key)
    {
        if(key== produceDate.item_id.ToString())
        count.text =string.Format(" 有<color=#15FF00>{0}</color>个待发货", produceDate.item_have.ToString());
    }
   
    private void Start()
    {
        PlayerData.Instance.addSelledAction += Refresh;
        button.onClick.AddListener(FaHuo);
    }

    public void FaHuoEvent()
    {

        SelledManager.Instance.CallBackFaHuoEvent();
    }
    public void FaHuo()
    {

        SelledManager.Instance.FaHuo(this);
    }
}
