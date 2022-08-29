using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NumberEffect : MonoBehaviour
{
    public Text text;
   // int currentCount;
   // string currentValue1;
    //string currentValue2;
   //int targetCount;
    Tweener tweener=null;
    bool isZhiXing = false;
    public void Animation(int taget, string value,string value1,float time=1f, int initCount=0,UnityEngine.Events.UnityAction unityAction=null)
    {
        //currentCount = initCount;
      
      

        if (tweener != null)
        {
            isZhiXing = false;
            //tweener.Pause();
            tweener.Kill();
            tweener.onUpdate = null;
        }
        tweener = DOTween.To(() => initCount, x => initCount = x, taget, time);
        tweener.SetUpdate(true);
        tweener.onKill = () => { SetText(value, taget, value1);
           // Debug.LogError("KILL");
            if (!isZhiXing)
            {
                unityAction?.Invoke();
            }
        };
        tweener.onUpdate = () => {
            //print("currentCount" + currentCount);
            SetText(value, initCount, value1);
            //Debug.LogError("UPDATA");
        };
        tweener.onComplete = () => { unityAction?.Invoke();
            SetText(value, taget, value1);
            tweener = null;
            isZhiXing = true;
        };
    }
    //private void Update()
    //{
    //    SetText(currentValue1,currentCount, currentValue2);
    //    if (currentCount == targetCount)
    //    {
    //        enabled = false;
    //    }
    //}

   public virtual void SetText(string value1 ,int value2,string value3)
    {

    
        text.text = string.Format("{0}{1}{2}", value1, value2, value3);
    }
}
