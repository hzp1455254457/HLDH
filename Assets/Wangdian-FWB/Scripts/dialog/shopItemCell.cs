using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopItemCell : MonoBehaviour
{
    public Text itemNameText;
    public Image notForSaleImage,itemImage;
    public Button shopButton;
    public Text itemMoneyText;
    public Text lockText;
    private int goldCost;
    public GameObject fingerObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 出售商品的面板初始化商品cell
    /// </summary>
    /// <param name="config"></param>
    /// <param name="produce"></param>
    /// <param name="forSale"></param>
    public void InitShopCell(shopItemCellConfig config,Produce produce,bool forSale = true)
    {
        if (forSale)
        {
            Debug.Log("id是"+produce.item_id);
            notForSaleImage.gameObject.SetActive(false);
            itemNameText.text = produce.item_name;
            itemImage.sprite = ResourceManager.Instance.GetSprite(produce.item_pic);
            goldCost = (int)(produce.item_cost_num * 1.5f);
            itemMoneyText.text = goldCost.ToString();
            lockText.gameObject.SetActive(false);
            shopButton.onClick.AddListener(() =>
            {
                shopButton.interactable = false;
                //如果在教程中，那么，则不收费，怕金币不足
                if (!userData.Instance.isInJiaoCheng)
                {
                    if (PlayerData.Instance.gold >= goldCost)
                    {
                        PlayerData.Instance.Expend(goldCost);
                        config.refreshCell(produce);

                        Debug.Log("上架成功红包");
                        /*
                        int hongbaoNumber = UnityEngine.Random.Range(40, 60);
                        PlayerData.Instance.GetRed(hongbaoNumber);
                        */
                        Destroy(FindObjectOfType<shopDialogConfig>().gameObject);
                    }
                    else
                    {
                        Debug.Log("金币不足飘窗");
                        tipsManager.Instance.openGoldNotEnoughPanel(() =>
                        {
                            AndroidAdsDialog.Instance.ShowRewardVideo("金币不足激励视频", () =>
                            {
                                AndroidAdsDialog.Instance.UploadDataEvent("finish_jinbi_less_in_newshop");
                                config.refreshCell(produce);
                                /*
                                int hongbaoNumber = UnityEngine.Random.Range(40, 60);
                                PlayerData.Instance.GetRed(hongbaoNumber);
                                */
                                Destroy(FindObjectOfType<shopDialogConfig>().gameObject);
                            });
                        }
                        );
                    }
                }
                else
                {
                    //FindObjectOfType<GuideMask>().inner_trans = null;
                    config.refreshCell(produce);
                    //FindObjectOfType<wangdianjiaocheng>().NextStep();
                    FindObjectOfType<wangdianjiaocheng>().SetStep(3);
                    Destroy(FindObjectOfType<shopDialogConfig>().gameObject);
                }
                shopButton.interactable = true;
            });
        }
        else
        {
            notForSaleImage.gameObject.SetActive(true);
            itemNameText.text = produce.item_name;
            itemImage.gameObject.SetActive(false);
            shopButton.gameObject.SetActive(false);
            lockText.gameObject.SetActive(true);
            lockText.text = produce.produce_level + "楼解锁";
        }
    }
}
