using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimtion : MonoBehaviour
{
    private Sequence queen;
    public Transform buttonTf;
    private void Start()
    {
        ButtonAnim();
    }
    public void ButtonAnim()
    {
        queen = DOTween.Sequence();
        queen.Append(buttonTf.DOScale(1.2f, 1.0f));
        queen.AppendInterval(0.3f);
        queen.Append(buttonTf.DOScale(1.0f, 1.0f));
        queen.SetLoops(-1);
    }
    public void StopButtonAnim()
    {
        if (queen != null)
        {
            queen.Kill();
            buttonTf.localScale = Vector3.one;
        }
    }
}
