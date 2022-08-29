using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZhuBoItem : MonoBehaviour
{
    public GameObject shengJiGo;
    public Text nameText, levelText, shouYiText,produceText;
    public Slider slider;
    public ZhiBoJian zhiBoJian;
    public Image image;
    public Image zhuboImage;
    public GameObject tips;
    // Update is called once per frame
    public void SetZhiBoJian(ZhiBoJian zhiBoJian)
    {
        shengJiGo.SetActive(false);
        this.zhiBoJian = zhiBoJian;
        nameText.text = string.Format("{0}¼¶{1}", zhiBoJian.actorDate.actor_level, zhiBoJian.actorDate.actor_name);
        levelText.text = string.Format("{0}Â¥", zhiBoJian.actorDate.actor_louceng);
        Produce produce = ConfigManager.Instance.GetProduce(zhiBoJian.actorDate.item_id);
        if (zhiBoJian.actorDate.item_id == 0)
        { image.gameObject.SetActive(false);
            produceText.text = "";
            shouYiText.text = "0";
            tips.SetActive(true);
        }
        else
        {
            tips.SetActive(false);
            shouYiText.text = ((int)(zhiBoJian.sellSuDu * produce.item_profit)).ToString();
          
            produceText.text = zhiBoJian.produceName.text;
            image.gameObject.SetActive(true);
            image.sprite = ResourceManager.Instance.GetSprite(produce.item_pic);
            if (zhiBoJian._skill.actorlevel_cost == 1)
            {
                shengJiGo.SetActive(true);
            }
            else if (zhiBoJian._skill.actorlevel_cost == 2&&PlayerData.Instance.diamond>=zhiBoJian._skill.actorlevel_cost_num)
            {
                shengJiGo.SetActive(true);
            }
        }
        if (zhiBoJian.actorDate.actor_sex == 1)
        {
            zhuboImage.sprite= Panel_ZhuBoList.Instance.zhuboSprits[0];
        }
        else
        {
            zhuboImage.sprite = Panel_ZhuBoList.Instance.zhuboSprits[1];
        }
    }
    public void ClickFun()
    {if (zhiBoJian != null)
        {
            ZhiBoPanel.Instance.MoveZhuBo(zhiBoJian.index);
            Panel_ZhuBoList.Instance.HideUI();
            //if (zhiBoJian._skill.actorlevel_cost == 1)
            //{
            //    zhiBoJian.ShengJi();
            //}
        }
    }
    void Update()
    {
        if(zhiBoJian!=null&&zhiBoJian.slider!=null)
        slider.value = zhiBoJian.slider.value;
    }
}
