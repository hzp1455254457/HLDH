using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelledManager : MonoSingleton<SelledManager>
{
   // public Dictionary<string, ProduceDate> produceDic;
    public Transform parentTf;
   public List<SelledProduce> selledInfos = new List<SelledProduce>();
    public GameObject selledGo;
    /// <summary>
    /// 0默认有仓库在卖该商品，1为没有仓库卖该商品
    /// </summary>
    public int type;
    public static bool isFirstGame = false;


    public void SetisFirstGame()
    {

        DataSaver.Instance.SetInt("isFirstGame", 1);
      //  isFirstGame = false;
    }

    public void GetisFirstGame()
    {
        if (DataSaver.Instance.HasKey("isFirstGame") == false)
        {
            isFirstGame = true;

        }
        else
        { isFirstGame = false; }
        SetisFirstGame();
    }
    public void CreactSelledProduce(string key, int index,bool isStart=true)
    {
        var pro = GameObjectPool.Instance.CreateObject("SelledProduce",selledGo, parentTf,Quaternion.identity).GetComponent<SelledProduce>();
        pro.index = index;
        if (UnityEngine.Random.Range(1, 11) <= 6)
        {
            //if (isFirstGame&&!GuideManager.Instance.isFirstGame)
            //{
            //    ToggleManager.Instance.SetRedTips(true);
            //    isFirstGame = false;
            //}

            pro.produceDate.type = 1;
        }
        else
        {
            pro.produceDate.type = 0;
        }
        pro.SetProduce(PlayerData.Instance.selledDic[key]);
        pro.Refresh(key);
      selledInfos.Add(pro);
      
        // return pro;
        // produceDic.Add(key, produce);
    }
    private void Awake()
    {
        GetisFirstGame();
        selledGo = ResourceManager.Instance.GetProGo("SelledProduce");
        //produceDic = PlayerDate.Instance.selledDic;
        if (PlayerData.Instance.selledDic != null && PlayerData.Instance.selledDic.Count > 0)
        {
            int i = 0;
            foreach (var item in PlayerData.Instance.selledDic.Keys)
            {
                if (PlayerData.Instance.selledDic[item].item_have > 0)
                {
                    CreactSelledProduce(item, i);
                    //selledInfos.Add()
                    i++;
                }
            }
        }
        PlayerData.Instance.creactSelledProduceEvent += CreactSelledProduce;
       PlayerData.Instance.addSelledProduceAction += CreactSelledProduce;
    }
    private SelledProduce currentselledProduce;
    public CanKu currentCanKu;
    public void FaHuo(SelledProduce selledProduce)
    {
        currentselledProduce = selledProduce;
        currentCanKu = (UIManager.Instance.GetPanel("Panel_CanKu") as CanKuPanel).GetCangKu(selledProduce.produceDate.item_id);
        if(currentCanKu == null)
            currentCanKu = ((UIManager.Instance.GetPanel("Panel_CanKu") as CanKuPanel).GetKonXianCanKu());
        else
        {
            if (selledProduce.produceDate.type == 1)
            {
               // AwardManagerNew.Instance.ShowUI(null, currentselledProduce);
                type = 0;
                return;
            }
            else
            {
                FaHuoEvent1(selledProduce, currentCanKu);
               type = 0;
               return;
            }
        }
        if (currentCanKu == null)
        {
            AndroidAdsDialog.Instance.ShowToasts("没有空闲的快递员啦", ResourceManager.Instance.GetSprite("仓库小人"), Color.black);
            return;
        }
        //canKu.Sell(selledProduce.produceDate.item_id, selledProduce.produceDate.item_have);
        //PlayerDate.Instance.RemoveSelledCount(selledProduce.produceDate.item_have);
        //selledProduce.SetSell();

        //GameObjectPool.Instance.CollectObject(gameObject, 0.5f);
        //AndroidAdsDialog.Instance.UploadDataEvent("sendscene_suc");
      
        if (GuideManager.Instance.isFirstGame)
        {
            FaHuoEventGuide(selledProduce, currentCanKu);
        }
        else
        {
            if (selledProduce.produceDate.type == 1)
            {
               // AwardManagerNew.Instance.ShowUI(null, currentselledProduce);
                type = 1;
            }
            else
            {
                FaHuoEvent2(selledProduce, currentCanKu);
               // type = 1;
            }
        }
    }

    private void FaHuoEventGuide(SelledProduce selledProduce, CanKu canKu)
    {
        PeopleEffect.Instance.HideTips();
        AndroidAdsDialog.Instance.UploadDataEvent("new_course_7");
        canKu.Sell(selledProduce.produceDate.item_id, selledProduce.produceDate.item_have);
        FaHuoed(selledProduce);
        AndroidAdsDialog.Instance.UploadDataEvent("sendscene_suc");
    }

    private void FaHuoEvent2(SelledProduce selledProduce, CanKu canKu)
    {
        canKu.Sell(selledProduce.produceDate.item_id, selledProduce.produceDate.item_have);
        FaHuoed(selledProduce);
        AndroidAdsDialog.Instance.UploadDataEvent("sendscene_suc");
    }

    private void FaHuoEvent1(SelledProduce selledProduce, CanKu canKu)
    {
        AndroidAdsDialog.Instance.ShowToasts(selledProduce.produceDate.item_have.ToString(), ResourceManager.Instance.GetSprite(selledProduce.produce.item_pic), Color.black);
        canKu.courier.deliever_item_num += selledProduce.produceDate.item_have;
        FaHuoed(selledProduce);
        AndroidAdsDialog.Instance.UploadDataEvent("sendscene_suc");
        (UIManager.Instance.GetPanel("Panel_CanKu") as CanKuPanel).MoveCanKu(canKu);
    }

    private void FaHuoed(SelledProduce selledProduce)
    {
       // PlayerDate.Instance.RemoveSelledCount(selledProduce.produceDate.item_have);
        selledProduce.SetSell();

        //canKu.Sell(selledProduce.produceDate.item_id, selledProduce.produceDate.item_have);
        GameObjectPool.Instance.CollectObject(selledProduce.gameObject, 0.02f);
    }
    public void CallBackFaHuoEvent()
    {
        if (type == 0)
        {
            FaHuoEvent1(currentselledProduce, currentCanKu);
        }
        else
        {
            FaHuoEvent2(currentselledProduce, currentCanKu);
        }
    }
}
