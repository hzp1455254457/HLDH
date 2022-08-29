using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class taskCell : MonoBehaviour
{
    public Text diamondText, hongbaoText;
    public Button changeButton;
    private List<GameObject> produceList = new List<GameObject>();
    public GameObject produceTaskCellPrefab;
    public RectTransform produceTaskCellParent;
    public GameObject fingerGameObject;

    public void InitTaskDetailCell(taskOrder task)
    {
        diamondText.text = task.reward_xyz.ToString();
        hongbaoText.text = (task.reward_hbq/1000.0f).ToString("F1")+"元";

        Dictionary<string, int> dict = userData.Instance.itemDataDictionary;

        if (task.item1_id != 0)
        {
            GameObject obj = Instantiate(produceTaskCellPrefab, produceTaskCellParent);
            obj.GetComponent<produceTaskCell>().InitProduceCell(task.item1_id, task.item1_num, dict[task.item1_id.ToString()]);
            obj.GetComponent<Button>().onClick.AddListener(()=>
            {
                findFree(task.item1_id,obj);
            });
            produceList.Add(obj);
        }

        if (task.item2_id != 0)
        {
            GameObject obj = Instantiate(produceTaskCellPrefab, produceTaskCellParent);
            obj.GetComponent<produceTaskCell>().InitProduceCell(task.item2_id, task.item2_num, dict[task.item2_id.ToString()]);
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                findFree(task.item2_id, obj);
            });
            produceList.Add(obj);
        }

        if (task.item3_id != 0)
        {
            GameObject obj = Instantiate(produceTaskCellPrefab, produceTaskCellParent);
            obj.GetComponent<produceTaskCell>().InitProduceCell(task.item3_id, task.item3_num, dict[task.item3_id.ToString()]);
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                findFree(task.item3_id, obj);
            });
            produceList.Add(obj);
        }
            changeButton.onClick.RemoveAllListeners();
            changeButton.onClick.AddListener(() =>
            {
                Dictionary<string, int> dict = userData.Instance.itemDataDictionary;
                task = userData.Instance.currentOneOrder;

                if (((task.item1_id != 0) ? (dict[task.item1_id.ToString()] >= task.item1_num) : true) &&
               ((task.item2_id != 0) ? (dict[task.item2_id.ToString()] >= task.item2_num) : true) &&
               ((task.item3_id != 0) ? (dict[task.item3_id.ToString()] >= task.item3_num) : true))
                {
                    changeButton.interactable = false;
                    AudioManager.Instance.PlaySound("version3_1");
                    if (userData.Instance.isInJiaoCheng)
                    {
                        tipsManager.Instance.createSingleOrderFinishPanel();
                        //FindObjectOfType<taskDetailPanelConfig>().InitTaskDetailPanelConfig();
                        //Destroy(FindObjectOfType<taskDetailPanelConfig>().gameObject);
                        //FindObjectOfType<wangdianjiaocheng>().NextStep();
                        FindObjectOfType<wangdianjiaocheng>().SetStep(5);
                    }
                    else
                    {
                        tipsManager.Instance.createSingleOrderFinishPanel();
                    }
                    changeButton.interactable = true;
                }
                else
                {
                    tipsManager.Instance.createPiaoChuang("订单还没完成哦！");
                    produceTaskCell[] t = FindObjectsOfType<produceTaskCell>();
                    foreach (produceTaskCell one in t)
                    {
                        one.fingerAnimationObject.SetActive(true);
                    }
                }
            });
    }

    public GameObject detailPart;
    public void OnDisable()
    {
        for (int i = 0; i < produceList.Count; i++)
        {
            Destroy(produceList[i]);
        }
        produceList.Clear();
        detailPart.SetActive(true);
        if (fingerGameObject.activeSelf)
            fingerGameObject.SetActive(false);

        userData.Instance.isCollectingOrder = false;
    }
    public void OnEnable()
    {
        InitTaskDetailCell(userData.Instance.currentOneOrder);
        if (userData.Instance.isInJiaoCheng)
            fingerGameObject.SetActive(true);
    }

    private Tweener scaleTweener = null;
    public void Start()
    {
        /*
        scaleTweener = changeButton.GetComponent<RectTransform>().DOScale(1.2f, 1.0f);
        scaleTweener.onComplete = () =>
        {
            changeButton.GetComponent<RectTransform>().DOScale(1.0f, 1.0f).SetDelay(0.5f);
        };
        scaleTweener.SetLoops(-1, LoopType.Yoyo);
        */
        //changeButton.GetComponent<RectTransform>().DOScale(1.2f, 1.0f).SetDelay(0.5f).SetLoops(-1, LoopType.Yoyo);
        Sequence queen = DOTween.Sequence();
        queen.Append(changeButton.GetComponent<RectTransform>().DOScale(1.2f, 1.0f));
        queen.AppendInterval(0.5f);
        queen.Append(changeButton.GetComponent<RectTransform>().DOScale(1.0f, 1.0f));
        queen.SetLoops(-1);
    }

    public void refreshOrder()
    {
        for (int i = 0; i < produceList.Count; i++)
        {
            Destroy(produceList[i]);
        }
        produceList.Clear();

        taskOrder task = userData.Instance.currentOneOrder;

        Dictionary<string, int> dict = userData.Instance.itemDataDictionary;

        if (task.item1_id != 0)
        {
            GameObject obj = Instantiate(produceTaskCellPrefab, produceTaskCellParent);
            obj.GetComponent<produceTaskCell>().InitProduceCell(task.item1_id, task.item1_num, dict[task.item1_id.ToString()]);
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                findFree(task.item1_id, obj);
            });
            produceList.Add(obj);
        }

        if (task.item2_id != 0)
        {
            GameObject obj = Instantiate(produceTaskCellPrefab, produceTaskCellParent);
            obj.GetComponent<produceTaskCell>().InitProduceCell(task.item2_id, task.item2_num, dict[task.item2_id.ToString()]);
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                findFree(task.item2_id, obj);
            });
            produceList.Add(obj);
        }

        if (task.item3_id != 0)
        {
            GameObject obj = Instantiate(produceTaskCellPrefab, produceTaskCellParent);
            obj.GetComponent<produceTaskCell>().InitProduceCell(task.item3_id, task.item3_num, dict[task.item3_id.ToString()]);
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                findFree(task.item3_id, obj);
            });
            produceList.Add(obj);
        }
        //InitTaskDetailCell(userData.Instance.currentOneOrder);
    }

    void findFree(int itemID,GameObject obj)
    {
        if (obj.GetComponent<produceTaskCell>().fingerAnimationObject.activeSelf)
        {
            obj.GetComponent<produceTaskCell>().fingerAnimationObject.SetActive(false);
        }

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
            freeShopItemCellConfig.preCellID = itemID;
        }
        else
        {
            tipsManager.Instance.openHuoJiaDialog(() =>
            {
                AndroidAdsDialog.Instance.ShowRewardVideo("点击加速播放激励视频", () =>
                {
                    AndroidAdsDialog.Instance.UploadDataEvent("finish_huojiabuzu");
                    shopItemCellConfig[] cells = FindObjectsOfType<shopItemCellConfig>();
                    foreach (shopItemCellConfig one in cells)
                    {
                        one.jiasu();
                    }
                });
            });
        }
    }
}
