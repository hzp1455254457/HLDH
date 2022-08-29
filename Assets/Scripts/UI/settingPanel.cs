using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingPanel : PanelBase
{
    // Start is called before the first frame update
    public static settingPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("settingPanel")) as settingPanel;
                instance.gameObject.SetActive(false);
                return instance;
            }
            return instance;
        }
    }
    static settingPanel instance;
    protected override void Awake()
    {
        instance = this;
    }
    public override void Show()
    {
       
      
        transform.SetParent(UIManager.Instance.showRootMain);
        //transform.SetAsLastSibling();

    }
    public void ShowUI()
    {
        base.Animation();
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }
    public override void Hide()
    {
        gameObject.SetActive(false);
    }
}
