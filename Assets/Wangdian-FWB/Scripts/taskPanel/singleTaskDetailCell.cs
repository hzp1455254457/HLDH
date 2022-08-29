using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class singleTaskDetailCell : MonoBehaviour
{
    public Image itemImage;
    public Text itemNameText;
    public Image progressImage;
    public Text progressText;
    public Button changeButton;

    public Sprite notFinishSprite, finishSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitSingleTaskDetailCell(int produceID, int allNum,int currentNum)
    {
        Produce p = ConfigManager.Instance.GetProduce(produceID);
        itemImage.sprite = ResourceManager.Instance.GetSprite(p.item_pic);
        itemNameText.text = p.item_name;
        progressImage.fillAmount = currentNum / (float)allNum;
        progressText.text = currentNum>=allNum?allNum+"/" + allNum:currentNum+"/"+allNum;

        changeButton.GetComponent<Image>().sprite = currentNum < allNum?notFinishSprite:finishSprite;

        if (currentNum < allNum)
        {
            changeButton.onClick.AddListener(() =>
            {
                shopItemCellConfig[] ss = FindObjectsOfType<shopItemCellConfig>();
                shopItemCellConfig freeShopItemCellConfig = null;
                bool isFree = false;
                for (int i = 0; i < ss.Length; i++)
                {
                    if (ss[i].status == shop_cell_status.cellReady)
                    {
                        isFree = true;
                        freeShopItemCellConfig = ss[i];
                        break;
                    }
                }

                if (isFree)
                {
                    freeShopItemCellConfig.fingerAnimation.SetActive(true);
                    freeShopItemCellConfig.preCellID = produceID;
                    Destroy(FindObjectOfType<singleTaskDetailPanelConfig>().gameObject);
                    Destroy(FindObjectOfType<taskDetailPanelConfig>().gameObject);
                    AndroidAdsDialog.Instance.CloseFeedAd();
                }
                else
                {
                    tipsManager.Instance.openHuoJiaDialog(()=>
                    {
                        AndroidAdsDialog.Instance.ShowRewardVideo("点击加速播放激励视频", () =>
                        {
                            AndroidAdsDialog.Instance.UploadDataEvent("finish_huojiabuzu");
                            shopItemCellConfig[] cells = FindObjectsOfType<shopItemCellConfig>();
                            foreach (shopItemCellConfig one in cells)
                            {
                                one.jiasu();
                            }
                            Destroy(FindObjectOfType<singleTaskDetailPanelConfig>().gameObject);
                            Destroy(FindObjectOfType<taskDetailPanelConfig>().gameObject);
                        });
                    });
                }
            });
        }
        else
        {
            changeButton.onClick.AddListener(() =>
            {
                Debug.Log("已完成");
                tipsManager.Instance.createFinishTips("已完成!");
            });
        }
    }
}
