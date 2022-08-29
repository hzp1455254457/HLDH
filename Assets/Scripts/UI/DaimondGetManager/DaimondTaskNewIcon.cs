using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaimondTaskNewIcon : MonoBehaviour
{
    // Start is called before the first frame update
    public Text daimondCount,showCount;
    public GameObject[] status;
    public Slider slider;
    public Image image;
    public RedConFig redConFig;
    public Button button;
    public Sprite[] sprites;
    private void Start()
    {
        //redConFig = ConfigManager.Instance.GetRedConfig(PlayerData.Instance.daimondTaskID);
        Init();
        PlayerData.Instance.ClickDaimondAction += RefreshCount;
        button.onClick.AddListener(ClickFun);
        UnityActionManager.Instance.AddAction("RefreshDaimondCount", Init);
    }

    private void Init()
    {
        redConFig = ConfigManager.Instance.GetRedConfig(PlayerData.Instance.daimondTaskID);
        if (redConFig != null)
        {
            RefreshCount();
            daimondCount.text = redConFig.NeedCount.ToString()+"��";
            showCount.text = redConFig.ShowCount.ToString()+"Ԫ";
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void RefreshCount()
    {
        slider.value = PlayerData.Instance.getDaimondCount / (float)redConFig.NeedCount;
        if (PlayerData.Instance.getDaimondCount>= redConFig.NeedCount)
        {
            SetStatus(true);
            image.sprite = sprites[0];
            ButtonAnim();
        }
        else
        {
            SetStatus(false);
            image.sprite = sprites[1];
            StopButtonAnim();
        }
    }
    private void SetStatus(bool value)
    {
        if (status[0].activeSelf != value)
        { status[0].SetActive(value);
            //image.sprite = sprites[1];
        }
        if (status[1].activeSelf != !value)
        { status[1].SetActive(!value);
            //image.sprite = sprites[0];
        }
        
    }
    public void ClickFun()
    {
        if(PlayerData.Instance.getDaimondCount>= redConFig.NeedCount)
        {
            StopButtonAnim();
            hongbao6.Instance.ShowUI((int)(PlayerData.Instance.FirstTableEcpm*0.3*100), 0, 1, () => {
                PlayerData.Instance.daimondTaskID++;
                StopButtonAnim();
                PlayerData.Instance.FirstTableEcpm = 0;
                if (redConFig != null)
                {
                    PlayerData.Instance.getDaimondCount = 0;

                }
                else
                {
                    
                }
                UnityActionManager.Instance.DispatchEvent("RefreshDaimondCount");

            }, "����ʯ����");
           
        }
        else
        {
            AndroidAdsDialog.Instance.ShowToasts(ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, "��������Ŷ!", Color.black, null, null, 1f);
           // Debug.LogError("��������");
        }
    }
    private void OnDestroy()
    {
        PlayerData.Instance.ClickDaimondAction -= RefreshCount;
    }
    public Transform buttonTf;
    Sequence queen;
    public void ButtonAnim()
    {if (queen == null)
        {
            queen = DOTween.Sequence();
            queen.Append(buttonTf.DOScale(1.2f, 0.8f));
            //queen.AppendInterval(0.3f);
            queen.Append(buttonTf.DOScale(1.0f, 0.8f));
            queen.SetLoops(-1);
        }
    }
    public void StopButtonAnim()
    {
        if (queen != null)
        {
            queen.Kill();
            buttonTf.localScale = Vector3.one;
            queen = null;
        }
    }
}
