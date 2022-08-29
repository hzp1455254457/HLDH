using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBase : PanelAnimation
{
 public  CanvasGroup canvasGroup;
    public string panelName;
    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public virtual void Hide()
    {
        //if (canvasGroup == null)
        //{ canvasGroup = GetComponent<CanvasGroup>(); 
        //}
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        transform.SetParent(UIManager.Instance.hideRoot);
    }
  public virtual void Show()
    {
        //if (canvasGroup == null)
        //{ canvasGroup = GetComponent<CanvasGroup>(); }
       // gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        transform.SetParent(UIManager.Instance.showRoot);
       
    }

    public virtual void  Unload()
    {
        Destroy(gameObject);
    }

    public virtual void Init()
    {
        
    }
    public virtual void SetHideOrShow(bool value)
    {
        if (value)
        {
            Show();
          
        }
        else
        {
            Hide();
        }
    }
}
