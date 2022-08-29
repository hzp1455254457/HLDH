using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ScrollManager : MonoBehaviour
{
    private ScrollRect scrollRect;
    public float[] pos;
    public bool isStart = true;
    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }
    public void RefreshScrollRect()
    {
        scrollRect.enabled = true;
    }
    
    /// <summary>
    /// 刷新各个房间位置
    /// </summary>
    public void RefreshPos(int count)
    {
        if (scrollRect == null) return;
        pos = new float[count];
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = 1 / (float)(pos.Length - 1) * i;
        }
        isStart = true;
     
        
    }
    public void Move(int index)
    {
        if (scrollRect == null) return;
        {
            if (!isStart) return;
            scrollRect.verticalNormalizedPosition = pos[index];
           //MoneyManager.Instance.shengJiTips.RefreshStatus();
        }
    }
    Tweener tweener;
    bool isZhiXi = false;

    public void MoveLerp(int index,float time=0.2f, UnityEngine.Events.UnityAction unityAction = null)
    {
        if (!isStart) return;
        if (tweener != null)
        {
            isZhiXi = false;
            tweener.Kill();
        }

        tweener = scrollRect.DOVerticalNormalizedPos(pos[index], time);
        tweener.onComplete = () =>
         {
             unityAction?.Invoke();
             isZhiXi = true;
             //Debug.LogError("执行com");
             tweener = null;
         };
        tweener.onKill = () =>
        {
            if (!isZhiXi)
            {
                unityAction?.Invoke();
                //Debug.LogError("执行kill");
            }
        };
        //MoneyManager.Instance.shengJiTips.RefreshStatus();
    }

    
}
