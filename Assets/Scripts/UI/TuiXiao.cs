using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Spine.Unity;
using DG.Tweening;
using UnityEngine.Events;

public class TuiXiao : MonoBehaviour
{[Header("推销显示")]
    public GameObject[] status;
    [Header("推销选项显示text")]
    public Text[] texts;
    [Header("推销选项显示btn")]
    public TuiXiaoBtn[] bts;
    public  bool isWin;
    [Header("推销弹幕")]
    public TuiXiaoDanMu[] tuiXiaoDanMus;
    [Header("成功弹幕")]
    public string[] winDanMuValues=new string[] { "买买买", "下单了",  "主播说的对",  "我非常需要","一定要加购" } ;
    [Header("失败弹幕")]
    public string[] FailedDanMuValues = new string[] { "纠结啊", "买不买呢", "十分犹豫", "我问下我妈买不买", "似乎不太行" };
    [Header("气泡")]
    public GameObject qipaoGo;
    [Header("气泡文本")]
    public Text qiPaoText;
    [Header("气泡选项")]
    public GameObject qipaoSelectGo;
    [Header("钻石数量")]
    public int daimondCount;
    public GameObject[] tipsGos;
   
    Graphic[] qipaoSelect;
  public  ZhiBoJian zhiBoJian;
     string[] tipsValues=new string[] { "带货成功!", "无人问津····" };
    private void Awake()
    {
        qipaoSelect = qipaoSelectGo.GetComponentsInChildren<Graphic>();
        tuiXiaoDanMus= GetComponentsInChildren<TuiXiaoDanMu>();
        //  zhiBoJian = GetComponent<ZhiBoJian>();
    }
    UnityAction completedAction;
    /// <summary>
    /// 开始推销
    /// </summary>
    /// <param name="value"></param>
    public void StartTuiXiao(Produce produce, int type = 0, UnityAction unityAction = null)
    {
        completedAction = unityAction;
        ShowSelect();
        status[0].SetActive(false);
        status[1].SetActive(true);


        if (type == 0)
        {
            bts[0].count = produce.item_ad1_result;
            bts[0].value = produce.item_ad1;
           // bts[0].SetValue();
            bts[1].count = produce.item_ad2_result;
            bts[1].value = produce.item_ad2;
           // bts[1].SetValue();
           
        }
        else
        {
            bts[1].count = produce.item_ad1_result;
            bts[1].value = produce.item_ad1;
            //bts[1].SetValue();
            bts[0].count = produce.item_ad2_result;
            bts[0].value = produce.item_ad2;
           // bts[0].SetValue();
         
        }
        bts[1].SetValue();
        bts[0].SetValue();
        if (bts[0].count > bts[1].count)
        {
            tipsGos[0].SetActive(true);
            tipsGos[1].SetActive(false);
            bts[0].isWin = true;
            bts[1].isWin = false;
        }
        else if (bts[0].count < bts[1].count)
        {
            tipsGos[0].SetActive(false);
            tipsGos[1].SetActive(true);
            bts[0].isWin = false;
            bts[1].isWin = true;

        }
        else
        {
            bts[0].isWin = true;
            bts[1].isWin = true;
        }
        bts[1].SetClick();
        bts[0].SetClick();
    }
    public IEnumerator ShowDanMu()
    {
      var arry=  Global.GetRandomSequence(5, 3);
       
        if (isWin)
        {
           
            for (int i = 0; i < tuiXiaoDanMus.Length; i++)
            {
                tuiXiaoDanMus[i].SetDanMU(winDanMuValues[arry[i]],ResourceManager.Instance.GetSprite(string.Format("喜欢{0}",i+1)));
                tuiXiaoDanMus[i].Show();
                yield return new WaitForSeconds(1.3f);
            }
        }
        else
        {
            for (int i = 0; i < tuiXiaoDanMus.Length; i++)
            {
                tuiXiaoDanMus[i].SetDanMU(FailedDanMuValues[arry[i]], ResourceManager.Instance.GetSprite(string.Format("生气{0}", i+1)));
                tuiXiaoDanMus[i].Show();
                yield return new WaitForSeconds(1.3f);
            }
        }
        yield return new WaitForSeconds(0.5f);
        HideDanMu();
        HideQiPao();
       
        status[0].SetActive(true);
        status[1].SetActive(false);
    }
    public void HideQiPao()
    {
        qipaoGo.transform.DOScale(Vector3.zero, 0.5f).onComplete += () =>
        {
            zhiBoJian.ShengJiRecoverClick();
            if (GuideManager.Instance.isFirstGame)
            {
                if (zhiBoJian.index != 0)
                {
                    zhiBoJian.shenjiButton.gameObject.SetActive(true);
                }
            }
            else
            {
                zhiBoJian.shenjiButton.gameObject.SetActive(true);
            }
        };
        
        
    }
    public void HideDanMu()
    {
        for (int i = 0; i < tuiXiaoDanMus.Length; i++)
        {
            //tuiXiaoDanMus[i].SetDanMU(winDanMuValues[Random.Range(0, winDanMuValues.Length)]);
            tuiXiaoDanMus[i].Hide();
        }
    }
    public  void ShowSelect()
    {
        gameObject.SetActive(true);
        qipaoSelectGo.SetActive(true);
        
        qipaoSelectGo.transform.localScale = Vector3.zero;
        qipaoSelectGo.transform.DOScale(Vector3.one, 0.5f);
        qipaoGo.SetActive(false);
        foreach (var item in qipaoSelect)
        {
            item.DOFade(1, 0.5f);
        }
        if(!GuideManager.Instance.isFirstGame)
       StartCoroutine( BtnAnim());
    }

    private IEnumerator BtnAnim()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < bts.Length; i++)
        {
          
            bts[i].SetAnim(true);
            yield return new WaitForSeconds(3.1f);
        }
    }

    public void HideSelect(string value)
    {
        
        foreach (var item in qipaoSelect)
        {
            item.DOFade(0, 0.5f);
        }
        
        StartCoroutine(DelayEvent(0.5f,()=> {
            zhiBoJian.RecorveSell();
            qipaoSelectGo.SetActive(false);
            ShowQipao(value,Vector3.one*0.9f,()=>StartCoroutine( ShowDanMu()));
        }));
    }
    private void ShowQipao(string value,Vector3 vector, UnityAction unityAction)
    {
        qiPaoText.text = value;
        qipaoGo.SetActive(true);
        qipaoGo.transform.DOScale(vector, 1f).onComplete += () => { qipaoGo.transform.DOLocalRotate(new Vector3(0, 0, -10), 0.5f).onComplete += () => FunCompletedFun(unityAction); };

        //qipao.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.5f).onComplete += FunCompletedFun;
    }
    void FunCompletedFun(UnityAction unityAction)
    {
      //  AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.LightImpact);
        print("震动++....light");
        StartCoroutine(Delay(0.2f, unityAction));
    }
    IEnumerator Delay(float time, UnityAction unityAction)
    {
        yield return new WaitForSeconds(time);
        qipaoGo.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f).onComplete += () => FunCompletedShake(unityAction);

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
       // AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.Warning);
        print("震动++....warning");
        if (unityAction != null)
        {
            unityAction();
        }
        //jixuGo.SetActive(true);
        // StartCoroutine(ShowDanMu(1));
    }
    public string GetStringValue()
    {
        completedAction?.Invoke();
        completedAction = null;
        return isWin == true ? tipsValues[0] : tipsValues[1];
    }
}
