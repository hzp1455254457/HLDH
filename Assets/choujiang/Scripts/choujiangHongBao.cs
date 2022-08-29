using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class choujiangHongBao : PanelAnimation
{
    public Button yesButton, noButton;
    public Transform rewardParent;
    public GameObject rewardObject1, rewardObject2, rewardObject3;
    public Image doubleImage;

    public Sprite normalSprite, doubleSprite;

    public RectTransform pivot;
    // Start is called before the first frame update
    private Vector2 height;
    private void Awake()
    {
        height = FindObjectOfType<ChouJiangSceneManager>()._uiCanvas.worldCamera.WorldToScreenPoint(pivot.transform.position);
        Debug.Log("height:" + height);
    }
    public void InitHongBao(int number1,float number2,int number3,bool isDouble,Canvas uiCanvas,Action yesAction,Action noAction = null)
    {
        //height = FindObjectOfType<ChouJiangSceneManager>()._uiCanvas.worldCamera.WorldToScreenPoint(pivot.transform.position);
        //Debug.Log("height:" + height);
        base.Animation();
        //AndroidAdsDialog.Instance.ShowFeedAd((int)height.y);
        if (number1 != 0)
        {
            GameObject obj = Instantiate(rewardObject1, rewardParent);
            obj.GetComponentInChildren<Text>().text = "+" + number1;
        }

        if (number2 != 0.0f)
        {
            GameObject obj = Instantiate(rewardObject2, rewardParent);
            obj.GetComponentInChildren<Text>().text = "+" + number2.ToString("F2")+"元";
        }

        if (number3 != 0)
        {
            GameObject obj = Instantiate(rewardObject3, rewardParent);
            obj.GetComponentInChildren<Text>().text = "+" + number3;
        }

        if (number1 == 0 && number2 == 0.0f && number3 == 0)
        {
            GameObject obj = Instantiate(rewardObject1, rewardParent);
            obj.GetComponentInChildren<Text>().text = "+" + 100;
        }

        doubleImage.sprite = isDouble ? doubleSprite : normalSprite;

        yesButton.onClick.AddListener(() =>
        {
            yesAction?.Invoke();
            //AndroidAdsDialog.Instance.CloseFeedAd();
            //Destroy(gameObject);
        });

        noButton.onClick.AddListener(() =>
        {
            noAction?.Invoke();
            //AndroidAdsDialog.Instance.CloseFeedAd();
            Destroy(gameObject);
        });

        Invoke("showNoButton", 1.0f);
    }

    void showNoButton()
    {
        noButton.gameObject.SetActive(true);
    }
}
