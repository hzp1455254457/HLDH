using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wangdianjiaocheng : MonoBehaviour
{
    public GameObject fingerAnimation;
    public Camera jiaochengCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (!userData.Instance.dataInitialed)
            userData.Instance.InitData();
        StartCoroutine(waitForFisrtEnterCheck());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �ȴ���֤�Ƿ��ǵ�һ�δ�
    /// </summary>
    /// <returns></returns>
    IEnumerator waitForFisrtEnterCheck()
    {
        yield return new WaitUntil(() => userData.Instance.IsFirstEnter != 2);
        
        gameObject.SetActive(userData.Instance.IsFirstEnter == 0);
        //ֻ�е���0ʱ�򣬲��ǵ�һ�ν���̳�
        if (userData.Instance.IsFirstEnter == 0)
        {
            AndroidAdsDialog.Instance.ShowBannerAd();
            userData.Instance.isInJiaoCheng = true;
            /*ToggleManager.Instance.ShowMask(true);*/
            step = 0;
        }
    }

    private int step
    {
        get {
            return _step;
        }
        set {
            _step = value;
            ShowStep();
        }
    }

    private int _step = -1;

    /// <summary>
    /// ��ȷ����ʵ��Ȼ��ȷ���������䣬Ȼ�����������ԣ�Ȼ������жϹ�ׯ��ͼ
    /// </summary>
    public void ShowStep()
    {
        Debug.Log("��ǰstep:" + step);
        switch (step)
        {
            case 0:
                DataSaver.Instance.SetInt("HasEnterWangDian", 1);

                RectTransform t3 = GameObject.Find("jiaochengRect3").GetComponent<RectTransform>();
                GetComponent<GuideMask>().inner_trans = t3;
                fingerAnimation.SetActive(false);
                //fingerAnimation.transform.position = t3.position;
                break;
            case 1:
                Debug.Log("����");
                RectTransform t = GameObject.Find("jiaochengRect1").GetComponent<RectTransform>();
                GetComponent<GuideMask>().inner_trans = t;
                Debug.Log("T Pos" + t.position);
                fingerAnimation.transform.position = GameObject.Find("fingerPos").transform.position;
                fingerAnimation.SetActive(true);
                break;
            case 2:
                Debug.Log("�̳�2");
                FindObjectOfType<shopDialogConfig>().GetComponentInChildren<ScrollRect>().vertical = false;
                GameObject tt = FindObjectOfType<shopDialogConfig>().shopCells[0];
                RectTransform t1 = tt.GetComponent<shopItemCell>().shopButton.transform.GetChild(0).GetComponent<RectTransform>();
                GetComponent<GuideMask>().inner_trans = t1;
                fingerAnimation.SetActive(true);
                break;
            case 3:
                Debug.Log("�̳�3");
                RectTransform t2 = GameObject.Find("jiaochengRect1").GetComponent<RectTransform>();
                fingerAnimation.SetActive(false);
                fingerAnimation.transform.position = GameObject.Find("fingerPos").transform.position;
                GetComponent<GuideMask>().inner_trans = t2;
                fingerAnimation.SetActive(true);
                break;
            /*     
             case 4:
                 fingerAnimation.SetActive(false);
                 GameObject obj = FindObjectOfType<taskDetailPanelConfig>().taskDetailCellList[0];
                 GetComponent<GuideMask>().inner_trans = obj.GetComponent<RectTransform>();
                 break;
            */
            case 4:
                Debug.Log("�̳�4");
                RectTransform t4 = GameObject.Find("jiaochengRect3").GetComponent<RectTransform>();
                fingerAnimation.SetActive(false);
                fingerAnimation.transform.position = t4.position;
                GetComponent<GuideMask>().inner_trans = t4;
                
             //FindObjectOfType<wangdianUserPanelConfig>().fingerAnimation.SetActive(true);
             break;
            case 5:

                userData.Instance.isInJiaoCheng = false;
                userData.Instance.IsFirstEnter = 1;
                transform.parent.GetComponent<Canvas>().enabled = false;
                break;
            default:
                break;
        }
    }

    public void NextStep()
    {
        step++;
        Debug.Log("currentStep��" + step);
    }
    public void SetStep(int i)
    {
        step = i;
    }

}
