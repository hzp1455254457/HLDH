using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Red3 : PanelAnimation
{
  //  public Sprite[] sprites;
   // public Text text;
   // public Text top;
   //// public Image image;
   // int type = 0;
    UnityEngine.Events.UnityAction unityAction;
    public void Show(UnityEngine.Events.UnityAction unityAction)
    {
        base.Animation();
       // image.sprite = sprites[Type];
       
        this.unityAction = unityAction;
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
        AndroidAdsDialog.Instance.CloseFeedAd();
        Destroy(gameObject);
    }
}
