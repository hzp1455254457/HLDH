using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTaskScctrl : SCCtrl
{
    protected override void Start()
    {
        base.Start();
        scrollRect =ShopTaskManager.Instance. scrollRect;
    }


}
