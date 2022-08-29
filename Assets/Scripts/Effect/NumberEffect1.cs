using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NumberEffect1 : NumberEffect
{
  

   public override void SetText(string value1 ,int value2,string value3)
    {

        // long value = 10000000000000;
        //  string v1=   value.ToString("n0");
        //string v2=  value2.ToString("n0");
        //  int count = v1.Length - v2.Length;
        // string v3=  v1.Substring(count);
        //  v3= v3.Replace('1', '0');
        //  value1 = v3;
        string v = value2.ToString("000,000,000,000");
        text.text = string.Format("{0}{1}{2}", value1, v, value3);
    }
}
