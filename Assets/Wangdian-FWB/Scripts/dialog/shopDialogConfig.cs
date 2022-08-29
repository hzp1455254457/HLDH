using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class shopDialogConfig : PanelAnimation
{
    public Button closeButton;

    public RectTransform contentRectTransform;

    public RectTransform notForSaleTransform;

    public GameObject forSaleCellPrefab;

    public RectTransform contentRectTransformForHeight;

    private List<Produce> forSaleList = new List<Produce>();

    private shopItemCellConfig config;

    public GameObject fingerAnimation;

    public List<GameObject> shopCells = new List<GameObject>();
    private void Start()
    {
        closeButton.onClick.AddListener(() => Destroy(gameObject));
    }

    /*public void OnEnable()
    {
        ConfigManager.Instance.Init();
        ResourceManager.Instance.Init();

        forSaleList = ConfigManager.Instance.GetProduces(5);

        for (int i = 0; i < forSaleList.Count; i++)
        {
            GameObject obj = Instantiate(forSaleCellPrefab, contentRectTransform);
            obj.GetComponent<shopItemCell>().InitShopCell(config,forSaleList[i]);
        }

    }*/

    void changeSize()
    {
        contentRectTransformForHeight.sizeDelta = new Vector2(0, contentRectTransform.sizeDelta.y); 
    }

    public void InitShopItemCellConfig(shopItemCellConfig config,bool showFinger = false,int preCellitemID = 0)
    {
        this.config = config;
        forSaleList = userData.Instance.shopProduceList;

        for (int i = 0; i < forSaleList.Count; i++)
        {
            GameObject obj = Instantiate(forSaleCellPrefab, contentRectTransform);
            obj.GetComponent<shopItemCell>().InitShopCell(this.config, forSaleList[i]);
            if (showFinger)
            {
                if (forSaleList[i].item_id == preCellitemID)
                {
                   obj.GetComponent<shopItemCell>().fingerObject.SetActive(true);
                }
            }
            shopCells.Add(obj);
        }


        if (userData.Instance.isInJiaoCheng)
        {
            FindObjectOfType<wangdianjiaocheng>().GetComponent<GuideMask>().inner_trans = null;
            FindObjectOfType<wangdianjiaocheng>().fingerAnimation.SetActive(false);
        }
        base.Animation(()=>
        {
            if (userData.Instance.isInJiaoCheng)
            {
                //FindObjectOfType<wangdianjiaocheng>().NextStep();
                FindObjectOfType<wangdianjiaocheng>().SetStep(2);
                FindObjectOfType<wangdianjiaocheng>().fingerAnimation.transform.position = FindObjectOfType<shopItemCell>().fingerObject.transform.position;
            }
        });
    }

}
