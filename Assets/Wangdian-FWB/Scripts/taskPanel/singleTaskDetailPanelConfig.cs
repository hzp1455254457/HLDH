using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class singleTaskDetailPanelConfig : PanelAnimation
{
    public Text diamondText,hongbaoText;
    public GameObject singleTaskDetailCellPrefab;
    public RectTransform singleTaskDetailCellParent;
    private List<GameObject> singleTaskDetailCellList = new List<GameObject>();
    public Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        base.Animation();
        closeButton.onClick.AddListener(() =>
        {
            AndroidAdsDialog.Instance.CloseFeedAd();
            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitSingleTaskDetailPanelConfig(taskOrder task)
    {
        AndroidAdsDialog.Instance.ShowFeedAd(1);

        diamondText.text = task.reward_xyz.ToString();
        hongbaoText.text = task.reward_hbq.ToString();

        Dictionary<string, int> dict = userData.Instance.itemDataDictionary;

        if (task.item1_id != 0)
        {
            GameObject obj = Instantiate(singleTaskDetailCellPrefab, singleTaskDetailCellParent);
            obj.GetComponent<singleTaskDetailCell>().InitSingleTaskDetailCell(task.item1_id, task.item1_num, dict[task.item1_id.ToString()]);
            singleTaskDetailCellList.Add(obj);
        }

        if (task.item2_id != 0)
        {
            GameObject obj = Instantiate(singleTaskDetailCellPrefab, singleTaskDetailCellParent);
            obj.GetComponent<singleTaskDetailCell>().InitSingleTaskDetailCell(task.item2_id, task.item2_num, dict[task.item2_id.ToString()]);
            singleTaskDetailCellList.Add(obj);
        }

        if (task.item3_id != 0)
        {
            GameObject obj = Instantiate(singleTaskDetailCellPrefab, singleTaskDetailCellParent);
            obj.GetComponent<singleTaskDetailCell>().InitSingleTaskDetailCell(task.item3_id, task.item3_num, dict[task.item3_id.ToString()]);
            singleTaskDetailCellList.Add(obj);
        }
    }
}
