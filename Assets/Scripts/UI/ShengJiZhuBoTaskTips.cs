using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShengJiZhuBoTaskTips : PanelAnimation
{
    public static ShengJiZhuBoTaskTips Instance;
    public Text name, level, creactCount,count,taskAward;
    public Image xiaohaoImg;
    public ZhiBoJian zhiBoJian;
    public RectTransform[] buttonGos;//0是免费1是花费
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ShowUI(ZhiBoJian zhiBoJian,int count, int redcount)
    {
        transform.SetAsLastSibling();

        gameObject.SetActive(true);
        Animation();
        taskAward.text = string.Format("{0}+大量经验", redcount);
        this. zhiBoJian = zhiBoJian;
        this.count.text =string.Format("还差{0}级",count) ;
        name.text = zhiBoJian.actorDate.actor_name;
        level.text =string.Format("{0}级", zhiBoJian.actorDate.actor_level) ;
        if (zhiBoJian._skill.actorlevel_cost == 1)
        {
            //buttonGos[0].gameObject.SetActive(true);
          
            buttonGos[1].gameObject.SetActive(false);
            buttonGos[0].anchoredPosition = new Vector2(0, -286);
        }
        else
        {
            buttonGos[1].gameObject.SetActive(true);
            buttonGos[0].anchoredPosition = new Vector2(-134, -286);
            creactCount.text = zhiBoJian._skill.actorlevel_cost_num.ToString();
            if (zhiBoJian._skill.actorlevel_cost == 2) 
                xiaohaoImg.sprite = ResourceManager.Instance.GetSprite("钻石");
            else
            {
                xiaohaoImg.sprite = ResourceManager.Instance.GetSprite("金币");
            }
        }
    
        //creactCount.text = zhiBoJian._skill.actorlevel_cost_num.ToString();
    }
    public void ShengJi()
    {
        zhiBoJian.ShengJi(() => gameObject.SetActive(false));
        //gameObject.SetActive(false);
    }
    public void FreeShengJi()
    {
        //zhiBoJian._skill.actorlevel_cost = 1;
        //  zhiBoJian.ShenJiEvent(zhiBoJian.index); 
        AndroidAdsDialog.Instance.ShowRewardVideo(zhiBoJian.index.ToString());
        gameObject.SetActive(false);
#if UNITY_EDITOR

        zhiBoJian.ShenJiEvent(zhiBoJian.index); 

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
