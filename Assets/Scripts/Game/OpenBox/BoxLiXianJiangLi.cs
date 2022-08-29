using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoxLiXianJiangLi : BoxPannel
{
    public Image BoxImage;
    public Text BoxText;

    [Header("普通、中级、高级快递")]
    public Sprite[] BoxSprites;


    //如果未解锁中级，就显示中级和高级 ，否则，只显示高级
    public GameObject NextBoxes, NextBox;

    public UnityEvent CloseEvent;

    public void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("OpenBox"));
        AndroidAdsDialog.Instance.UploadDataEvent("enter_chaikuaid");

    }
    public override void OnEnable()
    {
        ShowCurrent();
        ShowNext();
        base.OnEnable();
        Invoke("HidePannel", 3.5f);
    }

    void HidePannel()
    {
        CloseEvent.Invoke();
        //this.gameObject.SetActive(false);
    }
    void ShowCurrent()
    {
        int boxType = PlayerPrefs.GetInt(BoxGame.NextBoxType, 1);
        switch (boxType)
        {
            case 1:
                BoxImage.sprite = BoxSprites[0];
                BoxText.text = "普通快递";
                break;
            case 2:
                BoxImage.sprite = BoxSprites[1];
                BoxText.text = "中级快递";
                break;
            case 3:
                BoxImage.sprite = BoxSprites[2];
                BoxText.text = "高级快递";
                break;
            default:
                break;
        }
    }
    void ShowNext()
    {
        int boxType = PlayerPrefs.GetInt(BoxGame.NextBoxType, 1);
        if (boxType == 1)
        {
            NextBoxes.SetActive(true);
        }
        else
        {
            NextBox.SetActive(true);
        }
    }
}
