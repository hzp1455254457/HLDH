using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[DefaultExecutionOrder(1000)]
public class taskPanelConfig : MonoBehaviour
{
    public Button taskButton;
    public RectTransform taskBarRectTransform;

    public GameObject itemBar;
    public Text orderRewardText;

    public Image detailImage;

    public List<GameObject> taskBarList = new List<GameObject>();

    public List<Sprite> levelSpriteList = new List<Sprite>();

    public Image levelImage;
    // Start is called before the first frame update
    void Start()
    {
        taskButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound("bubble1");
            taskButton.interactable = false;
            Debug.Log("进入订单页面");
            tipsManager.Instance.openTaskDetailPanel();
            if (userData.Instance.isInJiaoCheng)
            {
                FindObjectOfType<wangdianjiaocheng>().NextStep();
            }
            taskButton.interactable = true;
        });

        detailImage.transform.DOLocalRotate(new Vector3(0,0,2.0f),0.6f).SetLoops(-1, LoopType.Yoyo);

    }

    private void OnEnable()
    {
        if (!userData.Instance.dataInitialed)
            userData.Instance.InitData();
        FindObjectOfType<wangdianUserPanelConfig>().levelGradeAction = changeLevel;
        userData.Instance.mostValveOrderAction = changeOrder;
        StartCoroutine(waitForData());
    }

    private void OnDisable()
    {
        StopCoroutine(waitForData());
    }

    public void changeLevel(int level)
    {
        levelImage.sprite = levelSpriteList[FindObjectOfType<wangdianUserPanelConfig>().levelGrade - 1];
    }

    public void changeOrder(taskOrder order)
    {
        

        for (int i = 0; i < taskBarList.Count; i++)
        {
            Destroy(taskBarList[i]);
        }

        taskBarList.Clear();

        if (order == null)
            return;

        orderRewardText.text = (order.reward_hbq / 10000.0f).ToString("F2") + "元";

        Dictionary<string,int> dict = userData.Instance.itemDataDictionary;


        if (order.item1_id != 0)
        {
            GameObject obj = Instantiate(itemBar, taskBarRectTransform);
            obj.GetComponent<taskItemBarConfig>().initTaskItemBar(ConfigManager.Instance.GetProduce(order.item1_id), 
                dict[order.item1_id.ToString()]>= order.item1_num?order.item1_num+"/"+ order.item1_num: dict[order.item1_id.ToString()]+"/"+ order.item1_num, dict[order.item1_id.ToString()] >= order.item1_num);
            taskBarList.Add(obj);
        }

        if (order.item2_id != 0)
        {
            GameObject obj = Instantiate(itemBar, taskBarRectTransform);
            obj.GetComponent<taskItemBarConfig>().initTaskItemBar(ConfigManager.Instance.GetProduce(order.item2_id), 
                dict[order.item2_id.ToString()] >= order.item2_num ? order.item2_num + "/" + order.item2_num : dict[order.item2_id.ToString()] + "/" + order.item2_num, dict[order.item2_id.ToString()] >= order.item2_num);
            taskBarList.Add(obj);
        }

        if (order.item3_id != 0)
        {
            GameObject obj = Instantiate(itemBar, taskBarRectTransform);
            obj.GetComponent<taskItemBarConfig>().initTaskItemBar(ConfigManager.Instance.GetProduce(order.item3_id),
                dict[order.item3_id.ToString()] >= order.item3_num ? order.item3_num + "/" + order.item3_num : dict[order.item3_id.ToString()] + "/" + order.item3_num, dict[order.item3_id.ToString()] >= order.item3_num);
            taskBarList.Add(obj);
        }
    }

    IEnumerator waitForData()
    {
        yield return new WaitUntil(() => (userData.Instance.mostValveOrder != null));
        Debug.Log("test------");
        changeOrder(userData.Instance.mostValveOrder);
    }
}
