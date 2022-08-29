using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WangDianDaRenUI : MonoBehaviour
{
    // Start is called before the first frame update
    // public static WangDianDaRenUI Instance;
    public Animator animator;
    public Text top, botton;
    public Button button;
    private void Start()
    {
        button.onClick.AddListener(ClickEvent);
        JavaCallUnity.Instance.SetMemberDialogText("����4��+����5��");
    }
    void ClickEvent()
    {
        AndroidAdsDialog.Instance.ShowMemberDialog();
        AndroidAdsDialog.Instance.UploadDataEvent("click_daren_mission");
    }
    public void PlayAnim(bool value)
    {
        animator.SetBool("walk", value);

    }
    public void SetText(string value,string value1)
    {
        top.text = value;
        botton.text = value1;
    }
}
