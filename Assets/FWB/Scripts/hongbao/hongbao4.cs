using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class hongbao4 : PanelBase
{
    // Start is called before the first frame update
    public Text titleText, hongbaoNumberText,leftHongbaoNumberText,NeedToMakeHongBaoNumberText;
    public Image lightImage;
    public Button sureButton, closeButton;
    public Transform hongbaoTransform;
    public static hongbao4 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("hongbao4")) as hongbao4;
                instance.gameObject.SetActive(false);
                //instance. Init();
                return instance;
            }
            return instance;
        }
    }
    static hongbao4 instance;

    protected override void Awake()
    { 
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
        closeButton.onClick.RemoveAllListeners();
    }
    /// <summary>
    /// �����ʼ��
    /// </summary>
    /// <param name="titleString">����</param>
    /// <param name="hongbaoNumber">������</param>
    /// <param name="lefthongbaoNumber">ʣ�������</param>
    /// <param name="needtomakehongbaoNumber">��Ҫ���������ֺ�����</param>
    /// <param name="clickAction">�����ȡ</param>
    /// <param name="closeAction">����ر�</param>
    public void InitHongBao(string titleString, float hongbaoNumber,float lefthongbaoNumber,float needtomakehongbaoNumber, Action clickAction = null, Action closeAction = null)
    {
        transform.SetAsLastSibling();
        titleText.text = titleString;
        gameObject.SetActive(true);
        hongbaoNumberText.text = "+" +hongbaoNumber + "Ԫ";
        leftHongbaoNumberText.text = "���:" + lefthongbaoNumber + "Ԫ";
        NeedToMakeHongBaoNumberText.text = "��׬" + needtomakehongbaoNumber + "��������";

        sureButton.onClick.AddListener(() =>
        {
            clickAction?.Invoke();
            //Destroy(gameObject);
        });

        closeButton.onClick.AddListener(() =>
        {
            closeAction?.Invoke();
            //Destroy(gameObject);
        });

        hongbaoTransform.localScale = Vector3.zero;
        hongbaoTransform.DOScale(Vector3.one * 1.1f, 0.5f).SetUpdate(true).onComplete
             = () =>
             {
                 hongbaoTransform.DOScale(Vector3.one * 1f, 0.3f).SetUpdate(true).onComplete
                 = () =>
                 {
                     //lightImage.GetComponent<Graphic>().DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);
                     //hongbaoImage.transform.DOShakeRotation(1.0f, 20, 5, 30);
                     lightImage.transform.DOLocalRotate(new Vector3(0, 0, -360 * 5000.0f), 5.0f * 5000, RotateMode.LocalAxisAdd);//.SetLoops(-1, LoopType.Restart);
                 };
             };
    }
    public void HideUI()
    {
        Hide();
    }
}
