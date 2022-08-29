using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Red4 : PanelAnimation
{
 public Sprite[] sprites;
   // public Text text;
   // public Text top;
   public Image image;
    public Text adwardText, countText;
   // int type = 0;
    UnityEngine.Events.UnityAction unityAction;
    public void Show(UnityEngine.Events.UnityAction unityAction,int type,int count,int videoCount)
    {
        base.Animation();
        // image.sprite = sprites[Type];
        image.sprite = sprites[type];
        image.SetNativeSize();
        this.unityAction = unityAction;
        adwardText.text = "+" + count.ToString()+ "Ԫ"; 
             countText.text = string.Format("�����ۻ��ۿ��� <size=60><color=yellow>{0}</color></size>  �ι��", videoCount);
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
