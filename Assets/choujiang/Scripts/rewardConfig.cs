using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rewardConfig : MonoBehaviour
{
    public Sprite[] spriteList;
    public Image rewardTypeImage;
    public Text text;
    

    public void InitRewardPanel(int type,string content)
    {
        rewardTypeImage.sprite = spriteList[type - 1];

        text.text = "+" + content;
    }
}
