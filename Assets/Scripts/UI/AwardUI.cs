using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class AwardUI : MonoBehaviour
{
    public Text awardCount;
    int count;
    public GameObject awardGo;
 public SkeletonGraphic awardAnim;
   public void RefreshText()
    {
        awardCount.text = string.Format("{0}",PlayerData.Instance.moneyReward) ;
        count = PlayerData.Instance.moneyReward;
    }
    public void RefreshAnimationText()
    {
        DOTween.To(() => count, x => count = x, PlayerData.Instance.moneyReward, 1f).SetEase(Ease.Linear).SetUpdate(true);
        StartCoroutine(Fun());
       awardAnim.AnimationState.SetAnimation(0, "lingquhou", false).Complete+=(s)=>awardAnim.AnimationState.SetAnimation(0,"daiji",true);
    }
    IEnumerator Fun()
    {
        while (true)
        {
            awardCount.text = string.Format("{0}", count);
            yield return null;
            if (count >= PlayerData.Instance.moneyReward)
            {
                awardCount.text = string.Format("{0}", PlayerData.Instance.moneyReward);
                break;
            }
        }
    }
    private void Start()
    {
        RefreshText();
        PlayerData.Instance.awardAction += RefreshAnimationText;
       // GuideManager.Instance.achieveGuideAction += RecorveGuideStatus;
        if (GuideManager.Instance.isFirstGame)
        {
            awardGo.SetActive(false);
        }
        UnityActionManager.Instance.AddAction("award",RefreshText);
    }
    public void OpenSjUI()
    {
        print("打开赏金提现面板");
        AndroidAdsDialog.Instance.OpenTiXianUI(true);
    }
    public void RecorveGuideStatus()
    {
        awardGo.SetActive(true);
    }
}
