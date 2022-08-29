using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DaimondFaHuo : Daimond
{

    //public  TextMeshPro newcountText;
    Vector3 vector3;
    //protected override void OnEnable()
    //{
    //    image.color = new Color(1, 1, 1, 0);

    //  newcountText.color = new Color(1, 0, 0, 0);

    //    newcountText.DOFade(1, 0.5f);
    //    image.DOFade(1, 0.5f).onComplete += () => { DaimondAnim(); };
    //}
    public override void SetCount(int count)
    {
      
        base.SetCount(count);
        vector3 = transform.position;
      

    }

    protected override void DaimondAnim()
    {

        quence = DOTween.Sequence();
        quence.Append(transform.DOMoveY(vector3.y+0.1f,1f)).SetEase(Ease.Linear).SetUpdate(true);

        quence.Append(transform.DOMoveY(vector3.y -0.1f,1f)).SetEase(Ease.Linear).SetUpdate(true);

        quence.SetEase(Ease.Linear);

        quence.SetLoops(-1);
    }
    
    protected override void FunEvent()
    {
       
       
        //gameObject.SetActive(false);

        AndroidEvent();
        // ResourceManager.Instance.RecoveryDimondEffect(gameObject);

        //else if (DaimondBornPosition.clickCount % 10 == 0 && DaimondBornPosition.clickCount != 0)
        //{
        //    AndroidAdsDialog.Instance.ShowFullVideo("¿ªÆÁ");
        //}
        PlayerData.Instance.GetDiamond(count);
        GetDaimond();
        //Destroy(gameObject);
        // ResourceManager.Instance.RecoveryDimondEffect(gameObject);
    }

  public void GetDaimond(bool isGetAll=false)
    {
        if (quence != null)
            quence.Pause();
       
        PlayerData.Instance.AddGetDiamondCount(isGetAll,false);
        //AndroidAdsDialog.Instance.AddSignDataCount(5);
        AndroidAdsDialog.Instance.addDiamondClick();
        ZhiBoPanel.GetDaimondCount(count);
        GameObjectPool.Instance.CollectObject(this.gameObject, 0.1f);
    }

    public override void AddDaimondAnim()
    {
        base.AddDaimondAnim();
        ProduceQiPaoManager.Instance.RevomeDaimond(this);
    }

}


