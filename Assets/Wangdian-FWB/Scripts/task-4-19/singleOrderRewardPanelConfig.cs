using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class singleOrderRewardPanelConfig : PanelAnimation
{
    public Text hongbaoText;
    public Button closeButton, sureButton;

    /// <summary>
    /// ��ʼ���������
    /// </summary>
    /// <param name="reward"></param>
    /// <param name="action"></param>
    public void InitRewardHongBao(float reward, System.Action action = null)
    {
        hongbaoText.text = reward.ToString("F3") + "Ԫ";

        PlayerData.Instance.GetRed((int)(reward * MoneyManager.redProportion));

        closeButton.onClick.AddListener(() =>
        {
            action?.Invoke();
            Destroy(gameObject);
        });

        sureButton.onClick.AddListener(() =>
        {
            action?.Invoke();
            Destroy(gameObject);
        });

        base.Animation();
    }
}
