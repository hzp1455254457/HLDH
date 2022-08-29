using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager :MonoSingleton<UIManager>
{
    // Start is called before the first frame update
    private Dictionary<string, PanelBase> panelDic = new Dictionary<string, PanelBase>();
    public GameObject canvas;//�洢�Ļ���
    public GameObject canvas_Main;//��פ����
    public Transform showRootMain;
    public Transform showRootMain1;
    public Transform showRoot;//��ʾ����ڵ�
    public Transform hideRoot;//���ؽ���ڵ�
    public Canvas _Canvas;
    public Canvas _ToggleCanvas;
    public Canvas Main_Canvas;
    /// <summary>
    /// ��¡����
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
    /// ���ؽ���
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
            //������ֵ���
            PanelBase pb = panel.GetComponent<PanelBase>();
            pb.panelName = panelName;
            panelDic.Add(panelName, pb);
            pb.Init();
           // panel.SetActive(false);
        }
    }
    /// <summary>
    /// �򿪽���
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
        //���ý�����ʾ
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
        //���ý�����ʾ
        return pb;
    }
    /// <summary>
    /// �رս���
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
    /// ж�ؽ���
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
