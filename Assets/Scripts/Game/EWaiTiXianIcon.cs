using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class EWaiTiXianIcon : MonoBehaviour
{
    //public Text text;
    public Image image;
    public Button button;
  //public  int initCount = 0;
    //public NumberEffect numberEffect;
    Transform parentTf;//UIManager.Instance.showRootMain;
                       // public Transform qipaoTf;
    public GameObject guideGo;

    int GetCount =0;
  
    Tweener tweener;
    void Animation(int value)
    {
        if (tweener != null)
        {
            tweener.Kill();
        }
        tweener= image.DOFillAmount(value/ (float)countMax, 1f);
        if (value >= countMax)
        {
            guideGo.SetActive(true);
        }
    }
    /// <summary>
    /// 设置额外提现面板父物体
    /// </summary>
    /// <param name="Parent"></param>
    public void SetParent(Transform Parent)
    {
        parentTf = Parent;
    }
    int countMax = 20;
    void Start()
    {
        GetCount = PlayerPrefs.GetInt("Ewai_GetCount", 0);
        button.onClick.AddListener(ClickFun);
     
        if (GetCount <= 0)
        {
            countMax = 20;
        }
        else if (GetCount == 1)
        {
            countMax = 30;

        }
        else
        {
            countMax = 40;
        }
        image.fillAmount = PlayerData.Instance.ShengJiCount/(float) countMax;
        // text.text = (PlayerData.Instance.TixianValues[0] * 100).ToString("f0") + "%";
        //  initCount =(int)( PlayerData.Instance.TixianValues[0] * 100);

        //SetParent(UIManager.Instance.showRootMain);
        UnityActionManager.Instance.AddAction<int>("EWaiTiXianIconAnim", Animation);
        //UnityActionManager.Instance.AddAction("ShowEWaiTiXianIcon", ShowUI);
       // gameObject.SetActive(PlayerData.Instance.IsShowEWaiTiXianUI);
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }
  public void ClickFun()
    {
    
        if (GetCount <= 0)
        {
            countMax = 20;
        }
        else if (GetCount == 1)
        {
            countMax = 30;

        }
        else
        {
            countMax = 40;
        }
        // EWaiTiXianPanel.Instance.ShowUI(parentTf);
        if (PlayerData.Instance.ShengJiCount < countMax)
        {
            AndroidAdsDialog.Instance.UploadDataEvent("click_ewaitixian_fail");
            AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
       {
                "成功升级任意主播"+countMax+"次可领","还差"+(countMax-PlayerData.Instance.ShengJiCount)+"次", }, null, new Color[]
           {
                    Color.black,Color.red
           }, null, 0.8f);
        
    }
        else
        {
            GetCount++;
            PlayerPrefs.SetInt("Ewai_GetCount", GetCount);
         int red=   NumberGenenater.GetRedCount1();
            AndroidAdsDialog.Instance.UploadDataEvent("click_ewaitixian_success");
            TipsShowBase.Instance.Show("TipsShow5", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
  {
            "升级奖励",string.Format("+{0}元", red/MoneyManager.redProportion)
      }, new Sprite[]
      {
                ResourceManager.Instance.GetSprite("红包")
      }, null, null);
            PlayerData.Instance.ShengJiCount = 0;

            PlayerData.Instance.GetRed(red);
            Animation(0);
            guideGo.SetActive(false);
        }
       
    }
   
}
