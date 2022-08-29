using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class hongbao3 : PanelBase
{
    // Start is called before the first frame update
    public Text  hongbaoNumberText;
    //public Image lightImage;
    public Button sureButton, closeButton, smallButton;
    // public Transform hongbaoTransform;
    // public GameObject go_icon;
    float animTime = 0.3f;
    public static hongbao3 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager.Instance.OpenPanel("hongbao3")) as hongbao3;
                instance.gameObject.SetActive(false);
                //instance. Init();
                return instance;
            }
            return instance;
        }
    }
    static hongbao3 instance;
    bool isAnim = false;
    public void SetAnimTime(bool value)
    {
        if (gameObject.activeInHierarchy)
        {
            Debug.Log("改变动画时间");
            if (value)
            {
                animTime = 0.3f;
            }
            else
            {
                animTime = 0.5f;
            }
            if (!isAnim)
            { base.Animation(null, 1.15f, animTime);
                isAnim = true;
            }
        }
    }
    protected override void Awake()
    {
        JavaCallUnity.Instance.BannerCallBackAction += SetAnimTime;
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
        smallButton.onClick.RemoveAllListeners();
    }
    bool isShow = false;
    RectTransform rectTransform;
    public void SetTransForm()
    {
        if (rectTransform == null)
        {
            rectTransform = backTf.GetComponent<RectTransform>();
        }
        rectTransform.anchorMin = new Vector2(0.5f, 1);
        rectTransform.anchorMax = new Vector2(0.5f, 1);
        rectTransform.DOAnchorPosY(-365, 0.8f);
    }
    /// <summary>
    /// 红包初始化
    /// </summary>
    /// <param name="titleString">标题</param>
    /// <param name="hongbaoNumber">红包金额</param>
    /// <param name="clickAction">点击领取</param>
    /// <param name="closeAction">点击关闭</param>
    public void InitHongBao(int count, Action clickAction = null, Action closeAction = null, Action smallAction = null)
    {
        if (isShow) return;
        isShow = true;
        isAnim = false;
        //SetTransForm();
        gameObject.SetActive(true);
        backTf.localScale = Vector3.zero;
        MaiDianAndSound();
        transform.SetAsLastSibling();

        animTime = 0.3f;
        ButtonAnim();
        //titleText.text = titleString;
        hongbaoNumberText.text ="+"+ count.ToString() + "元";
        //if (JavaCallUnity.Instance.IsFirstGetRedValue())
        //{
        //    go_icon.SetActive(false);
        //}
        //else
        //{
        //    go_icon.SetActive(true);
        //}
        sureButton.onClick.AddListener(() =>
        {
            StopButtonAnim();
            clickAction?.Invoke();
            AndroidAdsDialog.Instance.CloseBanner();
            isShow = false;
            // Destroy(gameObject);
        });

        closeButton.onClick.AddListener(() =>
        {
            StopButtonAnim();
            isShow = false;
               closeAction?.Invoke();
            AndroidAdsDialog.Instance.CloseBanner();
            AndroidAdsDialog.Instance.ShowTableVideo("0");
            //  Destroy(gameObject);
        });
        smallButton.onClick.AddListener(() =>
        {
            StopButtonAnim();
            isShow = false;
            smallAction?.Invoke();
            AndroidAdsDialog.Instance.CloseBanner();
            AndroidAdsDialog.Instance.ShowTableVideo("0");
            //  Destroy(gameObject);
        });
        //closeButton.gameObject.SetActive(false);
        //StartCoroutine(Global.Delay(1.5f, () => closeButton.gameObject.SetActive(true)));
        //hongbaoTransform.localScale = Vector3.zero;
        //hongbaoTransform.DOScale(Vector3.one * 1.1f, 0.5f).SetUpdate(true).onComplete
        //     = () =>
        //     {
        //         hongbaoTransform.DOScale(Vector3.one * 1f, 0.3f).SetUpdate(true).onComplete
        //         = () =>
        //         {
        //             lightImage.transform.DOLocalRotate(new Vector3(0, 0, -360 * 5000.0f), 5.0f * 5000, RotateMode.LocalAxisAdd);//.SetLoops(-1, LoopType.Restart);
        //         };
        //     };

        if (!AndroidAdsDialog.Instance.isBroadcast)
        {
            base.Animation(null, 1.15f, animTime);
        }

    }
    public Transform buttonTf;
    Sequence queen;
    public void ButtonAnim()
    {
        queen = DOTween.Sequence();
        queen.Append(buttonTf.DOScale(1.2f, 1.0f));
        queen.AppendInterval(0.3f);
        queen.Append(buttonTf.DOScale(1.0f, 1.0f));
        queen.SetLoops(-1);
    }
    public void StopButtonAnim()
    {
        if (queen != null)
        {
            queen.Kill();
            buttonTf.localScale = Vector3.one;
        }
    }
    private static void MaiDianAndSound()
    {
        AndroidAdsDialog.Instance.UploadDataEvent("show_new_shengji_redpacket");
        AndroidAdsDialog.Instance.UploadDataEvent("show_shengjihblast");
        AndroidAdsDialog.Instance.ShowBannerAd();
        AudioManager.Instance.PlaySound("show_redpacket");
        AndroidAdsDialog.Instance.requestSJHBWithDrawList();
        AndroidAdsDialog.Instance.CloseFeedAd();
    }
    public void RecoverCanShow()
    {
        isShow = false;
    }
    public void HideUI()
    {
        Hide();
    }
}
