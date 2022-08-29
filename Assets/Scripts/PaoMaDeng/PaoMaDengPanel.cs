using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using UnityEngine.UI;
using DG.Tweening;

public class PaoMaDengPanel : PanelBase
{
    public static PaoMaDengPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("PaoMaDengPanel")) as PaoMaDengPanel;
                instance.Hide();
                return instance;
            }
            return instance;
        }

    }
    static PaoMaDengPanel instance;
    public List<PaoMaDengData> paoMaDengDatas;
    public string NextTime;
    public string NextDayTime;
    public int id;
    //public Transform transform1, transform2, transform3, transform4;
    public Transform[] borns;

    public Button button, button1,button2;
    public Text timeText,redText,daimondText;
   public DateTime dataTime;
    public Sprite[] sprites;
    public bool IsRefresh = false;
    public int RedCount { set {

            redCount = value;
            redText.text = redCount / MoneyManager.redProportion + "元";
        } get { return redCount; } }
    int redCount;
    public int DaiMondCount { set {
            daimondCount = value;
            daimondText.text = daimondCount.ToString() + "个";
        } get { return daimondCount; } }
    int daimondCount;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
      string value=  DataSaver.Instance.GetString("PaoMaDeng","");
        NextTime= DataSaver.Instance.GetString("PaoMaDengTime", "");
        RedCount = PlayerPrefs.GetInt("PaoMaDeng_RedCount", 0);
        DaiMondCount = PlayerPrefs.GetInt("PaoMaDeng_DaiMondCount", 0);
         NextDayTime= DataSaver.Instance.GetString("PaoMaDengDayTime", "");
        id = DataSaver.Instance.GetInt("PaoMaDengID", 1);
        if (string.IsNullOrEmpty(value))
        {
            value = Resources.Load<TextAsset>("Config/PaoMaDengConfig").text;
            paoMaDengDatas = JsonMapper.ToObject<List<PaoMaDengData>>(value);
            var item = new PaoMaDengData();
            item.SetPaoMaDengData(16, 5, 1, 1);
            paoMaDengDatas.Add(item);
           
        }
        else
        {
            paoMaDengDatas = JsonMapper.ToObject<List<PaoMaDengData>>(value);
        }
      
        borns = new Transform[16];
     
        for (int i = 0; i < borns.Length; i++)
        {
           
               borns[i] = Global.FindChild<Transform>(transform, "born (" + i.ToString()+")");
        }
        rotaryCells =new List<RotaryCell>();
        IsRefresh = PlayerPrefs.GetInt("IsRefresh", 0) == 1 ? true : false;
        CheckTime();
        Creact();
        CanrotaryCells = rotaryCells.FindAll(s => s.index >= id);
        rotaryTablePanel.SetRoTatyArry(CanrotaryCells.ToArray());
    }

    private void CheckTime()
    {
        if (string.IsNullOrEmpty(NextTime))
        {
            RadomList();
            SetButtonStates(true);
            isTime = false;
        }
        else
        {
            dataTime = DateTime.Parse(NextTime);
            if (dataTime > DateTime.Now)
            {
                SetButtonStates(false);
                isTime = true;
            }
            else
            {
                isTime = false;
                SetButtonStates(true);
                
                RadomList();
            }
        }
       
       
    }

    public override void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        transform.SetParent(UIManager.Instance.showRootMain);
    }
    public override void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
List<  RotaryCell> rotaryCells;
    public List<RotaryCell> CanrotaryCells;
    bool isShow = false;
    public void ShowUI()
    {
        Show();
       
        transform.SetAsLastSibling();
        gameObject.SetActive(true);

       RefreshDay();
       // CheckTime();
       
        
        base.Animation();
      

        //AndroidAdsDialog.Instance.ShowFeedAd(540);
        isShow = true;
    }
    public RotaryTablePanel rotaryTablePanel;
    public void ChouJiang()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("click_start_choujiangs");
        NextTime = DateTime.Now.AddSeconds(paoMaDengDatas.Find(s=>s.id==id).time).ToString();
        dataTime = DateTime.Parse(NextTime);
        rotaryTablePanel.OnClickDrawFun(id);
     
        id++;
        SetButtonStates(false);
        isTime = true;
        IsRefresh = false;
        //button.interactable = false;
    }
    private void SetButtonStates(bool value)
    {
        button.gameObject.SetActive(value);
        button1.gameObject.SetActive(!value);
        if (value)
        {
            ButtonAnim();
        }
        else
        {
            if (queen != null)
            {
                queen.Kill();
            }
        }
    }
    private void Start()
    {
        button.onClick.AddListener(ChouJiang);
        button2.onClick.AddListener(HideUi);
    }
    public void RefreshRotary()
    {
        Collect();
        RadomList();
        Creact();
        CanrotaryCells = rotaryCells.FindAll(s => s.index >= id);
        rotaryTablePanel.SetRoTatyArry(CanrotaryCells.ToArray());
        ButtonAnim();
    }

    private void RadomList()
    {
        if (!IsRefresh)
            RandomSort(ref paoMaDengDatas);
        NextDayTime = DateTime.Now.AddDays(14).ToString();
        RefreshDay();
        IsRefresh = true;
    }

    private void RefreshDay()
    {

        //if (DateTime.Parse(NextDayTime) < DateTime.Now)
        //{
        //    id = 1;
        //    NextTime = "";
        //    NextDayTime = DateTime.Now.AddDays(14).ToString();
        //}
        // coldDay.text = (DateTime.Parse(NextDayTime) - DateTime.Now).Days.ToString() + "天";
    }

    private void Creact()
    {
        bool isShow =true;
        rotaryCells.Clear();
        for (int i = 0; i < borns.Length; i++)
        {
            isShow = true;
            if (paoMaDengDatas[i].type != 5)
            {
                var cell = GameObjectPool.Instance.CreateObject("paomadeng", ResourceManager.Instance.GetProGo("paomadengItem"), borns[i], Quaternion.identity).GetComponent<RotaryCell>();
                if (paoMaDengDatas[i].id < id)
                { isShow = false; }
                else
                {
                    
                }
                cell.SetType(paoMaDengDatas[i].type, paoMaDengDatas[i].num, paoMaDengDatas[i].id,isShow);
                rotaryCells.Add(cell);
            }
            else
            {
                var cell = GameObjectPool.Instance.CreateObject("paomadeng1", ResourceManager.Instance.GetProGo("paomadengItem1"), borns[i], Quaternion.identity).GetComponent<RotaryCell>();
                cell.SetType(paoMaDengDatas[i].type, paoMaDengDatas[i].num, paoMaDengDatas[i].id);
                rotaryCells.Add(cell);

            }
        }
    }
    bool isTime = false;
    public bool IsTime
    {
        get { return isTime; }
    }
    private void Update()
    {
       // if (!isShow) return;
        if (isTime)
        {
            if (dataTime > DateTime.Now)
            {
                SetButtonStates(false);
               TimeSpan date = dataTime - DateTime.Now;
                timeText.text = "<color=yellow>" + GetTimeString(date)+ "后</color> 可抽奖";
            }
            else
            {
               
                SetButtonStates(true);
                RefreshRotary();
                isTime = false;
            }
        }
    }
    public string GetTimeString(TimeSpan date)
    {
        string value = "";
        if (date.Days > 0)
        {
            value = date.Days+ "天";
        }
        else
        {
            if (date.Hours >= 1)
            {
                value = date.Hours+"小时";
            }
            else
            {
                if (date.Minutes >= 1)
                {
                    value = date.Minutes+"分钟";
                }
                else
                {
                    if (date.Seconds >= 0)
                    {
                        value = date.Seconds + "秒";
                    }
                    else
                    {

                    }
                }
            }
        }
        return value;
    }
    public void HideUi()
    {
       // Collect();
       // gameObject.SetActive(false);
        
        //AndroidAdsDialog.Instance.CloseFeedAd();
        Hide();
        AndroidAdsDialog.Instance.UploadDataEvent("close_choujiangs");
        isShow = false;
    }
    Sequence queen;
    public void ButtonAnim()
    {
        queen = DOTween.Sequence();
        queen.Append(button.transform.DOScale(1.2f, 1.0f));
        queen.AppendInterval(0.3f);
        queen.Append(button.transform.DOScale(1.0f, 1.0f));
        queen.SetLoops(-1);
    }
    private void Collect()
    {
        for (int i = 0; i < rotaryCells.Count; i++)
        {
            GameObjectPool.Instance.CollectObject(rotaryCells[i].gameObject);

        }
    }

    public static void RandomSort<T>(ref List<T> ts)
    {
        for (int i = 0; i < ts.Count; i++)
        {
            T t = ts[i];
            int index = UnityEngine.Random.Range(0, ts.Count);
            ts[i] = ts[index];
            ts[index] = t;
        }
       
    }
    private void SaveData()
    {
       
        DataSaver.Instance.SetString("PaoMaDeng",JsonMapper.ToJson(paoMaDengDatas));
        DataSaver.Instance.SetString("PaoMaDengTime",NextTime);
        DataSaver.Instance.SetInt("PaoMaDengID", id);
       DataSaver.Instance.SetString("PaoMaDengDayTime",NextDayTime);
        int value = IsRefresh == true ? 1 : 0;
        PlayerPrefs.SetInt("IsRefresh", value);
     PlayerPrefs.SetInt("PaoMaDeng_RedCount", RedCount);
      PlayerPrefs.SetInt("PaoMaDeng_DaiMondCount", DaiMondCount);
   
    }
   
    private void OnApplicationQuit()
    {

        SaveData();

    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
           
        }
    }
    }
[Serializable]
public class PaoMaDengData
{
    public int id;
    public int type;
    public int num;
    public int time;

    public void SetPaoMaDengData(int Id, int Type, int Count, int time)
    {
        id = Id;
        type = Type;
        num = Count;
        this.time = time;

    }
}
