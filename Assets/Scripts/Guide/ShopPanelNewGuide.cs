using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShopPanelNewGuide : MonoBehaviour, IGuideFunc
{
    int clickCount = 0;
    public void GuideFuncEvent()
    {
        if (GuideManager.Instance.isFirstGame)
        {
            clickCount++;
            //PeopleEffect.Instance.SetTips(targetGuide1, Vector2.one, false);
            if (clickCount == 1)
            {
                AndroidAdsDialog.Instance.UploadDataEvent("new_version_guide3");
                //tipsGo3.SetActive(false);
                //tipsGo4.SetActive(false);
                PeopleEffect.Instance.HideMask();
              PeopleEffect.Instance.ShowMask(1f, () => PeopleEffect.Instance.SetTips(targetGuide1, Vector2.one, false));
                ShopPanelNew.Instance.scrollRect.enabled = false;
                //for (int i = 0; i < guideGos.Length; i++)
                //{
                //    guideGos[i].SetActive(true);
                //}
                //StartCoroutine(Global.Delay(0.5F, ShowGuide));
            }
            else if(clickCount == 2)
            {
                //for (int i = 0; i < guideGos.Length; i++)
                //{
                //    guideGos[i].SetActive(false);
                //}
                //tipsGo3.SetActive(true);
                //tipsGo4.SetActive(true);
                AndroidAdsDialog.Instance.UploadDataEvent("new_version_guide9");
                PeopleEffect.Instance.SetTips(this.GetComponent<RectTransform>(), Vector2.one, false);
            }
          
        }
    }
    public GameObject[] guideGos;
    public Graphic[] graphics;
    public RectTransform targetGuide1;
    public GameObject tipsGo3;
    public GameObject tipsGo4;
    // Start is called before the first frame update
    public void ShowGuide()
    {
        if (GuideManager.Instance.isFirstGame)
        {
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].DOFade(1, 0f);
                graphics[i].gameObject.SetActive(true);
                graphics[i].DOFade(1, 0.8f);
            }
            //graphics.DOFade(1, 0.8f); }
        }

        //else
        //{
        //    for (int i = 0; i < graphics.Length; i++)
        //    {
        //        graphics[i].gameObject.SetActive(false);
        //        //graphics[i].DOFade(1, 0.8f);
        //    }
        //}
    }
    public void GuideFuncEvent2()
    {
        if (GuideManager.Instance.isFirstGame)
        {
            if (clickCount == 1)
            {
                //for (int i = 0; i < graphics.Length; i++)
                //{
                //    graphics[i].gameObject.SetActive(false);
                //    //graphics[i].DOFade(1, 0.8f);
                //}
                ShopPanelNew.Instance.scrollRect.enabled = true;
                PeopleEffect.Instance.SetTips((UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).zhibojianList[0].proRect, Vector2.zero, false);
                AndroidAdsDialog.Instance.UploadDataEvent("new_version_guide4");
            }
            else if(clickCount==2)
            {
                var list = (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).zhibojianList;
                list[0].RecoverGuide1Status();
              
                //PeopleEffect.Instance.HideMask();
                PeopleEffect.Instance.SetTips((UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).zhibojianList[1].proRect, (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).zhibojianList[1].guideTarget2.position, false, RotaryType.RightToLeft);

            }
            
        }

       
    }
}

