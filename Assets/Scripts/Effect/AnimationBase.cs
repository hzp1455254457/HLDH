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
    /// scale�仯�����ű�
    /// </summary>
    /// <param name="aniscale1">��һ�ζ����仯Ŀ��ֵ</param>
    /// <param name="time1">��һ�ζ����仯����ʱ��</param>
    /// <param name="aniscale2">�ڶ��ζ����仯Ŀ��ֵ</param>
    /// <param name="time2">�ڶ��ζ����仯����ʱ��</param>
    /// <param name="startAction">��ʼ�����¼�</param>
    /// <param name="startedAction">��ʼ�������ʱ�¼�</param>
    /// <param name=""></param>
    /// <param name="finishedAction">�������ʱ�¼�</param>
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
