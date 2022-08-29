using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System;
using Spine.Unity;

public class PeopleEffect : MonoBehaviour
{
    public static PeopleEffect Instance;
    public GameObject qipao;
    public GameObject people;
   public RectTransform peopleRtf;
    public Image peopleImg;
      public  Text text;
    Graphic mask;
    public Button jixuGo;
    public ToggleManager toggleManager;
    public GameObject tips,tip;
    public GameObject tips1;
    public GuideMask guideMask;
    public RectTransform rectTransform1;
    public HLDH.ShopPanel shopPanel;
    public GameObject JiaSuGo;
    public GameObject cankuGuideGo, cankuGuideGo1;
    private void Awake()
    {
        Instance = this;
           mask = GetComponent<Graphic>();
        guideMask = GetComponent<GuideMask>();
        tips = Resources.Load<GameObject>("Prefab/Effect/Tips");
        tip = Resources.Load<GameObject>("Prefab/Effect/Tip");
        //peopleRtf = people.GetComponent<RectTransform>();
       
       // Init();
        gameObject.SetActive(false);
    }
    void Start()
    {
        
        jixuGo.onClick.AddListener(JiXuEvent);
    }
    public void LeftShow(string value ,UnityAction unityAction=null, UnityAction unityAction1 = null, float time = 1f)
    {
        //mask.DOFade(0.8f, 0.5f).onComplete += () => people.SetActive(true);
        ShowPeople(new Vector3(-1, 1, 1), unityAction, unityAction1,time);
        people.transform.localScale = new Vector3(-1, 1, 1);
     //qipao.transform.localScale = new Vector3(-1, 1, 1);
        peopleRtf.anchorMax = new Vector2(0, 0);
        peopleRtf.anchorMin = new Vector2(0, 0);
        peopleRtf.anchoredPosition = new Vector2(peopleRtf.rect.width / 2f, peopleRtf.rect.height / 2f);
        text.text = value;
        text.transform.localScale = new Vector3(1, 1, 1);
    }
    public void RightShow(string value, UnityAction unityAction=null, UnityAction unityAction1=null, float time = 1f)
    {
        ShowPeople(new Vector3(-1, 1, 1), unityAction, unityAction1,time);
        //mask.DOFade(0.8f, 0.5f).onComplete += ()=>people.SetActive(true);
        people.transform.localScale = new Vector3(1, 1, 1);
      // qipao.transform.localScale = new Vector3(-1, 1, 1);
        peopleRtf.anchorMax = new Vector2(1, 0);
        peopleRtf.anchorMin= new Vector2(1, 0);
        peopleRtf.anchoredPosition = new Vector2(-peopleRtf.rect.width / 2f, peopleRtf.rect.height / 2f);
        text.text = value;
        text.transform.localScale = new Vector3(-1, 1, 1);
    }
  public void ShowJiaSu()
    {
        JiaSuGo.gameObject.SetActive(true);
    }
    public void HideJiaSu()
    {
        JiaSuGo.gameObject.SetActive(false);
    }
    private void ShowPeople(Vector3 vector,UnityAction unityAction,UnityAction unityAction1=null,float time=1f)
    {
        mask.DOFade(0.8f, 0.5f).onComplete += () => { people.SetActive(true);
            if (unityAction1 != null)
            {
                unityAction1();
            }
            peopleImg.DOFade(1, time).onComplete += () => ShowQipao(vector, unityAction); };
      //  peopleImg.DOFade(1, 1f).onComplete+= () => ShowQipao();
    }
    public void ShowMask(float time=0.5f,UnityAction unityAction=null)
    {
        gameObject.SetActive(true);
        guideMask.inner_trans = null;
        mask.DOFade(0.8f, time).onComplete+=()=>{
            if (unityAction != null)
            {
                unityAction();
            }
        };
       
    }
    public void ShowMask(float value, float time ,UnityAction unityAction = null)
    {
        gameObject.SetActive(true);
        guideMask.inner_trans = null;
        mask.DOFade(value, time).onComplete += () => {
            if (unityAction != null)
            {
                unityAction();
            }
        };

    }
    
    public void ShowMask(bool isNull)
    {
        gameObject.SetActive(true);
        mask.color = new Color(0, 0, 0, 0);
    }
    public SkeletonGraphic jiaocheng;
    /// <summary>
    /// 直播间教程
    /// </summary>
    public void PlayZhiBoAnimation(UnityAction unityAction=null)
    {
        jiaocheng.gameObject.SetActive(true);
        jiaocheng.AnimationState.SetAnimation(0, "huadongjiaocheng", true) ;
        //AudioManager.Instance.PlaySound("jiaocheng9");
    }
    public void HideZhiBoAnimation(UnityAction unityAction = null)
    {
        jiaocheng.gameObject.SetActive(false);
        jiaocheng.AnimationState.SetAnimation(0, "huadongjiaocheng", false);
        //AudioManager.Instance.PlaySound("jiaocheng9");
    }
    //public SkeletonGraphic jiaocheng1;
    ///// <summary>
    ///// 仓库教程
    ///// </summary>
    //public void PlayCanKuAnimation( UnityAction unityAction=null)
    //{
    //    jiaocheng1.gameObject.SetActive(true);
    //    jiaocheng1.AnimationState.SetAnimation(0, "jiaochengzuihou", false).Complete+=s=> { if (unityAction != null) unityAction(); jiaocheng1.gameObject.SetActive(false);
    //        AndroidAdsDialog.Instance.UploadDataEvent("new_course_8");
    //    };
    //    //AudioManager.Instance.PlaySound("jiaocheng9");
    //}
    public void ShowCanKu()
    {
        cankuGuideGo.SetActive(true);
        cankuGuideGo1.SetActive(true);
    }
    public void HideCanku()
    {
        cankuGuideGo.SetActive(false);
        cankuGuideGo1.SetActive(false);
    }
    private void ShowQipao(Vector3 vector,UnityAction unityAction)
    {
        qipao.SetActive(true);
        qipao.transform.DOScale(vector, 1f).onComplete+=()=> { qipao.transform.DOLocalRotate(new Vector3(0, 0, -10), 0.5f).onComplete +=()=> FunCompletedFun(unityAction); };
    
    //qipao.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.5f).onComplete += FunCompletedFun;
    }
void FunCompletedFun(UnityAction unityAction)
{
    AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.LightImpact);
    print("震动++....light");
    StartCoroutine(Delay(0.2f, unityAction));
}
IEnumerator Delay(float time, UnityAction unityAction)
{
    yield return new WaitForSeconds(time);
    qipao.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f).onComplete += ()=>FunCompletedShake(unityAction);
       
}
    IEnumerator DelayEvent(float time, UnityAction unityAction)
    {
        yield return new WaitForSeconds(time);
        if (unityAction != null)
        {
            unityAction();
        }
    }
void FunCompletedShake(UnityAction unityAction)
{
    AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.Warning);
    print("震动++....warning");
        if (unityAction != null)
        {
            unityAction();
        }
        //jixuGo.SetActive(true);
        // StartCoroutine(ShowDanMu(1));
    }
    public void ShowJixu()
    {
        jixuGo.gameObject.SetActive(true);
        //jixuGo.onClick.AddListener(JiXuEvent);
    }
    public void JiXuEvent()
    {
        print("点击房子");
       // HideMask();
    
       // toggleManager.ShowPanel(0);
      
       // gameObject.SetActive(true);
       // //RightShow("请选择你想要卖的商品", ShowJixu);
       // RightShow("请选择你想要卖的商品", JiXuEvent1);
       //// AudioManager.Instance.PlaySound("jiaocheng2");
      
    }

    public void JiXuEvent1()
    {
      //  jixuGo.onClick.RemoveAllListeners();
        HideMask();
      
        gameObject.SetActive(true);
        ShowMask();
        //Init();
        //var shop = shopPanel.shopArry[0].GetComponent<RectTransform>();

        // rectTransform1.sizeDelta = new Vector2(shop.rect.width+50f, shop.rect.height+200f);
        // rectTransform1.position = shop.position;
        //guideMask.inner_trans = rectTransform1;
        SetTips((UIManager.Instance.GetPanel("Panel_Shop") as HLDH.ShopPanel).rectTransform, (UIManager.Instance.GetPanel("Panel_Shop") as HLDH.ShopPanel).targetGuide1.position);
    }
    public void SetTips(RectTransform rect ,Vector2 targetVector2 ,bool creactTips=true,RotaryType rotaryType=RotaryType.RightToLeft)
    {
        guideMask.inner_trans = rect;
        if (creactTips)
        {
            tips1 = Instantiate<GameObject>(tips, this.transform);
            tips1.transform.position = targetVector2;
            print("tips1位置++"+tips1.transform.position);
            print("目标位置++" + targetVector2);
            switch (rotaryType)
            {
                case RotaryType.LeftToRight:
                    tips1.transform.localEulerAngles = ( new Vector3(0, 0, 90));
                    break;
                case RotaryType.RightToLeft:
                    tips1.transform.localEulerAngles= new Vector3(0, 0, -90);
                    break;
                case RotaryType.TopToBottom:
                    tips1.transform.localEulerAngles=(new Vector3(0, 0, 0));
                    break;
                case RotaryType.BottonToTop:
                    tips1.transform.localEulerAngles = (new Vector3(0, 0, 180));
                    break;
            }
            tips1.SetActive(true);
        }
        
    }
    public void SetTips(Transform targetVector2, bool creactTips = true, RotaryType rotaryType = RotaryType.RightToLeft)
    {
       // guideMask.inner_trans = rect;
        if (creactTips)
        {
            tips1 = Instantiate<GameObject>(tips, this.transform);
            tips1.transform.position = targetVector2.position;
           
            tips1.transform.SetParent(targetVector2);
            tips1.transform.localScale = Vector3.one;
            print("tips1位置++" + tips1.transform.position);
            print("目标位置++" + targetVector2);
            switch (rotaryType)
            {
                case RotaryType.LeftToRight:
                    tips1.transform.localEulerAngles = (new Vector3(0, 0, 90));
                    break;
                case RotaryType.RightToLeft:
                    tips1.transform.localEulerAngles = new Vector3(0, 0, -90);
                    break;
                case RotaryType.TopToBottom:
                    tips1.transform.localEulerAngles = (new Vector3(0, 0, 0));
                    break;
                case RotaryType.BottonToTop:
                    tips1.transform.localEulerAngles = (new Vector3(0, 0, 180));
                    break;
            }
            tips1.SetActive(true);
        }

    }
    public void SetTips1(RectTransform rect, Vector2 targetVector2,Transform target)
    {
        guideMask.inner_trans = rect;
        tips1 = Instantiate<GameObject>(tip, this.transform);
        tips1.transform.position = targetVector2;
        tips1.SetActive(true);
        tips1.transform.DOMoveY(target.position.y, 1.5f).SetLoops(-1);

    }
    public void SetTips2(RectTransform rect, Vector2 targetVector2,Transform parent=null)
    {
        guideMask.inner_trans = rect;
        if (parent == null) parent = transform;
        tips1 = Instantiate<GameObject>(ResourceManager.Instance.GetProGo("guideAnim", "Prefab/Effect/"), parent);
        tips1.transform.localScale = Vector3.one;
        tips1.transform.position = targetVector2;
        tips1.SetActive(true);
    }
   
    public void HideTips()
    {
        if (tips1 != null)
            Destroy(tips1);
        
    }
    public void HideMask()
    {
        gameObject.SetActive(false);
        guideMask.inner_trans = null;
    }
    public void HideMask(float time,UnityAction unityAction=null)
    {
        mask.DOFade(0, time).onComplete += () => { gameObject.SetActive(false); if (unityAction != null) { unityAction(); } };
    }
public void Init()
    {
        people.SetActive(false);
        peopleImg.color = new Color(1, 1, 1, 0);
        mask.color = new Color(0, 0, 0, 0);
        qipao.SetActive(false);
        qipao.transform.localScale = Vector3.zero;
        jixuGo.gameObject.SetActive(false);
        
    }
    private void OnEnable()
    {
       
        //mask.DOFade(0.8f, 0.5f)
        Init();


    }
  

}
public enum RotaryType
{
    LeftToRight =0,
    RightToLeft =1,
    TopToBottom=2,
        BottonToTop=3
}