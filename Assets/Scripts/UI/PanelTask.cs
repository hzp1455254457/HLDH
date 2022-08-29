using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTask : PanelBase
{
    public static PanelTask Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("PanelTask")) as PanelTask;
                instance.gameObject.SetActive(false);
                return instance;
            }
            return instance;
        }
    }
    static PanelTask instance;
    public Text nameTask,info, process, Daimondcount;
    public Sprite[] sprites;
    public Image image;
    public GameObject tips;
   public override void Show()
    {
        gameObject.SetActive(true);
        transform.SetParent(UIManager.Instance.showRootMain);
        //transform.SetAsLastSibling();

    }
    public void ShowUI(string value, string value1,string value2,string value3,int isAchive,UnityEngine.Events.UnityAction unityAction)
    {
        transform.SetAsLastSibling();

        gameObject.SetActive(true);
        base.Animation();
        nameTask.text = value;
        info.text = value1;
        process.text = value2;
        Daimondcount.text = value3;
        clickAction += unityAction;
        image.sprite = sprites[isAchive];
        if (isAchive == 1)
        {
            tips.SetActive(true);
        }
        else
        {
            tips.SetActive(false);
        }
        AndroidAdsDialog.Instance.ShowFeedAd(540);
    }
    public override void Hide()
    {
        AndroidAdsDialog.Instance.CloseFeedAd();
       gameObject.SetActive(false);
        clickAction = null;
    }
    public void HideUI()
    {
        Hide();
    }
    protected override void Awake()
    {
       
    }
    UnityEngine.Events.UnityAction clickAction;
    public void ClickEvent()
    {
        clickAction?.Invoke();
        Hide();
    }

}
