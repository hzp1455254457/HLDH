using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZhuBoItem1 : MonoBehaviour
{
    public Text text;
    public int type;
    public void SetText(string value)
    {
        text.text = value;

    }
    public void ClickFun()
    {
        if (type == 0)
        {
            ZhiBoPanel.Instance.MoveZhuBo(ZhiBoPanel.Instance.zhibojianList.Count-1);
            Panel_ZhuBoList.Instance.HideUI();
        }
        else
        {
            AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance. effectBorn, ToggleManager.Instance.effectTarget, "�����������Խ���������Ŷ", Color.black, null, null, 1f);
        }
    }
}
