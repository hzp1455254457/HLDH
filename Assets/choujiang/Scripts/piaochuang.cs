using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class piaochuang : MonoBehaviour
{
    public Text content;
    public Image contentImage;
    // Start is called before the first frame update

    public void InitPiaoChuang(Sprite g,string str,Vector2 start,Vector2 end)
    {
        contentImage.sprite = g;
        content.text = str;
        RectTransform t = GetComponent<RectTransform>();
        t.anchoredPosition = start;
        t.DOAnchorPos(end, 1.0f).onComplete = ()=> {
            Destroy(gameObject);
        };
    }
}
