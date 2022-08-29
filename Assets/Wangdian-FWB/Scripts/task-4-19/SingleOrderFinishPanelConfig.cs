using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleOrderFinishPanelConfig : PanelAnimation
{
    public Text hongbaoText;
    public Button videoClickButton,lowClickButton,closeButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 初始化订单结束页面
    /// </summary>
    /// <param name="videoClickAction"></param>
    /// <param name="lowClickAction"></param>
    public void InitHongBao(System.Action videoClickAction = null,System.Action lowClickAction=null)
    {
        AndroidAdsDialog.Instance.ShowBannerAd();

        hongbaoText.text = Random.Range(20.0f, 60.0f).ToString("F3")+"元";

        videoClickButton.onClick.AddListener(() =>
        {
            AndroidAdsDialog.Instance.CloseBanner();
            videoClickAction?.Invoke();
            Destroy(gameObject);
        });

        lowClickButton.onClick.AddListener(() =>
        {
            AndroidAdsDialog.Instance.CloseBanner();
            lowClickAction?.Invoke();
            Destroy(gameObject);
        });

        closeButton.onClick.AddListener(() =>
        {
            AndroidAdsDialog.Instance.CloseBanner();
            Destroy(gameObject);
        });
        base.Animation();
    }
}
