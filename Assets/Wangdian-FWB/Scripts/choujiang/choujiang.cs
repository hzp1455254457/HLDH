using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choujiang : MonoBehaviour
{
    public Button choujiangButton, freeChoujiangButton;
    public int choujiangbiNumber
    {
        get
        {
            return _choujiangbiNumber;
        }
        set
        {
            _choujiangbiNumber = value;
            choujiangbiNumberText.text = "��ӵ�У�" + _choujiangbiNumber + "ö";
            PlayerData.Instance.ChouJiangCount = _choujiangbiNumber;
        }
    }

    private int _choujiangbiNumber;

    private int timer;
    private float rewardFactor;
    public Text choujiangbiNumberText;

    public GameObject choujiangPrefab;
    // Start is called before the first frame update
    void Start()
    {
        choujiangButton.onClick.AddListener(() =>
        {
            if (choujiangbiNumber > 0)
            {
                choujiangbiNumber--;
                Instantiate(choujiangPrefab);
                tipsManager.Instance.uiTransform.GetComponent<Canvas>().enabled = false;
            }
        });

        freeChoujiangButton.onClick.AddListener(() =>
        {
            AndroidAdsDialog.Instance.UploadDataEvent("show_choujiang_ad");
            AndroidAdsDialog.Instance.ShowRewardVideo("��ѳ齱", () =>
            {
                //fingerControlGameObject.SetActive(false);
                AndroidAdsDialog.Instance.UploadDataEvent("finish_choujiang_ad");
                Instantiate(choujiangPrefab);
                tipsManager.Instance.uiTransform.GetComponent<Canvas>().enabled = false;
                //ButtonFade();

            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (!userData.Instance.dataInitialed)
            userData.Instance.InitData();

        //�ڶ��ն��2ö
        if (PlayerData.Instance.day == 2 && !DataSaver.Instance.HasKey("isDayTwoGet"))
        {
            DataSaver.Instance.SetString("isDayTwoGet", "yes");
            PlayerData.Instance.ChouJiangCount += 2;
        }

        if (PlayerData.Instance.day == 7 && !DataSaver.Instance.HasKey("isDaySevenGet"))
        {
            DataSaver.Instance.SetString("isDaySevenGet", "yes");
            PlayerData.Instance.ChouJiangCount += 2;
        }

        //�ر�UI
        //DaimondTaskUI.Instance.Show(false);

        choujiangbiNumber = PlayerData.Instance.ChouJiangCount;

        rewardFactor = PlayerData.Instance.actorDateList.Count * (1 + Mathf.Abs(10 - choujiangbiNumber));

        timer = PlayerData.Instance.chouJiangTime;

        choujiangbiNumberText.text = "��ӵ��:"+choujiangbiNumber+"ö";
    }

    private void LateUpdate()
    {
        choujiangbiNumber = PlayerData.Instance.ChouJiangCount;

        rewardFactor = PlayerData.Instance.actorDateList.Count * (1 + Mathf.Abs(10 - choujiangbiNumber));

        timer = PlayerData.Instance.chouJiangTime;

        choujiangbiNumberText.text = "��ӵ��:" + choujiangbiNumber + "ö";
    }
}
