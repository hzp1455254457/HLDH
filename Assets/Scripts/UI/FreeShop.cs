using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeShop : MonoBehaviour
{
    public Image shopImg;
    public Text prodceName, profit, countText;
   public int count, id;
    private void Start()
    {
        //SetFreeShop();
        //ShowGetFreeProduce(22, 555);
        //SetFreeShop(1, 50);
    }

    public void SetFreeShop(int id,int count)
    {
        var freeProduce = ConfigManager.Instance.GetProduce(id);
      shopImg.sprite =ResourceManager.Instance.GetSprite( freeProduce.item_pic);
       prodceName.text = freeProduce.item_name;
       //shopImg.sprite = ResourceManager.Instance.GetSprite(freeProduce.item_pic);
     profit.text = string.Format("����Ϊ{0}Ԫ/��", freeProduce.item_profit);
     countText.text = string.Format("{0}��", count*1000);
        print(string.Format("���õ��������Ʒ,id={0},count={1}", id, count));
    }
    public void ShowGetFreeProduce(int id,int count)
    {
       // var freeProduce = ConfigManager.Instance.GetProduce(id);
        
        this.id = id;
        this.count = count;
        print(string.Format("��ȡ���������Ʒ,id={0},count={1}", id, count));
       // PlayerDate.Instance.AddFreeProduce(this);
    }
}
