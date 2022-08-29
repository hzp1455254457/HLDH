using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetFreeDaimond : MonoBehaviour
{
    public bool isGetDaimond = false;
    public Text infoText, countText;
    public Button getButton;
    public Sign_up sign_Up;
    public GameObject tips;
 
    private void Awake()
    {
       // GetisGetDaimond();
    }
    private void Start()
    {
       sign_Up = PlayerData.Instance.GetSign_Up(PlayerData.Instance.day);
        GetSign_UP();
        getButton.onClick.AddListener(GetDaimond);
    }

    private void GetSign_UP()
    {
       
        //sign_Up = PlayerDate.Instance.GetSign_Up(PlayerDate.Instance.day);
        if (sign_Up == null)
        {
            gameObject.SetActive(false);
            return;
        }
        if (sign_Up.sign_state ==1)
        {
            if (sign_Up.sign_show_num != 0)
            {
                ShowStates(true);
              
            }
            else
            {
                gameObject.SetActive(false);
                getButton.interactable = false;
            }

        }
        else
        {
            if (sign_Up.sign_before_getnum != 0)
            {
                ShowStates(false);
                
            }
            else
            {
                ShowStates(true);
                sign_Up.sign_state = 1;
            }
        }
    }
    private void ShowStates(bool isGet)
    {
        tips.SetActive(!isGet);
        if (isGet)
        {
            infoText.color = new Color32(72, 32, 34, 255);
            countText.color = new Color32(72, 32, 34, 255);
            infoText.text = "明日登录送钻石";
            countText.text = sign_Up.sign_show_num.ToString();
        }
        else
        {
            infoText.text = "点击送钻石";
            infoText.color = Color.white;
            countText.color = Color.white;
            countText.text = sign_Up.sign_before_getnum.ToString();
            //tips.SetActive(isGet);
        }
    }
    public void GetDaimond()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("click_myshop_lingqu_btn_new");
        if (sign_Up.sign_state == 1)
        {
            AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, "明天才可以领取哦!", Color.black,null,null,1.5f);
        }
        else
        {
            AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, "领取成功", Color.black, null, null, 1.5f);
            sign_Up.sign_state = 1;
            PlayerData.Instance.GetDiamond(sign_Up.sign_before_getnum);
            GetSign_UP();
           
        }
    }
    public void JavaCallUnityEvent()
    {
        gameObject.SetActive(true);
        sign_Up = PlayerData.Instance.GetSign_Up(PlayerData.Instance.day);
        GetSign_UP();

    }
}
