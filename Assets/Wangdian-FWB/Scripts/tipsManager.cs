using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class tipsManager : MonoBehaviour
{
    public Transform uiTransform,contentTransform,jiaochengTransform;
    public static tipsManager Instance;

    public Transform middleUiTransform;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    public GameObject lingquDialog;
    /// <summary>
    /// 打开领取对话框
    /// </summary>
    /// <param name="lingquAction"></param>
    /// <param name="parent"></param>
    public void openLingQuDialog(Action lingquAction,Transform parent = null)
    {
        GameObject obj = Instantiate(lingquDialog, parent != null ? parent : uiTransform);
        obj.GetComponent<lingquDialogConfig>().InitLingQuDialog(()=> {
            lingquAction?.Invoke();
        });
    }

    public GameObject tip;
    /// <summary>
    /// 打开提示
    /// </summary>
    /// <param name="endAction"></param>
    /// <param name="parent"></param>
    public void createTips(Action endAction = null,Transform parent = null)
    {
        GameObject obj = Instantiate(tip, parent != null ? parent : uiTransform);
        obj.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 800), 0.8f).SetEase(Ease.OutQuint).onComplete = () =>
        {
            Graphic[] graphics = obj.GetComponentsInChildren<Graphic>();
            foreach (Graphic one in graphics)
            {
                one.DOFade(0, 0.8f).onComplete = () =>
                {
                    Destroy(obj);
                    endAction?.Invoke();
                };
            }
        };
    }

    public GameObject jiasuPanel;
    /// <summary>
    /// 打开加速对话框
    /// </summary>
    public void openJiaSuDialog(Action jiasuAction = null, Transform parent = null)
    {
        AndroidAdsDialog.Instance.ShowFeedAd(1);
        GameObject obj = Instantiate(jiasuPanel, parent != null ? parent : uiTransform);
        obj.GetComponent<jiasuDialogConfig>().InitJiaSuDialog(()=>
        {
            jiasuAction?.Invoke();
            Destroy(obj);
        });
    }

    public GameObject huojiaPanel;
    /// <summary>
    /// 打开加速对话框
    /// </summary>
    public void openHuoJiaDialog(Action jiasuAction = null, Transform parent = null)
    {
        AndroidAdsDialog.Instance.UploadDataEvent("show_huojiabuzu");
        AndroidAdsDialog.Instance.ShowFeedAd(1);
        GameObject obj = Instantiate(huojiaPanel, parent != null ? parent : uiTransform);
        obj.GetComponent<jiasuDialogConfig>().InitJiaSuDialog(() =>
        {   
            AndroidAdsDialog.Instance.UploadDataEvent("click_huojiabuzu");
            jiasuAction?.Invoke();
            Destroy(obj);
        },()=>
        {
            AndroidAdsDialog.Instance.UploadDataEvent("close_huojiabuzu");
            /*
            Destroy(FindObjectOfType<singleTaskDetailPanelConfig>().gameObject);
            Destroy(FindObjectOfType<taskDetailPanelConfig>().gameObject);
            */
        });
    }

    public GameObject shopPanel;
    /// <summary>
    /// 打开售卖商品页面
    /// </summary>
    /// <param name="config"></param>
    /// <param name="showFinger"></param>
    /// <param name="parent"></param>
    public void openShopPanel(shopItemCellConfig config, bool showFinger = false,int preCellitemID = 0,Transform parent = null)
    {
        GameObject obj = Instantiate(shopPanel, parent != null ? parent : uiTransform);
        obj.GetComponent<shopDialogConfig>().InitShopItemCellConfig(config,showFinger, preCellitemID);
    }

    public GameObject taskDetailPanel;
    /// <summary>
    /// 打开任务面板
    /// </summary>
    /// <param name="parent"></param>
    public void openTaskDetailPanel(Transform parent = null)
    {
        GameObject obj = Instantiate(taskDetailPanel, parent != null ? parent : uiTransform);
        obj.GetComponent<taskDetailPanelConfig>().InitTaskDetailPanelConfig();
    }

    public GameObject singleTaskDetailPanel;
    /// <summary>
    /// 打开单独任务面板
    /// </summary>
    /// <param name="task"></param>
    /// <param name="parent"></param>
    public void openSingleTaskDetailPanel(taskOrder task,Transform parent = null)
    {
        GameObject obj = Instantiate(singleTaskDetailPanel, parent != null ? parent : uiTransform);
        obj.GetComponent<singleTaskDetailPanelConfig>().InitSingleTaskDetailPanelConfig(task);
    }

    public GameObject goldNotEnough;
    /// <summary>
    /// 金币不足面板
    /// </summary>
    /// <param name="clickAction"></param>
    /// <param name="closeAction"></param>
    /// <param name="parent"></param>
    public void openGoldNotEnoughPanel(Action clickAction,Action closeAction = null, Transform parent = null)
    {
        AndroidAdsDialog.Instance.UploadDataEvent("show_jinbi_less_in_newshop");
        AndroidAdsDialog.Instance.ShowFeedAd(1);
        GameObject obj = Instantiate(goldNotEnough, parent != null ? parent : uiTransform);
        obj.GetComponent<goldNotEnough>().InitGoldEnoughPanel(() => clickAction?.Invoke(),()=>closeAction?.Invoke());
    }

    public GameObject orderFinishPanel;
    /// <summary>
    /// 订单结束面板，领取奖励
    /// </summary>
    /// <param name="order"></param>
    /// <param name="clickAction"></param>
    /// <param name="closeAction"></param>
    /// <param name="parent"></param>
    public void openOrderFinishPanel(taskOrder order,Action clickAction = null, Action closeAction = null, Transform parent = null)
    {
        GameObject obj = Instantiate(orderFinishPanel, parent != null ? parent : uiTransform);
        obj.GetComponent<orderFinishPanelConfig>().InitOrderFinishPanel(order,()=>
        {
            //userData.Instance.taskOrderList.Find((t) => t.order_id == order.order_id).is_begin_mission = 1;
            userData.Instance.currentOrderID++;
            userData.Instance.refreshOneOrder();
            FindObjectOfType<taskDetailPanelConfig>().InitTaskDetailPanelConfig();
        });
    }

    public GameObject shouqianPiaochuangObject;
    private Sprite diamondSprite;
    /// <summary>
    /// 收钱时候的飘窗
    /// </summary>
    /// <param name="p"></param>
    /// <param name="xinyuValve"></param>
    /// <param name="parent"></param>
/*    public void createShouQianPiaoChuang(Produce p,int itemNumber ,int xinyuValve, Transform parent = null)
    {
        GameObject obj = Instantiate(shouqianPiaochuangObject, parent != null ? parent : uiTransform);
        obj.GetComponent<shouqianpiaochuang>().Show(middleUiTransform, new string[] { "+" + itemNumber.ToString(), "+" + xinyuValve.ToString() },
            new Sprite[] { ResourceManager.Instance.GetSprite(p.item_pic),diamondSprite},
            null,
            new Color[] {Color.black,Color.black});
    }*/

    public Sprite crystalSprite, hongbaoSprite;
    public GameObject shouqianPiaoChuangObject2;

    public Sprite[] xinyuSprite;
    /// <summary>
    /// 创建多重飘窗
    /// </summary>
    /// <param name="p"></param>
    /// <param name="itemNumber"></param>
    /// <param name="xinyuValve"></param>
    /// <param name="diamondNumber"></param>
    /// <param name="hongbaoNumber"></param>
    /// <param name="parent"></param>
    public void createShouQianPiaoChuang(Produce p, int itemNumber, int xinyuValve, int diamondNumber, int hongbaoNumber, Transform parent = null)
    {
        if (userData.Instance.isInJiaoCheng)
            parent = jiaochengTransform;
        GameObject obj = Instantiate(shouqianPiaoChuangObject2, parent != null ? parent : uiTransform);
        diamondSprite = xinyuSprite[FindObjectOfType<wangdianUserPanelConfig>().levelGrade - 1];
        obj.GetComponent<shouqianpiaochuang>().Show(middleUiTransform, new string[] { "+" + itemNumber.ToString(), "+" + xinyuValve.ToString(), "+" + diamondNumber.ToString(), "+" + hongbaoNumber.ToString() },
            new Sprite[] { ResourceManager.Instance.GetSprite(p.item_pic), diamondSprite, crystalSprite, hongbaoSprite },
            null,
            new Color[] { Color.black, Color.black, Color.black, Color.black });
    }

    public GameObject wenzipiaochuang;
    /// <summary>
    /// 创建文字飘窗
    /// </summary>
    /// <param name="str"></param>
    /// <param name="parent"></param>
    public void createPiaoChuang(string str, Transform parent = null)
    {
        GameObject obj = Instantiate(wenzipiaochuang, parent != null ? parent : uiTransform);
        obj.GetComponentInChildren<Text>().text = str;
        obj.GetComponent<RectTransform>().DOAnchorPosY(middleUiTransform.position.y, 0.8f).SetEase(Ease.OutQuint).onComplete = () => Destroy(obj);
    }

    public GameObject finishTips;
    /// <summary>
    /// 创建文字飘窗
    /// </summary>
    /// <param name="str"></param>
    /// <param name="parent"></param>
    public void createFinishTips(string str, Transform parent = null)
    {
        GameObject obj = Instantiate(finishTips, parent != null ? parent : uiTransform);
        obj.GetComponentInChildren<Text>().text = str;
        obj.GetComponent<RectTransform>().DOAnchorPosY(middleUiTransform.position.y, 0.8f).SetEase(Ease.OutQuint).onComplete = () => Destroy(obj);
    }

    public GameObject singleOrderFinishPanel;
    /// <summary>
    /// 订单任务结束页面
    /// </summary>
    /// <param name="parent"></param>
    public void createSingleOrderFinishPanel(Transform parent = null)
    {
        AndroidAdsDialog.Instance.UploadDataEvent("show_new_ordermission_reward");
        GameObject obj = Instantiate(singleOrderFinishPanel, parent != null ? parent : uiTransform);
        obj.GetComponent<SingleOrderFinishPanelConfig>().InitHongBao(()=>
        {
            AndroidAdsDialog.Instance.UploadDataEvent("click_ordermission_video_reward");
            AndroidAdsDialog.Instance.ShowRewardVideo("订单奖励激励视频", () => {
                AndroidAdsDialog.Instance.UploadDataEvent("finish_ordermission_video_reward");
                createSingleOrderRewardPanel(JavaCallUnity.Instance.jiliEcpm/1000.0f * 0.3f * 100);
            });
        }, () => {
            AndroidAdsDialog.Instance.UploadDataEvent("click_ordermission_normal_reward");
            createSingleOrderRewardPanel(0.1f);
        });
    }

    public GameObject singleOrderRewardPanel;
    /// <summary>
    /// 任务奖励页面
    /// </summary>
    /// <param name="parent"></param>
    public void createSingleOrderRewardPanel(float reward, Transform parent = null)
    {
        GameObject obj = Instantiate(singleOrderRewardPanel, parent != null ? parent : uiTransform);
        obj.GetComponent<singleOrderRewardPanelConfig>().InitRewardHongBao(reward, () => {

            if(FindObjectOfType<taskCell>()!=null)
            FindObjectOfType<taskCell>().gameObject.SetActive(false);
            //userData.Instance.taskOrderList.Find((t) => t.order_id == userData.Instance.currentOneOrder.order_id).is_begin_mission = 1;
            userData.Instance.currentOrderID++;
            userData.Instance.refreshOneOrder();
            //Debug.Log("关闭，收下红包");
        });
    }

    /// <summary>
    /// 网店场景的UI隐藏与显示，用于外部调用
    /// </summary>
    /// <param name="status"></param>
    public void showUI(bool status)
    {
        uiTransform.GetComponent<Canvas>().enabled = status;
        contentTransform.GetComponent<Canvas>().enabled = status;
    }

}
