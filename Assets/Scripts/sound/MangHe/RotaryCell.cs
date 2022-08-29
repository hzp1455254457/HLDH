using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotaryCell : MonoBehaviour
{
    public Transform[] turnEff;
    public Transform[] seletEff;
    public int type;
    public int num;
   public Text text;
    public int index;
    public Sprite[] sprites;
    public Image image;
    //public Text text;
    public enum EffType
    {
        turn,
        select,
        all,
    }
    private void Awake()
    {
        sprites = PaoMaDengPanel.Instance.sprites;
    }
    public void ShowEff(EffType efftype, bool isShow)
    {

        switch (efftype)
        {
            case EffType.turn:
                for (int i = 0; i < turnEff.Length; i++)
                {

                    turnEff[i].gameObject.SetActive(isShow);
                }
                break;
            case EffType.select:
             
                for (int i = 0; i < turnEff.Length; i++)
                {

                    seletEff[i].gameObject.SetActive(isShow);
                }
                SelectEvent();
                break;
            case EffType.all:
                for (int i = 0; i < turnEff.Length; i++)
                {
                    turnEff[i].gameObject.SetActive(isShow);
                    seletEff[i].gameObject.SetActive(isShow);
                    //text.color = new Color32(254, 95, 180, 255);
                }
                break;
            default:
                break;
        }



    }
    int countClick = 0;

    public void SetType(int type, int count, int index, bool IsCanSelect = true)
    {
        this.type = type;
        num = count;
        if (type != 5)
        { if (type == 1 || type == 2)
            {
                text.text = count.ToString() + "个";
            }
            else
            {
                if (type == 3)
                {
                    text.text = "小额红包";
                }
                else
                {
                    text.text = "普通红包";
                }
            }
            image.sprite = sprites[type-1];
            image.SetNativeSize();
        }
       this. index = index;
       
            for (int i = 0; i < turnEff.Length; i++)
            {

                seletEff[i].gameObject.SetActive(!IsCanSelect);
            }
       
    }
    private void SelectEvent()
    {
        if (type == 1)
        {
            RedUIManager.ShowGoldAndDaimond(num, type-1, () => { PlayerData.Instance.GetGold(num);
                AndroidAdsDialog.Instance.UploadDataEvent("click_small_kaixinshouxia");
                PaoMaDengPanel.Instance.HideUi();
                TipsShowBase.Instance.Show("TipsShow4", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
       {
            string.Format("+{0}",num)
           }, new Sprite[]
           {
              ResourceManager.Instance.GetSprite( "金币")
           }, null,

           () => { PlayerData.Instance.AddWangDianGold(num);  });
            
            });
            
        }
        else if (type == 2)
        {
           
            
            RedUIManager.ShowGoldAndDaimond(num, type-1, () => { PlayerData.Instance.GetDiamond(num);
                PaoMaDengPanel.Instance.DaiMondCount += num;
                AndroidAdsDialog.Instance.UploadDataEvent("click_small_kaixinshouxia");
                PaoMaDengPanel.Instance.HideUi();
                TipsShowBase.Instance.Show("TipsShow4", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
   {
            string.Format("+{0}",num)
       }, new Sprite[]
       {
              ResourceManager.Instance.GetSprite( "钻石")
       }, null,

     null);
  });
        }
        else if (type == 3)
        {
            RedUIManager.ShowRed(num, 0, () => { PlayerData.Instance.GetRed((int)(num * MoneyManager.redProportion));
                AndroidAdsDialog.Instance.UploadDataEvent("click_small_kaixinshouxia");
                PaoMaDengPanel.Instance.HideUi();
                TipsShowBase.Instance.Show("TipsShow4", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
{
            string.Format("+{0}元",num)
}, new Sprite[]
{
              ResourceManager.Instance.GetSprite( "红包")
}, null,

null);
            });
        }
        else if (type == 4)
        {
          
            RedUIManager.ShowRed1(()=>RedUIManager.ShowRed(num, 0, () => {
                PaoMaDengPanel.Instance.HideUi();
                PlayerData.Instance.GetRed((int)(num * MoneyManager.redProportion));
                PaoMaDengPanel.Instance.RedCount += num;
                AndroidAdsDialog.Instance.UploadDataEvent("finish_normal_redpacket");
                TipsShowBase.Instance.Show("TipsShow4", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
{
            string.Format("+{0}元",num)
}, new Sprite[]
{
              ResourceManager.Instance.GetSprite( "红包")
}, null,

null);
            }));
        }
    }

    public void HideAllEff()
    {
        for (int i = 0; i < turnEff.Length; i++)
        {

            turnEff[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < turnEff.Length; i++)
        {

            seletEff[i].gameObject.SetActive(false);
        }
    }

  

    IEnumerator HideEffAni()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < turnEff.Length; i++)
        {

            turnEff[i].gameObject.SetActive(false);
        }
    }
}
