using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TipsEffect : MonoBehaviour
{
    public Image Img;
    public Text tipsInfo;
    Graphic[] graphics;
    public Transform backTf;
    private void Awake()
    {
        graphics = GetComponentsInChildren<Graphic>();
        foreach (var item in graphics)
        {
            item.DOFade(0, 0);
        }
    }
    float scale = 1f;
    public void Show(Transform targetTf, string tipsValue, Sprite sprite, UnityAction unityAction, Color color, float scale = 1f)
    {
        transform.localScale = Vector3.one * scale;
        this.scale = scale;
        Img.sprite = sprite;
        Animation(targetTf);
        tipsInfo.text = tipsValue;
        tipsInfo.color = color;
        onComplete += unityAction;

    }
    private void Animation(Transform targetTf)
    {
        foreach (var item in graphics)
        {
            item.DOFade(1, 1f).SetUpdate(true);
        }
        transform.DOLocalMoveY(targetTf.localPosition.y, 0.6f).SetEase(Ease.OutQuint).SetUpdate(true).onComplete += Fun;
    }
    public void Fun()
    {
        Animation();
    }
    public UnityAction onComplete;
    public  void Animation()
    {

        backTf.transform.DOScale(Vector3.one * 1.1f * scale, 0.3f).SetUpdate(true).onComplete
             += () => backTf.transform.DOScale(Vector3.one * 1f * scale, 0.3f).SetUpdate(true).onComplete += () =>
               {
                   foreach (var item in graphics)
                   {
                       item.DOFade(0, 0.3f).SetUpdate(true);
                   };
                   GameObjectPool.Instance.CollectObject(gameObject, 1f);
                   StartCoroutine(Global.Delay(0.8f, () =>
                   {
                       if (onComplete != null)
                       {
                           onComplete();
                           onComplete = null;
                       }
                   }));

               };
    }
}

