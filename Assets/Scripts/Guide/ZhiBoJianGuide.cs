using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZhiBoJianGuide : AnimationBase,IGuideFunc
{
    public void GuideFuncEvent()
    {
     
            tipsGo.SetActive(GuideManager.Instance.isFirstGame);
       
    }
    public GameObject tipsGo;
    // Start is called before the first frame update
    public GameObject tipsGo1;
    public GameObject tipsGo2;
    
  [Header("Éý¼¶ÌáÊ¾")]
    public GameObject tipsGo3;

    public void GuideFuncEvent1(bool value)
    {
        if (GuideManager.Instance.isFirstGame)
        {

            tipsGo1.SetActive(value);
            if (!value)
            {
                backTf.localScale = Vector3.zero;
                backTf.gameObject.SetActive(!value);
                base.Animation( 0.3f);
               // tipsGo2.SetActive(!value);
            }
        }
    }
    public void GuideFuncEvent2()
    {
        PeopleEffect.Instance.HideMask();
        backTf.gameObject.SetActive(false);
        //base.Animation();
       // tipsGo2.SetActive(false);
        GuideManager.Instance.AchieveGuide();
    }
    private void ShowTips3(
        )
    {
        tipsGo3.SetActive(true);
    }
    private void Start()
    {
        
    }
}
