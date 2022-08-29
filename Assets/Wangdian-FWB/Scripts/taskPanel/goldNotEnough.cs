using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class goldNotEnough : PanelAnimation
{
    public Button closeButton, clickButton;
    // Start is called before the first frame update
    void Start()
    {
        base.Animation();
    }

    public void InitGoldEnoughPanel(Action clickAction = null,Action closeAction = null)
    {
        clickButton.onClick.AddListener(() =>
        {
            AndroidAdsDialog.Instance.UploadDataEvent("click_jinbi_less_in_newshop");
            AndroidAdsDialog.Instance.CloseFeedAd();
            clickAction?.Invoke();
            Destroy(gameObject);
        });

        closeButton.onClick.AddListener(() =>
        {
            AndroidAdsDialog.Instance.UploadDataEvent("close_jinbi_less_in_newshop");
            AndroidAdsDialog.Instance.CloseFeedAd();
            closeAction?.Invoke();
            Destroy(gameObject);
        });
    }
}
