using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WangDianShengJi : PanelAnimation
{
    public Text text_Level;
    public Text text_count;
    public event UnityEngine.Events.UnityAction clickAction;
    public void ShowUI(Transform parentTf,string value1,string value2,UnityEngine.Events.UnityAction clickAction)
    {
        transform.SetParent(parentTf, false);
        transform.SetAsLastSibling();
        text_Level.text = string.Format("¹§Ï²Äú£¬ÍøµêÉýÖÁ{0}¼¶£¬½±Àø£º", value1);
        text_count.text = value2+"Ôª";
        this.clickAction += clickAction;
        base.Animation();
        gameObject.SetActive(true);
        AndroidAdsDialog.Instance.CloseFeedAd();
        AndroidAdsDialog.Instance.ShowBannerAd();
    }
    public void ClickFun()
    {
        clickAction?.Invoke();
        clickAction = null;
        gameObject.SetActive(false);
        AndroidAdsDialog.Instance.ShowTableVideo("0");
        AndroidAdsDialog.Instance.CloseBanner();
    }
}
