using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Panel_ZhuBoList : MonoSingleton<Panel_ZhuBoList>
{
    public CanvasGroup canvasGroup;
    public List<ZhuBoItem> zhuBoItems = new List<ZhuBoItem>();
    bool isInit = false;
    public Transform parentTf;
    public ZhuBoItem1 zhuBoItem1;
    public Button button;
    public RectTransform rectTransform;
    public Image image;
    public Sprite[] zhuboSprits;
    void Awake()
    {
        transform.SetParent(UIManager.Instance.showRootMain1);
        vector2 = rectTransform.localPosition.x;
        //transform.SetSiblingIndex()
    }
    public void SetShowOrHide(bool value)
    {
        canvasGroup.alpha = value == true ? 1 : 0;
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
    }
    public void HideUI()
    {
        Animation1();
    }
    float vector2;
    private void Animation()
    {
        image.DOFade(0.5f, 0.5f);
        rectTransform.localPosition = new Vector2(vector2 + rectTransform.rect.width, rectTransform.localPosition.y);
        rectTransform.DOLocalMoveX(vector2, 0.3f).SetEase(Ease.OutBack);
    }
    private void Animation1()
    {
        image.DOFade(0f, 0.5f);
        //rectTransform.localPosition = new Vector2(vector2 + rectTransform.rect.width, rectTransform.localPosition.y);
        rectTransform.DOLocalMoveX(vector2 + rectTransform.rect.width, 0.3f).SetEase(Ease.OutQuint).onComplete += () => { gameObject.SetActive(false); SetShowOrHide(false); };
    }
    public void ShowUI()
    {
        SetShowOrHide(true);
        gameObject.SetActive(true);
        Animation();
        if (!isInit)
        {
            CreactZhuBoList();
            isInit = true;
            UnityActionManager.Instance.AddAction<ActorDate>("CreactZhuBoItem", CreactZhuBoItem);
        }
        for (int i = 0; i < zhuBoItems.Count; i++)
        {
            zhuBoItems[i].SetZhiBoJian(ZhiBoPanel.Instance.zhibojianList[i]);
        }
        zhuBoItem1.SetText(string.Format("<color=red>{0}</color> 升至 <color=red>{1}</color>级 解锁新主播", ZhiBoPanel.Instance.zhibojianList[ZhiBoPanel.Instance.currentIndex].actorDate.actor_name,
            ZhiBoPanel.Instance.zhibojianList[ZhiBoPanel.Instance.currentIndex].actorDate.need_level_new_actor));
    }
    public void CreactZhuBoList()
    {
        if (ZhiBoPanel.Instance.zhibojianList != null && ZhiBoPanel.Instance.zhibojianList.Count > 0)
        {
            foreach (var item in ZhiBoPanel.Instance.zhibojianList)
            {
                var obj = Instantiate(ResourceManager.Instance.GetProGo("ZhuBoItem"), parentTf);
                ZhuBoItem zhuBoItem = obj.GetComponent<ZhuBoItem>();
                // zhuBoItem.SetZhiBoJian(item);
                zhuBoItems.Add(zhuBoItem);
                zhuBoItem.transform.SetSiblingIndex(zhuBoItems.Count - 1);
            }
        }
    }
    public void CreactZhuBoItem(ActorDate actorDate)
    {
        var obj = Instantiate(ResourceManager.Instance.GetProGo("ZhuBoItem"), parentTf);
        ZhuBoItem zhuBoItem = obj.GetComponent<ZhuBoItem>();
        // zhuBoItem.SetZhiBoJian(item);
        zhuBoItems.Add(zhuBoItem);
        zhuBoItem.transform.SetSiblingIndex(zhuBoItems.Count - 1);
        //zhuBoItem.transform.SetParent(parentTf);
    }
}
