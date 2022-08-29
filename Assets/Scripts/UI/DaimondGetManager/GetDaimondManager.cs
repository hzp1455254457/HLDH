using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GetDaimondManager : MonoBehaviour
{
    public NumberEffect numberEffect;
    public int init = 0;
    public Text text;
  public void Show(bool value)
    {
        
        if (value)
        {
            if (!gameObject.activeInHierarchy)
            { AndroidAdsDialog.Instance.UploadDataEvent("show_collect_zs_infahuo");
                gameObject.SetActive(true);
                transform.localScale = Vector3.zero;
                transform.DOScale(Vector3.one, 0.5f);
            }
        }
        else
        {
            if (gameObject.activeInHierarchy)
            {
                
                gameObject.SetActive(false);
            }
        }
    }
    public void AddCount(int count)
    {
        numberEffect.Animation(init+
            count, "","", 1f, init);
            init += count;
        //text.text = init.ToString();
    }
  public void RemoveCount(int count)
    {
        // numberEffect.Animation(count, "", "", 0f, init);
       
         init -= count;
        text.text = init.ToString();
    }
    public void ClickFun()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("click_collect_zs_infahuo");

        AndroidAdsDialog.Instance.ShowRewardVideo("Ò»¼ü×êÊ¯", () =>
         {
             AndroidAdsDialog.Instance.UploadDataEvent("finish_ad_collect_zs_infahuo");
             StartCoroutine(Global.Delay(0.1f, () => {
                 ProduceQiPaoManager.Instance.CleanDaimond();
                 init = 0;
             }));
           
         });
    }
}
