using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreactZhuBoTaskTips : PanelAnimation
{
    public static CreactZhuBoTaskTips Instance;
    public Text count, creactCount, taskAward;
    public ZhiBoPanel zhiBoPanel;
    public RectTransform[] buttonGos;
    public Image xiaohaoImg;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        zhiBoPanel= UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel;
        gameObject.SetActive(false);
    }
    public void ShowUI(int count,int redcount)
    {
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
        Animation();
        taskAward.text = string.Format("{0}+大量经验",redcount);
        this.count.text = string.Format("还差{0}个", count);
        //if (zhiBoPanel.floorup.floor_cost == 1)
        //{
           
            buttonGos[1].gameObject.SetActive(false);
            buttonGos[0].anchoredPosition = new Vector2(0, -286);
        //}
        //else
        //{
            //buttonGos[1].gameObject.SetActive(true);
            //buttonGos[0].anchoredPosition = new Vector2(-134, -286);
            //creactCount.text = zhiBoPanel.floorup.floor_cost_num.ToString();
            //if (zhiBoPanel.floorup.floor_cost == 2)
            //    xiaohaoImg.sprite = ResourceManager.Instance.GetSprite("钻石");
            //else
            //{
            //    xiaohaoImg.sprite = ResourceManager.Instance.GetSprite("金币");
            //}
        //}
    }
    public void CreactZhuBo()
    {
        zhiBoPanel.CreactZhiBoJian(()=>gameObject.SetActive(false));
        //gameObject.SetActive(false);
    }
    public void FreeCreactZhuBo()
    {
        AndroidAdsDialog.Instance.ShowRewardVideo(AndroidAdsDialog.TAG_ADDZHUBO_WANGDIAN);
        AndroidAdsDialog.Instance.UploadDataEvent("click_myshop_mission_get_video");
        gameObject.SetActive(false);
#if UNITY_EDITOR

        zhiBoPanel.PlayVideoShenJiEvent();

#elif UNITY_ANDROID
 
      
        
#endif
    }
    public void CloseUI()
    {
        gameObject.SetActive(false);
        ShopTaskManager.Instance.ShowUI();
       // AndroidAdsDialog.Instance.ShowTableVideo("0");
    }
}
