using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProduceInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public Image produceImg,baoliImg;
    public Text count, produceName, profit,sellCount,selledZhuBo;
    public ProduceDate produceDate;
    public int index;
    public Produce produce1;
    public GameObject[] statesGo;
    Button button;
    public Transform guideTarget1;
    [Header("已出售按钮动画Animator")]
    public Animator Creactanimator;
    public void PlayCreactAnim(bool value)

    {
        //if (!Creactanimator.GetCurrentAnimatorStateInfo(0).IsName("creactkuaidiyuan"))
        Creactanimator.SetBool("walk", value);


    }
    public void SetStates(bool value)
    {
        if (value)
        {
            statesGo[0].SetActive(true);
            statesGo[1].SetActive(false);
            
        }
        else
        {
            statesGo[0].SetActive(false);
            statesGo[1].SetActive(true);
        }
    }
    public void SetSellPrice(ZhiBoJian zhiBoJian)
    {
        sellCount.text = string.Format("每分钟赚{0}金币",zhiBoJian.actorDate.actor_sellbase *(1+ zhiBoJian.jiaChengValue));


    }
   // public bool isHave = true;
    public void SetZeroStates()
    {
       button = statesGo[0].GetComponent<Button>();
        button.interactable = false;
        gameObject.SetActive(false);//新增
        SetStates(true);
       // produceDate.state = 0;
        PlayCreactAnim(false);
        //isHave = false;
    }
    public void SetProduce(ProduceDate produce)
    {
        produceDate = produce;
         produce1 = ConfigManager.Instance.GetProduce(produce.item_id);
        if(produce1.profit_state==2)
        baoliImg.gameObject.SetActive(true);
        else { baoliImg.gameObject.SetActive(false); }

        produceImg.sprite = ResourceManager.Instance.GetSprite(produce1.item_pic);
        produceName.text = produce1.item_name; 
        profit.text = string.Format("利润:{0}", produce1.item_profit);
    }
    // Update is called once per frame
    public void SetCount(ProduceDate produce)
    {
        count.text =string.Format("库存有{0}个", produce.item_have.ToString());
    }

    public void Refresh()
    {
        SetCount(produceDate);
    }
    //private void OnEnable()
    //{   //{if (this.produceDate.item_have <= 0)
    ////    {
    ////        transform.SetAsLastSibling();
           
    ////    }
    //}
    public void SetSell()
    {
        
        
        StockManager.Instance.Sell(this);

        StockManager.Instance.HideUI();
       
    }
    //public void ShowProduceShop()
    //{
    //    AudioManager.Instance.PlaySound("bubble1");
    //    if (UnityEngine.Random.Range(1, 11) <= 5)
    //    {
          
    //        Shop.Instance.ShowUI();
    //    }
    //    else
    //    {
           
    //        AwardManagerNew.Instance.ShowUI(null);
    //    }
        
    //    //StartCoroutine(Shop.Instance.Delay(2f));
    //}
    private void OnDisable()
    {
        //SetStates(true);



    }
    public void SetSelled(string name)
    {
        SetStates(false);
        //produceDate.state = 1;
        selledZhuBo.text = string.Format("{0}正在出售", name);
        transform.SetAsLastSibling();
        PlayCreactAnim(true);
    }
    private void Start()
    {
        if (produceDate.item_have > 0)
        {
            button.interactable = true;
            gameObject.SetActive(true);
            //SetStates(true);
          // isHave = true;
        }
        else
        {
           // SetStates();
            button.interactable = false;
            gameObject.SetActive(false);
           // isHave = false;
        }
        Refresh();
        PlayerData.Instance.addProduceAction += RefreshButtonStates;

    }
    private void Awake()
    {
        button = statesGo[0].GetComponent<Button>();
     
    }
    private void RefreshButtonStates(string key)
    {
        if(key== produceDate.item_id.ToString())
        {
            if (produceDate.item_have > 0)
            {
                button.interactable = true;
                gameObject.SetActive(true);
                //isHave =true;
            }
            else
            {
                gameObject.SetActive(false);
                button.interactable = false;
               // isHave = false;
            }
            Refresh();
        }
    }
}
