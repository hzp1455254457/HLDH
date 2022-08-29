using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewWangDianItem : MonoBehaviour
{
    public NewWangDianData newWangDianData;

    public Sprite[] sprites;
    public Image image;
    public Text text;
    public Text tips_Text;
    public Slider slider;
    public Text level;
    public Text text_red;
    //public int Index;
   

   public void SetData()
    {
        SetStatus(newWangDianData.status);
        if(newWangDianData.myshop_redpacket_reward!=0)
        text_red.text = newWangDianData.myshop_redpacket_reward + "Ԫ";
        else
        {
            int value = 15;
            text_red.text = value.ToString("f2") + "Ԫ";
        }
        level.text = "����" + newWangDianData.myshop_level.ToString() + "��";
    }

    public void SetStatus(int type)
    {
       // int type = value == true ? 0 : 1;
        image.sprite = sprites[type];
      
    }
    public void RefreshText()
    {
        text.text = string.Format("{0}/{1}", newWangDianData.gold, newWangDianData.myshop_needgold);
        SetStatus(newWangDianData.status);
      
        if (newWangDianData.gold >= newWangDianData.myshop_needgold)
        {
           
            tips_Text.text = string.Format("����{0}��ң���ȡ����", 0);
            slider.value = 1;
        }
        else
        {
            if(newWangDianData.myshop_needgold - newWangDianData.gold>=10000)
            tips_Text.text = string.Format("����{0}���ң���ȡ����", (int)(newWangDianData.myshop_needgold - newWangDianData.gold)/10000);
            else
            {
                tips_Text.text = string.Format("����{0}��ң���ȡ����", newWangDianData.myshop_needgold - newWangDianData.gold);
            }



            slider.value = newWangDianData.gold / (float)newWangDianData.myshop_needgold;
        }
       
    }
    public void ClickFun()
    {
        switch (newWangDianData.status)
        {
            case 0:NewWangDianPanel.Instance.Hide();
                AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, "׬���������������", Color.black, null, null, 1f);
                break;
            case 1:
                print("��ú����");
                //SetPos();
             
                if (newWangDianData.myshop_level < 1)
                {
                    AndroidAdsDialog.Instance.UploadDataEvent("new_wdsjhb_1");
                    RedUIManager.ShowWangDianRed(() =>
                    {
                        PlayerData.Instance.AddStoreLevel(1);
                        newWangDianData.status = 2;
                        PlayerData.Instance.GetRed(newWangDianData.myshop_redpacket_reward * (int)MoneyManager.redProportion);
                       

                       UnityActionManager.Instance.DispatchEvent("WangDianIcon1");


                        NewWangDianPanel.Instance.Hide();
                    }, newWangDianData.myshop_redpacket_reward, "��ϲ���"
                        );
                }
                else
                {
                    AndroidAdsDialog.Instance.UploadDataEvent("show_wdsjhb");
                    RedUIManager.ShowWangDianRed1(() =>
                    {
                        // ResourceManager.Instance.LoadRed()

                        AndroidAdsDialog.Instance.UploadDataEvent("click_wdsjhb");
                        AndroidAdsDialog.Instance.ShowRewardVideo("������", () =>
                        {
                            AndroidAdsDialog.Instance.UploadDataEvent("finish_wdsjhb");
                            RedUIManager.ShowWangDianRed2(() =>
                            {
                                PlayerData.Instance.AddStoreLevel(1);
                                newWangDianData.status = 2;
                                PlayerData.Instance.GetRed(NumberGenenater.GetRedCount());

                                UnityActionManager.Instance.DispatchEvent("WangDianIcon1");


                                NewWangDianPanel.Instance.Hide();
                            }, NumberGenenater.GetRedCount()/ MoneyManager.redProportion, "��ϲ���");

                        });
                    }, newWangDianData.myshop_redpacket_reward, "�������");


                    
                    
                }
                //ToggleManager.Instance.wangDianShengJi.ShowUI(UIManager.Instance.showRootMain, (PlayerData.Instance.storeData.level+ 1).ToString(), (newWangDianData.myshop_redpacket_reward /MoneyManager.redProportion).ToString("f2"), () =>
                //{
                // PlayerData.Instance.AddStoreLevel(1);
                //    newWangDianData. status = 2;
                //    PlayerData.Instance.GetRed(newWangDianData.myshop_redpacket_reward);

                //    UnityActionManager.Instance.DispatchEvent("WangDianIcon1");

                  
                //    NewWangDianPanel.Instance.Hide();
                //}
                //   );;
        

        break;
            case 2:

                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    public void SetPos()
    {
        NewWangDianPanel.Instance.newWangDianItems.Remove(this);
        NewWangDianPanel.Instance.newWangDianItems.Add(this);
        transform.SetAsLastSibling();
    }
   
}
