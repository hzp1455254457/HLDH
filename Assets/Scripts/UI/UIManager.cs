using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager :MonoSingleton<UIManager>
{
    // Start is called before the first frame update
    private Dictionary<string, PanelBase> panelDic = new Dictionary<string, PanelBase>();
    public GameObject canvas;//存储的画布
    public GameObject canvas_Main;//常驻画布
    public Transform showRootMain;
    public Transform showRootMain1;
    public Transform showRoot;//显示界面节点
    public Transform hideRoot;//隐藏界面节点
    public Canvas _Canvas;
    public Canvas _ToggleCanvas;
    public Canvas Main_Canvas;
    /// <summary>
    /// 克隆画布
    /// </summary>
    /// 

    public override void Init()
    {
        base.Init();
        CreatCanvas();
    }
    public void SetUIStates(bool value)
    {
        _Canvas.enabled = value;
        Main_Canvas.enabled = value;
        _ToggleCanvas.enabled = value;
        //CamareManager.Instance.SetStates1(value);
        if (value)
        {
            ZhiBoPanel.Instance.daoHangLanManager.SetParent(ZhiBoPanel.Instance.daoHangLanManager.transform);
            ToggleManager.Instance.ShowUI();

        }
        //else
        //ToggleManager.Instance.HideUI();
    }
    void CreatCanvas()
    {
        GameObject canvasPrefab = Resources.Load<GameObject>("Prefab/Canvas");
        canvas = Instantiate(canvasPrefab);
        // canvas = JavaCallUnity.Instance.main;
        _Canvas = canvas.GetComponent<Canvas>();
        _Canvas.worldCamera = CamareManager.Instance.uiCamera;
          canvas_Main = Instantiate(Resources.Load<GameObject>("Prefab/Canvas_Main"));
        _ToggleCanvas= Instantiate(Resources.Load<GameObject>("Prefab/Canvas_Toggle")).GetComponent<Canvas>();
        //canvas_Main = JavaCallUnity.Instance.ui;
        Main_Canvas = canvas_Main.GetComponent<Canvas>();
        Main_Canvas.worldCamera = CamareManager.Instance.uiCamera1;
        _ToggleCanvas.worldCamera = CamareManager.Instance.camera_Toggle;
       showRootMain = canvas_Main.transform.Find("ShowRoot");
           //canvas.name = "canvas";
           //DontDestroyOnLoad(canvas);

        showRoot = canvas.transform.Find("ShowRoot");
        hideRoot = canvas.transform.Find("HideRoot");
        showRootMain1 = canvas_Main.transform.Find("ShowRoot1");
        //canvas.transform.localScale = Vector3.one;
    }
    
    /// <summary>
    /// 加载界面
    /// </summary>
    public void Load(string panelName)
    {
        if (!panelDic.ContainsKey(panelName))
        {
            GameObject prefab = Resources.Load<GameObject>("Prefab/Panel/" + panelName);
            GameObject panel = Instantiate<GameObject>(prefab, hideRoot);
            panel.name = panelName;
            panel.transform.localPosition = Vector3.zero;
            panel.transform.localScale = Vector3.one;
            //添加至字典中
            PanelBase pb = panel.GetComponent<PanelBase>();
            pb.panelName = panelName;
            panelDic.Add(panelName, pb);
            pb.Init();
           // panel.SetActive(false);
        }
    }
    /// <summary>
    /// 打开界面
    /// </summary>
    /// <param name="panelName"></param>
    /// <param name="args"></param>
    public PanelBase OpenPanel(string panelName)
    {
        Load(panelName);
        PanelBase pb = panelDic[panelName];
        //if (!pb.gameObject.activeSelf)
        {
            pb.Show();
           // pb.gameObject.SetActive(true);
        }
        //调用界面显示
        return pb;
    }
    public PanelBase GetPanel(string panelName)
    {
        PanelBase pb;
        //Load(panelName);
        // print(typeof(T).Name);
        if (panelDic.ContainsKey(panelName))
        {
             pb = panelDic[panelName];
        }
        else
        {
            pb = null;
        }
        //if (!pb.gameObject.activeSelf)
        //{
        //    pb.Show();
        //}
        //调用界面显示
        return pb;
    }
    /// <summary>
    /// 关闭界面
    /// </summary>
    /// <param name="panelName"></param>
    public void ClosePanel(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        {
            PanelBase pb = panelDic[panelName];
            //if (pb.gameObject.activeSelf)
            //{
            //    pb.Hide();
            //}
            pb.Hide();
        }

    }
    /// <summary>
    /// 卸载界面
    /// </summary>
    /// <param name="panelName"></param>
    public void Unload(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        {
            PanelBase pb = panelDic[panelName];
            pb.Unload();
            panelDic.Remove(panelName);
        }
    }


}
