using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCreact : MonoBehaviour
{
   BoxCollider2D _collider;
    RectTransform rect;
    void Start()
    {
        rect = GetComponent<RectTransform>();
          _collider = GetComponent<BoxCollider2D>();
        _collider.size = new Vector2(rect.rect.width, rect.rect.height);
        //if(rect.pivot.x!=0.5f)
        //_collider.offset = new Vector2(-( rect.pivot.x-0.5f) * rect.sizeDelta.x,0);
        //if (rect.pivot.y != 0.5f)
        //{
        //    if (rect.pivot.y > 0.5f)
        //    {
        //        _collider.offset = new Vector2(_collider.offset.x, -rect.pivot.y * 10);
        //    }
        //    else
        //    {
        //        _collider.offset = new Vector2(_collider.offset.x, rect.pivot.y * 10);
        //    }
        //}
    }


}
