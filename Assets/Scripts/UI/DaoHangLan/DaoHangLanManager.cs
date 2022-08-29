using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DaoHangLanManager : MonoBehaviour
{
    public RectTransform taskRectTransform;
    public RectTransform zhuBoListRectTransform;
    public RectTransform shangJinTaskTf;
    float taskVec;
    float zhuBoListVec;
    float shangJinVec;
    bool isShow =false;
    private void Awake()
    {
        if (taskRectTransform != null)
        { taskVec = taskRectTransform.localPosition.x;
            if (GuideManager.Instance.isFirstGame)
            {
                taskRectTransform.gameObject.SetActive(false);
            }
        }
        if (zhuBoListRectTransform != null)
        { zhuBoListVec = zhuBoListRectTransform.localPosition.x;
            if (GuideManager.Instance.isFirstGame)
            {
                zhuBoListRectTransform.gameObject.SetActive(false);
            }
        }
        if (shangJinTaskTf != null)
        {
            shangJinVec = shangJinTaskTf.localPosition.x;
        }
        GuideManager.Instance.achieveGuideAction += AchiveGuide;
       // taskRectTransform.SetParent(UIManager.Instance._ToggleCanvas.transform, false);
    }
    public void AchiveGuide()
    {
        if (taskRectTransform != null)
        {

            taskRectTransform.gameObject.SetActive(true);
        }
        if (zhuBoListRectTransform != null)
        {
            zhuBoListRectTransform.gameObject.SetActive(true);

        }
    }
    Tweener tweener;
    public void SetShow(bool value)
    {
      
        if (!value)
        {
            //if (isShow)
            //{
            //    taskRectTransform.DOLocalMoveX((taskVec - taskRectTransform.rect.width), 0.5f);
            //    zhuBoListRectTransform.DOLocalMoveX((zhuBoListVec + zhuBoListRectTransform.rect.width), 0.5f);
            //}
            //if (isShow == true)
            //{
            //    if (tweener != null)
            //    {
            //        tweener.Kill();
            //    }
            //    taskRectTransform.localPosition = new Vector2((taskVec - taskRectTransform.rect.width), taskRectTransform.localPosition.y);
            //    //zhuBoListRectTransform.localPosition = new Vector2((zhuBoListVec + zhuBoListRectTransform.rect.width), zhuBoListRectTransform.localPosition.y);
            //    tweener= taskRectTransform.DOLocalMoveX(taskVec, 0.5f);
            //    //zhuBoListRectTransform.DOLocalMoveX(zhuBoListVec, 0.5f);
            //}
           
        }
        else
        {
            
            if (isShow == false)
            {
                if (tweener != null)
                {
                    tweener.Kill();
                }
                //taskRectTransform.SetParent(this.transform, false);
                if (taskRectTransform != null)
                {
                    taskRectTransform.localPosition = new Vector2((taskVec - taskRectTransform.rect.width), taskRectTransform.localPosition.y);
                    tweener = taskRectTransform.DOLocalMoveX(taskVec, 0.5f);
                }
                if (zhuBoListRectTransform != null)
                {
                    zhuBoListRectTransform.localPosition = new Vector2((zhuBoListVec + zhuBoListRectTransform.rect.width), zhuBoListRectTransform.localPosition.y);

                    zhuBoListRectTransform.DOLocalMoveX(zhuBoListVec, 0.5f);
                }
                if (shangJinTaskTf != null)
                {
                    shangJinTaskTf.localPosition= new Vector2((shangJinVec + shangJinTaskTf.rect.width), shangJinTaskTf.localPosition.y);
                    shangJinTaskTf.DOLocalMoveX(shangJinVec, 0.5f);
                }
            }
        }
        isShow = value;
    }
    public void SetParent(Transform parentTF)
    {
       // taskRectTransform.SetParent(parentTF);
    }
    private void OnDestroy()
    {
        GuideManager.Instance.achieveGuideAction -= AchiveGuide;
    }
}
