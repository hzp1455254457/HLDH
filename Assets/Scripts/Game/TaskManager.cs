using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : PanelBase
{
    // Start is called before the first frame update
    public List<Task> taskList=new List<Task>();
    public Transform parentTf;
    public Button exitbutton;
    public Transform back;
   // public static TaskManager Instance;
    public static TaskManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("Panel_Task")) as TaskManager;
                instance.gameObject.SetActive(false);
                return instance;
            }
            return instance;
        }
    }
    static TaskManager instance;
  
    protected override void  Awake()
    {
      
    }
    private void Start()
    {
   RectTransform    canvas = UIManager.Instance.canvas.GetComponent<RectTransform>();
       if( 709 - (canvas.rect.width) >0)
        {
            back.localScale = Vector3.one / (719 / canvas.rect.width);
        }
        //back.localScale = 706- (canvas .rect.width)
        var go1 = ResourceManager.Instance.GetProGo("task");
        var pro1 = Instantiate(go1, parentTf).GetComponent<Task>();
        pro1.index = taskList.Count;
        pro1.mission_Redpacket.tid = 1;
       // taskList.Add(pro1);
       if(PlayerData.Instance.mission_RedpacketsList.Count <= 0)
        {
            PlayerData.Instance.mission_RedpacketsList = ConfigManager.Instance.taskList;
        }
        for (int i = 0; i < PlayerData.Instance.mission_RedpacketsList.Count; i++)
        {
            var go = ResourceManager.Instance.GetProGo("task");
            var pro = Instantiate(go, parentTf).GetComponent<Task>();
            pro.mission_Redpacket = PlayerData.Instance.mission_RedpacketsList[i];
            pro.index = i;
          
            taskList.Add(pro);
          
        }
        var go2 = ResourceManager.Instance.GetProGo("task");
        var pro2 = Instantiate(go2, parentTf).GetComponent<Task>();
        pro1.index = taskList.Count;
       taskList.Add(pro1);
        exitbutton.onClick.AddListener(Hide);
        //gameObject.SetActive(false);
        AndroidAdsDialog.Instance.RequestTaskList();
    }
    public void RefreshTaskObject(List<Mission_redpacket> mission_Redpackets)
    {
        if (mission_Redpackets != null)
        {
            for (int i = 0; i < mission_Redpackets.Count; i++)
            {
                taskList[i].mission_Redpacket = mission_Redpackets[i];
                taskList[i].RefreshGetStatus();
            }
        }
    }
    public override void Hide()
    {
        gameObject.SetActive(false);
        AndroidAdsDialog.Instance.ShowTableVideo("0");
    }
    public override void Show()
    {
        transform.SetParent(UIManager.Instance.showRootMain);
        transform.SetAsLastSibling();
        base.Animation();
        gameObject.SetActive(true);
        //transform.SetParent(UIManager.Instance.canvas_Main.transform);
       // transform.SetSiblingIndex(8);
    }
}
