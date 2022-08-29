using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SCCtrl : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public ScrollRect scrollRect;
    Button button;

    protected virtual void Start()
    {
        button = GetComponent<Button>();
        
    }
    public virtual void OnBeginDrag(PointerEventData data)
    {
        scrollRect.OnBeginDrag(data);
        button.enabled = false;
    }

    public virtual void OnDrag(PointerEventData data)
    {
   scrollRect.OnDrag(data);
    }

    public virtual void OnEndDrag(PointerEventData data)
    {
        scrollRect.OnEndDrag(data);
        button.enabled = true;
    }

}
