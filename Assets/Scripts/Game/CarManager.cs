using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CarManager : MonoBehaviour
{[Header("商品价值")]
    public Text text;
    [Header("商品价值按钮上Text")]
    public Text goldtext;
    public Animator animator,btnAnim,carAnim;
    public GameObject go,btnGo;
    public Graphic[] graphics;
    public Transform target, currentInitTf, initTf1;
    int goldCount;
    public Button btn;
    public Graphic[] zidonggraphics;
    public Transform zidongTf;
    public Slider slider;
    public Text zidongtext;
    public ZiDongFaHuo ziDongFaHuo;
  public void StartEvent(int value)
    {
        SetText(value.ToString());
        SetAnim(true);
        goldCount = value;
    }
    public void SetCount(int value)
    {
        goldCount = value;
    }
    public void EndEvent()
    {
       
        SetAnim(false);
      
        EndCarAnim();
    }
    private void EndCarAnim()
    {
        ProduceQiPaoManager.Instance.RemoveAllInCar();
        SetPosToTarget(transform.position, target.localPosition, 2f, ()=> {StartCoroutine( Global.Delay(1F, StartCarAnim));
            
            ShowGetGoldBtn(true);
            if(GuideManager.Instance.isFirstGame)
            FaHuoPanel.Instance.ShowTips();
            else
           // SetZiDongFaHuoTips();
            go.SetActive(false);
        });
    }
    public void SetZiDongFaHuoTips()
    {
        zidongTf.gameObject.SetActive(true);
        zidongtext.text = string.Format("{0}/{1}", PlayerData.Instance.ClickFaHuoRedCount, ZiDongFaHuo.count);
        slider.value = PlayerData.Instance.ClickFaHuoRedCount /(float) ZiDongFaHuo.count;
        foreach (var item in zidonggraphics)
        {
            item.DOFade(1, 0.5f);
        }
    }
    public IEnumerator HideZiDongFaHuoTips(bool value)
    {
        if (value)
        {
            //大世界状态刷新
            if (PlayerData.Instance.ClickFaHuoRedCount +1 == ZiDongFaHuo.count)
            {
                PlayerData.Instance.ClickFaHuoRedCount++;
                BigWorldData.IsBigWorldUnlocked = true;
            }
            else
            {
                PlayerData.Instance.ClickFaHuoRedCount++;
            }
            if (PlayerData.Instance.ClickFaHuoRedCount == 1)
            {
                FaHuoPanel.Instance.newWangDianIcon.ShowGuide(true);
            }
        }
           yield return new WaitForSeconds(0.5f);
       // slider.DOValue(PlayerData.Instance.ClickFaHuoRedCount / (float)ZiDongFaHuo.count, 1f).onComplete += () => { zidongtext.text = string.Format("{0}/{1}", PlayerData.Instance.ClickFaHuoRedCount, ZiDongFaHuo.count);
        //    AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.Warning);

        //};
        //slider.DOValue()
       // yield return new WaitForSeconds(1f);
        //foreach (var item in zidonggraphics)
        //{
        //    item.DOFade(0, 1f);
        //}
        //StartCoroutine(Global.Delay(1f, () => { zidongTf.gameObject.SetActive(false);
        //    if (PlayerData.Instance.ClickFaHuoRedCount >= ZiDongFaHuo.count)
        //        ziDongFaHuo.StartZiDongFaHuo();
        //}));
    }
    private void ShowGetGoldBtn(bool value)
    {
        btnGo.SetActive(value);
        SetBtnAnim(value);
    }
    int countClick = 0;
   public void  GetGoldBtnEvent()
    {
        if (GuideManager.Instance.isFirstGame)
        {
            countClick++;
            if (countClick == 1)
            {
                AndroidAdsDialog.Instance.UploadDataEvent("new_version_guide5");
                //AwardManagerNew.Instance.ShowUI(goldCount, NumberGenenater.GetRedCount(), 0,
                // () =>
                // {
                //     ToggleManager.Instance.ShowUI(); PeopleEffect.Instance.ShowMask();
                //     PeopleEffect.Instance.SetTips(ToggleManager.Instance.guiTaget3,ToggleManager.Instance.guiTaget4.position, true, RotaryType.TopToBottom);
                //     StartCoroutine(HideZiDongFaHuoTips(true));
                // },
                //   () =>
                //   {
                //       ToggleManager.Instance.ShowUI(); PeopleEffect.Instance.ShowMask();
                //       PeopleEffect.Instance.SetTips(ToggleManager.Instance.guiTaget3, ToggleManager.Instance.guiTaget4.position, true, RotaryType.TopToBottom);
                //       StartCoroutine(HideZiDongFaHuoTips(false));
                //   }
                // );
                ResourceManager.Instance.ShowGuideRed1(()=> {
                    //StartCoroutine(HideZiDongFaHuoTips(true));
                    ResourceManager.Instance.ShowGuideRed2(300, () => { PlayerData.Instance.GetRed(300 * (int)MoneyManager.redProportion, false);
                        PeopleEffect.Instance.ShowMask(0.5f);
                        MoneyManager.Instance.ShowRedTiXianGuide();
                    });
                });
            }
            else
            {
               
                  AwardManagerNew.Instance.ShowUI(goldCount, NumberGenenater.GetRedCount(),0,()=> {
                    
                    StartCoroutine(HideZiDongFaHuoTips(true));
                  
                },

                    () => { StartCoroutine(HideZiDongFaHuoTips(false)); }


                    );
            }
        }
        else
        {
            AwardManagerNew.Instance.ShowUI(goldCount, NumberGenenater.GetRedCount(), 0, () => {

                StartCoroutine(HideZiDongFaHuoTips(true));
                
            },

                    () => { StartCoroutine(HideZiDongFaHuoTips(false)); }


                    );
        
    }
        AndroidAdsDialog.Instance.UploadDataEvent("click_get_coins_infahuo");
        ShowGetGoldBtn(false);
       
        FaHuoPanel.Instance.faHuoTiMing.RecoverFaHuo();
    }
    public void SetBtnAnim(bool value)
    {
       // btnAnim.SetBool("walk", value);
    }
    private void StartCarAnim()
    {
        StartCoroutine(Global.Delay(0.2f, () => ProduceQiPaoManager.Instance.RemoveList()));
        print("执行StartCarAnim");
        SetPosToTarget(initTf1.position, currentInitTf.localPosition, 2f, null);

    }
    private void SetPosToTarget(Vector3 vector3,Vector3 target,float time,UnityEngine.Events.UnityAction unityAction)
    {
        transform.position = vector3;
        transform.DOLocalMove(target, time).onComplete+=()=> { unityAction?.Invoke(); };
    }
    public void SetText( string value)
    {
        go.SetActive(true);
       // Global.Fade(text, 1, 0.1f);
        text.text =string.Format("总价值:{0}", value) ;
        goldtext.text = value;
    }
    public void SetAnim(bool value)
    {
        animator.SetBool("walk", value);
        carAnim.SetBool("walk", value);
       
            carAnim.enabled = value;
       
       // animator.speed = 10;
    }
    private void Start()
    {
        btn.onClick.AddListener(GetGoldBtnEvent);
        zidonggraphics = zidongTf.GetComponentsInChildren<Graphic>();
    }
}
