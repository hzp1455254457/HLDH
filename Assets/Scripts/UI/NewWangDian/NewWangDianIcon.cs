using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewWangDianIcon : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public Text level;
    public NumberEffect numberEffect;
    public int initcount;
    public Transform qipaoTf;
    public GameObject guideGo;
    bool isClick;
    public void ClickFun()
    {
        NewWangDianPanel.Instance.ShowUI(UIManager.Instance.showRootMain);
        ShowGuide(false);
        if (!isClick)
        {
            isClick = true;
            AndroidAdsDialog.Instance.UploadDataEvent("click_wdsj_user");
            PlayerPrefs.SetString("NewWangDianIcon_isClick", "NewWangDianIcon_isClick");
        }
    }
    void Start()
    {
       // PlayerDate.Instance.storeData.level = AndroidAdsDialog.Instance.GetRedLevel();
        UnityActionManager.Instance.AddAction<int>("WangDianIcon", SetTextAnimation);
        UnityActionManager.Instance.AddAction("WangDianIcon1", SetText);
        Refresh();
        isClick = PlayerPrefs.HasKey("NewWangDianIcon_isClick");
    }
    public void ShowGuide(bool value)
    {
        guideGo.SetActive(value);
       
    }
    private void Refresh()
    {
        for (int i = 0; i < PlayerData.Instance.NewWangDianDatas.Count; i++)
        {
            if (PlayerData.Instance.NewWangDianDatas[i].status != 2)
            {
                SetText(PlayerData.Instance.NewWangDianDatas[i].gold * 100 / PlayerData.Instance.NewWangDianDatas[i].myshop_needgold);
                initcount = (int)((PlayerData.Instance.NewWangDianDatas[i].gold / (float)PlayerData.Instance.NewWangDianDatas[i].myshop_needgold) * 100);
                return;
            }
        }
    }

    public void SetText(int value)
    {
        initcount = value;
        text.text=( value).ToString("f0") + "%";
        level.text = PlayerData.Instance.storeData.level.ToString();
    }
    public void SetText()
    {
        Refresh();
    }
    public void SetTextAnimation(int target)
    {
        print("initcount" + initcount);
        qipaoTf.DOScale(1.3f, 0.5f).onComplete = () => numberEffect.Animation(target, "", "%",1, initcount, ()=> { qipaoTf.DOScale(1f, 0.5f); initcount = target; });
      
    }
   
    // Update is called once per frame
 
}
