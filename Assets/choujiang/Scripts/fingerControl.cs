using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Cinemachine;

public class fingerControl : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public GameObject cameraObject;
    public CinemachineVirtualCamera camera;
    private Vector3 cameraPos;
    private Vector2 oldPos, newPos;
    public float speedFactor = 0.3f;

    private Tweener moveTweener = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        camera.transform.localPosition = cameraObject.transform.localPosition;

        oldPos = eventData.position;
        cameraPos = camera.transform.localPosition;
        //moveTweener.Kill();
        //moveTweener = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //moveTweener.Kill();
        //moveTweener = null;
        newPos = eventData.position;
        cameraPos = camera.transform.localPosition;
        moveTweener = camera.transform.DOLocalMoveY(cameraPos.y+(newPos.y-oldPos.y)* speedFactor, 0.5f);
        oldPos = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        camera.transform.localPosition = cameraObject.transform.localPosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        camera.transform.localPosition = cameraObject.transform.localPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
        camera.transform.localPosition = cameraObject.transform.localPosition;
    }

}
