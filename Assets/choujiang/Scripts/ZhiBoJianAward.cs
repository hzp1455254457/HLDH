using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ZhiBoJianAward : MonoBehaviour
{
    // Start is called before the first frame update
    public Text redText, daimondText,time;
    public Button button;
    int red, daimond;
    public Transform back;
    private void Start()
    {
        button.onClick.AddListener(ClickFun);
    }
    public void Show(int red,int daimond)
    {
        if (!gameObject.activeInHierarchy)
        {
            back.transform.localScale = Vector3.zero;
            gameObject.SetActive(true);
            back.DOScale(1, 0.8f);
         
        }
        else
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            back.DOScale(0, 0.8f).onComplete += () => { back.DOScale(1, 0.8f); };
        }
       // this.red =UnityEngine.Random.Range(20f,40f);
        redText.text = string.Format("+{0:f3}元", UnityEngine.Random.Range(20f, 40f));
      
        this.daimond = daimond;
      daimondText.text= string.Format("+{0}个", daimond);
        //  StartCoroutine(Global.Delay(5, Hide));
        coroutine= StartCoroutine(Global.Timing1(time, Hide, 45));
        AndroidAdsDialog.Instance.UploadDataEvent("show_shengjiqipao");
    }
    Coroutine coroutine;
    public void ClickFun()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("click_shengjiqipao");
        AndroidAdsDialog.Instance.ShowRewardVideo("ZhiBoJianAward", () =>
        {
            AndroidAdsDialog.Instance.UploadDataEvent("finish_shengjiqipao");
            Hide();
           // PlayerData.Instance.GetDiamond(daimond);
            
            hongbao6.Instance.ShowUI(NumberGenenater.GetRedCount(), daimond, 1, () => {
                
            
            }, "气泡红包奖励");
        });
       
    }

    private void ShowPiaoChuang()
    {
        TipsShowBase.Instance.Show("TipsShow3", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
   {
            string.Format("+{0:f2}元",red/MoneyManager.redProportion),string.Format("+{0}",daimond)
       }, new Sprite[]
       {
                ResourceManager.Instance.GetSprite("红包"),  ResourceManager.Instance.GetSprite("钻石")
       }, null, null);
    }

    public void Hide()
    {if (gameObject.activeInHierarchy)
        {if(coroutine!=null)
            StopCoroutine(coroutine);
            back.DOScale(0, 0.8f).onComplete += ()=>gameObject.SetActive(false);
        }
    }
    
}
