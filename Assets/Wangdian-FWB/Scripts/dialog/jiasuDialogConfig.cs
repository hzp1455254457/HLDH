using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class jiasuDialogConfig : PanelAnimation
{
    public Button jiasuButton,closeButton;
    public void InitJiaSuDialog(Action jiasuAction = null, Action closeAction = null)
    {
        closeButton.onClick.AddListener(() =>
        {
            AndroidAdsDialog.Instance.CloseFeedAd();
            closeAction?.Invoke();
            Destroy(gameObject);
        });

        jiasuButton.onClick.AddListener(() =>
        {
            AndroidAdsDialog.Instance.CloseFeedAd();
            jiasuAction?.Invoke();
        });
    }
}
