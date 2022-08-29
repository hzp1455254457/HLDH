using System;
using UnityEngine;


public class BigWorld : MonoBehaviour
{


    public static BigWorld Instance;

    public Action DaFaHuoChange, TransportationUnlockStateChange, LockAreaChange;
    public Action<TransportationType> TransportationBack;

    //教程相机，只有第一次显示
    public GameObject ToturialCamera;
    public GameObject MainCamera;




    #region 需要切换的东西
    public GameObject[] HideObjects;
    #endregion
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        
    }
    //private void Start()
    //{
    //    //SceneManager.SetActiveScene(SceneManager.GetSceneByName("BigWorld"));
    //    //Debug.Log("BigWorld场景Start"+IsActive);
    //    //ToogleObjects();
    //}

    /// <summary>
    /// 前往大世界
    /// </summary>
   public void GoBigWorld()
    {
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("BigWorld"));
        //Debug.Log("大世界场景Start");
        ToogleObjects(true);
        //如果是新手教程，打开引导相机
        if (GuideManager.Instance.isFirstGame)
        {
            ToturialCamera.SetActive(true);
            MainCamera.SetActive(false);
        }
    }
    /// <summary>
    /// 前往主场景
    /// </summary>
    public void GoGame()
    {
        //禁用新手引导相机
        ToturialCamera.SetActive(false);
        //Debug.Log("切换到主场景");
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        //隐藏物体
        ToogleObjects(false);
        ToggleManager.Instance.ShowUI();
        UIManager.Instance.SetUIStates(true);

    }
    void ToogleObjects(bool tf)
    {
        foreach (var item in HideObjects)
        {
            //item.SetActive(IsActive);
            item.SetActive(tf);
        }
    }

    /// <summary>
    /// 判断当前是否是大世界场景
    /// </summary>
    //public bool IsActive
    //{
    //    get
    //    {
    //        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("BigWorld"))
    //        {
    //            return false;
    //        }
    //        return true;
    //    }
    //}
}
