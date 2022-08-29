using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedUIManager :MonoBehaviour
{
    // Start is called before the first frame update
    public static void ShowGoldAndDaimond(int count, int Type, UnityEngine.Events.UnityAction unityAction)
    {
      var go=Instantiate( ResourceManager.Instance.GetProGo("GoldRed"),UIManager.Instance.showRootMain);
        go.transform.SetAsLastSibling();
        go.GetComponent<Red1>().Show(count,Type,unityAction);
    }
    public static void ShowRed(float count, int Type, UnityEngine.Events.UnityAction unityAction)
    {
        var go = Instantiate(ResourceManager.Instance.GetProGo("Red2"), UIManager.Instance.showRootMain);
        go.transform.SetAsLastSibling();
        go.GetComponent<Red2>().Show(count, Type, unityAction);
    }
    public static void ShowRed1(UnityEngine.Events.UnityAction unityAction)
    {
        var go = Instantiate(ResourceManager.Instance.GetProGo("Red3"), UIManager.Instance.showRootMain);
        go.transform.SetAsLastSibling();
        go.GetComponent<Red3>().Show(unityAction);
    }
    public static void ShowRed2(UnityEngine.Events.UnityAction unityAction,int type,int count,int videoCount)
    {
        var go = Instantiate(ResourceManager.Instance.GetProGo("Red4"), UIManager.Instance.showRootMain);
        go.transform.SetAsLastSibling();
        go.GetComponent<Red4>().Show(unityAction, type, count,videoCount);
    }
    public static void ShowWangDianRed(UnityEngine.Events.UnityAction unityAction,int count,string value)
    {
        var go = Instantiate(ResourceManager.Instance.GetProGo("WangDianRed"), UIManager.Instance.showRootMain);
        go.transform.SetAsLastSibling();
        go.GetComponent<WangDianRed>().Show(count,value,unityAction);
    }
    public static void ShowWangDianRed1(UnityEngine.Events.UnityAction unityAction, int count, string value)
    {
        var go = Instantiate(ResourceManager.Instance.GetProGo("WangDianRed1"), UIManager.Instance.showRootMain);
        go.transform.SetAsLastSibling();
        go.GetComponent<WangDianRed1>().Show(count, value, unityAction);
    }
    public static void ShowWangDianRed2(UnityEngine.Events.UnityAction unityAction, float count, string value)
    {
        var go = Instantiate(ResourceManager.Instance.GetProGo("WangDianRed2"), UIManager.Instance.showRootMain);
        go.transform.SetAsLastSibling();
        go.GetComponent<WangDianRed2>().Show(count, value, unityAction);
    }
}
