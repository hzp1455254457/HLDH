
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanKuPanel : PanelBase
{
    // Start is called before the first frame update
    // public Transform bornTf;
    [Header("���вֿ�")]
    public List<CanKu> cankuList = new List<CanKu>();
   
  
    [Header("���п��вֿ�")]
    public List<CanKu> cankuKongXianList = new List<CanKu>();
    [Header("���вֿ�ø�����")]
    public Transform parentTf;
    [Header("���вֿ��CanvasGroup")]
    public List<CanvasGroup> canvasGroupsList = new List<CanvasGroup>();
    [Header("��������ű�")]
    public ScrollManager scrollManager;
    [Header("�������")]
    public ScrollRect scrollRect;
    [Header("���ɲֿⰴť")]
    public Button creactBt;
    //public Button konxianBt ;
    // public Text konxianCount;
    //public Button walletBt;
    //public Text walletCount;
    [Header("������������Text���")]
    public Text creactCount;
    [Header("�̳����ֽű�")]
    public PeopleEffect peopleEffect;
    [Header("�̳�������ʾ��Χ")]
    public RectTransform rectTransform;
    [Header("�̳�������ָ����λ��")]
    public RectTransform tipsTarget;
    [Header("�̳�������ָָ��Ŀ��λ��")]
    public Transform tipsMoveTarget;
    [Header("����������")]
    public Text kongxianCount;
    [Header("���вֿ�����")]
    public int konxianCount;
    [Header("���ɰ�ť����Animator")]
    public Animator Creactanimator;
   // [Header("���ɰ�ť��ͷ")]
   // public GameObject tipsGo;
    public static int selledCount;
    public static bool isFaHuo = false;
    /// <summary>
    /// ����
    /// </summary>
    public static void AddSelledCount(int count)
    {
        selledCount += 1;
        print("selledCount");
       if(addSelledCountAction != null)
        {
            addSelledCountAction(count);
        }
        addSelledCountAction1?.Invoke();
    }
    public static UnityEngine.Events.UnityAction<int> addSelledCountAction;
    public static UnityEngine.Events.UnityAction addSelledCountAction1;
    public  RectTransform canvas;
    private void Start()
    {
        StartCoroutine(InitStart());
    }

    private IEnumerator InitStart()
    {
        yield return null;
        scrollRect = scrollManager.GetComponent<ScrollRect>();
        canvas = UIManager.Instance.canvas.GetComponent<RectTransform>();
        kongxianCount = ToggleManager.Instance.tipsGos[2].GetComponentInChildren<Text>();
        var mask = UIManager.Instance.canvas_Main.transform.Find("Mask");
        // mask.gameObject.SetActive(true);
        peopleEffect = mask.GetComponent<PeopleEffect>();
        if (PlayerData.Instance.courierDateList.Count <= 0)
        {
            //AddCanKu();
            AddCanKu(ConfigManager.Instance.GetCourier());
        }
        else
        {
            yield return StartCoroutine(LoadDate());
        }

        PlayerData.Instance.sellCountAction += RefreshKonxianText;
        RefreshKonxianText();
        RefreshShenJIShow();
        SetCanKuRay(false);
        GetKonXianCanKuCount();
        RefreshKonXianCanKu();

    }

    private void GetKonXianCanKuCount()
    {
        var canKus = cankuList.FindAll(s => s.courier.Busy_state == 0);

        if (canKus == null || canKus.Count == 0)
        {
            konxianCount = 0;
        }
        else
        {
            konxianCount = canKus.Count;
        }
    }

    /// <summary>
    /// ���ӿ��вֿ�
    /// </summary>
    /// <param name="canKu"></param>
    public void AddKonXianCangku(CanKu canKu)
    {
        if (!cankuKongXianList.Contains(canKu))
            cankuKongXianList.Add(canKu);
    }
    /// <summary>
    /// �Ƴ����вֿ�
    /// </summary>
    /// <param name="canKu"></param>
    public  void RemoveKonXianCangku(CanKu canKu)
    {
       if(cankuKongXianList.Contains(canKu))
        cankuKongXianList.Remove(canKu);
    }

    public void SetKonxianCount(int count)
    {
        konxianCount += count;
        RefreshKonXianCanKu();
    }
    public override void Show()
    {
        base.Show();
     
    }
    public override void SetHideOrShow(bool value)
    {
        base.SetHideOrShow(value);
        if (value)
        {
           
           // AndroidAdsDialog.Instance.UploadDataEvent("send_scene");
            if (GuideManager.Instance.isFirstGame)
            {
               
                    //GuideManager.Instance.GetMask();
                   PeopleEffect.Instance.HideTips();
                PeopleEffect.Instance.HideMask();
                //PeopleEffect.Instance.ShowMask(0.5F, () => { peopleEffect.SetTips1(rectTransform, tipsTarget.position, tipsMoveTarget); PeopleEffect.Instance.ShowCanKu();AudioManager.Instance.PlaySound("jiaocheng4"); });
            
                   //peopleEffect.SetTips1(rectTransform,tipsTarget.position);
                
            }
            else
            {if(ToggleManager.Instance.tipsGo.activeInHierarchy)
                ToggleManager.Instance.SetRedTips(false);
                
                    AndroidAdsDialog.Instance.UploadDataEvent("click_scene_send");
                ToggleManager.Instance.HideTips(2);
               // ToggleManager.Instance.StartTime(true);
            }
        }
        else
        {
            RefreshKonXianCanKu();
        }
    }

    private IEnumerator LoadDate()
    {
        for (int i = 0; i < PlayerData.Instance.courierDateList.Count; i++)
        {
            var canku = Instantiate(ResourceManager.Instance.GetProGo("CanKu"), parentTf).GetComponent<CanKu>();
            canku.index = i;
            canku.courier = PlayerData.Instance.courierDateList[i];

            canvasGroupsList.Add(canku.GetComponent<CanvasGroup>());
            cankuList.Add(canku);
            AddKonXianCangku(canku);
            canku.Init();
            yield return null;
        }
        courierUp = ConfigManager.Instance.GetCurrentCourier_Up(PlayerData.Instance.courierDateList.Count);
        scrollManager.RefreshPos(PlayerData.Instance.courierDateList.Count);
    }

    public void CreactCanKu()
    {if (courierUp != null)
        {
            if (courierUp.kuaidiyuan_cost <= PlayerData.Instance.diamond&& courierUp.kuaidiyuan_cost!=0)
            {
                PlayerData.Instance.ExpendDiamond(courierUp.kuaidiyuan_cost);
                AddCanKu(ConfigManager.Instance.GetCourier());
              
                SetKonxianCount(1);//���������ı�
               // AddKonXianCangku()
                //RefreshKonXianCanKu();//����
                AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.Warning);
               // AndroidAdsDialog.Instance.UploadDataEvent("creat_deliever");
                PlayCreactAnim(false);
                AndroidAdsDialog.Instance.UploadDataEvent("unlock_deliever_suc");
                AndroidAdsDialog.Instance.ShowToasts("���Ա����+1", ResourceManager.Instance.GetSprite("�ֿ�С��"), Color.black);

            }
            else
            {
                AndroidAdsDialog.Instance.UploadDataEvent("unlock_deliever_faild");
                AndroidAdsDialog.Instance.ShowToasts("��ʯ��������",ResourceManager.Instance.GetSprite("��ʯ����"),Color.red);
             
               // Debug.LogError("��ʯ��������");
            }
        }
     
        //RefreshShenJIShow();
    }
    private void RefreshShenJIShow()
    {
        if (courierUp != null)
        {
            creactCount.text = courierUp.kuaidiyuan_cost.ToString();
            //if (courierUp.kuaidiyuan_cost > PlayerDate.Instance.diamond)
            //{
            //    creactBt.interactable = false;
            //}
            //else
            //{
            //    creactBt.interactable = true;
            //}
        }
    }
    [Header("������������")]
  public  Courier_Up courierUp;
    public void AddCanKu( CourierDate courier)
    {
        var canku = Instantiate(ResourceManager.Instance.GetProGo("CanKu"), parentTf).GetComponent<CanKu>();
        canku.index = PlayerData.Instance.courierDateList.Count;
        canku.courier = courier;
        var cankuCanvas = canku.GetComponent<CanvasGroup>();
        cankuCanvas.blocksRaycasts = false;
        canvasGroupsList.Add(cankuCanvas);
        cankuList.Add(canku);
        AddKonXianCangku(canku);
        canku.Init();
        PlayerData.Instance.courierDateList.Add(courier);
        courierUp = ConfigManager.Instance.GetCurrentCourier_Up(PlayerData.Instance.courierDateList.Count);
        //floorup = ConfigManager.Instance.GetCurrentLevel(PlayerDate.Instance.actorDateList.Count);
        scrollManager.RefreshPos(PlayerData.Instance.courierDateList.Count);
        RefreshShenJIShow();
        //  PlayerDate.Instance.SaveCourierDate();

    }
    /// <summary>
    /// ��λ��λ��
    /// </summary>
    public void MoveCanKu(CanKu canKu)
    {
      
        scrollManager.Move(canKu.index); 
    }
  
    public void PlayCreactAnim(bool value)

    {
        //tipsGo.SetActive(value);
            //if (!Creactanimator.GetCurrentAnimatorStateInfo(0).IsName("creactkuaidiyuan"))
            Creactanimator.SetBool("walk", value);
       

    }
    public CanKu GetCangKu(int id)
    {
     return   cankuList.Find(s => s.courier.item_id ==id);
    }
    public CanKu GetKonXianCanKu()
    {
        //var canku = cankuList.FindLast(s => s.courier.Busy_state == 0);
        //if (canku != null)
        //{
        //    return canku;
        //}
        //return null;
        if (cankuKongXianList != null && cankuKongXianList.Count != 0)
            return cankuKongXianList[Random.Range(0, cankuKongXianList.Count)];
        return null;
    }
    public void SetCanKuRay(bool value)
    {
        for (int i = 0; i < canvasGroupsList.Count; i++)
        {
            canvasGroupsList[i].blocksRaycasts = value;
        }
    }
    /// <summary>
    /// ˢ�¿��и���
    /// </summary>
    public void RefreshKonXianCanKu()
    {if (GuideManager.Instance.isFirstGame) return;
        if (!ToggleManager.Instance.toggles[2].isOn)
        {
            print("konxianCount++++"+konxianCount);
            print("konxianCount++++" + cankuKongXianList.Count);
            // var canku = cankuList.FindAll(s => s.courier.Busy_state == 0);
            if (cankuKongXianList.Count <= 0)
            {
                ToggleManager.Instance.HideTips(2);
                //konxianBt.interactable = false;
                // konxianCount.text = "0";
            }
            else
            {
                if (PlayerData.Instance.gold <= 20000d)
                {
                    if (SelledManager.Instance.parentTf.childCount >= 1)
                    {
                        ToggleManager.Instance.SetTips(2);
                    }
                    else
                    {
                        ToggleManager.Instance.HideTips(2);
                    }
                }
                else
                {
                    if (SelledManager.Instance.parentTf.childCount >= 3)
                    {
                        ToggleManager.Instance.SetTips(2);
                    }
                    else
                    {
                        ToggleManager.Instance.HideTips(2);
                    }

                    // ToggleManager.Instance.SetTips(2);
                }

              //  kongxianCount.text = canku.Count.ToString();
                //konxianBt.interactable = true;
            }
        }
        else
        {
          if(cankuKongXianList.Count <= 0)
            PlayCreactAnim(true);
            else
                PlayCreactAnim(false);
        }
    }
    void RefreshKonxianText()
    {
        kongxianCount.text = PlayerData.Instance.GetSelledCount().ToString();
    }
     
    public void JiaSu()
    {
        //AndroidAdsDialog.Instance.ShowRewardVideo("-70");
        AndroidAdsDialog.Instance.UploadDataEvent("click_tripble_speed");
#if UNITY_EDITOR
        JiaSuEvent();


#elif UNITY_ANDROID
 
      
        
#endif

    }
    public void JiaSuEvent()
    {
        print("ִ�м���");
        AndroidAdsDialog.Instance.UploadDataEvent("finish_tripble_speed");
        foreach (var item in cankuList)
        {
            item.JiaSu();
        }
        jiaSu.alpha = 0.3f;
        jiaSu.blocksRaycasts = false;
        jiaSu.interactable = false;
        timecanp.alpha = 1f;
       StartCoroutine( Timing(RecorveJiaSu));
    }
    void RecorveJiaSu()
    {
        jiaSu.alpha = 1f;
        jiaSu.blocksRaycasts = true;
        jiaSu.interactable = true;
        timecanp.alpha = 0f;
        foreach (var item in cankuList)
        {
            item.RecorveSuDu();
        }
    }
    int time = 180;
    [Header("�������")]
    public Text timeText;
    public CanvasGroup jiaSu, timecanp;

    IEnumerator Timing( UnityEngine.Events.UnityAction action)
    {
        time = 180;
        timeText.text = Global.GetMinuteTime(time);
        while (time >= 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            timeText.text = Global.GetMinuteTime(time);
        }
        time=0;
       timeText.text = Global.GetMinuteTime(time);
        if (action != null)
        {
            action();
        }
    }
}
