using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class lingquDialogConfig : PanelAnimation
{
    public Button closeButton, tixianButton;

    private void Start()
    {
        base.Animation();
    }
    public void InitLingQuDialog(Action tixianAction = null,Action closeAction = null)
    {
        closeButton.onClick.AddListener(() =>
        {
            closeAction?.Invoke();
            Destroy(gameObject);
        });

        tixianButton.onClick.AddListener(() =>
        {
            tixianAction?.Invoke();
        });
    }

}
