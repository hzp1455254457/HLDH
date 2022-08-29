using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TuiXiaoIcon : MonoBehaviour
{
    public RectTransform tuiXiaoIconRectTransform;
    bool isShow = false;
    float taskVec;
    public Image image;
    public Animator animator;
    float targetVec;
    int count;
    public Text text;
    private void Awake()
    {
        taskVec = tuiXiaoIconRectTransform.localPosition.x;
        animator.SetBool("walk", false);
        targetVec = (taskVec + tuiXiaoIconRectTransform.rect.width + 50);
        tuiXiaoIconRectTransform.localPosition = new Vector2(targetVec, tuiXiaoIconRectTransform.localPosition.y);
    }
    public void Show(int count)
    {if (!gameObject.activeInHierarchy) return;
        if (!isShow)
        {
            AndroidAdsDialog.Instance.UploadDataEvent("show_tuixiao_reward");
            this.count = count;
            isShow = true;
            text.text = Random.Range(30f, 60f).ToString("f3") + "元";
            //text.text = "+" + (count / MoneyManager.redProportion).ToString("f2") + "元";
            animator.SetBool("walk", true);
            image.fillAmount = 1;
           tuiXiaoIconRectTransform.localPosition = new Vector2(targetVec, tuiXiaoIconRectTransform.localPosition.y);
            tuiXiaoIconRectTransform.DOLocalMoveX(taskVec, 1f).onComplete += () =>
            {
                image.DOFillAmount(0, 20f).onComplete += () =>
                {
                    tuiXiaoIconRectTransform.DOLocalMoveX(targetVec, 0.8f).onComplete+=()=> {
                        isShow = false;
                    };
                    animator.SetBool("walk", false);
                };
            };
        }
    }
    public void ClickFun()
    {
        AndroidAdsDialog.Instance.ShowRewardVideo("TuiXiaoIcon", Fun);
        AndroidAdsDialog.Instance.UploadDataEvent("click_tuixiao_reward");
    }

    private void Fun()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("finish_tuixiao_reward");

        hongbao6.Instance.ShowUI(NumberGenenater.GetRedCount(), 0, 1, () => {


        }, "推销奖励");
        tuiXiaoIconRectTransform.DOLocalMoveX(taskVec + tuiXiaoIconRectTransform.rect.width + 50, 0.4f).onComplete += () => { isShow = false; };
        animator.SetBool("walk", false);
    }
}
