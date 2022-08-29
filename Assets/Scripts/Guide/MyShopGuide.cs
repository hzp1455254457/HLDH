using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShopGuide : MonoBehaviour, IGuideFunc
{
    public GameObject guideGo;
    public GameObject guideTips1, guideTips2;
    public void GuideFuncEvent()
    {
        guideGo.SetActive(!GuideManager.Instance.isFirstGame);
       // guideTips1.SetActive(GuideManager.Instance.isFirstGame);
        guideTips2.SetActive(GuideManager.Instance.isFirstGame);
    }
    public void GuideFuncEvent1()
    {
        guideGo.SetActive(true);
        //guideTips1.SetActive(false);
        guideTips2.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        //GuideFuncEvent();
    }

    
}
