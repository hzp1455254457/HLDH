using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class orderFinishPanelConfig : PanelAnimation
{
    public Button clickButton, closeButton;
    public Text diamondText, hongbaoText;
    // Start is called before the first frame update
    void Start()
    {
        base.Animation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitOrderFinishPanel(taskOrder order,Action clickAction = null, Action closeAction = null)
    {
        diamondText.text = order.reward_xyz.ToString();
        hongbaoText.text = order.reward_hbq.ToString();

        clickButton.onClick.AddListener(()=>
            {
                AndroidAdsDialog.Instance.UploadDataEvent("finish_any_order");
                userData.Instance.xinyu += order.reward_xyz;
                PlayerData.Instance.GetRed(order.reward_hbq);
                clickAction?.Invoke();
                Destroy(gameObject);
                Destroy(FindObjectOfType<taskDetailPanelConfig>().gameObject);
            });

        closeButton.onClick.AddListener(() =>
        {
            closeAction?.Invoke();
            Destroy(gameObject);
        });
    }
}
