using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoxMainPannel : BoxPannel
{
    public GameObject ZhongJiangGaiLv;

    public GameObject BoxObj;

    public Image BloodBar;

    public GameObject HongBaoUI;

    public GameObject UIParticle;

    void Start()
    {
        //根据盒子显示中奖概率
        //2s后隐藏中奖概率
        //Invoke("HideZhongJiangGaiLv", 3f);
    }
    public override void OnEnable()
    {
        base.OnEnable();
        BoxObj.SetActive(true);
        HP = 100;
        BloodBar.fillAmount = 1;
        BloodBar.gameObject.SetActive(true);
        BloodBar.transform.parent.gameObject.SetActive(true);
    }
    public void HideZhongJiangGaiLv()
    {
        ZhongJiangGaiLv.SetActive(false);
    }

    private int HP = 100;
    public UnityEvent BoxOpend;

    public void ReduceBlood()
    {
        if (HP > 0)
        {
            HP -= 10;
            Debug.Log("blood down");
        }
        else
        {
            AndroidAdsDialog.Instance.UploadDataEvent("box_empty_chaikuaidi");
        }

        BloodBar.fillAmount = HP / (float)100;

        if (HP<=0)
        {
            BoxObj.SetActive(false);
            UIParticle.SetActive(true);
            BloodBar.gameObject.SetActive(false);
            BloodBar.transform.parent.gameObject.SetActive(false);

            SetUIPositionByWorldPos(UIParticle.GetComponent<RectTransform>(),BoxObj.transform.position);
            StartCoroutine(ShowResult());
        }
        
    }
    
    public void SetUIPositionByWorldPos(RectTransform rectTransform, Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector3 uiWorldPos = Camera.main.ScreenToWorldPoint(screenPos);
        rectTransform.position = screenPos;
    }
    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(1f);
        BoxOpend?.Invoke();
    }

    public void AnimateHongBao()
    {
        HongBaoUI.transform.DOScale(Vector3.one * 1.1f, 0.3f).SetUpdate(true).onComplete
             += () => HongBaoUI.transform.DOScale(Vector3.one, 0.3f).SetUpdate(true);
    }
  
    // Update is called once per frame
    //void Update()
    //{
    //    var NextTime = DateTime.Now.AddMinutes(30);
    //    Debug.Log(NextTime);
    //    if (DateTime.Now> NextTime)
    //    {
    //        Debug.Log("ok");
    //    }
    //}
}
