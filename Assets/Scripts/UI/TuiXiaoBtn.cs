using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TuiXiaoBtn : MonoBehaviour
{
  public  int count;
  public  string value;
   public bool isWin;
    public Text text;
    public TuiXiao tuiXiao;
    public Animator animator;
    public Image image;
    public Button button;
    public RectTransform rectTransform;
    public void SetAnim(bool value)
    {
        animator.enabled = value;
        animator.SetBool("walk", value);
        transform.localScale = Vector3.one;
    }
    public void SetValue()
    {
        text.text = value;
        //SetAnim(true);
    }
    public void SetClick()
    {
        if (GuideManager.Instance.isFirstGame)
        {
            if (isWin)
            {
                rectTransform.localScale = Vector3.one * 1.1f;
                image.sprite = tuiXiao.zhiBoJian.shengjiSps[0];
                button.interactable = true;
            }
            else
            {
                rectTransform.localScale = Vector3.one * 1.0f;
                image.sprite = tuiXiao.zhiBoJian.shengjiSps[1];
                button.interactable = false;
            }
        }
        else
        {
            rectTransform.localScale = Vector3.one * 1.0f;
            image.sprite = tuiXiao.zhiBoJian.shengjiSps[0];
            button.interactable = true;
        }
    }
    int clickGuide = 0;
    public void ClickEvent()
    {
        SetAnim(false);
        AudioManager.Instance.PlaySound("bubble1");
        tuiXiao.isWin = isWin;
        tuiXiao.daimondCount = count;
        if (isWin)
        {
            if (GuideManager.Instance.isFirstGame)
            {
                if (tuiXiao.zhiBoJian.index == 0)
                {
                    clickGuide++;
                    if (clickGuide == 1)
                    {
                        AndroidAdsDialog.Instance.UploadDataEvent("new_guide_5");
                       // SevenLoginPanel.Instance.ShowUI(UIManager.Instance.showRootMain);
                    }
                }
            }
            else
            {
                ZhiBoPanel.Instance.tuiXiaoIcon.Show(UnityEngine.Random.Range(2000, 4000));
            }
        }
        else
        {

        }
        tuiXiao.HideSelect(value);
    }
}
