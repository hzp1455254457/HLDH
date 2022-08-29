using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyShopRedQiPao : Daimond
{
    Vector3 vector3;
  
    protected override void OnEnable()
    {
        base.OnEnable();
       // SetCount(count);
    }
    public override void SetCount(int count)
    {

        base.SetCount(count);
        vector3 = transform.position;


    }
    protected override void DaimondAnim()
    {

        quence = DOTween.Sequence();
        quence.Append(transform.DOMove(new Vector3(vector3.x, vector3.y + 0.1f, vector3.z), 1f)).SetEase(Ease.Linear).SetUpdate(true);

        quence.Append(transform.DOMove(new Vector3(vector3.x, vector3.y - 0.1f, vector3.z), 1f)).SetEase(Ease.Linear).SetUpdate(true);

        quence.SetEase(Ease.Linear);

        quence.SetLoops(-1);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (type == 1)
        {
            AddDaimondAnim();
            AndroidAdsDialog.Instance.UploadDataEvent("click_normal_RedQiPao");
        }
        AudioManager.Instance.PlaySound("bubble4");
    }
    public override void AddDaimondAnim()
    {
        //{if (value != currentBorn.index) return;
        //    if (isDouble)
        //    {
        //        count *= 2;
        //        SetCount(count);
        //    }
        if (quence != null)
            quence.Pause();
        transform.SetParent(UIManager.Instance.canvas.transform);
        transform.DOMove(MoneyManager.Instance.redTargetTf.position, 1f).SetUpdate(true).onComplete += () =>
        {
            FunEvent();
        };
        countText.color = new Color(1, 0, 0, 0);
        //  DaimondBornPosition.clickCount++;

        //ResourceManager.Instance.RecoveryDimondEffect(this.gameObject);
    }
    protected override void FunEvent()
    {
        DaimondBornPosition.clickCount++;

        gameObject.SetActive(false);
        // ResourceManager.Instance.RecoveryDimondEffect(gameObject);
        if (DaimondBornPosition.clickCount % 3 == 0 && DaimondBornPosition.clickCount != 0)
        {
            AndroidAdsDialog.Instance.ShowTableVideo("0");
        }
        PlayerData.Instance.GetRed(count);

        //ZhiBoPanel.GetDaimondCount(count);
        MyShopPanel.Instance.RemoveQiPao(this);
        GameObjectPool.Instance.CollectObject(this.gameObject, 0.1f);
        // ResourceManager.Instance.RecoveryDimondEffect(gameObject);
    }

}
