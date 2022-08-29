using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TuiXiaoDanMu : MonoBehaviour
{
    public Text text;
    public Image image;
    public Transform bornTf;
    public Vector3 targetTf;
    public int index;
    public Image xiadanImg;
    Graphic[] danmus;
    public TuiXiao tuiXiao;
   // public Animator animator;
    private void Start()
    {
       
        danmus = GetComponentsInChildren<Graphic>();
        foreach (var item in danmus)
        {
            item.DOFade(0, 0f);
        }
        StartCoroutine(Init());
    }
    IEnumerator Init()
    {
        yield return new WaitForSeconds(0.5f);
        targetTf = transform.localPosition;
    }
    public void SetDanMU(string value,Sprite sprite)
    {
       
        text.text = value;
        image.sprite = sprite;
    }
    public void Show()
    {
        //gameObject.SetActive(true);
        foreach (var item in danmus)
        {
            item.DOFade(1, 0.8f);
        }
        transform.position = bornTf.position;
        transform.DOLocalMove(targetTf, 0.8f);
        if (tuiXiao.isWin)
        {
            xiadanImg.gameObject.SetActive(true);
        }
        else
        {
            xiadanImg.gameObject.SetActive(false);
        }
    }
    public void Hide()
    {
        foreach (var item in danmus)
        {
            item.DOFade(0, 0.5f);
        }
        //SetAnim(false);
        //gameObject.SetActive(false);
    }
 
}
