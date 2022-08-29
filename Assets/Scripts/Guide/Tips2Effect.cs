using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tips2Effect : MonoBehaviour
{
    int a;
    private void Awake()
    {
        //transform.localPosition = transform.localPosition;
        a = (int)(FindObjectOfType<Canvas>().scaleFactor * 50);
      y1 = transform.localPosition.y + a;
       
        y2 = transform.localPosition.y;
        print("y2++++" + y2);
    }
    private void OnEnable()
    {
        //transform.localPosition = transform.localPosition;

        //y1 = transform.localPosition.y + (int)(FindObjectOfType<Canvas>().scaleFactor * 50);
        //if (quence == null)
            ButtonAnimation();
        //else
        //{
        //    quence.SetLoops(-1);
        //}
    }
    private void OnDisable()
    {
        quence.Pause();
        quence.SetLoops(0);
        DOTween.Kill(quence, true);
    }
 
     float y1;
float y2;
    Sequence quence;
    public void ButtonAnimation()
    {
         //quence = DOTween.Sequence();
        y1 = transform.localPosition.y + a;
        print("y1++++" + y1);
        y2 = transform.localPosition.y;
        print("y2++++" + y2);
        quence = DOTween.Sequence();
        quence.Append(transform.DOScale(new Vector3(1.2F, 0.8F, 0), 0.3F).SetUpdate(true));        
        quence.Append(transform.DOScale(new Vector3(0.8F, 1.2F, 0), 0.3F).SetUpdate(true));
        quence.Join(transform.DOLocalMoveY(y1, 0.15F).SetEase(Ease.OutQuint).SetUpdate(true));
        quence.Append(transform.DOLocalMoveY( y2, 0.15F).SetEase(Ease.OutQuint).SetUpdate(true));
        quence.Append(transform.DOScale(new Vector3(1.2F, 0.8F, 0), 0.3F).SetUpdate(true));
        quence.Append(transform.DOScale(new Vector3(1F, 1F, 0), 0.15F).SetUpdate(true));
        quence.SetUpdate(true);
        quence.SetEase(Ease.Linear);
        quence.AppendInterval(1);
        quence.SetLoops(-1);

    }
}