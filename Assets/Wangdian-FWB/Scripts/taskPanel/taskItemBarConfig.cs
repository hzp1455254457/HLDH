using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskItemBarConfig : MonoBehaviour
{
    public Text itemNameText;
    public Image itemImage;
    public Image finishImage;
    public Text progressText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// 初始化taskItemBar
    /// </summary>
    /// <param name="p">产品信息</param>
    /// <param name="progressString">进度text</param>
    /// <param name="finish">是否完成该Item</param>
    public void initTaskItemBar(Produce p,string progressString,bool finish = true)
    {
        itemNameText.text = p.item_name;
        itemImage.sprite = ResourceManager.Instance.GetSprite(p.item_pic);
        finishImage.gameObject.SetActive(finish);

        progressText.text = progressString;
        progressText.gameObject.SetActive(!finish);
    }
}
