using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ZhiBoJianRed : MonoBehaviour
{
    public NumberEffect numberEffect;
    public Transform textTf;
    public Text text,text1;
    Tweener tweener1, tweener2;
    public void Animation(int target,int init,float time=0,float time1=0,UnityEngine.Events.UnityAction unityAction=null)
    {
      
        numberEffect.Animation(target,"","", time1, init, unityAction);

        if (tweener1 != null)
        {
            tweener1.Kill();
        }
        if (tweener2 != null)
        {
            tweener2.Kill();
        }
        textTf.localScale = Vector3.one;
       tweener1 = textTf.DOScale(Vector3.one * 1.2f, time);
        tweener1.onComplete =()=> tweener2 = textTf.DOScale(Vector3.one * 1f, time);
      
    }
    public void SetCount(int count,int count1)
    {
        text.text = count.ToString();
        text1.text= count1.ToString()+"元";
    }
}
