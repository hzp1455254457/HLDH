using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
public class RedAndProduceAdward :MonoBehaviour
{
    public Image produceImg, redImg;
    public Text text1, text2, text3, text4;
    Graphic[] graphics;
  public  RectTransform[] rects;
   public RectTransform rectBack;

    public Transform backTf;
    private void Awake()
    {
        rectBack= GetComponent<RectTransform>();
        graphics = GetComponentsInChildren<Graphic>();
        rects = text1.GetComponentsInChildren<RectTransform>();
        foreach (var item in graphics)
        {
            item.DOFade(0, 0);
            
        }
    }
   
    public void Show(Transform targetTf, string count1, Sprite sprite,string count2, string count3, string count4)
    {
        Animation(targetTf);
        text1.text = string.Format("{0}", count1);
        text2.text = string.Format("{0}", count3);
        produceImg.sprite = sprite;
        text4.text = string.Format("{0}", count4);
        text3.text = string.Format("{0}", count2);
       StartCoroutine( Delay());
    }

    private IEnumerator Delay()
    {
        yield return 0;
        float weight = 0;
        foreach (var item in rects)
        {
            weight += item.sizeDelta.x;
        }
        rectBack.sizeDelta = new Vector2(weight + 110, rectBack.rect.height);
    }

    private void Animation(Transform targetTf)
    {
        foreach (var item in graphics)
        {
            item.DOFade(1, 1f);
        }
        transform.DOLocalMoveY(targetTf.localPosition.y, 0.6f,true).SetEase(Ease.OutQuint).SetUpdate(true).onComplete+=Fun;
    }
    public void Fun()
    {
        Animation();
    }
    public UnityAction onComplete;
    public  void Animation()
    {
     
        backTf.transform.DOScale(Vector3.one * 1.1f, 0.8f).SetUpdate(true).onComplete
             += () => backTf.transform.DOScale(Vector3.one * 1f, 0.8f).SetUpdate(true).onComplete += () =>
              {
                  foreach (var item in graphics)
                  {
                      item.DOFade(0, 0.8f);
                  };
                  GameObjectPool.Instance.CollectObject(gameObject,1f);
                  if (onComplete != null)
                  {
                      onComplete();
                      onComplete = null;
                  }
              };
    }
}
