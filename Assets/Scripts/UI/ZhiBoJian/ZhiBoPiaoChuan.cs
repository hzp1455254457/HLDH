using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ZhiBoPiaoChuan : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Text text;
    public void Show(string value)
    {
        text.text = value;
        canvasGroup.DOFade(1, 0.5f).onComplete = ()=>canvasGroup.DOFade(0, 0.5f).SetDelay(2f);
    }
}
