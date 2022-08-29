using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonRed : PanelAnimation
{
    public Text text;
    public int count;
    public void SetCount(int value)
    {
        count = value;
        text.text = "+" + count / MoneyManager.redProportion + "元";
    }
    void Start()
    {
        SetCount(NumberGenenater.GetRedCount());
        transform.SetParent(UIManager.Instance.showRootMain,false);
        Animation();
    }

    // Update is called once per frame
   public void clickFun()
    {
        PlayerData.Instance.GetRed(count);
        Destroy(gameObject);
    }
}
