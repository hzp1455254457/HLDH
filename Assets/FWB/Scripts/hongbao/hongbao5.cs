using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class hongbao5 : PanelBase
{
    // Start is called before the first frame update
  //  public Text titleText, hongbaoNumberText,leftHongbaoNumberText,NeedToMakeHongBaoNumberText;
   // public Image lightImage;
    public Button sureButton, closeButton;
    public Transform hongbaoTransform, groupTf;
    public ScrollRect scrollRect;
    public Slider slider;
    public TiXianItem[] tiXianItems;
    public float[] pos;
    public NumberEffect2 numberEffect;
    public Text text;

    public Text adwardtext;
    public static hongbao5 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("hongbao5")) as hongbao5;
                instance.gameObject.SetActive(false);
                //instance. Init();
                return instance;
            }
            return instance;
        }
    }
    static hongbao5 instance;
    //public float initValue;
    protected override void Awake()
    {
        tiXianItems = groupTf.GetComponentsInChildren<TiXianItem>();
        pos = new float[tiXianItems.Length];
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = 1 / (float)(pos.Length - 1) * i;
        }
        //initValue = PlayerDate.Instance.ShengJiRedValue;
        float value = 5f;
        text.text = (PlayerData.Instance.ShengJiRedValue * 100).ToString("F2") + "%";

        for (int i = 0; i < tiXianItems.Length; i++)
        {
            if(i>0&&i<=3)
                tiXianItems[i].SetStates(string.Format("{0}", JavaCallUnity.Instance.tiXianDatas[i-1].amount / 100f),2);
            if (i >= 3)
            {
                if (i >= 4)
                {
                    tiXianItems[i].SetStates(string.Format("{0}", value-5), 2);
                }
                tiXianItems[i].SetJinE(value.ToString() + "Ԫ");
                value += 5;
            }
            
        }
       // scrollRect.enabled = true;
    }
    public override void Show()
    {
        transform.SetParent(UIManager.Instance.showRootMain);
        //transform.SetAsLastSibling();
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
        RemoveAction();

    }
    public void RemoveAction()
    {
        sureButton.onClick.RemoveAllListeners();
        //closeButton.onClick.RemoveAllListeners();
    }
    private void Start()
    {
        //tiXianItems = groupTf.GetComponentsInChildren<TiXianItem>();
        //float value = 0.1f;
        //foreach (var item in tiXianItems)
        //{
        //    item.SetJinE(value.ToString() + "Ԫ");
        //    value += 0.1f;
        //    item.SetStates("��������");
        //}
    }
    bool isFirst = false;
    /// <summary>
    /// �����ʼ��
    /// </summary>
    /// <param name="titleString">����</param>
    /// <param name="hongbaoNumber">������</param>
    /// <param name="lefthongbaoNumber">ʣ�������</param>
    /// <param name="needtomakehongbaoNumber">��Ҫ���������ֺ�����</param>
    /// <param name="clickAction">�����ȡ</param>
    /// <param name="closeAction">����ر�</param>
    public void InitHongBao(float initValue,float targetValue,int index,int daimond,Action clickAction = null,Action action=null)
    {
        isFirst = false;
        transform.SetAsLastSibling();
       // titleText.text = titleString;
        gameObject.SetActive(true);
        int index1 = index;
        adwardtext.text= daimond.ToString();
        if (JavaCallUnity.Instance.IsFirstGetRedValue())
        {
            index1 = tiXianItems.Length-1;
            isFirst = true;
        }
        scrollRect.horizontalNormalizedPosition = pos[index1];
        sureButton.gameObject.SetActive(false);
        scrollRect.enabled = false;
        slider.value = PlayerData.Instance.ShengJiRedValue;
        AndroidAdsDialog.Instance.ShowFeedAd(540);
        //hongbaoNumberText.text = "+" +hongbaoNumber + "Ԫ";
        //leftHongbaoNumberText.text = "���:" + lefthongbaoNumber + "Ԫ";
        //NeedToMakeHongBaoNumberText.text = "��׬" + needtomakehongbaoNumber + "��������";

        sureButton.onClick.AddListener(() =>
        {

            AndroidAdsDialog.Instance.UploadDataEvent("finish_big_ecpm_shengjihblast");
            AndroidAdsDialog.Instance.UploadDataEvent("click_countine_shengjihb_news");
            clickAction?.Invoke();
            AndroidAdsDialog.Instance.CloseFeedAd();
            //Destroy(gameObject);
        });

        //closeButton.onClick.AddListener(() =>
        //{
        //    closeAction?.Invoke();
        //    //Destroy(gameObject);
        //});

        hongbaoTransform.localScale = Vector3.zero;
        hongbaoTransform.DOScale(Vector3.one * 1.1f, 0.5f).SetUpdate(true).onComplete
             = () =>
             {
                 hongbaoTransform.DOScale(Vector3.one * 1f, 0.3f).SetUpdate(true).onComplete
                 = () =>
                 {
                     if (!isFirst)
                     {
                         numberEffect.Animation(targetValue, "", "%", 1.5f, initValue);
                         slider.DOValue(targetValue, 1.5f).onComplete += () =>
                         {
                             PlayerData.Instance.ShengJiRedValue = targetValue;
                           
                             sureButton.gameObject.SetActive(true);
                             scrollRect.enabled = true;
                             action?.Invoke();
                         };
                         PlayerData.Instance.ClickShengJiRedCount++;
                         //lightImage.GetComponent<Graphic>().DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);
                         //hongbaoImage.transform.DOShakeRotation(1.0f, 20, 5, 30);

                         //lightImage.transform.DOLocalRotate(new Vector3(0, 0, -360 * 5000.0f), 5.0f * 5000, RotateMode.LocalAxisAdd);//.SetLoops(-1, LoopType.Restart);
                     }
                     else
                     {
                         scrollRect.DOHorizontalNormalizedPos(0, 2.5f);
                         numberEffect.Animation(targetValue, "", "%", 2.5f, initValue);
                         slider.DOValue(targetValue, 2.5f).onComplete += () =>
                         {
                            
                             PlayerData.Instance.ShengJiRedValue = targetValue;
                             sureButton.gameObject.SetActive(true);
                             scrollRect.enabled = true;
                             action?.Invoke();
                             PlayerData.Instance.ClickShengJiRedCount++;
                         };
                     }

                     };
                 
               
             };
    }
    public void NextDangWei()
    {
        PlayerData.Instance.ShengJiRedValue = 0;
        hongbao5.Instance.slider.value = 0;
        text.text = (PlayerData.Instance.ShengJiRedValue * 100).ToString("F2") + "%";
    }
    public void HideUI()
    {
        Hide();
    }
    
}
