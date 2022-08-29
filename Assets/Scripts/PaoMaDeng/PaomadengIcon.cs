using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PaomadengIcon : MonoBehaviour
{
    public Text text;
    public Image image;
    //public Sprite[] sprites;
  public void ClickFun()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("click_choujiang_scene");
        PaoMaDengPanel.Instance.ShowUI();
    }
    private void Update()
    {
        RefreshTime();
    }

    private void RefreshTime()
    {
        if (PaoMaDengPanel.Instance.IsTime)
        {
            if (PaoMaDengPanel.Instance.dataTime > DateTime.Now)
            {
                DateTime dataTime = PaoMaDengPanel.Instance.dataTime;
                TimeSpan date = dataTime - DateTime.Now;
                text.text = "<color=red>" + (PaoMaDengPanel.Instance.GetTimeString(date)) + "后</color>可抽奖";
               // image.sprite = sprites[0];
                // image.color = Color.black;
            }
            else
            {
                text.text = "抽巨额现金红包";
              //  image.sprite = sprites[1];
            }
        }
        else
        {
            // image.color = Color.white;
            text.text = "抽巨额现金红包";
            //image.sprite = sprites[1];
        }
    }

    private void Start()
    {
        RefreshTime();


    }
}
