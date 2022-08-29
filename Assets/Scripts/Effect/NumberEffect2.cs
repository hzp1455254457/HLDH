using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NumberEffect2 : MonoBehaviour
{
    public Text text;
    
    Tweener tweener = null;
    bool isZhiXing = false;
    public void Animation(float taget, string value,string value1,float time=1f, float initCount=0, UnityEngine.Events.UnityAction unityAction = null)
    {
      
        if (tweener != null)
        {
            isZhiXing = false;
            //tweener.Pause();
            tweener.Kill();
            tweener.onUpdate = null;
        }
        tweener = DOTween.To(() => initCount, x => initCount = x, taget, time);
        tweener.SetUpdate(true);
        tweener.onKill = () => {
            //Debug.LogError("KILL");
            SetText(value, taget, value1);
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
        tweener.onComplete = () => {

            unityAction?.Invoke();
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

   public virtual void SetText(string value1 ,float value2,string value3)
    {

        // long value = 10000000000000;
        //  string v1=   value.ToString("n0");
        //string v2=  value2.ToString("n0");
        //  int count = v1.Length - v2.Length;
        // string v3=  v1.Substring(count);
        //  v3= v3.Replace('1', '0');
        //  value1 = v3;
       // string v = value2.ToString("000,000,000,000");
        text.text = string.Format("{0}{1:f2}{2}", value1, value2*100, value3);
    }
}
