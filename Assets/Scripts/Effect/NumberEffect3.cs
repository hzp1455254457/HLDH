using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NumberEffect3 : NumberEffect2
{
   

   public override void SetText(string value1 ,float value2,string value3)
    {

        // long value = 10000000000000;
        //  string v1=   value.ToString("n0");
        //string v2=  value2.ToString("n0");
        //  int count = v1.Length - v2.Length;
        // string v3=  v1.Substring(count);
        //  v3= v3.Replace('1', '0');
        //  value1 = v3;
       // string v = value2.ToString("000,000,000,000");
        text.text = string.Format("{0}{1:f3}{2}", value1, value2, value3);
    }
}
