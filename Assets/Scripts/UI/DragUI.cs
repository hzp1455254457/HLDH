using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class DragUI : MonoBehaviour, IDragHandler,IEndDragHandler,IBeginDragHandler
{
    public Transform parentTf;
    HorizontalLayoutGroup layoutGroup;
    private CanvasGroup canvasGroup;
    private CanKuPanel cankuPanel;
    private SelledProduce selledProduce;
    Tweener tween;
    Tweener stoptween;
    ScrollRect scrollRect;
    private void Start()
    {
        scrollRect = transform.parent.parent.GetComponent<ScrollRect>();
        layoutGroup = transform.parent.GetComponent<HorizontalLayoutGroup>();
        selledProduce = GetComponent<SelledProduce>();
           parentTf = transform.parent;
        
          canvasGroup = GetComponent<CanvasGroup>();
        cankuPanel = UIManager.Instance.GetPanel("Panel_CanKu") as CanKuPanel;
    }
    bool isDrag = false;
    int index = 0;
    public void OnBeginDrag(PointerEventData eventData)
    {

        //parentTf = transform.parent;

        //   if (Mathf.Abs(eventData.delta.y) >Mathf.Abs( eventData.delta.x)&& eventData.delta.y>0)
        //  {
       
             tween = transform.DOScale(Vector3.one * 1.2f, 0.5f).SetUpdate(true);
        index = transform.GetSiblingIndex();
        
            if (GuideManager.Instance.isFirstGame)
            {
                transform.SetParent(UIManager.Instance.canvas_Main.transform);
            }
            else
            {
               transform.SetParent(transform.parent.parent.parent);
            }
         //   isDrag = true;
       // }
       // else
       // {
        //    scrollRect.OnBeginDrag(eventData);
       // }
        canvasGroup.blocksRaycasts = false;
        cankuPanel.SetCanKuRay(true);
        layoutGroup.enabled = false;
        CanKuPanel.isFaHuo = true;
    }

    // Start is called before the first frame update
    public void OnDrag(PointerEventData eventData)
    {
        //if(isDrag)
        //{ transform.position = eventData.position; }
        transform.position = eventData.position;
        //else
        //{
        //    scrollRect.OnDrag(eventData);
        //}
        //scrollRect.OnDrag(eventData);
        
        //scrollRect.OnDrag(eventData);
      
        //else
        //{
        //    scrollRect.OnDrag(eventData);
        //}
        //print(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    { 
        //    scrollRect.OnEndDrag(eventData);
        //isDrag = false;
        //print(eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            transform.SetParent(parentTf);
            transform.SetSiblingIndex(index);
            tween.Pause();
           
            transform.localScale = Vector3.one;
            //canvasGroup.blocksRaycasts = true;
            //cankuPanel.SetCanKuRay(false);
         
        }
        else
        {
            if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Canku"))
            {
                CanKu canKu = eventData.pointerCurrentRaycast.gameObject.GetComponent<CanKu>();
                if (canKu != null)
                {
                    if (canKu.courier != null)
                    {
                        if (canKu.courier.Busy_state == 0)
                        { tween.Pause();
                            print(eventData.pointerCurrentRaycast.gameObject.name);
                            stoptween = transform.DOScale(Vector3.one * 0f, 0.5f).SetUpdate(true);
                            canKu.Sell(selledProduce.produceDate.item_id, selledProduce.produceDate.item_have);
                            //PlayerDate.Instance.RemoveSelledCount(selledProduce.produceDate.item_have);
                            selledProduce.SetSell();
                           
                            GameObjectPool.Instance.CollectObject(gameObject,0.5f);
                            AndroidAdsDialog.Instance.UploadDataEvent("sendscene_suc");
                            //AndroidAdsDialog.Instance.UploadDataEvent("tuozhuaihuowu");
                            if (GuideManager.Instance.isFirstGame)
                            {
                                cankuPanel.peopleEffect.HideTips();
                                AndroidAdsDialog.Instance.UploadDataEvent("new_course_7");
                                //  AndroidAdsDialog.Instance.UploadDataEvent("jiaocheng7");
                            }
                            else
                            {
                                if (selledProduce.produceDate.type == 1)
                                {
                                    Shop.Instance.ShowUI(true);
                                }
                                //if (AwardManagerNew.Instance.isFirstDropSellProduce)
                                //{
                                //    AwardManagerNew.Instance.ShowUI(null);
                                //    AwardManagerNew.Instance.SetisFirstDropSellProduce();
                                //}
                                //else if (Random.Range(1, 11) <= 5)
                                //{
                                //    AwardManagerNew.Instance.ShowUI(null);
                                //    // AwardManager.Instance.ShowUI(null);
                                //}
                            }
                        }
                        else
                        {
                            print(eventData.pointerCurrentRaycast.gameObject.name);
                            canKu.FaHuoIngAnim();
                            tween.Pause();
                           
                           transform.SetParent(parentTf);
                            transform.SetSiblingIndex(index);
                            transform.localScale = Vector3.one;
                            AndroidAdsDialog.Instance.UploadDataEvent("sendscene_failed");
                        }
                    }
                }
            }
            else
            {
                tween.Pause();
               
                transform.SetParent(parentTf);
                transform.SetSiblingIndex(index);
                transform.localScale = Vector3.one;
            }
           
         
        }
        canvasGroup.blocksRaycasts = true;
        cankuPanel.SetCanKuRay(false);
        layoutGroup.enabled = true;
        CanKuPanel.isFaHuo = false;
    }
}
