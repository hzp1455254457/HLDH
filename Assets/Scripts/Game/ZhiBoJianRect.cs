using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ZhiBoJianRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    Vector2 vector2;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //print("�ƶ���ʼ");
        vector2 = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //print("�ƶ�����");
        if (Mathf.Abs(eventData.position.y - vector2.y) > 0)
        {
            //print("�ƶ�");
            if (eventData.position.y - vector2.y > 0)
            {
                Move(ZhiBoPanel.Instance, false);
                //print("�����ƶ�");
                print(eventData.delta.y);
            }
            else
            {
                //print("�����ƶ�");
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
        //print("�ƶ���");
    }
}
