using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewWangDianPanel : PanelBase
{
    public static NewWangDianPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("NewWangDianPanel")) as NewWangDianPanel;
                instance.Hide();
                return instance;
            }
            return instance;
        }
    }
    static NewWangDianPanel instance;
    public CanvasGroup canvasGroup1;
    public List<NewWangDianItem> newWangDianItems;
    public Transform parentTf;

    public Text level;
    public void SetStatus(bool value)
    {
        canvasGroup1.alpha = value==true?1:0;
        canvasGroup1.blocksRaycasts = value;
        canvasGroup1.interactable = value;
    }
    protected override void Awake()
    {
        //PlayerDate.Instance.storeData.level = AndroidAdsDialog.Instance.GetRedLevel();
      if (PlayerData.Instance.NewWangDianDatas!=null&& PlayerData.Instance.NewWangDianDatas.Count > 0)
        {
           var datas= PlayerData.Instance.NewWangDianDatas.FindAll(s => s.myshop_level < PlayerData.Instance.storeData.level);
            //PlayerDate.Instance.NewWangDianDatas.RemoveRange(0, datas.Count);
            //PlayerDate.Instance.NewWangDianDatas.AddRange(datas);
            for (int i = 0; i < datas.Count; i++)
            {
                datas[i].status = 2;
              
                datas[i].gold = datas[i].myshop_needgold;
              //  PlayerData.Instance.NewWangDianDatas.Remove(datas[i]);
               // PlayerData.Instance.NewWangDianDatas.Add(datas[i]);
            }
        }
        for (int i = 0; i < PlayerData.Instance.NewWangDianDatas.Count; i++)
        {
            var go = Instantiate(ResourceManager.Instance.GetProGo("NewWangDianItem"));
            var item = go.GetComponent<NewWangDianItem>();
            item.transform.SetParent(parentTf, false);
            item.newWangDianData = PlayerData.Instance.NewWangDianDatas[i];
            newWangDianItems.Add(item);
            //item.SetData();
            //item.RefreshText();
        }
    }
    private void OnDestroy()
    {
        instance = null;
    }
    public override void Hide()
    {
        SetStatus(false);
        AndroidAdsDialog.Instance.CloseBanner();
        //for (int i = 0; i < newWangDianItems.Count; i++)
        //{
        //    Destroy(newWangDianItems[i].gameObject);
        //}
        //newWangDianItems.Clear();
        //AndroidAdsDialog.Instance.ShowTableVideo("0");
    }
    public void ShowUI(Transform parentTF)
    {
        transform.SetParent( parentTF,false);
        transform.SetAsLastSibling();
        base.Animation();
        SetStatus(true);
        for (int i = 0; i < newWangDianItems.Count; i++)
        {
            
            newWangDianItems[i].newWangDianData = PlayerData.Instance.NewWangDianDatas[i];
            //newWangDianItems.Add(item);
            newWangDianItems[i].SetData();
            newWangDianItems[i].RefreshText();
        }
        level.text = PlayerData.Instance.storeData.level.ToString() + "¼¶";
        AndroidAdsDialog.Instance.ShowBannerAd();
    }
    public override void Show()
    {
        

    }
}
