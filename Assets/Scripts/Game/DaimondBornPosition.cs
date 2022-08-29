using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaimondBornPosition : MonoBehaviour
{
    public bool isHave;//位置是否被占用
    public static int clickCount;
    public string index;
    public void SetDaimond(Daimond Daimondtransform)
    {
        isHave = true;
        Daimondtransform.transform.position = transform.position;
        Daimondtransform.transform.SetParent(transform);
        Daimondtransform.currentBorn = this;
    }
   
}
