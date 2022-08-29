using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WangDianRed2 : PanelAnimation
{
  //  public Sprite[] sprites;
    public Text text;
    public Text top;
   // public Image image;
  //  int type = 0;
    UnityEngine.Events.UnityAction unityAction;
    //int count;
    public void Show(float count,string value ,UnityEngine.Events.UnityAction unityAction)
    {
        base.Animation();
       // image.sprite = sprites[Type];
       
        this.unityAction = unityAction;
        text.text = "+" + count.ToString("f2") + "元";
        top.text = value;
        //AndroidAdsDialog.Instance.CloseFeedAd();
        //AndroidAdsDialog.Instance.ShowFeedAd(540);
    }
    public void ClickFun()
    {
      
        //AndroidAdsDialog.Instance.CloseFeedAd();
        unityAction?.Invoke();
        Destroy(gameObject);
       
    }
    public void ExitFun()
    {
        ClickFun();
    }
}
