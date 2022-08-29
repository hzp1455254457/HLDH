using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaimondTipPanel : PanelAnimation
{ public Transform lightImage;


    void Start()
    {
        base.Animation();
        transform.SetAsLastSibling();
        if (lightImage != null)
        { lightImage.DOLocalRotate(new Vector3(0, 0, -360 * 5000.0f), 5.0f * 5000, RotateMode.LocalAxisAdd);}
    }
    public void ClickFun()
    {
        Destroy(gameObject);
    }
  
}
