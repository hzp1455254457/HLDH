using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FaHuoGuide :MonoBehaviour, IGuideFunc
{
    public GameObject guideGameObject;
   // public GameObject guideGameObject1;
    public Graphic[] graphics;
    public void GuideFuncEvent()
    {
        if (GuideManager.Instance.isFirstGame)
        {
            foreach (var item in graphics)
            {
                item.DOFade(1, 0.5f);
            }
            foreach (var item in guideColliderGo)
            {
                item.SetActive(false);
            }
            foreach (var item in guideColliderGo)
            {
                item.SetActive(true);
            }
        }
            guideGameObject.SetActive(GuideManager.Instance.isFirstGame);
     

    }
    public void GuideFuncEvent1()
    {

        guideGameObject.SetActive(!GuideManager.Instance.isFirstGame);
        foreach (var item in guideColliderGo)
        {
            item.SetActive(false);
        }

    }
    public void GuideFuncEvent2()
    {

        guideGameObject.SetActive(!GuideManager.Instance.isFirstGame);
        AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
             {
                "松手","开始发货" }, null, new Color[]
                 {
                    Color.red,Color.black
                 }, null);

    }


    private void Start()
    {
        if (GuideManager.Instance.isFirstGame)
        {
            foreach (var item in graphics)
            {
                item.DOFade(0, 0f);
            }
        }
        guideGameObject.SetActive(GuideManager.Instance.isFirstGame);
    }
    // Start is called before the first frame update
    public GameObject[] guideColliderGo;
}
