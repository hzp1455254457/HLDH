using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FaHuoTips : TipsEffectBase
{
    // Start is called before the first frame update
    public override void Animation()
    {
        transform.localScale = Vector3.one * scale;
        backTf.transform.DOScale(Vector3.one * 1.08f * scale, 0.3f).SetUpdate(true).onComplete
             += () =>StartCoroutine( Delay(0.3f, EndAnimation));
    }

    private void EndAnimation()
    {
        if (onComplete != null)
        {
            onComplete();
            onComplete = null;
        }
        backTf.transform.DOScale(Vector3.one * 1f * scale, 0.3f).SetUpdate(true).onComplete += () =>
        {
            foreach (var item in graphics)
            {
                item.DOFade(0, 0.5f).SetUpdate(true);
            };

            if (type == 0)
            {
                FaHuo.isLeftShow = false;
            }
            else
            {
                FaHuo.isRightShow = false;
            }
            if (ProduceQiPaoManager.Instance.produceQiPaolist.Count <= FaHuo.Maxcount)
            {
                EventQueueSystem.Instance.PlayerEvent();
            }
            GameObjectPool.Instance.CollectObject(gameObject, 1f);
        };
    }
    /// <summary>
    /// 0,×ó±ß£¬1ÓÒ±ß
    /// </summary>
    public int type;
    public override void Show(Transform targetTf, string[] tipsValue, Sprite[] sprite, UnityAction unityAction, Color[] color, float scale = 1f)
    {
        if (type == 0)
        {
            FaHuo.isLeftShow = true;
        }
        else
        {
            FaHuo.isRightShow = true;
        }
        this.scale = scale;
        transform.localScale = Vector3.one * scale;
        if (Img != null)
        {

            for (int i = 0; i < Img.Length; i++)
            {
                if (sprite != null && sprite.Length - 1 >= i)
                    Img[i].sprite = sprite[i];
                else
                {
                    Img[i].sprite = null;
                    Img[i].rectTransform.sizeDelta = new Vector2(0, Img[i].rectTransform.sizeDelta.y);
                }

            }
        }
        if (tipsInfo != null)
        {
            for (int i = 0; i < tipsInfo.Length; i++)
            {
                if (tipsValue != null && tipsValue.Length - 1 >= i)
                    tipsInfo[i].text = tipsValue[i];
                else
                {
                    tipsInfo[i].text = null;
                    tipsInfo[i].rectTransform.sizeDelta = new Vector2(0, tipsInfo[i].rectTransform.sizeDelta.y);
                }
                if (color != null && color.Length - 1 >= i)
                    tipsInfo[i].color = color[i];
                else
                {
                    tipsInfo[i].color = Color.black;

                }
            }
        }
        onComplete += unityAction;
        Animation(targetTf);
        StartCoroutine(ChangeX(x));

    }


    protected override void Animation(Transform targetTf)
    {
        foreach (var item in graphics)
        {
            item.DOFade(1, 0.8f).SetUpdate(true);
        }
        transform.DOMove(targetTf.position, 0.8f).SetUpdate(true).onComplete += Animation;
    }
    IEnumerator Delay(float time, UnityAction unityAction)
    {
      
        yield return new WaitForSeconds(time); ;
        // yield return new WaitForSeconds(0.8f);
      
        unityAction?.Invoke();
        //yield return time;
    }
}

