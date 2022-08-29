using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProInfoSCCtrl : SCCtrl
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        scrollRect = StockManager.Instance.scrollRect;
    }
}
