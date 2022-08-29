using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ZhiBoJianRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    Vector2 vector2;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //print("移动开始");
        vector2 = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //print("移动结束");
        if (Mathf.Abs(eventData.position.y - vector2.y) > 0)
        {
            //print("移动");
            if (eventData.position.y - vector2.y > 0)
            {
                Move(ZhiBoPanel.Instance, false);
                //print("向上移动");
                print(eventData.delta.y);
            }
            else
            {
                //print("向下移动");
                Move(ZhiBoPanel.Instance, true);
                print(eventData.delta.y);
            }
        }
    }
    public void Move(ZhiBoPanel zhiBoPanel, bool value)
    {
        if (value)
            zhiBoPanel.MoveNextZhuBo();
        else
        {
            zhiBoPanel.MoveLastZhuBo();
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        //print("移动中");
    }
}
