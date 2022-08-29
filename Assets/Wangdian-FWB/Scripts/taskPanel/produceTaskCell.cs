using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class produceTaskCell : MonoBehaviour
{
    public Image itemImage;
    public Text itemNameText;
    public Image progressImage;
    public Text progressText;
    public GameObject fingerAnimationObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitProduceCell(int produceID,int allNum,int currentNum)
    {
        Produce p = ConfigManager.Instance.GetProduce(produceID);
        itemImage.sprite = ResourceManager.Instance.GetSprite(p.item_pic);
        itemNameText.text = p.item_name;
        if(progressImage!=null)
        progressImage.fillAmount = currentNum / (float)allNum;
        progressText.text = currentNum>=allNum?allNum +"/"+ allNum:currentNum+"/"+allNum;
    }
}
