using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPannel : MonoBehaviour
{
    public GameObject AnimationPannel;
    public virtual void OnEnable()
    {
        if (AnimationPannel==null)
        {
            AnimationPannel = this.gameObject;
        }
        Animation();
    }
    public virtual void Animation()
    {
        AnimationPannel.transform.localScale = Vector3.zero;
        AnimationPannel.transform.DOScale(Vector3.one * 1.1f, 0.5f).SetUpdate(true).onComplete
             += () => AnimationPannel.transform.DOScale(Vector3.one * 1f, 0.3f).SetUpdate(true);
    }
}
