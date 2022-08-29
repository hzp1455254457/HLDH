using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dangweiItem : MonoBehaviour
{
    public TiXianData tiXianData;
    public int index;
    public Text text;
    public Toggle toggle;
    private void Awake()
    {
        toggle.onValueChanged.AddListener(ClickFun);
    }
    void Start()
    {
        SetText((tiXianData.amount / 100f).ToString()+"元");

        
    }
    public void SetText(string value)
    {
        text.text = value;
    }

  public void ClickFun(bool value)
    {
        if (value)
        {
            EWaiTiXianPanel.Instance.DangweiIndex = index;
        }
    }
}
