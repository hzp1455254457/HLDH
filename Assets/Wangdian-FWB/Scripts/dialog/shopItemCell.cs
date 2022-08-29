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
    /// ������Ʒ������ʼ����Ʒcell
    /// </summary>
    /// <param name="config"></param>
    /// <param name="produce"></param>
    /// <param name="forSale"></param>
    public void InitShopCell(shopItemCellConfig config,Produce produce,bool forSale = true)
    {
        if (forSale)
        {
            Debug.Log("id��"+produce.item_id);
            notForSaleImage.gameObject.SetActive(false);
            itemNameText.text = produce.item_name;
            itemImage.sprite = ResourceManager.Instance.GetSprite(produce.item_pic);
            goldCost = (int)(produce.item_cost_num * 1.5f);
            itemMoneyText.text = goldCost.ToString();
            lockText.gameObject.SetActive(false);
            shopButton.onClick.AddListener(() =>
            {
                shopButton.interactable = false;
                //����ڽ̳��У���ô�����շѣ��½�Ҳ���
                if (!userData.Instance.isInJiaoCheng)
                {
                    if (PlayerData.Instance.gold >= goldCost)
                    {
                        PlayerData.Instance.Expend(goldCost);
                        config.refreshCell(produce);

                        Debug.Log("�ϼܳɹ����");
                        /*
                        int hongbaoNumber = UnityEngine.Random.Range(40, 60);
                        PlayerData.Instance.GetRed(hongbaoNumber);
                        */
                        Destroy(FindObjectOfType<shopDialogConfig>().gameObject);
                    }
                    else
                    {
                        Debug.Log("��Ҳ���Ʈ��");
                        tipsManager.Instance.openGoldNotEnoughPanel(() =>
                        {
                            AndroidAdsDialog.Instance.ShowRewardVideo("��Ҳ��㼤����Ƶ", () =>
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
            lockText.text = produce.produce_level + "¥����";
        }
    }
}
