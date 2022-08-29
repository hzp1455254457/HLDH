using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChouJiangTiXian : MonoBehaviour
{
    public GameObject btnGo;
    public Text text, text1;
    public Image image;
    public Button button;
    public Slider slider;
    int count;
    public Sprite[] sprites;

  
   
    void Start()
    {
        button.onClick.AddListener(ClickEvent);
        AndroidAdsDialog.Instance.RequestQueryDrawCount();
    //JavaCallUnity.Instance.SetChouJiangCount("31+10");
    }
    public void SetCount(int count)
    {
        if (!btnGo.activeSelf)
            btnGo.SetActive(true);
        if (!slider.gameObject.activeSelf)
            slider.gameObject.SetActive(true);
      this.  count = count;
        if (count >= 30)
        {
            count = 30;
        }
        text1.text = (30 - count).ToString();
        text.text = count.ToString();
        slider.DOValue(count / 30f,0.1f);
        
        if (count >= 30)
        {
         
            SetStatus(true);
        }
        else
        {
            SetStatus(false);
        }
    }
   void SetStatus(bool value)
    {
        button.interactable = value;
        if (value)
        {
            image.sprite = sprites[0];
        }
        else
        {
            image.sprite = sprites[1];
        }
    }
    public void ClickEvent()
    {
        SetCount(0);
        AndroidAdsDialog.Instance.RequestDrawWithDraw();
    }
    public void AddChouJiangCount()
    {
        SetCount(++count);
    }
}
