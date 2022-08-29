using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnimationBase : MonoBehaviour
{
    public Transform backTf;
    Tweener tweener;
    //public UnityEngine.Events.UnityAction startAction;
    // public UnityEngine.Events.UnityAction finishedAction;
    /// <summary>
    /// scale变化动画脚本
    /// </summary>
    /// <param name="aniscale1">第一段动画变化目标值</param>
    /// <param name="time1">第一段动画变化消耗时间</param>
    /// <param name="aniscale2">第二段动画变化目标值</param>
    /// <param name="time2">第二段动画变化消耗时间</param>
    /// <param name="startAction">开始动画事件</param>
    /// <param name="startedAction">开始动画完成时事件</param>
    /// <param name=""></param>
    /// <param name="finishedAction">动画完成时事件</param>
    /// 
    public virtual void Animation(float time1=0.2f, UnityEngine.Events.UnityAction startAction = null, UnityEngine.Events.UnityAction startedAction = null, UnityEngine.Events.UnityAction finishedAction=null)
    {
        backTf.localScale = Vector3.one * 0.9f;
        //backTf.localScale=Vector3.one*0.8f;
        //backTf.localScale = Vector3.zero;
        startAction?.Invoke();
        if (tweener != null)
        {         
            tweener.Pause();
            tweener.Kill();
        }
        tweener = backTf.DOScale(Vector3.one, time1).SetEase(Ease.OutBack);
        //tweener = backTf.DOScale(Vector3.one, time1).SetEase(Ease.OutQuad);
    }
}
