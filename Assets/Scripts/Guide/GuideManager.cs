


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
public class GuideManager : MonoBehaviour
{
    
    public static GuideManager Instance;
  
    public PeopleEffect peopleEffect;
    private void Awake()
    {
        Instance = this;
        //  mask = guideMask.GetComponent<GuideMask>();


        GetisFirstGame();
    }

    //int questionIndex, cankuIndex, peopleIndex;
    private void Start()
    {

        if (!isFirstGame)
        {
            // train.SetActive(true);
        }
        else
        {

            SetisFistGame();
        }

    }
    //IEnumerator DelayShow()
    //{
    //    yield return new WaitForSeconds(1F);
    //    //guideBt.SetActive(true);//°´Å¥

    //   // train.SetActive(true);

    //}
    public void SetisFistGame()
    {

        DataSaver.Instance.SetInt("isGuide", 1);
    }
    public bool isFirstGame = true;
    public void GetisFirstGame()
    {
        if (DataSaver.Instance.HasKey("isGuide") == false)
        {
            isFirstGame =true;

        }
        else
            isFirstGame = false;
    }

    public void GetMask()
    {
        var mask = UIManager.Instance.canvas_Main.transform.Find("Mask");
        //mask.gameObject.SetActive(true);
        peopleEffect = mask.GetComponent<PeopleEffect>();
        //peopleEffect = var mask = UIManager.Instance.canvas_Main.transform.Find("Mask");
        //mask.gameObject.SetActive(true);
        //peopleEffect = mask.GetComponent<PeopleEffect>();
    }
    public UnityAction achieveGuideAction;
    public void AchieveGuide()
    {
        isFirstGame = false;
      
        MoneyManager.Instance.RecoverGuideStatus();
        RecoverZhiBoStatus();
        achieveGuideAction?.Invoke();
        DaimondTaskUI.Instance.Show(true);
        SevenLoginPanel.Instance.ShowUI(UIManager.Instance.showRootMain);
        SevenLoginPanel.Instance.ShowGuide(true);
    }
    public void RecoverZhiBoStatus()
    {
        (UIManager.Instance.GetPanel("Panel_ZhiBo") as ZhiBoPanel).RecoverGuideStates();
    }
    public void RecoverCanKuStatus()
    {
        for (int i = 0; i < (UIManager.Instance.GetPanel("Panel_CanKu") as CanKuPanel).cankuList.Count; i++)
        {
            (UIManager.Instance.GetPanel("Panel_CanKu") as CanKuPanel).cankuList[i].peopleAnimationEvent.RecoverSudu();
        }
    }

}
public interface IRecoverGuideStatus
{
    public void RecoverGuideStatus();
}
