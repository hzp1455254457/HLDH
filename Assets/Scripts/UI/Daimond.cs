using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Daimond : MonoBehaviour,IPointerClickHandler
{
    public Image image;
  public  int count;
    public TextMeshProUGUI countText;
    public DaimondBornPosition currentBorn;
    public int type;//1是普通状态，2是高额钻石

  
    protected virtual void OnEnable()
    {
        image.color = new Color(1,1,1,0);

        countText.color = new Color(1, 0, 0, 0);
       
        countText.DOFade(1, 0.5f);
        image.DOFade(1, 0.5f).onComplete = () => { DaimondAnim(); };
    }
  protected  Sequence quence;
    public virtual void SetCount(int count)
    {
        this.count = count;
        countText.text = count.ToString();
        image.raycastTarget = true;
    }
    protected virtual void DaimondAnim()
    {
       
      quence = DOTween.Sequence();
        quence.Append(transform.DOLocalMoveY(10f,1f)).SetEase(Ease.Linear).SetUpdate(true);

        quence.Append(transform.DOLocalMoveY(-10f, 1f)).SetEase(Ease.Linear).SetUpdate(true);
       
        quence.SetEase(Ease.Linear);
      
        quence.SetLoops(-1);
    }
    public virtual void AddDaimondAnim() { 
    //{if (value != currentBorn.index) return;
    //    if (isDouble)
    //    {
    //        count *= 2;
    //        SetCount(count);
    //    }
    if(quence!=null)
        quence.Pause();
        transform.SetParent(UIManager.Instance.showRootMain1);
        transform.SetAsLastSibling();
        transform.DOMove(MoneyManager.Instance.daimondTargetTf.position, 1f).SetUpdate(true).onComplete = () =>
        {
            FunEvent();
        };
        if(countText!=null)
        countText.color = new Color(1, 0, 0, 0);
      //  DaimondBornPosition.clickCount++;
       
        //ResourceManager.Instance.RecoveryDimondEffect(this.gameObject);
    }

    protected virtual void FunEvent()
    {
        AndroidEvent();

        //if (DaimondBornPosition.clickCount % 10!= 0 && DaimondBornPosition.clickCount != 0)
        //{

        //    if (DaimondBornPosition.clickCount % 10 == 4)
        //    {
        //        AndroidAdsDialog.Instance.ShowTableVideo("0");
        //    }


        //}
        //else if(DaimondBornPosition.clickCount % 10 == 0 && DaimondBornPosition.clickCount != 0)
        //{
        //    AndroidAdsDialog.Instance.ShowFullVideo("开屏");
        //}
        //AndroidAdsDialog.Instance.AddSignDataCount(5);
        AndroidAdsDialog.Instance.addDiamondClick();
        PlayerData.Instance.GetDiamond(count);
        PlayerData.Instance.AddGetDiamondCount(false);
        if (currentBorn != null)
            currentBorn.isHave = false;
        ZhiBoPanel.GetDaimondCount(count);
        //Destroy(gameObject);
        GameObjectPool.Instance.CollectObject(gameObject,0.1f);
    }

    public void AndroidEvent()
    {
       

      
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        image.raycastTarget = false;
        if (type == 1)
        {
            AddDaimondAnim();
            AndroidAdsDialog.Instance.UploadDataEvent("click_normal_diamond"); 
        }
        else
        {
            AndroidAdsDialog.Instance.UploadDataEvent("click_double_diamond");
            DaimondManager.Instance.ShowUI(this);

        }
        AudioManager.Instance.PlaySound("bubble4");
       // AndroidAdsDialog.Instance.UploadDataEvent("click_zuanshi");

    }
    private void Start()
    {
        //JavaCallUnity.Instance.getDaimondCallBack += AddDaimondAnim;
    }
}
