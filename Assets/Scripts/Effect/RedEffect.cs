using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
public class RedEffect : TipsEffectBase
{
    protected override void Awake()
    {
        rectBack = GetComponent<RectTransform>();
        //graphics = GetComponentsInChildren<Graphic>();
    }
    protected override void Animation(Transform targetTf)
    {
        transform.DOLocalMoveY(targetTf.localPosition.y, 0.4f).SetEase(Ease.OutQuint).SetUpdate(true).onComplete += Animation;

       
    }
    public override void Animation()
    {

        //foreach (var item in graphics)
        //{
        //    item.DOFade(0, 0.8f).SetUpdate(true);
        //};
        if (onComplete != null)
        {
            onComplete();
        }
        GameObjectPool.Instance.CollectObject(gameObject, 1f);
           
           

         

    }
}
