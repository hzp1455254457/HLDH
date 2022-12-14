using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Red2 : PanelAnimation
{
  //  public Sprite[] sprites;
    public Text text;
    public Text top;
   // public Image image;
    int type = 0;
    UnityEngine.Events.UnityAction unityAction;
    int count;
    public void Show(float count,int Type, UnityEngine.Events.UnityAction unityAction)
    {
        base.Animation();
       // image.sprite = sprites[Type];
        type = Type;
        this.unityAction = unityAction;
        text.text = "+" + count.ToString("0") + "元";
        top.text = Type == 0 ? "恭喜获得小额红包" : "恭喜获得普通红包";
        AndroidAdsDialog.Instance.CloseFeedAd();
        AndroidAdsDialog.Instance.ShowFeedAd(540);
    }
    public void ClickFun()
    {
      
        AndroidAdsDialog.Instance.CloseFeedAd();
        unityAction?.Invoke();
        Destroy(gameObject);
       
    }
    public void ExitFun()
    {
        ClickFun();
    }
}
