using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurseManager :MonoBehaviour
{
    // Start is called before the first frame update
    public Text count,level;
    public Button GetButton;
    public bool isAutoOpen = true;
    public void SetCount()
    {
      //  count.text = string.Format("{0}个", PlayerDate.Instance.temporaryDiamond);

    }
    void Start()
    {
       // level.text = string.Format("{0}级打赏", PlayerDate.Instance.burseConfig.wallet_level);
       // PlayerDate.Instance.temporaryDaimondAction += SetCount;
        GetButton.onClick.AddListener(GetDaimond);
        SetCount();
        // burseConfig = PlayerDate.Instance.burseConfig;
    }
    public void RefreshLevel()
    {
       // level.text = string.Format("{0}级打赏", PlayerDate.Instance.burseConfig.wallet_level);
    }
    //public int daimondCount;
    //public int maxCount=1200;
   // public BurseConfig burseConfig;
    // Update is called once per frame
    public void GetDaimond()
    {
     //   AndroidAdsDialog.Instance.UploadDataEvent("get_actor_wallet");
        //PlayerDate.Instance.GetDiamond(PlayerDate.Instance.temporaryDiamond);
       // PlayerDate.Instance.GetTemporaryDiamond(-PlayerDate.Instance.temporaryDiamond);
       // Time.timeScale = 1;
        HideBurse();
      
    }
    public void ShowBurse()
    {

    }
    public void CloseBurse()
    {
        isAutoOpen = false;
        HideBurse();
    }
    public void HideBurse()
    {
        AndroidAdsDialog.Instance.CloseFeedAd();
       //  AndroidAdsDialog.Instance.ShowTableVideo("0");
        gameObject.SetActive(false);
    }
   
}
