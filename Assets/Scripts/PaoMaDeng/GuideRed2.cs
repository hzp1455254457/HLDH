using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GuideRed2 : PanelAnimation
{
  //  public Sprite[] sprites;
    //public Text text;
   // public Text top;
   public NumberEffect numberEffect;
   // public Image image;
   // int type = 0;
    UnityEngine.Events.UnityAction unityAction;
    int count;
    public GameObject buGo;
    public void Show(int count,UnityEngine.Events.UnityAction unityAction)
    {
        transform.SetParent(UIManager.Instance.showRootMain,false);
        backTf.localScale = Vector3.zero;
        backTf.DOScale(1, 1.5f);
        numberEffect.Animation(count, "+", "元", 1.5f, 0,()=>buGo.SetActive(true));
        // image.sprite = sprites[Type];
       // type = Type;
        this.unityAction = unityAction;
        //text.text = "+" + count.ToString("0") + "元";
       // top.text = Type == 0 ? "恭喜获得小额红包" : "恭喜获得普通红包";
        //AndroidAdsDialog.Instance.CloseFeedAd();
       // AndroidAdsDialog.Instance.ShowFeedAd(540);
    }
    public void ClickFun()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("finish_xinrenhb");

        //AndroidAdsDialog.Instance.CloseFeedAd();
        unityAction?.Invoke();
        Destroy(gameObject);
       
    }
    public void ExitFun()
    {
        ClickFun();
    }
}
