using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TipsUI : PanelAnimation
{
    public event UnityAction clickAction;
    // Start is called before the first frame update
  public void ShowUI(UnityEngine.Events.UnityAction unityAction)
    {
        clickAction = unityAction;
        gameObject.SetActive(true);
        base.Animation();
        AndroidAdsDialog.Instance.ShowFeedAd(600);
    }
  public void ClickFun()
    {
        clickAction?.Invoke();
        gameObject.SetActive(false);
        clickAction = null;
        AndroidAdsDialog.Instance.CloseFeedAd();
    }
}
