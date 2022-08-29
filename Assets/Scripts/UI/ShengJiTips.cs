using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ShengJiTips : MonoBehaviour
{
    public Text nameText, valueText;
    public CanvasGroup canvasGroup ;
    public GameObject[] gos;
    public void Show(string name,string value,UnityEngine.Events.UnityAction unityAction=null)
    {
        SetShowOrHide(false, 0);
        nameText.text = name;
        valueText.text = value;
        SetShowOrHide(true, 1f, unityAction);
        RefreshStatus();
    }
    public void SetText(string name, string value)
    {
        nameText.text = name;
        valueText.text = value;
    }
    public void ClickFun()
    {
        ZhiBoPanel.Instance.MoveZhuBo(ZhiBoPanel.Instance.currentIndex,true, RefreshStatus);
        ToggleManager.Instance.GoGame();
    }
    public void RefreshStatus()
    {
        if (ZhiBoPanel.Instance.currentZhuBoIndex == ZhiBoPanel.Instance.currentIndex)
        {

            gos[0].SetActive(false);
        }
        else
        {
            gos[0].SetActive(true);

        }
    }
    public void ClickFun1()
    {
        ZhiBoPanel.Instance.zhibojianList[ZhiBoPanel.Instance.currentIndex].ShengJi();
    }
    public void Hide()
    {
        SetShowOrHide(false, 0.5f);
    }
    Coroutine coroutine;
  public  void SetShowOrHide(bool value,float time,UnityEngine.Events.UnityAction unityAction=null)
    {
        int valueInt = 0;
        canvasGroup.blocksRaycasts = false;
        if (value)
        {
            valueInt = 1;
            //canvasGroup.DOFade(0, 0);
        }
        else
        {
            valueInt = 0;
            //canvasGroup.DOFade(1, 0);
        }
        canvasGroup.DOFade(valueInt, time).onComplete += () =>
        {
            canvasGroup.blocksRaycasts = value;
            unityAction?.Invoke();
        };
        //if (isContinue)
        //{
        //    if (coroutine != null)
        //    {
        //        StopCoroutine(coroutine);
        //    }
        //    // coroutine = StartCoroutine(Global.Delay(4f, () => Hide()));
        //}
    }
    
}
