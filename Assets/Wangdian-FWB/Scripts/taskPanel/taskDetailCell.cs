using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskDetailCell : MonoBehaviour
{
    public Text diamondText, hongbaoText;
    public Button changeButton;
    public Sprite duihuanSprite, lingjiangSprite;
    private List<GameObject> produceList = new List<GameObject>();
    public GameObject produceTaskCellPrefab;
    public RectTransform produceTaskCellParent;
    public GameObject fingerGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void InitTaskDetailCell(taskOrder task)
    {
        for (int i = 0; i < produceList.Count; i++)
        {
            Destroy(produceList[i]);
        }
        produceList.Clear();

        diamondText.text = task.reward_xyz.ToString();
        hongbaoText.text = task.reward_hbq.ToString();

        Dictionary<string, int> dict = userData.Instance.itemDataDictionary;

        if (task.item1_id != 0)
        {
            GameObject obj = Instantiate(produceTaskCellPrefab, produceTaskCellParent);
            obj.GetComponent<produceTaskCell>().InitProduceCell(task.item1_id, task.item1_num, dict[task.item1_id.ToString()]);
            produceList.Add(obj);
        }

        if (task.item2_id != 0)
        {
            GameObject obj = Instantiate(produceTaskCellPrefab, produceTaskCellParent);
            obj.GetComponent<produceTaskCell>().InitProduceCell(task.item2_id, task.item2_num, dict[task.item2_id.ToString()]);
            produceList.Add(obj);
        }

        if (task.item3_id != 0)
        {
            GameObject obj = Instantiate(produceTaskCellPrefab, produceTaskCellParent);
            obj.GetComponent<produceTaskCell>().InitProduceCell(task.item3_id, task.item3_num, dict[task.item3_id.ToString()]);
            produceList.Add(obj);
        }

        if (((task.item1_id!=0)?(dict[task.item1_id.ToString()] >= task.item1_num):true)&& 
            ((task.item2_id!=0)?(dict[task.item2_id.ToString()] >= task.item2_num):true) && 
            ((task.item3_id!=0)?(dict[task.item3_id.ToString()] >= task.item3_num):true))
        {
            changeButton.GetComponent<Image>().sprite = lingjiangSprite;

            if(userData.Instance.isInJiaoCheng)
                fingerGameObject.SetActive(true);

            changeButton.onClick.AddListener(() =>
            {
                changeButton.interactable = false;
                AudioManager.Instance.PlaySound("version3_1");
                if (userData.Instance.isInJiaoCheng)
                {
                    userData.Instance.taskOrderList.Find((t) => t.order_id == task.order_id).is_begin_mission = 1;
                    userData.Instance.refreshFourOrders();
                    FindObjectOfType<taskDetailPanelConfig>().InitTaskDetailPanelConfig();
                    Destroy(FindObjectOfType<taskDetailPanelConfig>().gameObject);
                    FindObjectOfType<wangdianjiaocheng>().NextStep();
                }
                else
                {
                    tipsManager.Instance.openOrderFinishPanel(task);
                }
                changeButton.interactable = true;
            });
        }
        else
        {
            changeButton.GetComponent<Image>().sprite = duihuanSprite;
            changeButton.onClick.AddListener(() =>
            {
                tipsManager.Instance.openSingleTaskDetailPanel(task);
            });
        }

       
    }

    public void clearProduce()
    {
        for (int i = 0; i < produceList.Count; i++)
        {
            Destroy(produceList[i]);
        }
        produceList.Clear();
    }
}
