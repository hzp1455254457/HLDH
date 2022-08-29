using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class shouqianpiaochuang : TipsEffectBase
{
    public Image itemImage;
    public Text itemNumberText, xinyuText;

    // Start is called before the first frame update
    
    public override void Animation()
    {
        backTf.transform.DOScale(Vector3.one * 1.1f * scale, 0.8f).SetUpdate(true).onComplete
             += () => backTf.transform.DOScale(Vector3.one * 1f * scale, 0.8f).SetUpdate(true).onComplete += () =>
             {
                 foreach (var item in graphics)
                 {
                     item.DOFade(0, 0.8f).SetUpdate(true);
                 };

                 StartCoroutine(Global.Delay(0.7f, () =>
                 {
                     if (onComplete != null)
                     {
                         onComplete();
                         Destroy(gameObject);
                     }
                     else
                     {
                         Destroy(gameObject);
                     }
                     //  onComplete = null;
                 }));

             };
    }
}
