using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskDetailPanelConfig : PanelAnimation
{
    public GameObject taskDetailCellPrefab;
    public Button closeButton;

    public List<GameObject> taskDetailCellList = new List<GameObject>();

    public RectTransform oderDetailCellParent;

    // Start is called before the first frame update
    void Start()
    {
        base.Animation();
        //InitTaskDetailPanelConfig();
        closeButton.onClick.AddListener(() =>
        {
            userData.Instance.closeOrderTimes++;
            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 初始化taskDetailPanel
    /// </summary>
    public void InitTaskDetailPanelConfig()
    {
        for (int i = 0; i < taskDetailCellList.Count; i++)
            Destroy(taskDetailCellList[i]);
        taskDetailCellList.Clear();

        if (userData.Instance.currentFourOrder == null)
            return;

        for (int j = 0; j < userData.Instance.currentFourOrder.Count; j++)
        {
            GameObject obj = Instantiate(taskDetailCellPrefab, oderDetailCellParent);
            obj.GetComponent<taskDetailCell>().InitTaskDetailCell(userData.Instance.currentFourOrder[j]);
            taskDetailCellList.Add(obj);
        }
    }
}

[SerializeField]
public class taskOrder
{
    public int order_id;
    public int reward_hbq;
    public int reward_xyz;
    public int item1_id;
    public int item1_num;
    public int item2_id;
    public int item2_num;
    public int item3_id;
    public int item3_num;
    /// <summary>
    /// 0表示未完成(有可能未开启)，1表示已完成
    /// </summary>
    public int is_begin_mission;
}
