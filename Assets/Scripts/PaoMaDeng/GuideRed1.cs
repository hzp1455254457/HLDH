using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GuideRed1: PanelAnimation
{

    UnityEngine.Events.UnityAction unityAction;
    bool isClicked = false;
   // public GameObject buGo;
    public void Show(UnityEngine.Events.UnityAction unityAction)
    {
        transform.SetParent(UIManager.Instance.showRootMain, false);
        base.Animation();

        AndroidAdsDialog.Instance.UploadDataEvent("xinrenhb");
        this.unityAction = unityAction;
   
    }

    public void ClickFun()
    {
        if (!isClicked)
        {
            AndroidAdsDialog.Instance.UploadDataEvent("click_xinrenhb");

            transform.DOScale(0, 0.2f).onComplete = () => { Destroy(gameObject); unityAction?.Invoke(); };
            isClicked = true;
        }
       
    }
    public void ExitFun()
    {
        ClickFun();
    }
}
